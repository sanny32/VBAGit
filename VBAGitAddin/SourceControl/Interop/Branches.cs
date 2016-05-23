using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace VBAGitAddin.SourceControl.Interop
{
    [ComVisible(true)]
    [Guid("F2BBB099-9DD4-4669-AF30-A62F2CACE0CF")]
    [ProgId("VBAGit.SourceControl.Branches")]
    [ClassInterface(ClassInterfaceType.None)]
    [Description("Collection of string representation of branches in a repository.")]
    public class Branches : IEnumerable
    {
        private readonly IEnumerable<IBranch> _branches;
        internal Branches(IEnumerable<IBranch> branches)
        {
            this._branches = branches;
        }

        [DispId(-4)]
        public IEnumerator GetEnumerator()
        {
            return _branches.GetEnumerator();
        }
    }
}
