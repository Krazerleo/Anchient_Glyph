using System.Collections.Generic;

namespace AncientGlyph.GameScripts.AlgorithmsAndStructures.PriorityQueue
{
    public interface IPriorityQueue<in TKey, T>
    {
        void Enqueue(T item);
        void Enqueue(IEnumerable<T> items);
        T Dequeue();
        void Clear();
        bool TryGet(TKey key, out T value);
        void Modify(T value);
        int Count { get; }        
    }
}