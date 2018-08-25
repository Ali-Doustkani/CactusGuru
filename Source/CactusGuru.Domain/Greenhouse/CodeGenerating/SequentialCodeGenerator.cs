using System.Collections.Generic;
using System.Linq;
using CactusGuru.Domain.Persistance.Repositories;

namespace CactusGuru.Domain.Greenhouse.CodeGenerating
{
    public class SequentialCodeGenerator : ICollectionItemCodeGenerator
    {
        public SequentialCodeGenerator(ICollectionItemRepository repository)
        {
            _repository = repository;
        }

        private readonly ICollectionItemRepository _repository;

        public string Generate()
        {
            var stringCodes = _repository.GetAllCodes();
            var integerCodes = new List<int>();
            foreach (var strCode in stringCodes)
            {
                int result;
                if (int.TryParse(strCode, out result))
                    integerCodes.Add(result);
            }

            if (!integerCodes.Any())
                return "1";

            return (integerCodes.Max() + 1).ToString();
        }
    }
}
