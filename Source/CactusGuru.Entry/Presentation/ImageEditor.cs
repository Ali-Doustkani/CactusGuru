using CactusGuru.Infrastructure.Utils;
using System;
using System.IO;
using System.Windows.Media.Imaging;

namespace CactusGuru.Entry.Presentation
{
    public class ImageEditor : IImageEditor
    {
        public byte[] CreateThumbnail(string filePath)
        {
            var img = new BitmapImage();
            img.BeginInit();
            img.UriSource = new Uri(filePath);
            img.DecodePixelWidth = 200;
            img.EndInit();

            var encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(img));
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                return ms.ToArray();
            }
        }
    }
}
