using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace VBEGit.Interop
{
    [ComVisible(true)]
    [Guid("8156FBA2-AD35-48F1-B6DA-C6154EC920BB")]
    [ProgId("VBEGit.FileStatusEntries")]
    [ClassInterface(ClassInterfaceType.None)]
    [Description("Collection of IFileEntries representing the status of the repository files.")]
    public class FileStatusEntries : IEnumerable
    {
        private IEnumerable<IFileStatusEntry> entries;
        public FileStatusEntries(IEnumerable<IFileStatusEntry> entries)
        {
            this.entries = entries;
        }

        [DispId(-4)]
        public IEnumerator GetEnumerator()
        {
            return entries.GetEnumerator();
        }
    }
}
