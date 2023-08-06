
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media.Imaging;

namespace UnfocusedWindowScreenshotter.Screenshotting
{
    public static class BitmapConverters
    {
        public static BitmapImage ImageToBitmap(Image image)
        {
            if (image == null)
                return null;
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, ImageFormat.Bmp);
                ms.Seek(0, SeekOrigin.Begin);

                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.StreamSource = ms;
                bitmapImage.EndInit();

                return bitmapImage;
            }
        }
    }
}
