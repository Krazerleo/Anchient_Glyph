using System.Collections.Generic;
using System.IO;
using System.Linq;

using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace AncientGlyph.EditorScripts.Editors
{
    public class AssetLibrary : EditorWindow
    {
        private const string UxmlPath = "Assets/Scripts/EditorScripts/Editors/AssetView/AssetLibrary/AssetLibrary.uxml";
        private const string HighlightStylePath = "Assets/Scripts/EditorScripts/Editors/AssetView/Styles/highlight.uss";
        private const string AssetListViewName = "Asset-List-View";
        private const string AssetSearchFieldName = "Asset-Search-Field";
        private const string AssetTypeToolbarName = "Asset-Type-Toolbar";

        private static StyleSheet _highlightStyle;
        private ListView _assetListView;
        private ToolbarSearchField _assetSearchField;
        private Toolbar _assetTypeToolbar;

        private static List<(ToolbarButton button, TypeAsset type, string name)> _typeButtonList = new List<(ToolbarButton, TypeAsset, string)>()
        {
            (null, TypeAsset.Tile, "Tiles-Type-Button"),
            (null, TypeAsset.Wall, "Walls-Type-Button"),
            (null, TypeAsset.Item, "Items-Type-Button"),
            (null, TypeAsset.Object, "Objects-Type-Button"),
            (null, TypeAsset.Creature, "Creatures-Type-Button"),
        };

        private static TypeAsset _selectedTypeAsset = TypeAsset.None;
        private static string _propmt = "";

        private List<AssetInfo> _foundAssets = new List<AssetInfo>();

        private const string _tilesPath = "Level/Prefab/Tiles/";
        private const string _wallPath = "Level/Prefab/Walls";
        private const string _objectPath = "Level/Prefab/Objects";
        private const string _itemPath = "Level/Prefab/Items";
        private const string _creaturePath = "Level/Prefab/Creatures";

        private List<string> _tilesAssetsPath = new List<string>();
        private List<string> _wallsAssetsPath = new List<string>();
        private List<string> _objectsAssetsPath = new List<string>();
        private List<string> _itemsAssetsPath = new List<string>();
        private List<string> _creaturesAssetsPath = new List<string>();

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

        private void InitAssetTypeToolbar()
        {
            _assetTypeToolbar = rootVisualElement.Q<Toolbar>(AssetTypeToolbarName);

            for (var index = 0; index < _typeButtonList.Count; index++)
            {
                var typebutton = _assetTypeToolbar.Q<ToolbarButton>(_typeButtonList[index].name);
                typebutton.userData = _typeButtonList[index].type;

                typebutton.RegisterCallback<ClickEvent>(click =>
                {
                    _selectedTypeAsset = (TypeAsset) typebutton.userData;
                    RepaintAssetToolbar(typebutton);

                    _assetSearchField.value = "";

                    RefreshSelectedAssets();
                    RefreshAssetListView();
                });

                _typeButtonList[index] = (typebutton, _typeButtonList[index].type, _typeButtonList[index].name);
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

        private void RefreshAssetListView()
        {
            _assetListView.RefreshItems();
        }

        private void RefreshSelectedAssets()
        {
            _foundAssets.Clear();

            switch (_selectedTypeAsset)
            {
                case TypeAsset.Tile:
                {
                    var findedAssetsDirectories = _tilesAssetsPath.Where(path => path.StartsWith(_propmt) || _propmt == "");
                    foreach (var directoryName in findedAssetsDirectories)
                    {
                        var directoryInfo = new DirectoryInfo(string.Join('/', Application.dataPath, directoryName));
                        var image = (Texture2D) AssetDatabase.LoadAssetAtPath("Assets/Level/Prefab/Tiles/floor_placeholder/Cat03.jpg", typeof(Texture2D));
                        _foundAssets.Add(new AssetInfo(directoryName, image));
                    }
                    break;
                }

                default:
                {
                    Debug.LogError($"{nameof(AssetLibrary)} == {nameof(RefreshSelectedAssets)}");
                    break;
                }
            }
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

        private void FindAssetsPath(List<string> assetOfTypeNames, string pathAssetsOfType)
        {
            var directoryInfo = new DirectoryInfo(string.Join('/', Application.dataPath, pathAssetsOfType));
            foreach (var directory in directoryInfo.GetDirectories())
            {
                assetOfTypeNames.Add(directory.Name);
            }
        }
    }
}