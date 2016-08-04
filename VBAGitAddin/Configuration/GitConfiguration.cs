using LibGit2Sharp;
using System.Collections.Generic;
using System;

namespace VBAGitAddin.Configuration
{
    public interface IGitUserSettings
    {
        string UserName { get; set; }
        string EmailAddress { get; set; }
        string DefaultRepositoryLocation { get; set; }
    }
  
    public class RepositorySettings
    {
        public string Name { get; set; }
        public string LocalPath { get; set; }
        public string RemotePath { get; set; }        
    }

    public class GitConfiguration : IGitUserSettings
    {
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public string DefaultRepositoryLocation { get; set; }
        public List<RepositorySettings> Repositories;

        public GitConfiguration()
        {
            this.Repositories = new List<RepositorySettings>();
            this.UserName = string.Empty;
            this.EmailAddress = string.Empty;
            this.DefaultRepositoryLocation = string.Empty;
        }

        public GitConfiguration
            (
                string username, 
                string email, 
                string defaultRepoLocation,
                List<RepositorySettings> repositories
            )
        {
            this.Repositories = repositories;
            this.UserName = username;
            this.EmailAddress = email;
            this.DefaultRepositoryLocation = defaultRepoLocation;
        }
    }
}
