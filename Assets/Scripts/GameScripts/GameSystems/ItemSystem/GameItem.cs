using AncientGlyph.GameScripts.Constants;
using AncientGlyph.GameScripts.Geometry.Shapes.PlanarShapes;
using UnityEngine;

namespace AncientGlyph.GameScripts.GameSystems.ItemSystem
{
    [CreateAssetMenu(
        fileName = "Game Item Config",
        menuName = ProjectConstants.ScriptableObjectAssetMenuName + "/" + "Game Item Config")]
    public class GameItem : ScriptableObject
    {
        public uint ItemId;
        public string Name;
        public Sprite Icon;
        public Sprite GhostIcon;
        public CellSet CellSet;
    }
}