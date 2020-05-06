using System;
using System.Drawing;
using System.Net.NetworkInformation;

namespace UnfocusedWindowScreenshotter.Screenshotting
{
    public static class Screenshotter
    {
        public static Image CaptureScreen()
        {
            return CaptureWindow(User32.GetDesktopWindow());
        }

        public static Image CaptureWindow(IntPtr handle)
        {

            IntPtr hdcSrc = User32.GetWindowDC(handle);

            ScreenshotterUtilities.RECT windowRect = new ScreenshotterUtilities.RECT();
            User32.GetWindowRect(handle, ref windowRect);

            int width = windowRect.right - windowRect.left;
            int height = windowRect.bottom - windowRect.top;

            IntPtr hdcDest = Gdi32.CreateCompatibleDC(hdcSrc);
            IntPtr hBitmap = Gdi32.CreateCompatibleBitmap(hdcSrc, width, height);
            if (hBitmap != IntPtr.Zero && hdcDest != IntPtr.Zero)
            {
                IntPtr hOld = Gdi32.SelectObject(hdcDest, hBitmap);
                Gdi32.BitBlt(hdcDest, 0, 0, width, height, hdcSrc, 0, 0, ScreenshotterUtilities.SRCCOPY);
                Gdi32.SelectObject(hdcDest, hOld);
                Gdi32.DeleteDC(hdcDest);
                User32.ReleaseDC(handle, hdcSrc);
                Image image = Image.FromHbitmap(hBitmap);
                Gdi32.DeleteObject(hBitmap);
                return image;
            }
            Gdi32.DeleteObject(hBitmap);
            return null;
        }
    }
}
