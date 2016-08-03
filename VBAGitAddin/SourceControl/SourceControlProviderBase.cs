﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Vbe.Interop;
using VBAGitAddin.VBEditor.Extensions;

namespace VBAGitAddin.SourceControl
{
    public abstract class SourceControlProviderBase : ISourceControlProvider
    {
        protected VBProject Project;

        protected SourceControlProviderBase(VBProject project)
        {
            this.Project = project;
        }

        protected SourceControlProviderBase(VBProject project, IRepository repository)
            :this(project)
        {
            this.CurrentRepository = repository;
        }

        public IRepository CurrentRepository { get; private set; }
        public abstract IBranch CurrentBranch { get; }
        public abstract IEnumerable<IBranch> Branches { get; }
        public abstract string Author { get; }
        public abstract IList<ICommit> UnsyncedLocalCommits { get; }
        public abstract IList<ICommit> UnsyncedRemoteCommits { get; }
        public abstract IRepository Clone(string remotePathOrUrl, string workingDirectory);
        public abstract void Push();
        public abstract void Fetch(string remoteName);
        public abstract void AddFile(string filePath);
        public abstract void RemoveFile(string filePath);
        public abstract void CreateBranch(string branch);
        public abstract void DeleteBranch(string branch);
        public abstract IRepository Init(string directory, bool bare = false);
        public abstract void Commit(string message, string author, DateTimeOffset when, CommitOptions options);
      
        public virtual void Pull()
        {
            Refresh();
        }

        public virtual void Stage(string filePath)
        {
            this.Project.ExportSourceFiles(this.CurrentRepository.LocalLocation);
        }

        public virtual void Stage(IEnumerable<string> filePaths)
        {
            this.Project.ExportSourceFiles(this.CurrentRepository.LocalLocation);
        }

        public virtual void Merge(string sourceBranch, string destinationBranch)
        {
            Refresh();
        }

        public virtual void Checkout(string branch)
        {
            Refresh();
        }

        public virtual void Undo(string filePath)
        {
            //this might need to cherry pick from the tip instead.

           var componentName = Path.GetFileNameWithoutExtension(filePath);

           //GetFileNameWithoutExtension returns empty string if it's not a file
           //https://msdn.microsoft.com/en-us/library/system.io.path.getfilenamewithoutextension%28v=vs.110%29.aspx
            if (componentName != String.Empty)
            {
                var component = this.Project.VBComponents.Item(componentName);
                this.Project.VBComponents.RemoveSafely(component);
                this.Project.VBComponents.ImportSourceFile(filePath);
            }
        }

        public virtual void Revert()
        {
            Refresh();
        }

        public virtual IEnumerable<IFileStatusEntry> Status()
        {
            this.Project.ExportSourceFiles(this.CurrentRepository.LocalLocation);
            return null;
        }

        protected string GetProjectNameFromDirectory(string directory)
        {
            var separators = new[] { '/', '\\', '.' };
            return directory.Split(separators, StringSplitOptions.RemoveEmptyEntries)
                            .LastOrDefault(c => c != "git");
        }

        private void Refresh()
        {
            //Because refreshing removes all components, we need to store the current selection,
            // so we can correctly reset it once the files are imported from the repository.
            var selection = Project.VBE.ActiveCodePane.GetSelection();
            string name = null;
            if (selection.QualifiedName.Component != null)
            {
                name = selection.QualifiedName.Component.Name;
            }

            Project.RemoveAllComponents();
            Project.ImportSourceFiles(CurrentRepository.LocalLocation);

            Project.VBE.SetSelection(selection.QualifiedName.Project, selection.Selection, name);
        }
    }
}
