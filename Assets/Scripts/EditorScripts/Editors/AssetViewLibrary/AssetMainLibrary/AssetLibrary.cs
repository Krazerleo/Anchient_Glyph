using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using AncientGlyph.EditorScripts.Editors.AssetViewLibrary.AssetInfoVisualElement;
using AncientGlyph.GameScripts.ForEditor;

using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace AncientGlyph.EditorScripts.Editors.AssetViewLibrary.AssetMainLibrary
{
    public class AssetLibrary : EditorWindow
    {
        public static EventHandler<string> OnAssetNameChangeHandler;
        public static EventHandler<AssetType> OnAssetTypeChangeHandler;

        private const string CreaturePath = "Level/Prefab/Creatures";
        private const string ItemPath = "Level/Prefab/Items";
        private const string ObjectPath = "Level/Prefab/Objects";
        private const string TilesPath = "Level/Prefab/Tiles/";
        private const string WallPath = "Level/Prefab/Walls";
        private const string AssetListViewName = "Asset-List-View";
        private const string AssetSearchFieldName = "Asset-Search-Field";
        private const string AssetTypeToolbarName = "Asset-Type-Toolbar";
        private const string HighlightStylePath = "Assets/Scripts/EditorScripts/Editors/AssetViewLibrary/Styles/highlight.uss";
        private const string LayoutPath = "Assets/Scripts/EditorScripts/Editors/AssetViewLibrary/AssetMainLibrary/AssetLibrary.uxml";
        private static StyleSheet _highlightStyle;
        private static string _prompt = "";

        private static readonly List<(ToolbarButton button, AssetType type, string name)> TypeButtonList = new()
        {
            (null, AssetType.Tile, "Tiles-Type-Button"),
            (null, AssetType.Wall, "Walls-Type-Button"),
            (null, AssetType.Item, "Items-Type-Button"),
            (null, AssetType.Object, "Objects-Type-Button"),
            (null, AssetType.Entity, "Creatures-Type-Button"),
        };

        private ListView _assetListView;
        private ToolbarSearchField _assetSearchField;
        private Toolbar _assetTypeToolbar;
        private readonly List<AssetInfo> _foundAssets = new();

        private static readonly List<string> CreaturesAssetsPath = new();
        private static readonly List<string> ItemsAssetsPath = new();
        private static readonly List<string> ObjectsAssetsPath = new();
        private static readonly List<string> TilesAssetsPath = new();
        private static readonly List<string> WallsAssetsPath = new();

        public static string SelectedAssetName { get; private set; } = "";
        public static AssetType SelectedTypeAsset { get; private set; } = AssetType.Tile;

        [MenuItem("Project Instruments / Asset Library")]
        private static void ShowWindow()
        {
            GetWindow<AssetLibrary>("Asset Library");
        }

        private void OnEnable()
        {
            FindAssetsPath(TilesAssetsPath, TilesPath);
            FindAssetsPath(WallsAssetsPath, WallPath);
            FindAssetsPath(ItemsAssetsPath, ItemPath);
            FindAssetsPath(ObjectsAssetsPath, ObjectPath);
            FindAssetsPath(CreaturesAssetsPath, CreaturePath);

            _highlightStyle = AssetDatabase.LoadAssetAtPath<StyleSheet>(HighlightStylePath);
            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(LayoutPath);
            rootVisualElement.Add(visualTree.CloneTree());

            InitAssetListView();
            InitAssetSearchField();
            InitAssetTypeToolbar();
        }

        private void BindItemForAssetListView(VisualElement visualElement, int index)
        {
            var assetInfo = _foundAssets[index];
            var assetVisualElement = visualElement as AssetInfoVisualElement.AssetInfoVisualElement;
            assetVisualElement!.BindToAssetInfoVisualElement(assetInfo.AssetName, assetInfo.AssetPreviewImage);

            assetVisualElement.RegisterCallback<ClickEvent, AssetInfo>(OnItemClickEventCallback,
                new AssetInfo(assetInfo.AssetName, null, assetInfo.AssetPath));
            
            void OnItemClickEventCallback(ClickEvent clickEvent, AssetInfo info)
            {
                SelectedAssetName = info.AssetName;
                OnAssetNameChangeHandler?.Invoke(null, info.AssetPath);
            }
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
                AssetType.Tile => TilesPath,
                AssetType.Wall => WallPath,
                AssetType.Item => ItemPath,
                AssetType.Entity => CreaturePath,
                AssetType.Object => ObjectPath,
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
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
                _prompt = changeStringEvent.newValue;
            });
            
            _assetSearchField.RegisterCallback<KeyDownEvent>(OnAssetSearchPressed);
            
            void OnAssetSearchPressed(KeyDownEvent keyDownEvent)
            {
                if (keyDownEvent.keyCode == KeyCode.Return)
                {
                    RefreshSelectedAssets();
                    RefreshAssetListView();
                }
            }
        }

        private void InitAssetTypeToolbar()
        {
            _assetTypeToolbar = rootVisualElement.Q<Toolbar>(AssetTypeToolbarName);

            for (var index = 0; index < TypeButtonList.Count; index++)
            {
                var typeButton = _assetTypeToolbar.Q<ToolbarButton>(TypeButtonList[index].name);
                typeButton.userData = TypeButtonList[index].type;

                typeButton.RegisterCallback<ClickEvent>(click =>
                {
                    SelectedTypeAsset = (AssetType) typeButton.userData;
                    RepaintAssetToolbar(typeButton);

                    _assetSearchField.value = string.Empty;

                    RefreshSelectedAssets();
                    RefreshAssetListView();

                    OnAssetTypeChangeHandler?.Invoke(null, SelectedTypeAsset);
                    OnAssetNameChangeHandler?.Invoke(null, string.Empty);
                });

                TypeButtonList[index] = (typeButton, TypeButtonList[index].type, TypeButtonList[index].name);
            }
        }

        private VisualElement MakeItemForAssetListView()
        {
            return new AssetInfoVisualElement.AssetInfoVisualElement();
        }

        private void RefreshAssetListView()
        {
            _assetListView.RefreshItems();
        }

        private void RefreshSelectedAssets()
        {
            _foundAssets.Clear();
            IEnumerable<string> foundAssetsDirectories;

            switch (SelectedTypeAsset)
            {
                case AssetType.Tile:
                {
                    foundAssetsDirectories = TilesAssetsPath.Where(path => path.StartsWith(_prompt) || _prompt == "");
                    break;
                }
                case AssetType.Wall:
                {
                    foundAssetsDirectories = WallsAssetsPath.Where(path => path.StartsWith(_prompt) || _prompt == "");
                    break;
                }
                case AssetType.Item:
                {
                    foundAssetsDirectories = ItemsAssetsPath.Where(path => path.StartsWith(_prompt) || _prompt == "");
                    break;
                }
                case AssetType.Object:
                {
                    foundAssetsDirectories = ObjectsAssetsPath.Where(path => path.StartsWith(_prompt) || _prompt == "");
                    break;
                }
                case AssetType.Entity:
                {
                    foundAssetsDirectories = CreaturesAssetsPath.Where(path => path.StartsWith(_prompt) || _prompt == "");
                    break;
                }
                default:
                {
                    Debug.LogError($"{nameof(AssetLibrary)} == {nameof(RefreshSelectedAssets)}");
                    return;
                }
            }

            foreach (var directoryPrefabName in foundAssetsDirectories)
            {
                var prefabDirectoryFullName = string.Join('/', Application.dataPath, GetAssetDirectory(SelectedTypeAsset), directoryPrefabName);
                var directoryInfo = new DirectoryInfo(prefabDirectoryFullName);

                var prefabInfo = directoryInfo.GetFiles("*.prefab").FirstOrDefault();

                if (prefabInfo == null)
                {
                    Debug.LogError($"Cannot get asset file from {directoryInfo.FullName}");
                }

                var imageInfo = directoryInfo.GetFiles("*.png")
                    .Concat(directoryInfo.GetFiles("*.jpeg"))
                    .Concat(directoryInfo.GetFiles("*jpg"))
                    .FirstOrDefault();

                if (imageInfo == null)
                {
                    Debug.LogError($"Cannot get asset image from {directoryInfo.FullName}");
                    return;
                }

                if (prefabInfo == null)
                {
                    Debug.LogError($"Cannot get asset image from {directoryInfo.FullName}");
                    return;
                }

                var prefabPath = string.Join('/', "Assets", GetAssetDirectory(SelectedTypeAsset),
                                                    directoryPrefabName, prefabInfo.Name)
                                                    .Replace("//", "/");
                
                var imagePath = string.Join('/', "Assets", GetAssetDirectory(SelectedTypeAsset),
                                                   directoryPrefabName, imageInfo.Name)
                                                   .Replace("//", "/");
                var image = (Texture2D) AssetDatabase.LoadAssetAtPath(imagePath, typeof(Texture2D));

                _foundAssets.Add(new AssetInfo(directoryPrefabName, image, prefabPath));
            }
        }

        private void RepaintAssetToolbar(ToolbarButton selectedButton)
        {
            foreach (var button in TypeButtonList.Select(b => b.button))
            {
                button.styleSheets.Remove(_highlightStyle);
            }

            selectedButton.styleSheets.Add(_highlightStyle);
        }
    }
}