using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace VBAGitAddin.SourceControl
{
    [ComVisible(true)]
    [Guid("51D7CD6D-7922-4893-84C5-209335D1D297")]
    public interface IRepository
    {
        [DispId(0)]
        string Name { get; }       

        [DispId(1)]
        [Description("FilePath of local repository.")]
        string LocalLocation { get; }

        [DispId(2)]
        [Description("FilePath or URL of remote repository.")]
        string RemoteLocation { get; }
    }
}
