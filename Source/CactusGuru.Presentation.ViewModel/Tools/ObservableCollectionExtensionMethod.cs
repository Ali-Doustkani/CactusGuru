using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CactusGuru.Presentation.ViewModel.Tools
{
    public static class ObservableCollectionExtensionMethod
    {
        public static int RemoveAll<T>(this ObservableCollection<T> coll, Func<T, bool> condition)
        {
            var itemsToRemove = coll.Where(condition).ToList();
            foreach (var itemToRemove in itemsToRemove)
                coll.Remove(itemToRemove);
            return itemsToRemove.Count;
        }

        public static void AddRange<T>(this ObservableCollection<T> coll, IEnumerable<T> items)
        {
            foreach (var item in items)
                coll.Add(item);
        }
    }
}
