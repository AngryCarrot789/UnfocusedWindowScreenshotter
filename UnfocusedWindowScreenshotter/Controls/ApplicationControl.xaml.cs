using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UnfocusedWindowScreenshotter.Controls
{
    /// <summary>
    /// Interaction logic for ApplicationControl.xaml
    /// </summary>
    public partial class ApplicationControl : UserControl
    {
        public Process Process { get; set; }
        public IntPtr HWND { get; set; }
        public ApplicationControl(Process process)
        {
            InitializeComponent();
            Process = process;
            if (process.MainWindowTitle != string.Empty)
                ApplicationTitle.Content = process.MainWindowTitle;
        }
        public ApplicationControl(Process process, IntPtr hWnd)
        {
            InitializeComponent();
            Process = process;
            if (process.MainWindowTitle != string.Empty)
                ApplicationTitle.Content = process.MainWindowTitle;
            if (hWnd != IntPtr.Zero)
                HWND = hWnd;
        }
        public ApplicationControl(IntPtr hWnd, string title)
        {
            InitializeComponent();
            if (hWnd != IntPtr.Zero)
                HWND = hWnd;
            ApplicationTitle.Content = title;
        }
    }
}
