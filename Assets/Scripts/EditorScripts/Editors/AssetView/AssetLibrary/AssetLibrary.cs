using System.Collections.Generic;
using System.IO;

using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace AncientGlyph.EditorScripts.Editors
{
    public class AssetLibrary : EditorWindow
    {
        private const string UxmlPath = "Assets/Scripts/EditorScripts/Editors/AssetView/AssetLibrary/AssetLibrary.uxml";
        private const string AssetListViewName = "Asset-List-View";
        private const string AssetSearchFieldName = "Asset-Search-Field";

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

        private List<AssetInfo> _foundAssets = new List<AssetInfo>();
        private ListView _assetListView;
        private ToolbarSearchField _assetSearchField;

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

            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(UxmlPath);
            rootVisualElement.Add(visualTree.CloneTree());

            InitAssetListView();
        }

        #endregion UnityMessages

        private void InitAssetListView()
        {
            _assetListView = rootVisualElement.Q<ListView>(AssetListViewName);
            _assetSearchField = rootVisualElement.Q<ToolbarSearchField>(AssetSearchFieldName);

            _assetListView.makeItem = MakeItemForAssetListView;
            _assetListView.bindItem = BindItemForAssetListView;
            _assetListView.selectionType = SelectionType.Single;
        }

        private VisualElement MakeItemForAssetListView()
        {
            return new AssetInfoVisualElement();
        }

        private void BindItemForAssetListView(VisualElement visualElement, int index)
        {
            var assetInfo = _foundAssets[index];
            var assetVisualElement = visualElement as AssetInfoVisualElement;
            assetVisualElement.BindToAssetInfoVisualElement(assetInfo.AssetName, assetInfo.AssetPreviewImage);
        }

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