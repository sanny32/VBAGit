using System.IO;
using System.Linq;
using Microsoft.Vbe.Interop;
using System.Collections.Generic;

namespace VBAGitAddin.VBEditor.Extensions
{
    public static class VBComponentsExtensions
    {
        /// <summary>
        /// Safely removes the specified VbComponent from the collection.
        /// </summary>
        /// <remarks>
        /// UserForms, Class modules, and Standard modules are completely removed from the project.
        /// Since Document type components can't be removed through the VBE, all code in its CodeModule are deleted instead.
        /// </remarks>
        public static void RemoveSafely(this VBComponents components, VBComponent component)
        {
            switch (component.Type)
            {
                case vbext_ComponentType.vbext_ct_ClassModule:

                case vbext_ComponentType.vbext_ct_StdModule:
                case vbext_ComponentType.vbext_ct_MSForm:
                    components.Remove(component);
                    break;
                case vbext_ComponentType.vbext_ct_ActiveXDesigner:
                case vbext_ComponentType.vbext_ct_Document:
                    component.CodeModule.Clear();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Find component by name
        /// </summary>
        /// <param name="components"></param>
        /// <param name="name">component`s name</param>
        /// <returns>null if component not found, otherwise VBComponent</returns>
        public static VBComponent Find(this VBComponents components, string name)
        {
            foreach(VBComponent component in components)
            {
                if(component.Name == name)
                {
                    return component;
                }
            }

            return null;
        }

        /// <summary>
        /// Select components by type
        /// </summary>
        /// <param name="components"></param>
        /// <param name="type">type to select</param>
        /// <returns></returns>
        public static IEnumerable<VBComponent> Select(this VBComponents components, vbext_ComponentType type)
        {
            IEnumerable<VBComponent> list = components.Cast<VBComponent>();
            return list.Where(c => c.Type == type);
        }
                
        /// <summary>
        /// Import component from source file
        /// </summary>
        /// <param name="components"></param>
        /// <param name="filePath">file to import</param>
        public static void ImportSourceFile(this VBComponents components, string filePath)
        {
            var ext = Path.GetExtension(filePath);
            var fileName = Path.GetFileNameWithoutExtension(filePath);

            if (ext == VBComponentExtensions.DocClassExtension)
            {
                var component = components.Item(fileName);
                if (component != null)
                {
                    component.CodeModule.Clear();

                    var text = File.ReadAllText(filePath);
                    component.CodeModule.AddFromString(text);
                }

            }
            else if(ext != VBComponentExtensions.FormBinaryExtension)
            {
                components.Import(filePath);
            }
        }
    }
}
