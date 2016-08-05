using System.IO;
using Microsoft.Vbe.Interop;
using VBAGitAddin.Diagnostics;

namespace VBAGitAddin.VBEditor.Extensions
{
    public static class VBComponentExtensions
    {
        internal const string ClassExtesnion = ".cls";
        internal const string FormExtension = ".frm";
        internal const string StandardExtension = ".bas";
        internal const string FormBinaryExtension = ".frx";
        internal const string DocClassExtension = ".doccls";

        internal const string Module = "module";
        internal const string Form = "form";
        internal const string ClassModule = "class module";        

        /// <summary>
        /// Exports the component to the directoryPath. The file is name matches the component name and file extension is based on the component's type.
        /// </summary>
        /// <param name="component">The component to be exported to the file system.</param>
        /// <param name="directoryPath">Destination Path for the resulting source file.</param>
        public static void ExportAsSourceFile(this VBComponent component, string directoryPath)
        {
            Trace.TraceInformation("Exporting {0}... {1}", component.Type.GetName(), component.Name);

            string filePath = Path.Combine(directoryPath, component.Name + component.Type.GetFileExtension());
            if (component.Type == vbext_ComponentType.vbext_ct_Document)
            {
                int lineCount = component.CodeModule.CountOfLines;
                if (lineCount > 0)
                {
                    var text = component.CodeModule.get_Lines(1, lineCount);
                    File.WriteAllText(filePath, text);
                }
            }
            else
            {
                component.Export(filePath);
            }
        }

        /// <summary>
        /// Returns the proper file extension for the Component Type.
        /// </summary>        
        /// <param name="componentType"></param>
        /// <returns>File extension that includes a preceeding "dot" (.) </returns>
        public static string GetFileExtension(this vbext_ComponentType componentType)
        {
            switch (componentType)
            {
                case vbext_ComponentType.vbext_ct_ClassModule:
                    return ClassExtesnion;
                case vbext_ComponentType.vbext_ct_MSForm:
                    return FormExtension;
                case vbext_ComponentType.vbext_ct_StdModule:
                    return StandardExtension;
                case vbext_ComponentType.vbext_ct_Document:                    
                    return DocClassExtension;
                case vbext_ComponentType.vbext_ct_ActiveXDesigner:
                default:
                    return string.Empty;
            }
        }

        /// <summary>
        /// Returns component type name
        /// </summary>
        /// <param name="componentType"></param>
        /// <returns>The string represents component type name</returns>
        public static string GetName(this vbext_ComponentType componentType)
        {
            switch (componentType)
            {
                case vbext_ComponentType.vbext_ct_ClassModule:
                    return ClassModule;
                case vbext_ComponentType.vbext_ct_MSForm:
                    return Form;
                case vbext_ComponentType.vbext_ct_StdModule:
                    return Module;
                case vbext_ComponentType.vbext_ct_Document:
                    return Module;
                case vbext_ComponentType.vbext_ct_ActiveXDesigner:
                default:
                    return string.Empty;
            }
        }
    }
}