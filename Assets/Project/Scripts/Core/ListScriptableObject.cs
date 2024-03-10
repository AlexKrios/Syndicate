using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Syndicate.Core
{
    public class ListScriptableObject<T> : ScriptableObject, IList<T>, IReadOnlyList<T>
    {
        [SerializeField] private List<T> items = new();

        public List<T> Items => items;

        public T this[int index]
        {
            get => items[index];
            set => items[index] = value;
        }

        public int Count => items.Count;

        public bool IsReadOnly => ((IList<T>)items).IsReadOnly;

        public void Add(T item) => items.Add(item);

        public void AddRange(IEnumerable<T> collection) => items.AddRange(collection);

        public void Clear() => items.Clear();

        public bool Contains(T item) => items.Contains(item);

        public void CopyTo(T[] array, int arrayIndex) => items.CopyTo(array, arrayIndex);

        public IEnumerator<T> GetEnumerator() => items.GetEnumerator();

        public int IndexOf(T item) => items.IndexOf(item);

        public void Insert(int index, T item) => items.Insert(index, item);

        public bool Remove(T item) => items.Remove(item);

        public void RemoveAt(int index) => items.RemoveAt(index);

        public int RemoveAll(Predicate<T> match) => items.RemoveAll(match);

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)items).GetEnumerator();
    }
}