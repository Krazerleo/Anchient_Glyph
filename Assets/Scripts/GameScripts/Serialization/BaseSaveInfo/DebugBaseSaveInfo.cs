using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace AncientGlyph.GameScripts.Serialization.BaseSaveInfo
{
    public class DebugBaseSaveInfo : IBaseSaveInfo
    {
        public string SaveName => string.Empty;
        public DateTime SaveTimeStamp => new DateTime(1, 1, 1);
        public Image SavePreviewImage => null;
        public string LevelName => string.Empty;
        public Vector3Int PlayerPosition { get; }
        
        public DebugBaseSaveInfo(Vector3Int playerPosition)
        {
            PlayerPosition = playerPosition;
        }
    }
}