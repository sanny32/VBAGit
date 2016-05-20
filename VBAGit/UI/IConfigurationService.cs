using System;

namespace VBAGit.UI
{
    public interface IConfigurationService<T>
    {
        T LoadConfiguration();
        void SaveConfiguration(T toSerialize);
        void SaveConfiguration(T toSerialize, bool languageChanged);
        event EventHandler SettingsChanged;
    }
}
