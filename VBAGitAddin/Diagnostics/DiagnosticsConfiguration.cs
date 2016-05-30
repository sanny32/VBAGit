
namespace VBAGitAddin.Diagnostics
{
    internal static class DiagnosticsConfiguration
    {
        // setting for TraceInternal.AutoFlush
        internal static bool AutoFlush
        {
            get
            {
                return false; // the default
            }
        }

        // setting for TraceInternal.UseGlobalLock
        internal static bool UseGlobalLock
        {
            get
            {
                return true; // the default
            }
        }

        // setting for TraceInternal.IndentSize
        internal static int IndentSize
        {
            get
            {
                return 4; // the default
            }
        }
    }
}
