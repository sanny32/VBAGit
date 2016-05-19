using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using Microsoft.Vbe.Interop;

namespace VBEGit.Interop
{
    [ComVisible(true)]
    [Guid("28A67992-7EA2-4B9A-8F8E-2508A53E1780")]
    // ReSharper disable once InconsistentNaming; the underscore hides the interface from VBE's object browswer
    public interface _ISourceControlClassFactory
    {
        [DispId(1)]
        ISourceControlProvider CreateGitProvider(VBProject project, [Optional] IRepository repository, [Optional] ICredentials credentials);

        [DispId(2)]
        IRepository CreateRepository(string name, string localDirectory, [Optional] string remotePathOrUrl);

        [DispId(3)]
        ICredentials CreateCredentials(string username, string password);
    }

    [ComVisible(true)]
    [Guid("CA3477DC-3604-48E5-A553-C9512AB6F0EB")]
    [ProgId("VBAGit.SourceControlClassFactory")]
    [ClassInterface(ClassInterfaceType.None)]
    public class SourceControlClassFactory : _ISourceControlClassFactory
    {
        [Description("Returns a new GitProvider. IRepository must be supplied if also passing user credentials.")]
        public ISourceControlProvider CreateGitProvider(VBProject project, [Optional] IRepository repository, [Optional] ICredentials credentials)
        {
            if (credentials != null)
            {
                if (repository == null)
                {
                    throw new ArgumentNullException("Must supply an IRepository if supplying credentials.");
                }

                return new GitProvider(project, repository, credentials);
            }

            if (repository != null) 
            {
                return new GitProvider(project, repository);
            }

            return new GitProvider(project);
        }

        [Description("Returns new instance of type IRepository.")]
        public IRepository CreateRepository(string name, string localDirectory, [Optional] string remotePathOrUrl)
        {
            if (remotePathOrUrl == null)
            {
                remotePathOrUrl = string.Empty;
            }

            return new Repository(name, localDirectory, remotePathOrUrl);
        }

        [Description("Returns a new instance of type ICredentials.")]
        public ICredentials CreateCredentials(string username, string password)
        {
            return new Credentials(username, password);
        }
    }
}
