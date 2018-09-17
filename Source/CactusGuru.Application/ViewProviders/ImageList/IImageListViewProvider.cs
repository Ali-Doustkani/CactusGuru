using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CactusGuru.Application.ViewProviders.ImageList
{
    public interface IImageListViewProvider
    {
        Task GetImagesAsync(IProgress<ImageDto> progress);
        void SaveToFiles(IEnumerable<ImageDto> images, string path);
    }
}
