using System.IO;
using VBAGitAddin.Configuration;

namespace VBAGitAddin.UI
{
    public class RepositoryFileWatcher : FileSystemWatcher
    {
        public RepositoryFileWatcher()
            :base()
        {
        }

        public RepositorySettings Repository
        {
            get;
            set;
        }
    }
}
