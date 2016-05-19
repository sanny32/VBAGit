using System.Runtime.InteropServices;
using LibGit2Sharp;

namespace VBEGit
{
    [ComVisible(true)]
    [Guid("3301599E-C1BA-46BC-BB31-3B7FE96826A9")]
    public interface IFileStatusEntry
    {
        [DispId(0)]
        string FilePath { get; }

        //todo: find a way to make this com visible, even if you have to borrow the source code and cast (int) between them.
        [DispId(1)]
        FileStatus FileStatus { get; }
    }

    [ComVisible(true)]
    [Guid("2F004FFB-B368-46D5-85A7-C6DEB28CB9FB")]
    [ProgId("VBEGit.FileStatus")]
    [ClassInterface(ClassInterfaceType.AutoDual)]
    public class FileStatusEntry : IFileStatusEntry
    {
        public string FilePath { get; private set; }
        public FileStatus FileStatus { get; private set; }

        private FileStatusEntry(string filePath)
        {
            this.FilePath = filePath;
        }

        public FileStatusEntry(string filePath, LibGit2Sharp.FileStatus fileStatus)
            :this(filePath)
        {
            this.FileStatus = (FileStatus)fileStatus;
        }

        public FileStatusEntry(string filePath, FileStatus fileStatus)
            :this(filePath)
        {
            this.FileStatus = fileStatus;
        }

        public FileStatusEntry(StatusEntry status)
            : this(status.FilePath, status.State) { }
    }
}
