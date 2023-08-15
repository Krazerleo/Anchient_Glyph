using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace AncientGlyph.EditorScripts.Editors
{
    public class LevelEditorDummy : EditorWindow
    {
        private const string _floorPath = "Level/Prefab/Floors/";
        private const string _wallPath = "Level/Prefab/Walls";
        private const string _envPath = "Level/Prefab/Environment";
        private const string _itemPath = "Level/Prefab/Items";

        private List<string> _floorAssetsPath = new List<string>();
        private List<string> _wallsAssetsPath = new List<string>();
        private List<string> _envsAssetsPath = new List<string>();
        private List<string> _itemsAssetsPath = new List<string>();

        private static string _currentPrompt = "";
        private static string _previousPrompt = " ";
        private static List<GUIStyle> _styles = new List<GUIStyle>();
        private bool _buttonsSettled = false;

        private GameObject _gridPlane;
        public static int FloorLevel = 0;

        private static TypeAsset selectedTypeAsset = TypeAsset.None;
        private static string selectedAsset = "";


        #region UnityMessages

        [MenuItem("Project Instruments / Level Editor")]
        private static void ShowWindow()
        {
            GetWindow<LevelEditor>("Asset Picker");
        }

        private void OnEnable()
        {
            CreateGrid();
            FindAssetsPath(_floorAssetsPath, _floorPath);
            FindAssetsPath(_wallsAssetsPath, _wallPath);
            FindAssetsPath(_itemsAssetsPath, _itemPath);
            FindAssetsPath(_envsAssetsPath, _envPath);
            SceneView.duringSceneGui += OnSceneGUI;
        }

        private void OnDisable()
        {
            _buttonsSettled = false;
            _styles.Clear();
            DestroyImmediate(_gridPlane);
            SceneView.duringSceneGui -= OnSceneGUI;
        }

        private void OnGUI()
        {
            if (_buttonsSettled == false)
            {
                for (int i = 0; i < 4; i++)
                {
                    var style = new GUIStyle(GUI.skin.button);
                    _styles.Add(style);
                }
                ResetButtons();
                _buttonsSettled = true;
            }

            var typeChanged = false;

            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("Floor", _styles[0]))
            {
                ResetButtons();
                ChangeButtonStyle(_styles[0]);
                typeChanged = true;
                selectedTypeAsset = TypeAsset.Floor;
            }
            if (GUILayout.Button("Wall", _styles[1]))
            {
                ResetButtons();
                ChangeButtonStyle(_styles[1]);
                typeChanged = true;
                selectedTypeAsset = TypeAsset.Wall;
            }
            if (GUILayout.Button("Environment", _styles[2]))
            {
                ResetButtons();
                ChangeButtonStyle(_styles[2]);
                typeChanged = true;
                selectedTypeAsset = TypeAsset.Environment;
            }
            if (GUILayout.Button("Item", _styles[3]))
            {
                ResetButtons();
                ChangeButtonStyle(_styles[3]);
                typeChanged = true;
                selectedTypeAsset = TypeAsset.Item;
            }

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Asset Name");
            _currentPrompt = EditorGUILayout.TextField(_currentPrompt);

            if (GUILayout.Button("Find"))
            {
                _previousPrompt = _currentPrompt;
            }

            EditorGUILayout.EndHorizontal();

            if (_currentPrompt == _previousPrompt && typeChanged == false)
                return;

            var assetsResult = GetAssetList();
            DrawAssetList(assetsResult);
        }

        private void OnSceneGUI(SceneView sceneView)
        {

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

        private void CreateGrid()
        {
            _gridPlane = GameObject.CreatePrimitive(PrimitiveType.Plane);
            _gridPlane.name = "Level Grid";
            _gridPlane.transform.localScale = new Vector3(72.1f, 72.1f, 72.1f);
            SceneVisibilityManager.instance.Hide(_gridPlane, true);
        }

        private void ResetButtons()
        {
            foreach (var style in _styles)
            {
                style.normal.textColor = Color.gray;
            }
        }

        private void ChangeButtonStyle(GUIStyle style)
        {
            style.normal.textColor = Color.white;
        }

        private void DrawAssetList(IEnumerable<string> assetsPath)
        {
            foreach (var asset in assetsPath)
            {
                if (GUILayout.Button(asset[..^7]))
                {
                    selectedAsset = asset;
                }
            }
        }

        private IEnumerable<string> GetAssetList()
        {
            List<string> path;

            switch (selectedTypeAsset)
            {
                case TypeAsset.Floor:
                    path = _floorAssetsPath;
                    break;

                case TypeAsset.Wall:
                    path = _wallsAssetsPath;
                    break;

                case TypeAsset.Environment:
                    path = _envsAssetsPath;
                    break;

                case TypeAsset.Item:
                    path = _itemsAssetsPath;
                    break;

                default:
                    path = new List<string>();
                    break;
            }

            _currentPrompt ??= string.Empty;

            return path.Where(x => x.Contains(_currentPrompt, System.StringComparison.OrdinalIgnoreCase));
        }
    }
}