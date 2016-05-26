using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.Vbe.Interop;

namespace VBAGitAddin.SourceControl.Interop
{
    [ComVisible(true)]
    [Guid("DFEF2151-6FE7-4172-B55D-36DC35F04991")]
    [ProgId("VBAGit.GitProvider")]
    [ClassInterface(ClassInterfaceType.None)]
    [Description("VBA Editor integrated access to Git.")]
    class GitProvider : VBAGitAddin.SourceControl.GitProvider, ISourceControlProvider
    {
        public GitProvider(VBProject project) 
            : base(project)
        { }

        public GitProvider(VBProject project, IRepository repository)
            : base(project, repository)
        { }

        [Obsolete]
        public GitProvider(VBProject project, IRepository repository, string userName, string passWord)
            : base(project, repository, userName, passWord)
        { }

        public GitProvider(VBProject project, IRepository repository, ICredentials credentials)
            :base(project, repository, credentials.Username, credentials.Password)
        { }

        public new string CurrentBranch
        {
            get { return base.CurrentBranch.Name; }
        }

        /// <summary>
        /// Returns only local branches to COM clients.
        /// </summary>
        public new IEnumerable Branches
        {
            get { return new Branches(base.Branches.Where(b => !b.IsRemote)); }
        }

        /// <summary>
        /// Returns Iterable Collection of FileStatusEntry objects.
        /// </summary>
        /// <returns></returns>
        public new IEnumerable Status()
        {
            return new FileStatusEntries(base.Status());
        }

        /// <summary>
        /// Stages and commits all modified files.
        /// </summary>
        /// <param name="message"></param>
        public override void Commit(string message)
        {
            var filePaths = base.Status()
                .Where(s => s.FileStatus.HasFlag(FileStatus.Modified))
                .Select(s => s.FilePath).ToList();

            Stage(filePaths);
            base.Commit(message);
        }
    }
}
