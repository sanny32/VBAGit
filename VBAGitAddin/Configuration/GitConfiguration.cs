using System.Collections.Generic;

namespace VBAGitAddin.Configuration
{
    public interface IGitUserSettings
    {
        string UserName { get; set; }
        string EmailAddress { get; set; }
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
        public List<RepositorySettings> Repositories;

        public GitConfiguration()
        {
            Repositories = new List<RepositorySettings>();
            UserName = string.Empty;
            EmailAddress = string.Empty;
        }

        public GitConfiguration
            (
                string username, 
                string email, 
                List<RepositorySettings> repositories
            )
        {
            Repositories = repositories;
            UserName = username;
            EmailAddress = email;
        }
    }
}
