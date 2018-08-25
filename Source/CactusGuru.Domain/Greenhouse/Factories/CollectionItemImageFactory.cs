using CactusGuru.Infrastructure.ObjectCreation;
using CactusGuru.Infrastructure.Utils;
using System;

namespace CactusGuru.Domain.Greenhouse.Factories
{
    public class CollectionItemImageFactory : IFactory<CollectionItemImage>
    {
        public CollectionItemImageFactory(IImageEditor imageEditor)
        {
            _imageEditor = imageEditor;
        }

        private readonly IImageEditor _imageEditor;

        public CollectionItemImage CreateNew()
        {
            throw new InvalidOperationException("You can not create this object without argument.");
        }

        public CollectionItemImage CreateNew(FactoryArg argument)
        {
            var arg = ArgumentChecker.CheckFactoryArgument(argument).Is<CollectionItemImageFactoryArg>();
            var ret = new CollectionItemImage();
            ret.DateAdded = DateTime.Now;
            ret.Id = Guid.NewGuid();
            ret.CollectionItemId = arg.CollectionItemId;
            ret.Thumbnail = _imageEditor.CreateThumbnail(arg.FilePath);
            return ret;
        }
    }
}
