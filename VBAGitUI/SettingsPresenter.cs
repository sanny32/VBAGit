﻿using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using VBEGit;

namespace VBAGitUI
{
    public interface ISettingsPresenter : IProviderPresenter, IRefreshable
    {
    }

    public class SettingsPresenter : ProviderPresenterBase, ISettingsPresenter
    {
        private readonly ISettingsView _view;
        private readonly IConfigurationService<SourceControlConfiguration> _configurationService;
        private readonly IFolderBrowserFactory _folderBrowserFactory;
        private SourceControlConfiguration _config;

        public SettingsPresenter(ISettingsView view, IConfigurationService<SourceControlConfiguration> configService, IFolderBrowserFactory folderBrowserFactory)
        {
            _configurationService = configService;
            _folderBrowserFactory = folderBrowserFactory;

            _view = view;

            _view.BrowseDefaultRepositoryLocation += OnBrowseDefaultRepositoryLocation;
            _view.Cancel += OnCancel;
            _view.EditIgnoreFile += OnEditIgnoreFile;
            _view.EditAttributesFile += OnEditAttributesFile;
            _view.Save += OnSave;
        }

        public void RefreshView()
        {
            _config = _configurationService.LoadConfiguration();

            _view.UserName = _config.UserName;
            _view.EmailAddress = _config.EmailAddress;
            _view.DefaultRepositoryLocation = _config.DefaultRepositoryLocation;
        }

        private void OnSave(object sender, EventArgs e)
        {
            if (_config == null)
            {
                _config = _configurationService.LoadConfiguration();
            }

            _config.EmailAddress = _view.EmailAddress;
            _config.UserName = _view.UserName;
            _config.DefaultRepositoryLocation = _view.DefaultRepositoryLocation;

            _configurationService.SaveConfiguration(_config);
        }

        private void OnEditAttributesFile(object sender, EventArgs e)
        {
            OpenFileInExternalEditor(GitSettingsFile.Attributes);
        }

        private void OnEditIgnoreFile(object sender, EventArgs e)
        {
            OpenFileInExternalEditor(GitSettingsFile.Ignore);
        }

        private void OpenFileInExternalEditor(GitSettingsFile fileType)
        {
            if (this.Provider == null)
            {
                return;
            }

            var fileName = String.Empty;
            var defaultContents = String.Empty;
            switch (fileType)
            {
                case GitSettingsFile.Ignore:
                    fileName = ".gitignore";
                    defaultContents = DefaultSettings.GitIgnoreText();
                    break;
                case GitSettingsFile.Attributes:
                    fileName = ".gitattributes";
                    defaultContents = DefaultSettings.GitAttributesText();
                    break;
            }

            var repo = this.Provider.CurrentRepository;
            var filePath = Path.Combine(repo.LocalLocation, fileName);

            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, defaultContents);
            }

            Process.Start(filePath);
        }

        private void OnCancel(object sender, EventArgs e)
        {
            RefreshView();
        }

        private void OnBrowseDefaultRepositoryLocation(object sender, EventArgs e)
        {
            using (var folderPicker = _folderBrowserFactory.CreateFolderBrowser("Default Repository Directory"))
            {
                if (folderPicker.ShowDialog() == DialogResult.OK)
                {
                    _view.DefaultRepositoryLocation = folderPicker.SelectedPath;
                }
            }
        }
    }
}
