using System;
using System.Collections.Generic;

namespace CactusGuru.Application.ViewProviders.ImageList
{
    public interface IImageListViewProvider
    {
        bool GetImagesAsync(Action<IEnumerable<ImageDto>> callback);
        void SaveToFiles(IEnumerable<ImageDto> images, string path);
    }
}
