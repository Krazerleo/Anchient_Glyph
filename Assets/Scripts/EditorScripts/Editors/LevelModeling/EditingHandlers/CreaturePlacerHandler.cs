using AncientGlyph.GameScripts.EntityModel;
using AncientGlyph.GameScripts.ForEditor;
using AncientGlyph.GameScripts.Geometry;
using AncientGlyph.GameScripts.Geometry.Shapes;
using UnityEngine;

namespace AncientGlyph.EditorScripts.Editors.LevelModeling.EditingHandlers
{
    public class CreaturePlacerHandler : IAssetPlacerHandler
    {
        private GameObject _entityPrefab;
        private readonly LevelSceneEditor _sceneEditor = new();

        public void OnMouseButtonPressedHandler(Vector3 position)
        {
        }

        public void OnMouseButtonReleasedHandler(Vector3 position)
        {
            var traits = _entityPrefab.GetComponent<CreatureTraitsSource>()?.CreatureTraits;

            if (traits == null)
            {
                Debug.LogError("Entity must have creature traits config " +
                               "before placement of scene");

                return;
            }

            var entity = new CreatureModel(traits, _entityPrefab.name, position.ToVector3Int());

            _sceneEditor.PlaceEntities(new Point(position.ToVector3Int()),
                _entityPrefab, entity.Identifier);
        }

        public void OnMouseMoveHandler(Vector3 position)
        {
        }

        public void SetPrefabObject(GameObject prefab)
            => _entityPrefab = prefab;
    }
}