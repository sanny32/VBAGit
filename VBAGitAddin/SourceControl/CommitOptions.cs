
namespace VBAGitAddin.SourceControl
{
    public sealed class CommitOptions 
    {
        private LibGit2Sharp.CommitOptions _options;
       
        public CommitOptions()
        {
            _options = new LibGit2Sharp.CommitOptions();
        }

        public static implicit operator LibGit2Sharp.CommitOptions(CommitOptions options)
        {
            return options._options;
        }

        public bool AllowEmptyCommit
        {
            get { return _options.AllowEmptyCommit; }
            set { _options.AllowEmptyCommit = value; }
        }
       
        public bool AmendPreviousCommit
        {
            get { return _options.AmendPreviousCommit; }
            set { _options.AmendPreviousCommit = value; }
        }
       
        public char? CommentaryChar
        {
            get { return _options.CommentaryChar; }
            set { _options.CommentaryChar = value; }
        }
       
        public bool PrettifyMessage
        {
            get { return _options.PrettifyMessage; }
            set { _options.PrettifyMessage = value; }
        }
    }
}
