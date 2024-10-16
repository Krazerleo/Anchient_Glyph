using System.Collections.Generic;
using System.IO;
using System.Linq;
using AncientGlyph.EditorScripts.Editors.AssetLibrary.AgAssets;
using AncientGlyph.EditorScripts.Editors.AssetLibrary.AgBrushes;
using AncientGlyph.GameScripts.Common;
using AncientGlyph.GameScripts.ForEditor;
using UnityEditor;
using UnityEngine;

namespace AncientGlyph.EditorScripts.Editors.AssetLibrary
{
    public enum ExploreErrorCode
    {
        AssetDirectoryNotFound,
    }

    public class AssetsExplorer
    {
        private const string CreatureAssetsDirName = "Creatures";
        private const string ItemAssetsDirName = "Items";
        private const string TileAssetsDirName = "Tiles";
        private const string WallAssetsDirName = "Walls";
        private const string BrushAssetsDirName = "Brushes";

        private readonly Dictionary<AssetType, string> _assetDirNames = new()
        {
            { AssetType.Tile, TileAssetsDirName },
            { AssetType.Wall, WallAssetsDirName },
            { AssetType.Item, ItemAssetsDirName },
            { AssetType.Entity, CreatureAssetsDirName },
        };

        private readonly Dictionary<AssetType, List<AgAsset>> _assetPaths = new()
        {
            { AssetType.Tile, new List<AgAsset>() },
            { AssetType.Wall, new List<AgAsset>() },
            { AssetType.Item, new List<AgAsset>() },
            { AssetType.Entity, new List<AgAsset>() },
        };

        private List<AgBrush> _brushes;

        private const string PreviewAssetImagePlaceholderPath = "Debug/preview_image_placeholder.png";
        private static Texture2D _previewPlaceholderImage;

        private readonly string _assetRootDirectory;

        public AssetsExplorer(string assetRootDirectory)
        {
            _assetRootDirectory = assetRootDirectory;
            string placeholderImagePath = Path.Join(_assetRootDirectory, PreviewAssetImagePlaceholderPath);
            _previewPlaceholderImage = AssetDatabase.LoadAssetAtPath<Texture2D>(placeholderImagePath);
        }

        public List<AgAsset> GetAssets(AssetType assetType, string assetName)
        {
            return assetName == ""
                ? _assetPaths[assetType]
                : _assetPaths[assetType].Where(asset => asset.Prefab.name.StartsWith(assetName)).ToList();
        }

        public List<AgBrush> GetBrushes() => _brushes;

        public Result<Unit, ExploreErrorCode> Explore()
        {
            foreach ((AssetType assetType, string assetDirName) in _assetDirNames)
            {
                Result<List<AgAsset>, ExploreErrorCode> assetPathsResult =
                    ExploreAssetDirectory(Path.Join(_assetRootDirectory, assetDirName));
                if (assetPathsResult.IsFailed() &&
                    assetPathsResult.FailStatus == ExploreErrorCode.AssetDirectoryNotFound)
                {
                    Debug.LogError($"Not found asset directory of type: {assetType}");
                    return assetPathsResult.FailStatus;
                }

                _assetPaths[assetType] = assetPathsResult.Value;
            }

            Result<List<AgBrush>, ExploreErrorCode> resultBrushes =
                ExploreBrushDirectory(Path.Join(_assetRootDirectory, BrushAssetsDirName));
            if (resultBrushes.IsFailed())
            {
                Debug.LogError("Cannot deserialize brush configs");
                return resultBrushes.FailStatus;
            }

            _brushes = resultBrushes.Value;
            return new Unit();
        }

        private Result<List<AgAsset>, ExploreErrorCode> ExploreAssetDirectory(string assetDirectoryPath)
        {
            DirectoryInfo assetDirectory = new(assetDirectoryPath);
            if (assetDirectory.Exists == false)
            {
                return ExploreErrorCode.AssetDirectoryNotFound;
            }

            List<AgAsset> assets = new();
            
            string[] assetGuids = AssetDatabase.FindAssets($"t:{nameof(AgAsset)}", new[] { assetDirectoryPath });
            foreach (string assetGuid in assetGuids)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(assetGuid);
                AgAsset loadedAsset = AssetDatabase.LoadAssetAtPath<AgAsset>(assetPath);
                assets.Add(loadedAsset);
            }

            return assets;
        }

        private Result<List<AgBrush>, ExploreErrorCode> ExploreBrushDirectory(string brushDirectoryPath)
        {
            List<AgBrush> brushes = new();
            
            string[] brushGuids = AssetDatabase.FindAssets($"t:{nameof(AgBrush)}", new[] { brushDirectoryPath });
            foreach (string brushGuid in brushGuids)
            {
                string brushPath = AssetDatabase.GUIDToAssetPath(brushGuid);
                AgBrush brush = AssetDatabase.LoadAssetAtPath<AgBrush>(brushPath);
                
                brush.PreviewImage ??= _previewPlaceholderImage;
                
                brushes.Add(brush);
            }

            return brushes;
        }
    }
}