using System.Runtime.InteropServices;

namespace UnfocusedWindowScreenshotter.Screenshotting
{
    public static class ScreenshotterUtilities
    {
        public const int SRCCOPY = 13369376;

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }
    }
}
