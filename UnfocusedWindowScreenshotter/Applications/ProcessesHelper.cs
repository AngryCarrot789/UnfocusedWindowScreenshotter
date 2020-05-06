using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Documents;

namespace UnfocusedWindowScreenshotter.Applications
{
    public static class ProcessesHelper
    {
        // not using these anymore. scrapped processes and instead
        // fetched only the windows using user32.dll (ApplicationFetcher.cs)
        public static List<Process> GetActiveApplications()
        {
            List<Process> _apps = new List<Process>();
            foreach (Process process in Process.GetProcesses())
            {
                if (!string.IsNullOrEmpty(process.MainWindowTitle))
                {
                    _apps.Add(process);
                }
            }
            return _apps;
        }

        public static IntPtr GetMainHWNDFromProcess(Process process)
        {
            if (process.MainWindowTitle != string.Empty)
            {
                return process.MainWindowHandle;
            }
            return IntPtr.Zero;
        }
    }
}
