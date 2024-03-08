using AncientGlyph.GameScripts.Serialization.BaseSaveInfo;
using UnityEngine;

namespace AncientGlyph.GameScripts.Services.SaveDataService
{
    public interface ISaveDataService
    {
        public void LoadSaveFile(string saveFilePath);
        
        public IBaseSaveInfo BaseSaveInfo { get; }
    }
}