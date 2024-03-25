using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace AncientGlyph.GameScripts.Serialization.SaveInfo
{
    public readonly struct BaseSaveInfo
    {
        public readonly string SaveName;
        public readonly DateTime SaveTimeStamp;
        public readonly Image SavePreviewImage;
        public readonly string LevelName;
        public readonly Vector3Int PlayerPosition;
        
        public BaseSaveInfo(Vector3Int playerPosition,
            string saveName,
            DateTime saveTimeStamp,
            Image savePreviewImage,
            string levelName)
        {
            PlayerPosition = playerPosition;
            SaveName = saveName;
            SaveTimeStamp = saveTimeStamp;
            SavePreviewImage = savePreviewImage;
            LevelName = levelName;
        }
    }
}