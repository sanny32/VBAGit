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

        private ContextMenu _ctxMenu;
        private VBAGitAddinMenu _menu;
        private VBAGitAddinApp _app;
        private VBProjectsEventsSink _sink;
        private IConnectionPoint _projectsEventsConnectionPoint;
        private int _projectsEventsCookie;

        public App(VBE vbe, AddIn addIn)
        {
            _vbe = vbe;
            _addIn = addIn;

            Setup();
        }       

        private void Setup()
        {
            _app = new VBAGitAddinApp(_vbe);

            _menu = new VBAGitAddinMenu(_app);
            _menu.Initialize();

            _ctxMenu = new ContextMenu(_app);
            _ctxMenu.Initialize();

            _sink = new VBProjectsEventsSink();
            var connectionPointContainer = (IConnectionPointContainer)_vbe.VBProjects;
            var interfaceId = typeof(_dispVBProjectsEvents).GUID;
            connectionPointContainer.FindConnectionPoint(ref interfaceId, out _projectsEventsConnectionPoint);
            _projectsEventsConnectionPoint.Advise(_sink, out _projectsEventsCookie);
            _sink.ProjectActivated += _sink_ProjectActivated;
            _sink.ProjectRemoved += _sink_ProjectRemoved;
            _sink.ProjectAdded += _sink_ProjectAdded;
        }

        private void _sink_ProjectActivated(object sender, DispatcherEventArgs<VBProject> e)
        {
            if (_menu != null)
            {
                _menu.Initialize();
            }

            if(_ctxMenu != null)
            {
                _ctxMenu.Initialize();
            }
        }

        private void _sink_ProjectAdded(object sender, DispatcherEventArgs<VBProject> e)
        {
            if(_app != null)
            {
                _app.AddVBProject(e.Item);
            }
        }

        private void _sink_ProjectRemoved(object sender, DispatcherEventArgs<VBProject> e)
        {
            if (_app != null)
            {
                _app.RemoveVBProject(e.Item);
            }
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
            if (_sink != null)
            {
                _sink.ProjectActivated -= _sink_ProjectActivated;
                _sink.ProjectRemoved -= _sink_ProjectRemoved;
                _sink.ProjectAdded -= _sink_ProjectAdded;
            }

            if (_projectsEventsConnectionPoint != null)
            {
                _projectsEventsConnectionPoint.Unadvise(_projectsEventsCookie);
            }

            if(_ctxMenu != null)
            {
                _ctxMenu.Dispose();
            }

            if (_menu != null)
            {
                _menu.Dispose();
            }

            if (_app != null)
            {
                _app.Dispose();
            }
        }
    }
}
