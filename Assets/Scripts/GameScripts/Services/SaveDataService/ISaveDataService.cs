using AncientGlyph.GameScripts.Serialization.SaveInfo;
using UnityEngine;

namespace AncientGlyph.GameScripts.Services.SaveDataService
{
    public interface ISaveDataService
    {
        public BaseSaveInfo BaseInfo { get; }
        public PlayerSaveInfo PlayerInfo { get; }
    }
}