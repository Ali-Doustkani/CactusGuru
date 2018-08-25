using CactusGuru.Infrastructure.Utils;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace CactusGuru.Entry.Presentation
{
    internal class ImageEditor : IImageEditor
    {
        public byte[] CreateThumbnail(string filePath)
        {
            var image = Image.FromFile(filePath);
            var thumb = image.GetThumbnailImage(200, 200, () => false, IntPtr.Zero);
            var ms = new MemoryStream();
            thumb.Save(ms, ImageFormat.Jpeg);
            return ms.ToArray();
        }
    }
}
