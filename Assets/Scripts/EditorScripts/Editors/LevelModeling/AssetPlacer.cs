using AncientGlyph.EditorScripts.Constants;
using AncientGlyph.EditorScripts.Editors.AssetLibrary.AgAsset;
using AncientGlyph.EditorScripts.Editors.LevelModeling.EditingHandlers;
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
        private const int AvailableCheckTicks = 1000;
        private const string GridName = "Level Grid";
        private const float InitialGridPlaneHeight = 1.5f;
        private int _availableCurrentTicks;
        private GameObject _gridPlane;
        private bool _isAvailable;
        private bool _mouseButtonWasPressed;
        private IAssetPlacerHandler _placerHandler;
        private GameObject _selectedGameObject;

        public override bool IsAvailable()
        {
            return _isAvailable;
        }

        public override GUIContent toolbarIcon => EditorGUIUtility.IconContent("Grid.BoxTool@2x");

        public override void OnActivated()
        {
            AssetLibrary.AssetLibrary.OnAssetChangeHandler += OnAssetNameChanged;
            AssetLibrary.AssetLibrary.OnAssetTypeChangeHandler += OnAssetTypeChanged;
        }

        public override void OnToolGUI(EditorWindow window)
        {
            if (_isAvailable && window.hasFocus)
            {
                HandleInput();
            }
        }

        public override void OnWillBeDeactivated()
        {
            AssetLibrary.AssetLibrary.OnAssetChangeHandler -= OnAssetNameChanged;
            AssetLibrary.AssetLibrary.OnAssetTypeChangeHandler -= OnAssetTypeChanged;
        }

        [Shortcut("Activate Place Tool", typeof(SceneView), KeyCode.C, ShortcutModifiers.Shift)]
        private static void PlatformToolShortcut()
        {
            ToolManager.SetActiveTool<AssetPlacer>();
        }

        private void OnDisable()
        {
            EditorApplication.update -= UpdateAvailable;
        }

        private void OnEnable()
        {
            EditorApplication.update += UpdateAvailable;

            if (GameObject.FindGameObjectsWithTag("GridPlaneTag") == null)
            {
                InstantiateGridPlane();
            }
            else
            {
                _gridPlane = GameObject.FindGameObjectWithTag("GridPlaneTag");
            }
        }

        private void InstantiateGridPlane()
        {
            _gridPlane = GameObject.CreatePrimitive(PrimitiveType.Plane);
            _gridPlane.name = GridName;
            _gridPlane.transform.localScale =
                new Vector3(EditorConstants.GridSizeX, EditorConstants.FloorHeight, EditorConstants.GridSizeZ);
            _gridPlane.transform.Translate(
                                           Vector3.up * (EditorConstants.DistanceTolerance + InitialGridPlaneHeight)
                                         + Vector3.right * GameConstants.LevelCellsSizeX / 2
                                         + Vector3.forward * GameConstants.LevelCellsSizeZ / 2);

            SceneVisibilityManager.instance.Hide(_gridPlane, true);
        }

        private void HandleInput()
        {
            Event current = Event.current;
            int controlID = GUIUtility.GetControlID(FocusType.Keyboard);

            switch (current.type)
            {
                case EventType.MouseDown:
                {
                    if (current.button == 0 && !_mouseButtonWasPressed)
                    {
                        _mouseButtonWasPressed = true;

                        Ray worldRay = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
                        if (Physics.Raycast(worldRay, out RaycastHit hitInfo))
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

                        if (Physics.Raycast(worldRay, out RaycastHit hitInfo))
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

            if (Event.current.type == EventType.KeyDown && Event.current.shift &&
                Event.current.keyCode == KeyCode.UpArrow)
            {
                _gridPlane.transform.Translate(Vector3.up * EditorConstants.FloorHeight, Space.World);
            }

            if (Event.current.type == EventType.KeyDown && Event.current.shift &&
                Event.current.keyCode == KeyCode.DownArrow)
            {
                _gridPlane.transform.Translate(Vector3.down * EditorConstants.FloorHeight, Space.World);
            }
        }

        private void OnAssetNameChanged(object obj, AssetViewModel assetViewModel)
        {
            if (assetViewModel is null)
            {
                Debug.LogError("Asset info is null");
                return;
            }

            _selectedGameObject = AssetDatabase.LoadAssetAtPath<GameObject>(assetViewModel.PrefabFile.FullName);
            _placerHandler.SetPrefabObject(_selectedGameObject);
        }

        private void OnAssetTypeChanged(object obj, AssetType type)
        {
            _placerHandler = PlacerHandlerCreator.CreatePlacerHandler(type);
        }

        private void UpdateAvailable()
        {
            _availableCurrentTicks++;

            if (_availableCurrentTicks > AvailableCheckTicks)
            {
                _availableCurrentTicks = 0;
                _isAvailable = EditorWindow.HasOpenInstances<AssetLibrary.AssetLibrary>();
            }
        }
    }
}