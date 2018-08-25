using System.Collections.Generic;
using System.Linq;

namespace CactusGuru.Infrastructure.Persistance.Merging
{
    public abstract class MergerAlgorithm<T> : IMerger<T>
       where T : DomainEntity
    {
        public void Merge(IEnumerable<T> originalItems, IEnumerable<T> currentItems)
        {
            DeleteItems(originalItems, currentItems);

            foreach (var current in currentItems)
            {
                if (ShouldOperate(originalItems, current))
                {
                    AddImp(current);
                    continue;
                }
                UpdateImp(current);
            }
        }

        private void DeleteItems(IEnumerable<T> originalItems, IEnumerable<T> currentItems)
        {
            foreach (var original in originalItems)
            {
                if (ShouldOperate(currentItems, original))
                    DeleteImp(original);
            }
        }

        private bool ShouldOperate(IEnumerable<T> originalItems, T currentItem)
        {
            return !originalItems.Any(x => x.Equals(currentItem));
        }

        protected abstract void AddImp(T item);
        protected abstract void UpdateImp(T item);
        protected abstract void DeleteImp(T item);
    }
}
