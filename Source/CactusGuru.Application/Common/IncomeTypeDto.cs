using System;

namespace CactusGuru.Application.Common
{
    public class IncomeTypeDto : TransferObjectBase
    {
        public int Value { get; set; }

        public static IncomeTypeDto Plant => new IncomeTypeDto { Id = Guid.NewGuid(), Value = 2 };

        public static IncomeTypeDto Seed => new IncomeTypeDto { Id = Guid.NewGuid(), Value = 1 };
    }
}
