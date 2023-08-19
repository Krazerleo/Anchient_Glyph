using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AncientGlyph.GameScripts.GameWorldModel
{
    public class WorldModel
    {
        public WorldModel(Dictionary<(string, Vector3Int), (string, Vector3Int)> levelInterop)
        {
            _levelInterop = levelInterop;
        }

        private Dictionary<(string exitLevelId, Vector3Int exitPosition), (string enterLevelId, Vector3Int enterPosition)> _levelInterop;

        public (string enterLevelId, Vector3Int enterPosition) GetEnterPoint((string exitLevelId, Vector3Int exitPosition) exitPoint)
        {
            if (_levelInterop.ContainsKey(exitPoint))
            {
                return _levelInterop[exitPoint];
            }
            else if (_levelInterop.ContainsValue(exitPoint))
            {
                return _levelInterop.First(x => x.Value == exitPoint).Key;
            }
            else
            {
                throw new System.ArgumentException("World model doesn`t contains this pair of exit/enter");
            }
        }
    }
}