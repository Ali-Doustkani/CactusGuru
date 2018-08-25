using CactusGuru.Domain.Greenhouse;
using CactusGuru.Domain.Persistance.Repositories;
using System;
using CactusGuru.Application.Common;

namespace CactusGuru.Application.Implementation.Assemblers
{
    public class CollectionItemAssembler : AssemblerBase<CollectionItem, CollectionItemDto>
    {
        public CollectionItemAssembler(ICollectorRepository collectorRepo,
            ISupplierRepository supplierRepo,
            ITaxonRepository taxonRepo)
        {
            _collectorRepo = collectorRepo;
            _supplierRepo = supplierRepo;
            _taxonRepo = taxonRepo;
        }

        private readonly ICollectorRepository _collectorRepo;
        private readonly ISupplierRepository _supplierRepo;
        private readonly ITaxonRepository _taxonRepo;

        protected override void FillDataTransferEntityImp(CollectionItemDto dto, CollectionItem domainEntity)
        {
            dto.Code = domainEntity.Code;
            dto.Count = domainEntity.Count;
            dto.Description = domainEntity.Description;
            dto.FieldNumber = domainEntity.FieldNumber;
            dto.IncomeDate = domainEntity.IncomeDate;
            dto.IncomeType = GetIncomeTypeFromDomainEntity(domainEntity);
            dto.Locality = domainEntity.Locality;
            dto.SupplierCode = domainEntity.SupplierCode;
            dto.Collector = domainEntity.Collector.Equals(Collector.Empty) ? (Guid?)null : domainEntity.Collector.Id;
            dto.Supplier = domainEntity.Supplier.Equals(Supplier.Empty) ? (Guid?)null : domainEntity.Supplier.Id;
            dto.Taxon = domainEntity.Taxon.Equals(Taxon.Empty) ? (Guid?)null : domainEntity.Taxon.Id;
        }

        private IncomeTypeDto GetIncomeTypeFromDomainEntity(CollectionItem domainEntity)
        {
            if (domainEntity.IncomeType == IncomeType.None)
                return null;
            if (domainEntity.IncomeType == IncomeType.Plant)
                return IncomeTypeDto.Plant;
            return IncomeTypeDto.Seed;
        }

        protected override void FillDomainEntityImp(CollectionItem domainEntity, CollectionItemDto dto)
        {
            domainEntity.Code = dto.Code;
            domainEntity.Count = dto.Count;
            domainEntity.Description = dto.Description;
            domainEntity.FieldNumber = dto.FieldNumber;
            domainEntity.IncomeDate = dto.IncomeDate;
            domainEntity.IncomeType = GetIncomeTypeFromDto(dto);
            domainEntity.Locality = dto.Locality;
            domainEntity.SupplierCode = dto.SupplierCode;
            if (dto.Collector.HasValue)
                domainEntity.Collector = _collectorRepo.Get(dto.Collector.Value);
            if (dto.Supplier.HasValue)
                domainEntity.Supplier = _supplierRepo.Get(dto.Supplier.Value);
            if (dto.Taxon.HasValue)
                domainEntity.Taxon = _taxonRepo.Get(dto.Taxon.Value);
        }

        private IncomeType GetIncomeTypeFromDto(CollectionItemDto dto)
        {
            if (dto.IncomeType == null)
                return IncomeType.None;
            if (dto.IncomeType.Value == (int)IncomeType.Plant)
                return IncomeType.Plant;
            return IncomeType.Seed;
        }
    }
}
