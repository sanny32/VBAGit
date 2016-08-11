using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Vbe.Interop;

namespace VBAGitAddin.VBEditor.Extensions
{
    public static class VBProjectExtensions
    {
        public static IEnumerable<string> ComponentNames(this VBProject project)
        {
            foreach (VBComponent component in project.VBComponents)
            {
                yield return component.Name;
            }
        }

        /// <summary>
        /// Exports all code modules in the VbProject to a destination directory. Files are given the same name as their parent code Module name and file extensions are based on what type of code Module it is.
        /// </summary>
        /// <param name="project">The <see cref="VBProject"/> to be exported to source files.</param>
        /// <param name="directoryPath">The destination directory path.</param>
        public static void ExportSourceFiles(this VBProject project, string directoryPath)
        {            
            IEnumerable<VBComponent> components = project.VBComponents.Cast<VBComponent>();
            components.ToList().ForEach(component => component.ExportAsSourceFile(directoryPath));
        }

        /// <summary>
        /// Removes All VbComponents from the VbProject.
        /// </summary>
        /// <remarks>
        /// Document type Components cannot be physically removed from a project through the VBE.
        /// Instead, the code will simply be deleted from the code Module.
        /// </remarks>
        /// <param name="project"></param>
        public static void RemoveAllComponents(this VBProject project)
        {
            IEnumerable<VBComponent> components = project.VBComponents.Cast<VBComponent>();
            components.ToList().ForEach(component => project.VBComponents.RemoveSafely(component));            
        }

        /// <summary>
        /// Determine than VBComponents contains VBProject with name
        /// </summary>
        /// <param name="project"></param>
        /// <param name="name">Name of project</param>
        /// <returns>true, if VBComponents contains VBProject with name, otherwise false</returns>
        public static bool Contains(this VBProject project, string name)
        {
            return project.VBComponents.Find(name) != null;
        }

        /// <summary>
        /// Returns then name for the repository
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        public static string GetRepoName(this VBProject project)
        {
            return Path.GetFileNameWithoutExtension(project.FileName);
        }

        /// <summary>
        /// Remove Vbcomponent from VbProject.
        /// </summary>        
        /// <param name="project"></param>
        /// <param name="name">name of removed component</param>   
        /// <returns>true, if component was removed successfully, otherwise false</returns>
        public static bool RemoveComponent(this VBProject project, string name)
        {
            var component = project.VBComponents.Find(name);
            if(component != null)
            {
                project.VBComponents.RemoveSafely(component);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Imports all source code files from target directory into project.
        /// </summary>
        /// <remarks>
        /// Only files with extensions "cls", "bas, "frm" are imported.
        /// It is the callers responsibility to remove any existing components prior to importing.
        /// </remarks>
        /// <param name="project"></param>
        /// <param name="path">Directory path containing the source files.</param>
        public static void ImportSourceFiles(this VBProject project, string path)
        {
            var dirInfo = new DirectoryInfo(path);

            var files = dirInfo.EnumerateFiles()
                                .Where(f => f.Extension == VBComponentExtensions.StandardExtension ||
                                            f.Extension == VBComponentExtensions.ClassExtesnion ||
                                            f.Extension == VBComponentExtensions.DocClassExtension ||
                                            f.Extension == VBComponentExtensions.FormExtension
                                            );

            files.ToList().ForEach(file => project.ImportSourceFile(file.FullName));            
        }

        /// <summary>
        /// Import source code from target file.
        /// </summary>
        /// <param name="project"></param>
        /// <param name="filePath">File path containing the source file</param>
        public static void ImportSourceFile(this VBProject project, string filePath)
        {
            project.VBComponents.ImportSourceFile(filePath);
        }
    }
}
