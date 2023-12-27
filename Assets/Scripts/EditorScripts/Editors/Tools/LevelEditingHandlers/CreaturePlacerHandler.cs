using AncientGlyph.EditorScripts.Editors.Tools.LevelEditingHandlers.Interfaces;
using AncientGlyph.GameScripts.GameWorldModel;
using AncientGlyph.GameScripts.Geometry.Extensions;
using AncientGlyph.GameScripts.Geometry.Shapes;

using UnityEngine;

namespace AncientGlyph.EditorScripts.Editors.Tools.LevelEditingHandlers
{
    public class CreaturePlacerHandler : IAssetPlacerHandler
    {
        #region Public Methods

        private GameObject _creaturePrefab;
        private LevelModelEditor _levelEditor;
        private LevelSceneEditor _sceneEditor;

        public CreaturePlacerHandler()
        {
            _levelEditor = new LevelModelEditor();
            _sceneEditor = new LevelSceneEditor();
        }

        public void OnMouseButtonClickHandler(Vector3 position)
        {
        }

        public void OnMouseButtonPressedHandler(Vector3 position)
        {
        }

        public void OnMouseButtonReleasedHandler(Vector3 position)
        {
            _sceneEditor.PlaceCreature(new Point(position.ToVector3Int()), _creaturePrefab);
        }

        public void OnMouseMoveHandler(Vector3 position)
        {
        }

        public void SetPrefabObject(GameObject prefab)
        {
            _creaturePrefab = prefab;
        }

        #endregion Public Methods
    }
}