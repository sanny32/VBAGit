using System.Runtime.InteropServices;

namespace VBAGitAddin.SourceControl.Interop
{
    [ComVisible(true)]
    [Guid("D1F6AD72-35A2-4483-B3A0-37B089466EE4")]
    public interface ICredentials 
    {
        string Username { get; set; }
        string Password { get; set; }
    }

    [ComVisible(true)]
    [Guid("8E63F81E-C01F-4C51-92C2-34F8CC658601")]
    [ProgId("VBAGit.Credentials")]
    [ClassInterface(ClassInterfaceType.None)]
    public class Credentials : ICredentials
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public Credentials() { }

        internal Credentials(string username, string password)
            :this()
        {
            this.Username = username;
            this.Password = password;
        }
    }
}
