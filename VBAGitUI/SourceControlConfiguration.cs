﻿using System.Collections.Generic;
using VBEGit;

namespace VBAGitUI
{
    public interface ISourceControlUserSettings
    {
        string UserName { get; set; }
        string EmailAddress { get; set; }
        string DefaultRepositoryLocation { get; set; }
    }

    public class SourceControlConfiguration : ISourceControlUserSettings
    {
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public string DefaultRepositoryLocation { get; set; }
        public List<Repository> Repositories;

        public SourceControlConfiguration()
        {
            this.Repositories = new List<Repository>();
            this.UserName = string.Empty;
            this.EmailAddress = string.Empty;
            this.DefaultRepositoryLocation = string.Empty;
        }

        public SourceControlConfiguration
            (
                string username, 
                string email, 
                string defaultRepoLocation,
                List<Repository> repositories
            )
        {
            this.Repositories = repositories;
            this.UserName = username;
            this.EmailAddress = email;
            this.DefaultRepositoryLocation = defaultRepoLocation;
        }
    }
}
