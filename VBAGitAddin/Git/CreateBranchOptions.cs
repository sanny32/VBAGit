using LibGit2Sharp;

namespace VBAGitAddin.Git
{
    public class CreateBranchOptions
    {
        public CreateBranchOptions()
        {
            BaseOn = Base.Head;
        }

        public bool? Track { get; set; }
        public bool Force { get; set; }
        public bool Switch { get; set; }

        public enum Base
        {
            Head = 0,
            Branch = 1,
            Tag = 2,
            Commit = 3
        }

        public Base BaseOn { get; set; }

        public Branch Branch { get; set; }
        public Tag Tag { get; set; }
        public Commit Commit { get; set; }
    }
}
