using CactusGuru.Domain.Greenhouse;
using CactusGuru.Domain.Persistance.Repositories;
using CactusGuru.Infrastructure.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;

namespace CactusGuru.Application.Implementation.Services
{
    public class InstagramPackageMaker
    {
        public InstagramPackageMaker(ICollectionItemImageRepository collectionItemImageRepository,ICollectionItemRepository collectionItemRepository)
        {
            _collectionItemImageRepository = collectionItemImageRepository;
            _collectionItemRepository = collectionItemRepository;
        }

        private readonly ICollectionItemImageRepository _collectionItemImageRepository;
        private readonly ICollectionItemRepository _collectionItemRepository;

        public void SaveToZip(IEnumerable<CollectionItemImage> images, string targetPath)
        {
            var memoryStream = new MemoryStream();
            using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
            {
                for (int i = 0; i < images.Count(); i++)
                {
                    var fileName = $"{i + 1}.jpg";
                    var entry = archive.CreateEntry(fileName);
                    WriteImageToZip(images, i, entry);
                }
                WriteInfoToZip(images, archive);
            }
            CreateZipFile(targetPath, memoryStream);
        }

        private void WriteImageToZip(IEnumerable<CollectionItemImage> images, int nth, ZipArchiveEntry entry)
        {
            using (var entryStream = entry.Open())
            {
                using (var binaryWriter = new BinaryWriter(entryStream))
                {
                    var content = _collectionItemImageRepository.GetFullImage(images.ElementAt(nth).Id);
                    binaryWriter.Write(content);
                }
            }
        }

        private void WriteInfoToZip(IEnumerable<CollectionItemImage> images, ZipArchive archive)
        {
            var sb = new StringBuilder();
            var counter = 1;
            foreach (var image in images)
            {
                sb.Append(counter++);
                sb.AppendLine();
                sb.Append( _collectionItemRepository.Get(image.CollectionItemId).Format("{GENUS} {taxon}"));
                sb.AppendLine();
                sb.Append(image.DateAdded.Year);
                sb.AppendLine();
                sb.AppendLine();
                sb.Append("cactusbaz.ir");
                sb.AppendLine();
                sb.AppendLine();
            }

            var textEntry = archive.CreateEntry("Info.txt");
            using (var entryStream = textEntry.Open())
            using (var binaryWriter = new BinaryWriter(entryStream))
                binaryWriter.Write(sb.ToString());
        }

        private void CreateZipFile(string targetPath, MemoryStream memoryStream)
        {
            var fileName = $"{DateUtil.ToPersianDate(DateTime.Now, "-")}.zip";
            var path = Path.Combine(targetPath, fileName);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                memoryStream.Seek(0, SeekOrigin.Begin);
                memoryStream.CopyTo(fileStream);
            }
        }
    }
}