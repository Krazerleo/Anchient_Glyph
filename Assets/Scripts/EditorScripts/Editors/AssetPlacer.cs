using AncientGlyph.EditorScripts.Helpers;
using AncientGlyph.EditorScripts.LevelEditingHandlers;
using AncientGlyph.GameScripts.GameWorldModel;
using UnityEditor;
using UnityEditor.EditorTools;
using UnityEditor.ShortcutManagement;
using UnityEngine;

namespace AncientGlyph.EditorScripts.Editors
{
    [EditorTool("Place Selected Asset")]
    public class AssetPlacer : EditorTool
    {
        #region Private Fields

        private LevelModel _levelModel;
        private GameObject _selectedGameObject;

        private const int _availableCheckTicks = 1000;
        private int _availabeCurrentTicks = 0;
        private bool _isAvailable = false;

        private bool _mouseButtonWasPressed = false;
        private IAssetPlacerHandler _placerHandler;

        private int _currentHeightOfGridPlane = 0;
        private GameObject _gridPlane;
        private const string _gridName = "Level Grid";

        #endregion Private Fields
        
        #region Public Methods
        public override bool IsAvailable()
        {
            return false;
        }

        public override void OnActivated()
        {
            AssetLibrary.OnAssetNameChangeHandler += OnAssetNameChanged;
            AssetLibrary.OnAssetTypeChangeHandler += OnAssetTypeChanged;

            _placerHandler = PlacerHandlerCreator.CreatePlacerHandler(AssetLibrary.SelectedTypeAsset, _levelModel);
            _selectedGameObject = PrefabHelper.LoadPrefabFromFile(AssetLibrary.SelectedAssetName);
        }

        public override void OnToolGUI(EditorWindow window)
        {
            if (_isAvailable == true)
            {
                HandleInput();
            }
        }

        public override void OnWillBeDeactivated()
        {
            AssetLibrary.OnAssetNameChangeHandler -= OnAssetNameChanged;
            AssetLibrary.OnAssetTypeChangeHandler -= OnAssetTypeChanged;
        }

        #endregion Public Methods

        #region Private Methods

        [Shortcut("Activate Platform Tool", typeof(SceneView), KeyCode.C, ShortcutModifiers.Shift)]
        private static void PlatformToolShortcut()
        {
            ToolManager.SetActiveTool<AssetPlacer>();
        }

        private void HandleInput()
        {
            var current = Event.current;
            var controlID = GUIUtility.GetControlID(FocusType.Passive);

            switch (current.type)
            {
                case EventType.MouseDown:
                {
                    if (current.button == 0 && !_mouseButtonWasPressed)
                    {
                        _mouseButtonWasPressed = true;

                        Ray worldRay = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
                        RaycastHit hitInfo;

                        if (Physics.Raycast(worldRay, out hitInfo))
                        {
                            _placerHandler.OnMouseButtonPressedHandler(hitInfo.point);
                        }

                        Event.current.Use();
                    }

                    break;
                }

                case EventType.MouseUp:
                {
                    Ray worldRay = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
                    RaycastHit hitInfo;

                    if (Physics.Raycast(worldRay, out hitInfo))
                    {
                        _placerHandler.OnMouseButtonReleasedHandler(hitInfo.point);
                    }

                    _mouseButtonWasPressed = false;
                    Event.current.Use();

                    break;
                }

                case EventType.MouseMove:
                {
                    if (_mouseButtonWasPressed)
                    {
                        Ray worldRay = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
                        RaycastHit hitInfo;

                        if (Physics.Raycast(worldRay, out hitInfo))
                        {
                            _placerHandler.OnMouseMoveHandler(hitInfo.point);
                        }
                    }

                    Event.current.Use();

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
                _currentHeightOfGridPlane++;
            }

            if (Event.current.type == EventType.KeyDown && Event.current.shift && Event.current.keyCode == KeyCode.DownArrow)
            {
                _currentHeightOfGridPlane--;
            }
        }

        private void OnAssetNameChanged(object obj, string name)
        {
            _selectedGameObject = AssetDatabase.LoadAssetAtPath<GameObject>(name);
        }

        private void OnAssetTypeChanged(object obj, AssetType type)
        {
            _placerHandler = PlacerHandlerCreator.CreatePlacerHandler(type, _levelModel);
        }

        private void OnDisable()
        {
            EditorApplication.update -= UpdateAvailable;

            DestroyImmediate(_gridPlane);
        }

        private void OnEnable()
        {
            EditorApplication.update += UpdateAvailable;

            _gridPlane = GameObject.CreatePrimitive(PrimitiveType.Plane);
            _gridPlane.name = _gridName;
            _gridPlane.transform.localScale = new Vector3(72.1f, 72.1f, 72.1f);
            SceneVisibilityManager.instance.Hide(_gridPlane, true);
        }

        private void UpdateAvailable()
        {
            _availabeCurrentTicks++;

            if (_availabeCurrentTicks > _availableCheckTicks)
            {
                _availabeCurrentTicks = 0;

                _isAvailable = (EditorWindow.HasOpenInstances<AssetLibrary>() && AssetLibrary.SelectedAssetName != "");
            }
        }

        #endregion Private Methods
    }
}