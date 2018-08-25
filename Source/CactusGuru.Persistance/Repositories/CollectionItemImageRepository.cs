using CactusGuru.Domain.Greenhouse;
using CactusGuru.Domain.Persistance.Repositories;
using CactusGuru.Infrastructure.Persistance;
using CactusGuru.Persistance.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CactusGuru.Persistance.Repositories
{
    public class CollectionItemImageRepository : ICollectionItemImageRepository
    {
        public CollectionItemImageRepository(CactusGuruEntities context, TranslatorBase<CollectionItemImage, tblCollectionItemImage> translator)
        {
            _context = context;
            _translator = translator;
        }

        private readonly CactusGuruEntities _context;
        private readonly TranslatorBase<CollectionItemImage, tblCollectionItemImage> _translator;

        public void Add(CollectionItemImage collectionItemImage)
        {
            var entity = _translator.ToDataEntity(collectionItemImage);
            _context.tblCollectionItemImage.Add(entity);
        }

        public void Update(CollectionItemImage collectionItemImage)
        {
            var entity = _context.tblCollectionItemImage.Find(collectionItemImage.Id);
            _translator.FillDataEntity(entity, collectionItemImage);
        }

        public void Delete(Guid id)
        {
            var entity = _context.tblCollectionItemImage.Find(id);
            _context.tblCollectionItemImage.Remove(entity);
        }

        public IEnumerable<CollectionItemImage> GetByCollectionItemId(Guid collectionItemId)
        {
            var entities = _context.tblCollectionItemImage.Where(x => x.tblCollectionItemId == collectionItemId);
            return _translator.ToDomainEntities(entities);
        }

        public IEnumerable<Guid> GetIdsByCollectionItemId(Guid collectionItemId)
        {
            return _context.tblCollectionItemImage.Where(x => x.tblCollectionItemId == collectionItemId).Select(x => x.Id).ToList();
        }

        public void AddFullImage(Guid id, byte[] imageContent)
        {
            var item = new tblCollectionItemFullImage();
            item.Id = id;
            item.Image = imageContent;
            _context.tblCollectionItemFullImage.Add(item);
        }

        public void DeleteFullImage(Guid id)
        {
            var item = new tblCollectionItemFullImage();
            item.Id = id;
            _context.tblCollectionItemFullImage.Attach(item);
            _context.tblCollectionItemFullImage.Remove(item);
        }

        public CollectionItemImage Get(Guid id)
        {
            var entity = _context.tblCollectionItemImage.Find(id);
            return _translator.ToDomainEntity(entity);
        }

        public IEnumerable<CollectionItemImage> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CollectionItemImage> GetByRange(int startIndex, int count)
        {
            var entities = _context.tblCollectionItemImage
                .Where(x => !x.SharedOnInstagram)
                .OrderByDescending(x => x.DateAdded)
                .Skip(startIndex)
                .Take(count)
                .ToList();
            return _translator.ToDomainEntities(entities);
        }

        public byte[] GetFullImage(Guid id)
        {
            return _context.tblCollectionItemFullImage.Single(x => x.Id == id).Image;
        }

        public void UpdateSharedOnInstagram(IEnumerable<Guid> ids)
        {
            var entities = _context.tblCollectionItemImage.Where(x => ids.Contains(x.Id));
            foreach (var entity in entities)
                entity.SharedOnInstagram = true;
        }
    }
}
