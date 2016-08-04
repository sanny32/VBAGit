using System;
using System.IO;

namespace VBAGitAddin.Configuration
{
    public class VBAGitConfigurationService : XmlConfigurationServiceBase<GitConfiguration>
    {

        protected override string ConfigFile
        {
            get { return Path.Combine(this.rootPath, "VBAGitAddin.cfg"); }
        }

        public override GitConfiguration LoadConfiguration()
        {
            return base.LoadConfiguration();
        }

        protected override GitConfiguration HandleIOException(IOException ex)
        {
            //couldn't load file
            return new GitConfiguration();
        }

        protected override GitConfiguration HandleInvalidOperationException(InvalidOperationException ex)
        {
            //couldn't load file
            return new GitConfiguration();
        }
    }
}
