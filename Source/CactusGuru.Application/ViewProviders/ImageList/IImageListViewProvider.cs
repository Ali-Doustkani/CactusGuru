using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CactusGuru.Application.ViewProviders.ImageList
{
    public interface IImageListViewProvider
    {
        Task GetImagesAsync(IProgress<ImageDto> progress);
        Task Delete(IEnumerable<ImageDto> images);
        Task SaveForInstagram(IEnumerable<ImageDto> images, string path);
        Task SaveToFile(IEnumerable<ImageDto> images, string path);
    }
}
