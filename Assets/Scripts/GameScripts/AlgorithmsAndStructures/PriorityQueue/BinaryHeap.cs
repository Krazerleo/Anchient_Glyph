﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace AncientGlyph.GameScripts.AlgorithmsAndStructures.PriorityQueue
{
    internal class BinaryHeap<TKey, T> : IPriorityQueue<TKey, T> where TKey : IEquatable<TKey>
    {
        private readonly IDictionary<TKey, int> _map;
        private readonly IList<T> _collection;
        private readonly IComparer<T> _comparer;
        private readonly Func<T, TKey> _lookupFunc;

        public BinaryHeap(IComparer<T> comparer, Func<T, TKey> lookupFunc, int capacity)
        {
            _comparer = comparer;
            _lookupFunc = lookupFunc;
            _collection = new List<T>(capacity);
            _map = new Dictionary<TKey, int>(capacity);
        }

        public int Count => _collection.Count;

        public void Enqueue(T item)
        {
            _collection.Add(item);
            int i = _collection.Count - 1;
            _map[_lookupFunc(item)] = i;

            while (i > 0)
            {
                int j = (i - 1) / 2;

                if (_comparer.Compare(_collection[i], _collection[j]) <= 0)
                    break;

                Swap(i, j);
                i = j;
            }
        }

        public T Dequeue()
        {
            if (_collection.Count == 0) return default;

            T result = _collection.First();
            RemoveRoot();
            _map.Remove(_lookupFunc(result));
            return result;
        }

        public void Clear()
        {
            _collection.Clear();
            _map.Clear();
        }

        public bool TryGet(TKey key, out T value)
        {
            if (!_map.TryGetValue(key, out int index))
            {
                value = default;
                return false;
            }

            value = _collection[index];
            return true;
        }

        public void Modify(T value)
        {
            if (!_map.TryGetValue(_lookupFunc(value), out int index))
                throw new KeyNotFoundException(nameof(value));

            _collection[index] = value;
        }

        private void RemoveRoot()
        {
            _collection[0] = _collection.Last();
            _map[_lookupFunc(_collection[0])] = 0;
            _collection.RemoveAt(_collection.Count - 1);

            var i = 0;
            while (true)
            {
                int largest = LargestIndex(i);
                if (largest == i)
                    return;

                Swap(i, largest);
                i = largest;
            }
        }

        private void Swap(int i, int j)
        {
            T temp = _collection[i];
            _collection[i] = _collection[j];
            _collection[j] = temp;
            _map[_lookupFunc(_collection[i])] = i;
            _map[_lookupFunc(_collection[j])] = j;
        }

        private int LargestIndex(int i)
        {
            int leftInd = 2 * i + 1;
            int rightInd = 2 * i + 2;
            int largest = i;

            if (leftInd < _collection.Count && _comparer.Compare(_collection[leftInd], _collection[largest]) > 0)
                largest = leftInd;

            if (rightInd < _collection.Count && _comparer.Compare(_collection[rightInd], _collection[largest]) > 0)
                largest = rightInd;

            return largest;
        }
    }
}