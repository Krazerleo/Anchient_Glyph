using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace AncientGlyph.EditorScripts.Utils
{
    public class PrefabHelper
    {
        private static Dictionary<string, GameObject> prefabCache = new Dictionary<string, GameObject>();
        public static GameObject LoadPrefabFromFile(string filename)
        {
            if (prefabCache.ContainsKey(filename))
            {
                return prefabCache[filename];
            }
            else
            {
                var loadedAsset = AssetDatabase.LoadAssetAtPath<GameObject>(filename);

                if (loadedAsset == null)
                {
                    return null;
                }

                prefabCache[filename] = loadedAsset;
                return loadedAsset;
            }
        }
    }
}