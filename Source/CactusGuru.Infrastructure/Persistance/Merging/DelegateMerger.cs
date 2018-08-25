using System;

namespace CactusGuru.Infrastructure.Persistance.Merging
{
    public class DelegateMerger<T> : MergerAlgorithm<T>
        where T : DomainEntity
    {
        public DelegateMerger(Action<T> add, Action<T> update, Action<T> delete)
        {
            _add = add;
            _update = update;
            _delete = delete;
        }

        private readonly Action<T> _add;
        private readonly Action<T> _update;
        private readonly Action<T> _delete;

        protected override void AddImp(T item)
        {
            _add(item);
        }

        protected override void UpdateImp(T item)
        {
            _update(item);
        }

        protected override void DeleteImp(T item)
        {
            _delete(item);
        }
    }
}
