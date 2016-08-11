using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Runtime.InteropServices;
using LibGit2Sharp;
using LibGit2Sharp.Handlers;
using Microsoft.Vbe.Interop;
using VBAGitAddin.Diagnostics;
using VBAGitAddin.VBEditor.Extensions;
using VBAGitAddin.Configuration;
using VBAGitAddin.Git.Extensions;

namespace VBAGitAddin.Git
{
    public class GitProvider
    {
        private VBProject _project;
        private readonly IRepository _repo;
        private readonly Credentials _credentials;
        private readonly CredentialsHandler _credentialsHandler;
        private List<Commit> _unsyncedLocalCommits;
        private List<Commit> _unsyncedRemoteCommits;

        public GitProvider(VBProject project)
        {
            _project = project;
            _unsyncedLocalCommits = new List<Commit>();
            _unsyncedRemoteCommits = new List<Commit>();
        }

        public GitProvider(VBProject project, RepositorySettings repoSettings)
        {
            _project = project;
            _unsyncedLocalCommits = new List<Commit>();
            _unsyncedRemoteCommits = new List<Commit>();
            _repo = new Repository(repoSettings.LocalPath);           
        }
        
        public GitProvider(VBProject project, RepositorySettings repository, string userName, string passWord)
            : this(project, repository)
        {
            _credentials = new UsernamePasswordCredentials()
            {
                Username = userName,
                Password = passWord
            };

            _credentialsHandler = (url, user, cred) => _credentials;
        }       

        ~GitProvider()
        {
            if (_repo != null)
            {
                _repo.Dispose();
            }
        }

        public Branch CurrentBranch
        {
            get
            {
                return this.Branches.FirstOrDefault(b => !b.IsRemote && b.IsCurrentRepositoryHead);
            }
        }

        public IEnumerable<Branch> Branches
        {
            get
            {
                //note: consider doing this once and refreshing if necessary
                return _repo.Branches;
            }
        }

        public IEnumerable<Tag> Tags
        {
            get
            {
                return _repo.Tags;
            }
        }

        public IEnumerable<Commit> Commits
        {
            get
            {
                return _repo.Commits;
            }
        }

        public IRepository Repository
        {
            get
            {
                return _repo;
            }
        }

        public string Author
        {
            get
            {
                var signature = GetSignature();
                return string.Format("{0} <{1}>", signature.Name, signature.Email);
            }
        }

        public IList<Commit> UnsyncedLocalCommits
        {
            get { return _unsyncedLocalCommits; }
        }

        public IList<Commit> UnsyncedRemoteCommits
        {
            get { return _unsyncedRemoteCommits; }
        }
       
        public IRepository Clone(string remotePathOrUrl, string workingDirectory)
        {
            try
            {
                var separators = new[] { '/', '\\', '.' };
                var name = remotePathOrUrl.Split(separators, StringSplitOptions.RemoveEmptyEntries)
                                .LastOrDefault(c => c != "git");

                string path = LibGit2Sharp.Repository.Clone(remotePathOrUrl, workingDirectory);
                return new Repository(path);
            }
            catch (LibGit2SharpException ex)
            {
                throw new GitException("Failed to clone remote repository.", ex);
            }
        }      

        public IRepository Init(string directory, bool bare = false)
        {
            try
            {
                string path = LibGit2Sharp.Repository.Init(directory, bare);
                Trace.TraceInformation("Init Git repository in {0}", directory);

                return new Repository(path);
            }
            catch (LibGit2SharpException ex)
            {
                throw new GitException("Unable to initialize repository.", ex);
            }
        }
        
        public void Push()
        {
            try
            {
                //Only use credentials if we've been given credentials to use in the constructor.
                PushOptions options = null;
                if (_credentials != null)
                {
                    options = new PushOptions()
                    {
                        CredentialsProvider = _credentialsHandler
                    };
                }

                var branch = _repo.Branches[this.CurrentBranch.FriendlyName];
                _repo.Network.Push(branch, options);

                RequeryUnsyncedCommits();
            }
            catch (LibGit2SharpException ex)
            {

                throw new GitException("Push Failed.", ex);
            }
        }

        /// <summary>
        /// Fetches the specified remote for tracking.
        /// If not argument is supplied, fetches the "origin" remote.
        /// </summary>
        public void Fetch([Optional] string remoteName)
        {
            if (remoteName == null)
            {
                remoteName = "origin";
            }

            try
            {
                var remote = _repo.Network.Remotes[remoteName];
                _repo.Network.Fetch(remote);

                RequeryUnsyncedCommits();
            }
            catch (LibGit2SharpException ex)
            {
                throw new GitException("Fetch failed.", ex);
            }
        }

        public void Pull()
        {
            try
            {
                var options = new PullOptions()
                {
                    MergeOptions = new MergeOptions()
                    {
                        FastForwardStrategy = FastForwardStrategy.Default
                    }
                };

                var signature = GetSignature();
                _repo.Network.Pull(signature, options);

                Refresh();

                RequeryUnsyncedCommits();
            }
            catch (LibGit2SharpException ex)
            {
                throw new GitException("Pull Failed.", ex);
            }
        }

        public void Commit(string message, string author, DateTimeOffset when, CommitOptions options)
        {
            try
            {
                MailAddress mailAddress = new MailAddress(string.IsNullOrEmpty(author) ? Author : author);
                var signature = new Signature(mailAddress.DisplayName, mailAddress.Address, when);               
                var commit = _repo.Commit(message, signature, signature, options);

               Trace.TraceInformation("[{0} ({1}) {2}] {3}", 
                   _repo.Head.FriendlyName, 
                   _repo.Head.CanonicalName,
                   commit.Id.ToString(7), 
                   commit.MessageShort);               
            }
            catch(FormatException)
            {
                throw new GitException(string.Format("author '{0}' is not 'Name <email>' and matches no existing author", author));
            }
            catch (LibGit2SharpException ex)
            {
                throw new GitException("Commit Failed.", ex);
            }
        }

        public void Stage(string filePath)
        {
            try
            {
                _repo.Stage(filePath);
            }
            catch (LibGit2SharpException ex)
            {
                throw  new GitException("Failed to stage file.", ex);
            }
        }

        public void Stage(IEnumerable<string> filePaths)
        {
            try
            {
                _repo.Stage(filePaths);
            }
            catch (LibGit2SharpException ex)
            {
                throw new GitException("Failed to stage file.", ex);
            }
        }

        public void Merge(string sourceBranch, string destinationBranch)
        {
            _repo.Checkout(_repo.Branches[destinationBranch]);

            var oldHeadCommit = _repo.Head.Tip;
            var signature = GetSignature();
            var result = _repo.Merge(_repo.Branches[sourceBranch], signature);

            switch (result.Status)
            {
                case MergeStatus.Conflicts:
                    //abort the merge by resetting to the state prior to the merge
                    _repo.Reset(ResetMode.Hard, oldHeadCommit);
                    break;
                case MergeStatus.NonFastForward:
                    //https://help.github.com/articles/dealing-with-non-fast-forward-errors/
                    Pull();
                    Merge(sourceBranch, destinationBranch); //a little leary about this. Could stack overflow if I'm wrong.
                    break;
            }

            Refresh();
        }

        public void Checkout(string branch, bool refresh)
        {
            try
            {
                _repo.Checkout(_repo.Branches[branch]);
                Trace.TraceInformation("git checkout '{0}'", branch);
                Trace.TraceInformation("switched to branch '{0}'", branch);

                if (refresh)
                {
                    Refresh();
                }

                RequeryUnsyncedCommits();
            }
            catch (LibGit2SharpException ex)
            {
                throw new GitException("Checkout failed.", ex);
            }
        }

        public void CreateBranch(string branch)
        {
            CreateBranchOptions options = new CreateBranchOptions();
            options.BaseOn = CreateBranchOptions.Base.Head;
            options.Switch = true;
            CreateBranch(branch, string.Empty, options);
        }

        public void CreateBranch(string branch, string description, CreateBranchOptions options)
        {
            Branch newBranch = null;
            try
            {
                switch(options.BaseOn)
                {
                    case CreateBranchOptions.Base.Head:
                        newBranch = _repo.Branches.Add(branch, _repo.Head.FriendlyName, options.Force);
                        break;

                    case CreateBranchOptions.Base.Branch:
                        newBranch = _repo.Branches.Add(branch, options.Branch.FriendlyName, options.Force);
                        break;

                    case CreateBranchOptions.Base.Tag:
                        throw new NotImplementedException("creation branch based on tag is not implemented.");                       

                    case CreateBranchOptions.Base.Commit:
                        newBranch = _repo.Branches.Add(branch, options.Commit, options.Force);
                        break;
                }

                Trace.TraceInformation("git create branch '{0}'", branch);

                _repo.SetBranchDescription(branch, description);

                RequeryUnsyncedCommits();

                if (options.Switch)
                {
                    Checkout(branch, false);                  
                }
               
            }
            catch (LibGit2SharpException ex)
            {
                if(newBranch != null)
                {
                    _repo.Branches.Remove(newBranch);
                }

                throw new GitException("Branch creation failed.", ex);
            }
        }

        public void Revert()
        {
            try
            {
                var results = _repo.Revert(_repo.Head.Tip, GetSignature());

                if (results.Status == RevertStatus.Conflicts)
                {
                    throw new GitException("Revert resulted in conflicts. Revert failed.");
                }

                Refresh();
            }
            catch (LibGit2SharpException ex)
            {
                throw new GitException("Revert failed.", ex);
            }
        }

        public void AddFile(string filePath)
        {
            try
            {
                // https://github.com/libgit2/libgit2sharp/wiki/Git-add
                _repo.Stage(filePath);
            }
            catch (LibGit2SharpException ex)
            {
                throw new GitException(string.Format("Failed to stage file {0}", filePath), ex);
            }
        }

        /// <summary>
        /// Removes file from staging area, but leaves the file in the working directory.
        /// </summary>
        /// <param name="filePath"></param>
        public void RemoveFile(string filePath)
        {
            try
            {
                _repo.Remove(filePath, false);
            }
            catch (LibGit2SharpException ex)
            {
                throw new GitException(string.Format("Failed to remove file {0} from staging area.", filePath), ex);
            }
        }

        public IEnumerable<StatusEntry> Status()
        {
            try
            {                
                return _repo.RetrieveStatus();
            }
            catch (LibGit2SharpException ex)
            {
                throw new GitException("Failed to retrieve repository status.", ex);
            }
        }

        public void Undo(string filePath)
        {
            try
            {
                _repo.CheckoutPaths(this.CurrentBranch.FriendlyName, new List<string> {filePath});

                //this might need to cherry pick from the tip instead.

                var componentName = Path.GetFileNameWithoutExtension(filePath);

                //GetFileNameWithoutExtension returns empty string if it's not a file
                //https://msdn.microsoft.com/en-us/library/system.io.path.getfilenamewithoutextension%28v=vs.110%29.aspx
                if (componentName != String.Empty)
                {
                    var component = _project.VBComponents.Item(componentName);
                    _project.VBComponents.RemoveSafely(component);
                    _project.VBComponents.ImportSourceFile(filePath);
                }
            }
            catch (LibGit2SharpException ex)
            {
                throw new GitException("Undo failed.", ex);
            }
        }

        public void DeleteBranch(string branch)
        {
            try
            {
                if (_repo.Branches.Any(b => 
                    b.FriendlyName == branch && !b.IsRemote))
                {
                    _repo.Branches.Remove(branch);
                }
            }
            catch(LibGit2SharpException ex)
            {
                throw new GitException("Branch deletion failed.", ex);
            }
        }

        private Signature GetSignature()
        {
            return _repo.Config.BuildSignature(DateTimeOffset.Now);
        }

        private void RequeryUnsyncedCommits()
        {
            var currentBranch = _repo.Branches[this.CurrentBranch.FriendlyName];
            var local = currentBranch.Commits;

            if (currentBranch.TrackedBranch == null)
            {
                _unsyncedLocalCommits = local.ToList();
                _unsyncedRemoteCommits = new List<Commit>();
            }
            else
            {
                var remote = currentBranch.TrackedBranch.Commits;
                _unsyncedLocalCommits = local.Where(c => !remote.Contains(c)).ToList();
                _unsyncedRemoteCommits = remote.Where(c => !local.Contains(c)).ToList();
            }
        }

        private void Refresh()
        {
            //Because refreshing removes all components, we need to store the current selection,
            // so we can correctly reset it once the files are imported from the repository.
            var selection = _project.VBE.ActiveCodePane.GetSelection();
            string name = null;
            if (selection.QualifiedName.Component != null)
            {
                name = selection.QualifiedName.Component.Name;
            }

            _project.RemoveAllComponents();
            _project.ImportSourceFiles(_repo.Info.WorkingDirectory);

            _project.VBE.SetSelection(selection.QualifiedName.Project, selection.Selection, name);
        }
    }
}
