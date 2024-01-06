using AncientGlyph.EditorScripts.Editors.Tools.LevelEditingHandlers.Interfaces;
using AncientGlyph.GameScripts.ForEditor;
using AncientGlyph.GameScripts.Geometry.Extensions;
using AncientGlyph.GameScripts.Geometry.Shapes;
using AncientGlyph.GameScripts.Interactors.Creatures;

using UnityEditor;
using UnityEngine;

namespace AncientGlyph.EditorScripts.Editors.Tools.LevelEditingHandlers
{
    public class CreaturePlacerHandler : IAssetPlacerHandler
    {
        private GameObject _entityPrefab;
        private LevelModelEditor _levelEditor = new();
        private LevelSceneEditor _sceneEditor = new();

        public void OnMouseButtonClickHandler(Vector3 position) { }

        public void OnMouseButtonPressedHandler(Vector3 position) { }

        public void OnMouseButtonReleasedHandler(Vector3 position)
        {
            var traits = _entityPrefab.GetComponent<CreatureTraitsSource>()?.CreatureTraits;

            if (traits == null)
            {
                Debug.LogError("Entity must have creature traits config" +
                               "before placement of scene");

                return;
            }

            var entity = new CreatureModel(traits, position.ToVector3Int(), GUID.Generate().ToString());

            if (_levelEditor.TryPlaceEntity(new Point(position.ToVector3Int()), entity))
            {
                _sceneEditor.PlaceEntity(new Point(position.ToVector3Int()),
                                           _entityPrefab, entity.Identifier);
            }
        }

        public void OnMouseMoveHandler(Vector3 position) { }

        public void SetPrefabObject(GameObject prefab)
            => _entityPrefab = prefab;
    }
}