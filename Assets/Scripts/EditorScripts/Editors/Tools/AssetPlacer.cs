using System;
using System.Reflection;

using AncientGlyph.EditorScripts.Constants;
using AncientGlyph.EditorScripts.Helpers;
using AncientGlyph.EditorScripts.LevelEditingHandlers;
using AncientGlyph.EditorScripts.LevelEditingHandlers.Interfaces;
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

        private const int _availableCheckTicks = 1000;
        private const string _gridName = "Level Grid";
        private int _availabeCurrentTicks = 0;
        private GameObject _gridPlane;
        private bool _isAvailable = false;
        private LevelModel _levelModel;
        private bool _mouseButtonWasPressed = false;
        private IAssetPlacerHandler _placerHandler;
        private GameObject _selectedGameObject;

        #endregion Private Fields

        #region Unity Messages

        public override bool IsAvailable()
        {
            return _isAvailable;
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

        [Shortcut("Activate Platform Tool", typeof(SceneView), KeyCode.C, ShortcutModifiers.Shift)]
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

            _gridPlane = GameObject.CreatePrimitive(PrimitiveType.Plane);
            _gridPlane.name = _gridName;
            _gridPlane.transform.localScale = new Vector3(EditorConstants.GridSizeX, 1, EditorConstants.GridSizeZ);
            _gridPlane.transform.Translate(Vector3.up * EditorConstants.DistanceTolerance);
            SceneVisibilityManager.instance.Hide(_gridPlane, true);
        }

        #endregion Unity Messages

        #region Private Methods

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

                case EventType.MouseMove:
                {
                    if (_mouseButtonWasPressed)
                    {
                        Ray worldRay = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);

                        if (Physics.Raycast(worldRay, out var hitInfo))
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
                _gridPlane.transform.Translate(Vector3.up * EditorConstants.LevelHeightDifference, Space.World);
            }

            if (Event.current.type == EventType.KeyDown && Event.current.shift && Event.current.keyCode == KeyCode.DownArrow)
            {
                _gridPlane.transform.Translate(Vector3.down * EditorConstants.LevelHeightDifference, Space.World);
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
            _placerHandler = PlacerHandlerCreator.CreatePlacerHandler(type, _levelModel);
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