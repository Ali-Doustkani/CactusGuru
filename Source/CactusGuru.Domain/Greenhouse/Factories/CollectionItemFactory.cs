﻿using CactusGuru.Domain.Greenhouse.CodeGenerating;
using CactusGuru.Infrastructure.ObjectCreation;
using System;

namespace CactusGuru.Domain.Greenhouse.Factories
{
    public class CollectionItemFactory : IFactory<CollectionItem>
    {
        public CollectionItemFactory(ICollectionItemCodeGenerator codeGenerator)
        {
            _codeGenerator = codeGenerator;
        }

        private readonly ICollectionItemCodeGenerator _codeGenerator;

        public CollectionItem CreateNew()
        {
            var ret = new CollectionItem();
            ret.Id = Guid.NewGuid();
            ret.Code = _codeGenerator.Generate();
            ret.IncomeDate = DateTime.Now;
            return ret;
        }

        public CollectionItem CreateNew(FactoryArg argument)
        {
            return CreateNew();
        }
    }
}
