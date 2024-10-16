using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AncientGlyph.EditorScripts.Editors.AssetLibrary.AgAsset;
using AncientGlyph.EditorScripts.Editors.LevelModeling.Brushes;
using AncientGlyph.GameScripts.Common;
using AncientGlyph.GameScripts.ForEditor;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace AncientGlyph.EditorScripts.Editors.AssetLibrary
{
    public class AssetLibrary : EditorWindow
    {
        public static EventHandler<AssetViewModel> OnAssetChangeHandler;
        public static EventHandler<AssetType> OnAssetTypeChangeHandler;
        private static StyleSheet _highlightStyle;

        private const string AssetListViewName = "Asset-List-View";
        private const string BrushListViewName = "Brush-List-View";
        private const string AssetSearchFieldName = "Asset-Search-Field";
        private const string AssetTypeToolbarName = "Asset-Type-Toolbar";

        private const string HighlightStylePath =
            "Assets/Scripts/EditorScripts/Editors/AssetViewLibrary/Styles/highlight.uss";

        private const string LayoutPath =
            "Assets/Scripts/EditorScripts/Editors/AssetViewLibrary/AssetMainLibrary/AssetLibrary.uxml";

        private static readonly List<(ToolbarButton button, AssetType type, string name)> TypeButtonList = new()
        {
            (null, AssetType.Tile, "Tiles-Type-Button"),
            (null, AssetType.Wall, "Walls-Type-Button"),
            (null, AssetType.Item, "Items-Type-Button"),
            (null, AssetType.Entity, "Creatures-Type-Button"),
        };

        private static AssetsExplorer _assetsExplorer;

        private ListView _assetListView;
        private readonly List<AssetViewModel> _foundAssets = new();

        private ListView _brushListView;
        private readonly List<BaseBrush> _foundBrushes = new();

        private Toolbar _assetTypeToolbar;
        private AssetType SelectedTypeAsset { get; set; } = AssetType.Tile;

        private static string _prompt = "";
        private ToolbarSearchField _assetSearchField;

        [MenuItem("Project Instruments / Asset Library")]
        private static void ShowWindow()
        {
            GetWindow<AssetLibrary>("Asset Library");
        }

        private void Awake()
        {
            string pathToPrefabFolder = Path.Combine(Application.dataPath, "Level", "Prefab");
            _assetsExplorer = new AssetsExplorer(pathToPrefabFolder);
            Result<Unit, ExploreErrorCode> exploreResult = _assetsExplorer.Explore();
            if (exploreResult.IsFailed())
            {
                Debug.LogError("Failed to discover assets. Asset Library window closes." +
                               $" Error status: {exploreResult.FailStatus}");
                Close();
            }

            _highlightStyle = AssetDatabase.LoadAssetAtPath<StyleSheet>(HighlightStylePath);
            VisualTreeAsset visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(LayoutPath);
            rootVisualElement.Add(visualTree.CloneTree());

            InitAssetListView();
            InitBrushListView();
            InitAssetSearchField();
            InitAssetTypeToolbar();
        }

        private void InitAssetListView()
        {
            _assetListView = rootVisualElement.Q<ListView>(AssetListViewName);
            _assetListView.itemsSource = _foundAssets;
            _assetListView.makeItem = () => new AssetVisualElement();
            _assetListView.bindItem = (visualElement, index) =>
            {
                AssetViewModel assetViewModel = _foundAssets[index];
                AssetVisualElement assetVisualElement = (AssetVisualElement)visualElement;
                assetVisualElement.BindToAssetInfoVisualElement(assetViewModel);
                assetVisualElement
                    .RegisterCallback((ClickEvent _, AssetViewModel info)
                                          => OnAssetChangeHandler?.Invoke(null, info), assetViewModel);
            };
            _assetListView.selectionType = SelectionType.Single;
        }

        private void InitAssetSearchField()
        {
            _assetSearchField = rootVisualElement.Q<ToolbarSearchField>(AssetSearchFieldName);
            _assetSearchField.RegisterValueChangedCallback(updatedSearchStringEvent =>
                                                               _prompt = updatedSearchStringEvent.newValue);
            _assetSearchField.RegisterCallback<KeyDownEvent>(keyDownEvent =>
            {
                if (keyDownEvent.keyCode == KeyCode.Return)
                {
                    RefreshSelectedAssets();
                    _assetListView.RefreshItems();
                }
            });
        }

        private void InitAssetTypeToolbar()
        {
            _assetTypeToolbar = rootVisualElement.Q<Toolbar>(AssetTypeToolbarName);

            for (int index = 0; index < TypeButtonList.Count; index++)
            {
                ToolbarButton typeButton = _assetTypeToolbar.Q<ToolbarButton>(TypeButtonList[index].name);
                typeButton.userData = TypeButtonList[index].type;
                typeButton.RegisterCallback((ClickEvent _, ToolbarButton selectedButton) =>
                {
                    SelectedTypeAsset = (AssetType)selectedButton.userData;
                    RepaintAssetToolbar(selectedButton);

                    _assetSearchField.value = string.Empty;
                    RefreshSelectedAssets();
                    _assetListView.RefreshItems();

                    OnAssetTypeChangeHandler?.Invoke(null, SelectedTypeAsset);
                }, typeButton);
                TypeButtonList[index] = (typeButton, TypeButtonList[index].type, TypeButtonList[index].name);
            }
        }

        private void InitBrushListView()
        {
            _brushListView = rootVisualElement.Q<ListView>(BrushListViewName);
            _brushListView.itemsSource = _foundBrushes;
            _brushListView.makeItem = () => new AssetVisualElement();
            _brushListView.bindItem = (visualElement, index) =>
            {
                AssetViewModel assetViewModel = _foundAssets[index];
                AssetVisualElement assetVisualElement = (AssetVisualElement)visualElement;
                assetVisualElement.BindToAssetInfoVisualElement(assetViewModel);
                assetVisualElement.RegisterCallback((ClickEvent _, AssetViewModel info) =>
                                                        OnAssetChangeHandler?.Invoke(null, info),
                                                    assetViewModel);
            };
            _brushListView.selectionType = SelectionType.Single;
        }

        private void BindItemForAssetListView(VisualElement visualElement, int index)
        {
            AssetViewModel assetViewModel = _foundAssets[index];
            AssetVisualElement assetVisualElement = (AssetVisualElement)visualElement;
            assetVisualElement.BindToAssetInfoVisualElement(assetViewModel);
            assetVisualElement.RegisterCallback((ClickEvent _, AssetViewModel info) =>
                                                    OnAssetChangeHandler?.Invoke(null, info),
                                                assetViewModel);
        }

        private void RefreshSelectedAssets()
        {
            Result<List<AssetViewModel>, ExploreErrorCode> exploredAssets =
                _assetsExplorer.GetAssets(SelectedTypeAsset, _prompt);
            if (exploredAssets.IsFailed() && exploredAssets.FailStatus == ExploreErrorCode.AssetDirectoryNotFound)
            {
                Debug.LogError("Error: not found asset directory");
                return;
            }

            _foundAssets.Clear();
            _foundAssets.AddRange(exploredAssets.Value);
        }

        private void RepaintAssetToolbar(ToolbarButton selectedButton)
        {
            foreach (ToolbarButton button in TypeButtonList.Select(b => b.button))
            {
                button.styleSheets.Remove(_highlightStyle);
            }

            selectedButton.styleSheets.Add(_highlightStyle);
        }
    }
}