﻿using CactusGuru.Infrastructure.Qualification;
using System;

namespace CactusGuru.Domain.Greenhouse.Qualification.Validators
{
    public class CollectionItemImageValidator : ValidatorBase<CollectionItemImage>
    {
        public const string COLLECTION_ITEM_IS_EMPTY = "انتخاب یکی از اقلام مجموعه اجباری است.";
        public const string CONTENT_IS_EMPTY = "انتخاب تصویر اجباری است.";

        protected override ErrorCollection ValidateImp(CollectionItemImage image)
        {
            var ret = new ErrorCollection();
            if (image.CollectionItemId.Equals(Guid.Empty))
                ret.Add(COLLECTION_ITEM_IS_EMPTY);
            if (image.Thumbnail == null || image.Thumbnail.Length == 0)
                ret.Add(CONTENT_IS_EMPTY);
            return ret;
        }
    }
}
