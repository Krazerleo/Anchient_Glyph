using System.Collections.Generic;
using AncientGlyph.GameScripts.AlgorithmsAndStructures.PathFinding;
using UnityEngine;

namespace AncientGlyph.GameScripts.AlgorithmsAndStructures.PriorityQueue
{
    public class BinaryHeap: IPriorityQueue<Vector3Int, PathNode>
    {
        private readonly Dictionary<Vector3Int, int> _map = new();
        private readonly List<PathNode> _collection = new();
        private readonly IComparer<PathNode> _comparer;
        
        public BinaryHeap(IComparer<PathNode> comparer)
        {
            _comparer = comparer;
        }
        
        public int Count => _collection.Count;
        
        public void Enqueue(PathNode item)
        {
            _collection.Add(item);
            var i = _collection.Count - 1;
            _map[item.Position] = i;

            while(i > 0)
            {
                var j = (i - 1) / 2;
                
                if (_comparer.Compare(_collection[i], _collection[j]) <= 0)
                    break;

                Swap(i, j);
                i = j;
            }
        }

        public PathNode Dequeue()
        {
            if (_collection.Count == 0) return default;
          
            PathNode result = _collection[0];
            RemoveRoot();
            _map.Remove(result.Position);
            return result;
        }

        public void Clear()
        {
            _collection.Clear();
            _map.Clear();
        }

        public bool TryGet(Vector3Int key, out PathNode value)
        {
            if (!_map.TryGetValue(key, out int index))
            {
                value = default;
                return false;
            }
            
            value = _collection[index];
            return true;
        }

        public void Modify(PathNode value)
        {
            if (!_map.TryGetValue(value.Position, out int index))
                throw new KeyNotFoundException(nameof(value));
            
            _collection[index] = value;
        }
        
        private void Swap(int i, int j)
        {
            (_collection[i], _collection[j]) = (_collection[j], _collection[i]);
            _map[_collection[i].Position] = i;
            _map[_collection[j].Position] = j;
            
            _map[_collection[i].Position] = i;
            _map[_collection[j].Position] = j;
        }
        
        private void RemoveRoot()
        {
            _collection[0] = _collection[^1];
            _map[_collection[0].Position] = 0;
            _collection.RemoveAt(_collection.Count - 1);

            int i = 0;
            while(true)
            {
                int largest = LargestIndex(i);
                if (largest == i)
                    return;

                Swap(i, largest);
                i = largest;
            }
        }
	
        private int LargestIndex(int i)
        {
            int leftInd = 2 * i + 1;
            int rightInd = 2 * i + 2;
            int largest = i;

            if (leftInd < _collection.Count 
                && _comparer.Compare(_collection[leftInd], _collection[largest]) > 0) 
                largest = leftInd;

            if (rightInd < _collection.Count 
                && _comparer.Compare(_collection[rightInd], _collection[largest]) > 0) 
                largest = rightInd;
            
            return largest;
        }
    }
}