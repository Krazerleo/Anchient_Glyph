using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AncientGlyph.EditorScripts.Editors.AssetLibrary.AgBrushes
{
    [CreateAssetMenu(fileName = "AG Noise Brush", menuName = "AG Assets")]
    public class NoiseBrush : AgBrush
    {
        [SerializeField]
        private List<GameObject> _assets;

        public NoiseBrush(IEnumerable<GameObject> assets)
        {
            _assets = assets.ToList();
        }

        public override GameObject PlaceAsset(Vector3Int position)
            => _assets[position.GetHashCode() % _assets.Count];
    }
}