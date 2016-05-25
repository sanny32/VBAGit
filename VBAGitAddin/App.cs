using System;
using Microsoft.Vbe.Interop;
using VBAGitAddin.UI;
using VBAGitAddin.VBEditor;
using System.Runtime.InteropServices.ComTypes;

namespace VBAGitAddin
{
    public class App : IDisposable
    {
        private readonly VBE _vbe;
        private readonly AddIn _addIn;
        private VBAGitAddinMenu _menu;

        private VBProjectsEventsSink _sink;
        private IConnectionPoint _projectsEventsConnectionPoint;
        private int _projectsEventsCookie;

        public App(VBE vbe, AddIn addIn)
        {
            _vbe = vbe;
            _addIn = addIn;

            CleanUp();

            Setup();
        }

        private void _sink_ProjectActivated(object sender, DispatcherEventArgs<VBProject> e)
        {
            if (_menu != null)
            {
                _menu.Initialize();
            }
        }

        private void _configService_SettingsChanged(object sender, EventArgs e)
        {
            CleanUp();

            Setup();
        }        

        private void Setup()
        {            
            _menu = new VBAGitAddinMenu(_vbe, _addIn);

            _sink = new VBProjectsEventsSink();
            var connectionPointContainer = (IConnectionPointContainer)_vbe.VBProjects;
            var interfaceId = typeof(_dispVBProjectsEvents).GUID;
            connectionPointContainer.FindConnectionPoint(ref interfaceId, out _projectsEventsConnectionPoint);
            _projectsEventsConnectionPoint.Advise(_sink, out _projectsEventsCookie);
            _sink.ProjectActivated += _sink_ProjectActivated;
        }
        
        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) { return; }

            CleanUp();
        }

        private void CleanUp()
        {
            if (_menu != null)
            {
                _menu.Dispose();
            }

            if (_projectsEventsConnectionPoint != null)
            {
                _projectsEventsConnectionPoint.Unadvise(_projectsEventsCookie);
            }
        }
    }
}
