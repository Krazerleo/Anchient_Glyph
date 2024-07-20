using AncientGlyph.GameScripts.Constants;
using AncientGlyph.GameScripts.Geometry.Shapes.PlanarShapes;
using UnityEngine;
using UnityEngine.Serialization;

namespace AncientGlyph.GameScripts.GameSystems.ItemSystem
{
    [CreateAssetMenu(
        fileName = "Game Item Config",
        menuName = ProjectConstants.ScriptableObjectAssetMenuName + "/" + "Game Item Config")]
    public class GameItem : ScriptableObject
    {
        public uint ItemId;
        public string Name;
        public Sprite MiniIcon;
        public Sprite InventoryIcon;
        [FormerlySerializedAs("CellSetRotation")]
        public CellSet CellSet;
    }
}