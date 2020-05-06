
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows;
using UnfocusedWindowScreenshotter.Applications;
using UnfocusedWindowScreenshotter.Controls;
using UnfocusedWindowScreenshotter.Screenshotting;

namespace UnfocusedWindowScreenshotter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            IntPtr hWnd = GetSelectedAppHWND();
            if (hWnd != IntPtr.Zero)
            {
                Image desktopImage = Screenshotter.CaptureWindow(hWnd);
                ImageBox.Source = BitmapConverters.ImageToBitmap(desktopImage);
            }
        }

        private void refreshButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (KeyValuePair<IntPtr, string> thing in ApplicationFetcher.GetOpenWindows())
            {
                AddApplicationControl(thing);
            }
        }

        #region ApplicationControl Helpers

        public void AddApplicationControl(Process process)
        {
            ApplicationControl ac = new ApplicationControl(process);
            ApplicationSources.Items.Add(ac);
        }
        public void AddApplicationControl(KeyValuePair<IntPtr, string> val)
        {
            ApplicationControl ac = new ApplicationControl(val.Key, val.Value);
            ApplicationSources.Items.Add(ac);
        }

        public ApplicationControl GetSelectedApplicationControl()
        {
            if (ApplicationSources.Items.Count > 0 &&
                ApplicationSources.SelectedItem != null &&
                ApplicationSources.SelectedItem is ApplicationControl ac)
            {
                return ac;
            }
            return null;
        }

        public Process GetSelectedAppProcess()
        {
            ApplicationControl ac = GetSelectedApplicationControl();
            if (ac != null && ac.Process != null)
            {
                return ac.Process;
            }
            return null;
        }
        public IntPtr GetSelectedAppHWND()
        {
            ApplicationControl ac = GetSelectedApplicationControl();
            if (ac != null && ac.HWND != IntPtr.Zero)
            {
                return ac.HWND;
            }
            return IntPtr.Zero;
        }

        public Process GetAppControlProcess(ApplicationControl ac)
        {
            return ac.Process;
        }

        public void ClearApplications()
        {
            ApplicationSources.Items.Clear();
        }

        #endregion
    }
}
