﻿using CactusGuru.Infrastructure.Qualification;
using System;

namespace CactusGuru.Infrastructure.Persistance
{
    public class Terminator<T> : SimpleTerminator<T>
        where T : DomainEntity
    {
        public Terminator(IUnitOfWork uow, IRepository<T> repo, InquiryBase<T> inquiry)
            : base(repo, inquiry)
        {
            _uow = uow;
        }

        private readonly IUnitOfWork _uow;

        public override void Terminate(Guid id)
        {
            base.Terminate(id);
            _uow.SaveChanges();
        }
    }
}
