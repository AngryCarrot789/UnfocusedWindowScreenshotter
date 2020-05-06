
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
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
        public bool IsCapturing { get; set; }
        public int FPS { get; set; }
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SelectedHWND = GetSelectedAppHWND();
            if (SelectedHWND != IntPtr.Zero)
            {
                Task.Run(() =>
                {
                    Image image = Screenshotter.CaptureWindow(SelectedHWND);
                    OnFrameCaptured(image);
                });
            }
        }

        private void capturingButton_Click(object sender, RoutedEventArgs e)
        {
            if (!IsCapturing)
            {
                capturingButton.Background = new SolidColorBrush(Colors.Red);
                capturingButton.Content = "Stop Capturing";
                IsCapturing = true;
                BeginCapturing();
            }
            else
            {
                capturingButton.Background = new SolidColorBrush(Colors.Green);
                capturingButton.Content = "Start Capturing";
                StopCapturing();
                IsCapturing = false;
            }
        }
        private IntPtr SelectedHWND { get; set; }
        public void BeginCapturing()
        {
            SelectedHWND = GetSelectedAppHWND();

            Task.Run(async() =>
            {
                while (IsCapturing)
                {
                    if (SelectedHWND != IntPtr.Zero)
                    {
                        Image image = Screenshotter.CaptureWindow(SelectedHWND);
                        OnFrameCaptured(image);
                    }
                    await Task.Delay(1000 / FPS);
                }

            });
        }

        public void StopCapturing()
        {
            SelectedHWND = IntPtr.Zero;
        }

        public void OnFrameCaptured(Image image)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                ImageBox.Source = BitmapConverters.ImageToBitmap(image);
            });
        }

        private void refreshButton_Click(object sender, RoutedEventArgs e)
        {
            ClearApplications();
            foreach (KeyValuePair<IntPtr, string> thing in ApplicationFetcher.GetOpenWindows())
            {
                AddApplicationControl(thing);
            }
        }

        private void FPSValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            FPS = Convert.ToInt32(e.NewValue);
        }

        #region ApplicationControl Helpers

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

        public IntPtr GetSelectedAppHWND()
        {
            ApplicationControl ac = GetSelectedApplicationControl();
            if (ac != null && ac.HWND != IntPtr.Zero)
            {
                return ac.HWND;
            }
            return IntPtr.Zero;
        }

        public void ClearApplications()
        {
            ApplicationSources.Items.Clear();
        }
        #endregion
    }
}
