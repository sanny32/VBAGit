using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace VBEGit.Interop
{
    [ComVisible(true)]
    [Guid("8E63F81E-C01F-4C51-92C2-34F8CC658601")]
    [ProgId("VBAGit.FileStatusEntries")]
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
