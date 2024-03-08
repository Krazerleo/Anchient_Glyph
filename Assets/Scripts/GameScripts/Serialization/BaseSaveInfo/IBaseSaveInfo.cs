using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace AncientGlyph.GameScripts.Serialization.BaseSaveInfo
{
    public interface IBaseSaveInfo
    {
        public string SaveName { get; }
        
        public DateTime SaveTimeStamp { get; }
        
        public Image SavePreviewImage { get; }
        
        public string LevelName { get; }
        
        public Vector3Int PlayerPosition { get; }
    }
}