using System.Linq;
using Microsoft.Vbe.Interop;

namespace VBAGit.VBEditor.Extensions
{
    public static class VbeExtensions
    {
        public static void SetSelection(this VBE vbe, VBProject vbProject, Selection selection, string name)
        {
            var project = vbe.VBProjects.Cast<VBProject>()
                             .SingleOrDefault(p => p.Protection != vbext_ProjectProtection.vbext_pp_locked 
                                               && ReferenceEquals(p, vbProject));

            VBComponent component = null;
            if (project != null)
            {
                component = project.VBComponents.Cast<VBComponent>()
                    .SingleOrDefault(c => c.Name == name);
            }

            if (component == null)
            {
                return;
            }

            component.CodeModule.CodePane.SetSelection(selection);
        }

        public static CodeModuleSelection FindInstruction(this VBE vbe, QualifiedModuleName qualifiedModuleName, Selection selection)
        {
            var module = qualifiedModuleName.Component.CodeModule;
            if (module == null)
            {
                return null;
            }

            return new CodeModuleSelection(module, selection);
        }       
    }
}