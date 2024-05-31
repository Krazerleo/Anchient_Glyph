using System.Collections.Generic;
using UnityEngine;

namespace AncientGlyph.GameScripts.GameSystems.ItemSystem
{
    // Just a container for uninitialized items on scene.
    // Somewhere items will be enumerated and created on scene 
    // using data from that container
    public class ItemsSerializationContainer
    {
        public ItemsSerializationContainer(IEnumerable<ItemSerializationInfo> items)
        {
            ItemData = items;
        }
        
        public IEnumerable<ItemSerializationInfo> ItemData { get; }
    }

    public readonly struct ItemSerializationInfo
    {
        public readonly string Uid;
        public readonly Vector3 Position;
        
        public ItemSerializationInfo(string uid, Vector3 position)
        {
            Uid = uid;
            Position = position;
        }
    }
}