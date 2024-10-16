using UnityEngine;

namespace AncientGlyph.EditorScripts.Editors.AssetLibrary.AgAssets
{
    [CreateAssetMenu(fileName = "AG Asset", menuName = "AG Assets")]
    public class AgAsset : ScriptableObject
    {
        public GameObject Prefab;
        public Texture2D AssetPreviewImage;
    }
}