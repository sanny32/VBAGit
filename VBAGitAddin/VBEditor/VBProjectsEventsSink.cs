using System;
using Microsoft.Vbe.Interop;

namespace VBAGitAddin.VBEditor
{
    public class DispatcherEventArgs<T> : EventArgs
    where T : class
    {
        private readonly T _item;

        public DispatcherEventArgs(T item)
        {
            _item = item;
        }

        public T Item { get { return _item; } }
    }

    public class DispatcherRenamedEventArgs<T> : DispatcherEventArgs<T>
    where T : class
    {
        private readonly T _item;
        private readonly string _oldName;

        public DispatcherRenamedEventArgs(T item, string oldName)
            :base(item)
        {
            _oldName = oldName;
        }
        
        public string OldName { get { return _oldName; } }
    }

    public class VBProjectsEventsSink : _dispVBProjectsEvents
    {
        public event EventHandler<DispatcherEventArgs<VBProject>> ProjectAdded;
        public void ItemAdded(VBProject VBProject)
        {
            if (VBProject.Protection == vbext_ProjectProtection.vbext_pp_none)
            {
                OnDispatch(ProjectAdded, VBProject);
            }
        }

        public event EventHandler<DispatcherEventArgs<VBProject>> ProjectRemoved;
        public void ItemRemoved(VBProject VBProject)
        {
            if (VBProject.Protection == vbext_ProjectProtection.vbext_pp_none)
            {
                OnDispatch(ProjectRemoved, VBProject);
            }
        }

        public event EventHandler<DispatcherRenamedEventArgs<VBProject>> ProjectRenamed;
        public void ItemRenamed(VBProject VBProject, string OldName)
        {
            var handler = ProjectRenamed;
            if (handler != null && VBProject.Protection == vbext_ProjectProtection.vbext_pp_none)
            {
                handler.Invoke(this, new DispatcherRenamedEventArgs<VBProject>(VBProject, OldName));
            }
        }

        public event EventHandler<DispatcherEventArgs<VBProject>> ProjectActivated;
        public void ItemActivated(VBProject VBProject)
        {
            if (VBProject.Protection == vbext_ProjectProtection.vbext_pp_none)
            {
                OnDispatch(ProjectActivated, VBProject);
            }
        }

        private void OnDispatch(EventHandler<DispatcherEventArgs<VBProject>> dispatched, VBProject project)
        {
            var handler = dispatched;
            if (handler != null)
            {
                handler.Invoke(this, new DispatcherEventArgs<VBProject>(project));
            }
        }
    }
}
