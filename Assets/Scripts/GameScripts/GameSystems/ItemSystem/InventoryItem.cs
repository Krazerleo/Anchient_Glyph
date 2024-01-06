using AncientGlyph.GameScripts.Constants;
using AncientGlyph.GameScripts.Geometry.Shapes;

using UnityEngine;

namespace AncientGlyph.GameScripts.GameSystems.ItemSystem
{
    [CreateAssetMenu(fileName = "Game Item Config", menuName = ProjectConstants.ScriptableObjectAssetMenuName)]
    public class GameItem : ScriptableObject
    {
        [SerializeField]
        public uint ItemId;


        [SerializeField]
        public CellSet CeilSet;
    }
}