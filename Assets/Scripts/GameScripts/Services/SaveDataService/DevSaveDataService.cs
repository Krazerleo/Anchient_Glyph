using AncientGlyph.GameScripts.Serialization.BaseSaveInfo;
using UnityEngine;

namespace AncientGlyph.GameScripts.Services.SaveDataService
{
    public class DevSaveDataService : ISaveDataService
    {
        public IBaseSaveInfo BaseSaveInfo { get; }

        public DevSaveDataService(Vector3Int playerPosition)
        {
            BaseSaveInfo = new DebugBaseSaveInfo(playerPosition);
        }
        
        public void LoadSaveFile(string saveFilePath)
        {
            throw new System.NotImplementedException();
        }
    }
}