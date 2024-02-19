using AncientGlyph.EditorScripts.Constants;
using AncientGlyph.EditorScripts.Editors.AssetViewLibrary.AssetMainLibrary;
using AncientGlyph.EditorScripts.Editors.LevelModeling.LevelEditingHandlers;
using AncientGlyph.EditorScripts.Helpers;
using AncientGlyph.GameScripts.Constants;
using AncientGlyph.GameScripts.ForEditor;

using UnityEditor;
using UnityEditor.EditorTools;
using UnityEditor.ShortcutManagement;
using UnityEngine;

namespace AncientGlyph.EditorScripts.Editors.LevelModeling
{
    [EditorTool("Place Selected Asset")]
    public class AssetPlacer : EditorTool
    {
        private const int _availableCheckTicks = 1000;
        private const string _gridName = "Level Grid";
        private const float _initialGridPlaneHeight = 1.5f;
        private int _availableCurrentTicks = 0;
        private GameObject _gridPlane;
        private bool _isAvailable = false;
        private bool _mouseButtonWasPressed = false;
        private IAssetPlacerHandler _placerHandler;
        private GameObject _selectedGameObject;

        public override bool IsAvailable()
        {
            return _isAvailable;
        }

        public override GUIContent toolbarIcon => EditorGUIUtility.IconContent("Grid.BoxTool@2x");

        public override void OnActivated()
        {
            AssetLibrary.OnAssetNameChangeHandler += OnAssetNameChanged;
            AssetLibrary.OnAssetTypeChangeHandler += OnAssetTypeChanged;

            _placerHandler = PlacerHandlerCreator.CreatePlacerHandler(AssetLibrary.SelectedTypeAsset);
            _selectedGameObject = PrefabHelper.LoadPrefabFromFile(AssetLibrary.SelectedAssetName);
        }

        public override void OnToolGUI(EditorWindow window)
        {
            if (_isAvailable == true && window.hasFocus)
            {
                HandleInput();
            }
        }

        public override void OnWillBeDeactivated()
        {
            AssetLibrary.OnAssetNameChangeHandler -= OnAssetNameChanged;
            AssetLibrary.OnAssetTypeChangeHandler -= OnAssetTypeChanged;
        }

        [Shortcut("Activate Place Tool", typeof(SceneView), KeyCode.C, ShortcutModifiers.Shift)]
        private static void PlatformToolShortcut()
        {
            ToolManager.SetActiveTool<AssetPlacer>();
        }

        private void OnDisable()
        {
            EditorApplication.update -= UpdateAvailable;

            DestroyImmediate(_gridPlane);
        }

        private void OnEnable()
        {
            EditorApplication.update += UpdateAvailable;

            if (GameObject.FindGameObjectsWithTag("GridPlaneTag") == null)
            {
                InstantiateGridPlane();
            }
        }

        private void InstantiateGridPlane()
        {
            _gridPlane = GameObject.CreatePrimitive(PrimitiveType.Plane);
            _gridPlane.name = _gridName;
            _gridPlane.transform.localScale = new Vector3(EditorConstants.GridSizeX, 1, EditorConstants.GridSizeZ);
            _gridPlane.transform.Translate(
                Vector3.up * (EditorConstants.DistanceTolerance + _initialGridPlaneHeight)
                + Vector3.right * GameConstants.LevelCellsSizeX / 2
                + Vector3.forward * GameConstants.LevelCellsSizeZ / 2);

            SceneVisibilityManager.instance.Hide(_gridPlane, true);
        }

        private void HandleInput()
        {
            var current = Event.current;
            var controlID = GUIUtility.GetControlID(FocusType.Keyboard);
            
            switch (current.type)
            {
                case EventType.MouseDown:
                {
                    if (current.button == 0 && !_mouseButtonWasPressed)
                    {
                        _mouseButtonWasPressed = true;

                        Ray worldRay = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);

                        if (Physics.Raycast(worldRay, out var hitInfo))
                        {
                            _placerHandler.OnMouseButtonPressedHandler(hitInfo.point);
                        }

                        Event.current.Use();
                    }

                    break;
                }

                case EventType.MouseUp:
                {
                    if (_mouseButtonWasPressed)
                    {
                        Ray worldRay = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);

                        if (Physics.Raycast(worldRay, out var hitInfo))
                        {
                            _placerHandler.OnMouseButtonReleasedHandler(hitInfo.point);
                        }

                        _mouseButtonWasPressed = false;

                        Event.current.Use();
                    }

                    break;
                }

                case EventType.MouseDrag:
                {
                    if (_mouseButtonWasPressed)
                    {
                        Ray worldRay = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);

                        if (Physics.Raycast(worldRay, out var hitInfo))
                        {
                            _placerHandler.OnMouseMoveHandler(hitInfo.point);
                        }

                        Event.current.Use();
                    }

                    break;
                }

                case EventType.Layout:
                {
                    HandleUtility.AddDefaultControl(controlID);
                    break;
                }
            }

            if (Event.current.type == EventType.KeyDown && Event.current.shift && Event.current.keyCode == KeyCode.UpArrow)
            {
                _gridPlane.transform.Translate(Vector3.up * EditorConstants.LevelHeightDifference, Space.World);
                Debug.Log($"Grid Plane is on {_gridPlane.transform.position.y} height");
            }

            if (Event.current.type == EventType.KeyDown && Event.current.shift && Event.current.keyCode == KeyCode.DownArrow)
            {
                _gridPlane.transform.Translate(Vector3.down * EditorConstants.LevelHeightDifference, Space.World);
                Debug.Log($"Grid Plane is on {_gridPlane.transform.position.y} height");
            }
        }

        private void OnAssetNameChanged(object obj, string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return;
            }

            _selectedGameObject = AssetDatabase.LoadAssetAtPath<GameObject>(name);
            _placerHandler.SetPrefabObject(_selectedGameObject);
        }

        private void OnAssetTypeChanged(object obj, AssetType type)
        {
            _placerHandler = PlacerHandlerCreator.CreatePlacerHandler(type);
        }

        private void UpdateAvailable()
        {
            _availableCurrentTicks++;

            if (_availableCurrentTicks > _availableCheckTicks)
            {
                _availableCurrentTicks = 0;

                _isAvailable = (EditorWindow.HasOpenInstances<AssetLibrary>() && AssetLibrary.SelectedAssetName != "");
            }
        }
    }
}