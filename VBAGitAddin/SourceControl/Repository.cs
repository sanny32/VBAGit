using System;
using System.Runtime.InteropServices;

namespace VBAGitAddin.SourceControl
{
    [ComVisible(true)]
    [Guid("8032591E-E75D-48E4-90CB-D71F98335E7D")]
    [ProgId("VBAGit.Repository")]
    [ClassInterface(ClassInterfaceType.None)]
    public class Repository : IRepository
    {
        public const string BareExt = ".git";

        public string Name { get; set; }
        public string LocalLocation { get; set; }
        public string RemoteLocation { get;  set; }

        //parameterless constructor for serialization
        public Repository() { }

        public Repository(string name, string localDirectory, string remotePathOrUrl)
        {
            Name = name;
            LocalLocation = localDirectory;
            RemoteLocation = remotePathOrUrl;
        }
        
        public bool IsEqual(Repository repo)
        {
            return (Name == repo.Name &&
                    LocalLocation == repo.LocalLocation &&
                    RemoteLocation == repo.RemoteLocation);
        }
    }
}
