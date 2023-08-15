using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace AncientGlyph.EditorScripts.Editors
{
    public class AssetLibrary : EditorWindow
    {
        private const string _floorPath = "Level/Prefab/Floors/";
        private const string _wallPath = "Level/Prefab/Walls";
        private const string _envPath = "Level/Prefab/Environment";
        private const string _itemPath = "Level/Prefab/Items";

        private List<string> _floorAssetsPath = new List<string>();
        private List<string> _wallsAssetsPath = new List<string>();
        private List<string> _envsAssetsPath = new List<string>();
        private List<string> _itemsAssetsPath = new List<string>();

        private static TypeAsset selectedTypeAsset = TypeAsset.None;
        private static string selectedAsset = "";

        private ListView _assetListView;

        #region UnityMessages

        [MenuItem("Project Instruments / Asset Library")]
        private static void ShowWindow()
        {
            GetWindow<AssetLibrary>("Asset Library");
        }

        private void OnEnable()
        {
            FindAssetsPath(_floorAssetsPath, _floorPath);
            FindAssetsPath(_wallsAssetsPath, _wallPath);
            FindAssetsPath(_itemsAssetsPath, _itemPath);
            FindAssetsPath(_envsAssetsPath, _envPath);

            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Scripts/EditorScripts/Editors/AssetView/AssetLibrary.uxml");
            var tree = visualTree.CloneTree();

            _assetListView = rootVisualElement.Q("Asset-List-View-Container").Children().First() as ListView;

            foreach (ListView xmlListView in tree.Children().ToList().Cast<ListView>())
            {
                xmlListView.styleSheets.Add(AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Examples/Editor/styles.uss"));
                xmlListView.itemsSource = items;
                xmlListView.makeItem = makeItem;
                xmlListView.bindItem = bindItem;
                root.schedule.Execute(() => { xmlListView.Refresh(); });
                AddListView(xmlListView);
            }
        }

        #endregion

        private void FindAssetsPath(List<string> assetNames, string path)
        {
            var directoryInfo = new DirectoryInfo(string.Join('/', Application.dataPath, path));
            foreach (var file in directoryInfo.GetFiles())
            {
                var filename = file.Name;

                if (filename.Contains("meta") || !filename.Contains(".prefab"))
                {
                    continue;
                }

                assetNames.Add(filename);
            }
        }
    }
}