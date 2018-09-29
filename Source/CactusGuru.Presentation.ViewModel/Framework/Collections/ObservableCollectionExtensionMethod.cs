using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CactusGuru.Presentation.ViewModel.Framework.Collections
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

        public static void RemoveAll<T>(this ObservableCollection<T> col, T[] items)
        {
            for (int i = 0; i < items.Count(); i++)
                col.Remove(items[i]);
        }

        public static void AddRange<T>(this ObservableCollection<T> coll, IEnumerable<T> items)
        {
            foreach (var item in items)
                coll.Add(item);
        }
    }
}
