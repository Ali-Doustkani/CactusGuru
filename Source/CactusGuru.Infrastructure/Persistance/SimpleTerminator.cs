using CactusGuru.Infrastructure.Qualification;
using CactusGuru.Infrastructure.Utils;
using System;

namespace CactusGuru.Infrastructure.Persistance
{
    public class SimpleTerminator<T> : ITerminator<T>
        where T : DomainEntity
    {
        public SimpleTerminator(IRepository<T> repo, InquiryBase<T> inquiry)
        {
            _repo = ArgumentChecker.CheckUp(repo);
            _inquiry = ArgumentChecker.CheckUp(inquiry);
        }

        private readonly IRepository<T> _repo;
        private readonly InquiryBase<T> _inquiry;

        public virtual void Terminate(Guid id)
        {
            ArgumentChecker.CheckEmpty(id);
            _inquiry.Inquiry(id);
            _repo.Delete(id);
        }
    }
}
