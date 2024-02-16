using AncientGlyph.EditorScripts.Editors.LevelModeling.LevelEditingHandlers.Interfaces;
using AncientGlyph.GameScripts.ForEditor;
using AncientGlyph.GameScripts.Geometry;
using AncientGlyph.GameScripts.Geometry.Shapes;
using AncientGlyph.GameScripts.Interactors.Entities;

using UnityEditor;
using UnityEngine;

namespace AncientGlyph.EditorScripts.Editors.LevelModeling.LevelEditingHandlers
{
    public class CreaturePlacerHandler : IAssetPlacerHandler
    {
        private GameObject _entityPrefab;
        private LevelModelEditor _levelEditor = new();
        private LevelSceneEditor _sceneEditor = new();

        public void OnMouseButtonPressedHandler(Vector3 position) { }

        public void OnMouseButtonReleasedHandler(Vector3 position)
        {
            var traits = _entityPrefab.GetComponent<CreatureTraitsSource>()?.CreatureTraits;

            if (traits == null)
            {
                Debug.LogError("Entity must have creature traits config " +
                               "before placement of scene");

                return;
            }

            var entity = new CreatureModel(traits, position.ToVector3Int(), GUID.Generate().ToString(), _entityPrefab.name);

            if (_levelEditor.TryPlaceEntity(new Point(position.ToVector3Int()), entity))
            {
                _sceneEditor.PlaceEntities(new Point(position.ToVector3Int()),
                                           _entityPrefab, entity.Identifier);
            }
        }

        public void OnMouseMoveHandler(Vector3 position) { }

        public void SetPrefabObject(GameObject prefab)
            => _entityPrefab = prefab;
    }
}