using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AncientGlyph.GameScripts.Helpers;

using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace AncientGlyph.EditorScripts.Editors
{
    public class AssetLibrary : EditorWindow
    {
        #region Public Fields

        public static EventHandler<string> OnAssetNameChangeHandler;
        public static EventHandler<AssetType> OnAssetTypeChangeHandler;

        #endregion Public Fields

        #region Private Fields

        private const string _creaturePath = "Level/Prefab/Creatures";
        private const string _itemPath = "Level/Prefab/Items";
        private const string _objectPath = "Level/Prefab/Objects";
        private const string _tilesPath = "Level/Prefab/Tiles/";
        private const string _wallPath = "Level/Prefab/Walls";
        private const string AssetListViewName = "Asset-List-View";
        private const string AssetSearchFieldName = "Asset-Search-Field";
        private const string AssetTypeToolbarName = "Asset-Type-Toolbar";
        private const string HighlightStylePath = "Assets/Scripts/EditorScripts/Editors/AssetView/Styles/highlight.uss";
        private const string UxmlPath = "Assets/Scripts/EditorScripts/Editors/AssetView/AssetLibrary/AssetLibrary.uxml";
        private static StyleSheet _highlightStyle;
        private static string _propmt = "";

        private static List<(ToolbarButton button, AssetType type, string name)> _typeButtonList = new List<(ToolbarButton, AssetType, string)>()
        {
            (null, AssetType.Tile, "Tiles-Type-Button"),
            (null, AssetType.Wall, "Walls-Type-Button"),
            (null, AssetType.Item, "Items-Type-Button"),
            (null, AssetType.Object, "Objects-Type-Button"),
            (null, AssetType.Creature, "Creatures-Type-Button"),
        };

        private ListView _assetListView;
        private ToolbarSearchField _assetSearchField;
        private Toolbar _assetTypeToolbar;
        private List<AssetInfo> _foundAssets = new List<AssetInfo>();

        #region

        private List<string> _creaturesAssetsPath = new List<string>();

        private List<string> _itemsAssetsPath = new List<string>();

        private List<string> _objectsAssetsPath = new List<string>();

        private List<string> _tilesAssetsPath = new List<string>();

        private List<string> _wallsAssetsPath = new List<string>();

        #endregion

        #endregion Private Fields

        #region Public Properties

        public static string SelectedAssetName { get; private set; } = "";

        public static AssetType SelectedTypeAsset { get; private set; } = AssetType.Tile;

        #endregion Public Properties

        #region UnityMessages

        [MenuItem("Project Instruments / Asset Library")]
        private static void ShowWindow()
        {
            GetWindow<AssetLibrary>("Asset Library");
        }

        private void OnEnable()
        {
            FindAssetsPath(_tilesAssetsPath, _tilesPath);
            FindAssetsPath(_wallsAssetsPath, _wallPath);
            FindAssetsPath(_itemsAssetsPath, _itemPath);
            FindAssetsPath(_objectsAssetsPath, _objectPath);
            FindAssetsPath(_creaturesAssetsPath, _creaturePath);

            _highlightStyle = AssetDatabase.LoadAssetAtPath<StyleSheet>(HighlightStylePath);
            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(UxmlPath);
            rootVisualElement.Add(visualTree.CloneTree());

            InitAssetListView();
            InitAssetSearchField();
            InitAssetTypeToolbar();
        }

        #endregion UnityMessages

        #region Private Methods

        private void BindItemForAssetListView(VisualElement visualElement, int index)
        {
            var assetInfo = _foundAssets[index];
            var assetVisualElement = visualElement as AssetInfoVisualElement;
            assetVisualElement.BindToAssetInfoVisualElement(assetInfo.AssetName, assetInfo.AssetPreviewImage);

            assetVisualElement.RegisterCallback<ClickEvent>(click =>
            {
                SelectedAssetName = assetInfo.AssetName;
                OnAssetNameChangeHandler?.Invoke(null, assetInfo.AssetPath);
            });
        }

        private void FindAssetsPath(List<string> assetOfTypeNames, string pathAssetsOfType)
        {
            var directoryInfo = new DirectoryInfo(string.Join('/', Application.dataPath, pathAssetsOfType));
            foreach (var directory in directoryInfo.GetDirectories())
            {
                assetOfTypeNames.Add(directory.Name);
            }
        }

        private string GetAssetDirectory(AssetType type)
        {
            return type switch
            {
                AssetType.Tile => _tilesPath,
                AssetType.Wall => _wallPath,
                AssetType.Item => _itemPath,
                AssetType.Creature => _creaturePath,
                AssetType.Object => _objectPath,
                AssetType.None or _ => null,
            };
        }

        private void InitAssetListView()
        {
            _assetListView = rootVisualElement.Q<ListView>(AssetListViewName);
            _assetSearchField = rootVisualElement.Q<ToolbarSearchField>(AssetSearchFieldName);

            _assetListView.itemsSource = _foundAssets;
            _assetListView.makeItem = MakeItemForAssetListView;
            _assetListView.bindItem = BindItemForAssetListView;
            _assetListView.selectionType = SelectionType.Single;
        }

        private void InitAssetSearchField()
        {
            _assetSearchField = rootVisualElement.Q<ToolbarSearchField>(AssetSearchFieldName);

            _assetSearchField.RegisterValueChangedCallback(changeStringEvent =>
            {
                _propmt = changeStringEvent.newValue;
            });

            _assetSearchField.RegisterCallback<KeyDownEvent>(keydownEvent =>
            {
                if (keydownEvent.keyCode == KeyCode.Return)
                {
                    RefreshSelectedAssets();
                    RefreshAssetListView();
                }
            });
        }

        private void InitAssetTypeToolbar()
        {
            _assetTypeToolbar = rootVisualElement.Q<Toolbar>(AssetTypeToolbarName);

            for (var index = 0; index < _typeButtonList.Count; index++)
            {
                var typebutton = _assetTypeToolbar.Q<ToolbarButton>(_typeButtonList[index].name);
                typebutton.userData = _typeButtonList[index].type;

                typebutton.RegisterCallback<ClickEvent>(click =>
                {
                    SelectedTypeAsset = (AssetType) typebutton.userData;
                    RepaintAssetToolbar(typebutton);

                    _assetSearchField.value = string.Empty;

                    RefreshSelectedAssets();
                    RefreshAssetListView();

                    OnAssetTypeChangeHandler?.Invoke(null, SelectedTypeAsset);
                    OnAssetNameChangeHandler?.Invoke(null, string.Empty);
                });

                _typeButtonList[index] = (typebutton, _typeButtonList[index].type, _typeButtonList[index].name);
            }
        }

        private VisualElement MakeItemForAssetListView()
        {
            return new AssetInfoVisualElement();
        }

        private void RefreshAssetListView()
        {
            _assetListView.RefreshItems();
        }

        private void RefreshSelectedAssets()
        {
            _foundAssets.Clear();
            IEnumerable<string> findedAssetsDirectories;

            switch (SelectedTypeAsset)
            {
                case AssetType.Tile:
                {
                    findedAssetsDirectories = _tilesAssetsPath.Where(path => path.StartsWith(_propmt) || _propmt == "");
                    break;
                }
                case AssetType.Wall:
                {
                    findedAssetsDirectories = _wallsAssetsPath.Where(path => path.StartsWith(_propmt) || _propmt == "");
                    break;
                }
                case AssetType.Item:
                {
                    findedAssetsDirectories = _itemsAssetsPath.Where(path => path.StartsWith(_propmt) || _propmt == "");
                    break;
                }
                case AssetType.Object:
                {
                    findedAssetsDirectories = _objectsAssetsPath.Where(path => path.StartsWith(_propmt) || _propmt == "");
                    break;
                }
                case AssetType.Creature:
                {
                    findedAssetsDirectories = _creaturesAssetsPath.Where(path => path.StartsWith(_propmt) || _propmt == "");
                    break;
                }
                default:
                {
                    Debug.LogError($"{nameof(AssetLibrary)} == {nameof(RefreshSelectedAssets)}");
                    return;
                }
            }

            foreach (var directoryPrefabName in findedAssetsDirectories)
            {
                var prefabDirectoryFullName = string.Join('/', Application.dataPath, GetAssetDirectory(SelectedTypeAsset), directoryPrefabName);
                var directoryInfo = new DirectoryInfo(prefabDirectoryFullName);

                var prefabInfo = directoryInfo.GetFiles("*.prefab").FirstOrDefault();

                if (prefabInfo == null)
                {
                    LogTools.LogWarning(this);
                }

                var imageInfo = directoryInfo.GetFiles("*.png").Concat(directoryInfo.GetFiles("*.jpeg")).Concat(directoryInfo.GetFiles("*jpg")).FirstOrDefault();

                if (imageInfo == null)
                {
                    LogTools.LogWarning(this);
                }

                var prefabPath = string.Join('/', "Assets", GetAssetDirectory(SelectedTypeAsset), directoryPrefabName, prefabInfo.Name).Replace("//", "/");
                var imagePath = string.Join('/', "Assets", GetAssetDirectory(SelectedTypeAsset), directoryPrefabName, imageInfo.Name).Replace("//", "/");
                var image = (Texture2D) AssetDatabase.LoadAssetAtPath(imagePath, typeof(Texture2D));

                _foundAssets.Add(new AssetInfo(directoryPrefabName, image, prefabPath));
            }
        }

        private void RepaintAssetToolbar(ToolbarButton selectedButton)
        {
            foreach (var button in _typeButtonList.Select(b => b.button))
            {
                button.styleSheets.Remove(_highlightStyle);
            }

            selectedButton.styleSheets.Add(_highlightStyle);
        }

        #endregion Private Methods
    }
}