using AncientGlyph.GameScripts.Constants;
using AncientGlyph.GameScripts.Geometry.Shapes;

using UnityEngine;

namespace AncientGlyph.GameScripts.GameSystems.ItemSystem
{
    [CreateAssetMenu(
        fileName = "Game Item Config", 
        menuName = ProjectConstants.ScriptableObjectAssetMenuName + "/" + "Game Item Config")]
    public class GameItem : ScriptableObject
    {
        [SerializeField]
        public uint ItemId;

        [SerializeField]
        public string Name;

        [SerializeField]
        public Sprite Icon;

        [SerializeField]
        public Sprite GhostIcon;

        [SerializeField]
        public CellSet CellSet = new();
    }
}