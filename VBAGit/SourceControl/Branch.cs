using System.Runtime.InteropServices;

namespace VBAGit.SourceControl
{
    [ComVisible(true)]
    [Guid("DEA7782C-F1D6-4692-9297-14F952C12249")]
    public interface IBranch
    {
        [DispId(0)]
        string Name { get; }

        [ComVisible(false)]
        [DispId(3)]
        string CanonicalName { get; }

        [ComVisible(false)]
        [DispId(2)]
        bool IsRemote { get; }

        [DispId(3)]
        bool IsCurrentHead { get; }
    }

    [ComVisible(true)]
    [Guid("1CA9DB25-1C47-460B-9368-A9D283FD4C28")]
    [ProgId("VBEGit.Branch")]
    [ClassInterface(ClassInterfaceType.None)]
    public class Branch : IBranch
    {
        public string Name { get; private set; }
        public string CanonicalName { get; private set; }
        public bool IsRemote { get; private set; }
        public bool IsCurrentHead { get; private set; }

        public Branch(LibGit2Sharp.Branch branch)
            : this(branch.FriendlyName, branch.CanonicalName, branch.IsRemote, branch.IsCurrentRepositoryHead)
        { }

        public Branch(string name, string canonicalName, bool isRemote, bool isCurrentHead)
        {
            this.Name = name;
            this.CanonicalName = canonicalName;
            this.IsRemote = isRemote;
            this.IsCurrentHead = isCurrentHead;
        }
    }
}
