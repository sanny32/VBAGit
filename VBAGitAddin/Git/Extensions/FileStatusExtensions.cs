using LibGit2Sharp;

namespace VBAGitAddin.Git.Extensions
{
    public static class FileStatusExtensions
    {
        public static string AsString(this FileStatus status)
        {
            switch(status)
            {             
                case FileStatus.Conflicted:
                    return "Conflicted";

                case FileStatus.DeletedFromIndex:
                    return "Deleted";

                case FileStatus.DeletedFromWorkdir:
                    return "Missing";

                case FileStatus.Ignored:
                    return "Ignored";              
            
                case FileStatus.ModifiedInIndex:
                    return "Modified";

                case FileStatus.ModifiedInWorkdir:
                    return "Modified";

                case FileStatus.NewInIndex:
                    return "Added";

                case FileStatus.NewInWorkdir:
                    return "Unversioned";

                case FileStatus.Nonexistent:
                    return "Nonexistent";

                case FileStatus.RenamedInIndex:
                    return "Renamed";

                case FileStatus.RenamedInWorkdir:
                    return "Renamed";

                case FileStatus.TypeChangeInIndex:
                    return "Type changed";

                case FileStatus.TypeChangeInWorkdir:
                    return "Type changed";

                case FileStatus.Unaltered:
                    return "Unaltered";

                case FileStatus.Unreadable:
                    return "Unreadable";

                default:
                    return "Unknown";                
            }
        }
    }
}
