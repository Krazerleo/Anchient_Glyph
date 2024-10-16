using UnityEngine;
using UnityEngine.Serialization;

namespace AncientGlyph.EditorScripts.Editors.AssetLibrary.AgAssets
{
    [CreateAssetMenu(fileName = "AG Asset", menuName = "AG Assets/Asset")]
    public class AgAsset : ScriptableObject
    {
        [FormerlySerializedAs("Name")]
        public string AssetName;
        public GameObject Prefab;
        public Texture2D PreviewImage;
    }
}