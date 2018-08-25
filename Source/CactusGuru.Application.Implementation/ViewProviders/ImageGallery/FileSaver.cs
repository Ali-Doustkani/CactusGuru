﻿using CactusGuru.Application.ViewProviders.ImageGallery;
using CactusGuru.Domain.Greenhouse;
using CactusGuru.Domain.Greenhouse.Formatting;
using CactusGuru.Domain.Persistance.Repositories;
using CactusGuru.Infrastructure.Utils;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CactusGuru.Application.Implementation.ViewProviders.ImageGallery
{
    public class FileSaver
    {
        public FileSaver(ICollectionItemImageRepository collectionItemImageRepository,
            ICollectionItemRepository collectionItemRepository,
            IFormatter<CollectionItem> formatter)
        {
            _collectionItemImageRepository = collectionItemImageRepository;
            _collectionItemRepository = collectionItemRepository;
            _formatter = formatter;
        }

        private readonly ICollectionItemImageRepository _collectionItemImageRepository;
        private readonly ICollectionItemRepository _collectionItemRepository;
        private readonly IFormatter<CollectionItem> _formatter;

        public void SaveToFiles(IEnumerable<ImageDto> images, string directoryPath)
        {
            for (int i = 0; i < images.Count(); i++)
            {
                var image = images.ElementAt(i);
                var content = _collectionItemImageRepository.GetFullImage(image.Id);
                var path = Path.Combine(directoryPath, CreateName(image, i + 1));
                File.WriteAllBytes(path, content);
            }
        }

        private string CreateName(ImageDto image, int number)
        {
            var collectionItem = _formatter.Format(_collectionItemRepository.Get(image.CollectionItemId));
            var date = DateUtil.ToPersianDate(image.DateAdded, "-");
            var strNumber = number.ToString().PadLeft(2, '0');
            return $"{collectionItem} {date} ({strNumber}).jpg";
        }
    }
}