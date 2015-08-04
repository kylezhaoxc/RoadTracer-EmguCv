using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Emgu.CV;
using Emgu.CV.Structure;
using Image = System.Windows.Controls.Image;

namespace TestWpf
{
    internal class UiHandler
    {
        public static void show_Image(Image destUiToShow, Image<Bgr, byte> imageToDisplay)
        {
            if (imageToDisplay != null)
                destUiToShow.Source = ToBitmapSource(imageToDisplay.ToBitmap());
        }

        public static Bitmap Bmimg2Bitmap(BitmapImage img)
        {
            using (var outstream = new MemoryStream())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(img));
                enc.Save(outstream);
                var bitmap = new Bitmap(outstream);
                return new Bitmap(bitmap);
            }
        }

        [DllImport("gdi32")]
        private static extern int DeleteObject(IntPtr o);

        public static ImageSource ToBitmapSource(Bitmap image)
        {
            var ptr = image.GetHbitmap();

            ImageSource bs = Imaging.CreateBitmapSourceFromHBitmap(
                ptr,
                IntPtr.Zero,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());

            DeleteObject(ptr);

            return bs;
        }

    }
}