using System.Runtime.InteropServices;

namespace VBEGit
{
    [ComVisible(true)]
    [Guid("8032591E-E75D-48E4-90CB-D71F98335E7D")]
    [ProgId("VBEGit.Repository")]
    [ClassInterface(ClassInterfaceType.None)]
    public class Repository : IRepository
    {
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
    }
}
