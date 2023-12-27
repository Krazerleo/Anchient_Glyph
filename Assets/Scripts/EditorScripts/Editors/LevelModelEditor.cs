using AncientGlyph.GameScripts.Enums;
using AncientGlyph.GameScripts.GameWorldModel;
using AncientGlyph.GameScripts.Geometry.Shapes.Interfaces;
using AncientGlyph.GameScripts.Interactors.Interfaces;
using AncientGlyph.GameScripts.Interactors.Extentions;

using UnityEngine;
using AncientGlyph.EditorScripts.Editors.Tools.LevelFileEditing;

namespace AncientGlyph.EditorScripts.Editors
{
    public class LevelModelEditor
    {
        #region Public Fields

        private LevelModel _levelModel;

        #endregion Public Fields

        #region Public Constructors

        public LevelModelEditor()
        {
            _levelModel = LevelModelDatabase.LevelModelInstance;
        }

        #endregion Public Constructors

        #region Public Methods

        public void PlaceTile(IShape shape)
        {
            foreach (var cellCoordinates in shape.GetDefinedGeometry())
            {
                try
                {
                    _levelModel[cellCoordinates.x, cellCoordinates.z, cellCoordinates.y]
                    .SetWall(WallType.Whole, Direction.Down);

                    _levelModel[cellCoordinates.x, cellCoordinates.z, cellCoordinates.y - 1]
                        .SetWall(WallType.Whole, Direction.Up);
                }
                catch
                {
                    Debug.LogError("Place asset in required field");
                    break;
                }
            }
        }

        public void PlaceEntity(IShape shape, IEntityModel entity)
        {
            foreach (var entityCoordinates in shape.GetDefinedGeometry())
            {
                try
                {
                    var entities = _levelModel[entityCoordinates.x, entityCoordinates.z, entityCoordinates.y]
                    .EntityModelsInCell.Value;

                    if (!entities.IsCellOccupied())
                    {
                        entities.Add(entity);
                    }
                }
                catch
                {
                    Debug.LogError("Place asset in required field");
                    break;
                }
            }
        }

        public void PlaceWall(IShape shape, Direction direction)
        {
            foreach (var wallCoordinates in shape.GetDefinedGeometry())
            {
                try
                {
                    _levelModel[wallCoordinates.x, wallCoordinates.y, wallCoordinates.z]
                            .SetWall(WallType.Whole, direction);
                }
                catch
                {
                    Debug.LogError("Place asset in required field");
                    break;
                }
            }
        }

        public void RemoveEntity(Vector3Int coordinates, string creatureId)
        {
        }

        public void RemoveTile(Vector3Int coordinates)
        {
        }

        public void RemoveWall(Vector3Int coordinates, Direction direction)
        {
        }

        #endregion Public Methods
    }
}