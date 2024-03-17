using System.Linq;
using AncientGlyph.GameScripts.EntityModel;
using AncientGlyph.GameScripts.Enums;
using AncientGlyph.GameScripts.GameWorldModel;
using AncientGlyph.GameScripts.Geometry.Shapes.Interfaces;
using UnityEngine;

namespace AncientGlyph.EditorScripts.Editors.LevelModeling
{
    public class LevelModelEditor
    {
        private readonly LevelModel _levelModel;
        
        public LevelModelEditor(LevelModel levelModel)
        {
            _levelModel = levelModel;
        }

        public bool TryPlaceTile(IShape3D shape)
        {
            foreach (var cellCoordinates in shape.GetDefinedGeometry())
            {
                try
                {
                    _levelModel[cellCoordinates.x, cellCoordinates.y, cellCoordinates.z]
                        .SetWall(WallType.Whole, Direction.Down);
                }
                catch
                {
                    Debug.LogError("Tried to place asset out of build range\n"+
                                   $"Coordinates: x:{cellCoordinates.x} " +
                                                 $"y:{cellCoordinates.y} " +
                                                 $"z:{cellCoordinates.z} ");

                    return false;
                }

                try
                {
                    _levelModel[cellCoordinates.x, cellCoordinates.y - 1, cellCoordinates.z]
                        .SetWall(WallType.Whole, Direction.Up);
                }
                catch
                {
                    Debug.LogError("Tried to place asset out of build range\n" +
                                   $"Coordinates: x:{cellCoordinates.x} " +
                                                 $"y:{cellCoordinates.y - 1} " +
                                                 $"z:{cellCoordinates.z} ");
                    return false;
                }
            }

            return true;
        }

        public bool TryPlaceEntity(IShape3D shape, IEntityModel entity)
        {
            if (entity == null)
            {
                Debug.LogError("Entity is null");
                return false;
            }
            
            foreach (var entityCoordinates in shape.GetDefinedGeometry())
            {
                try
                {
                    var cell = _levelModel[entityCoordinates.x, entityCoordinates.y, entityCoordinates.z];

                    if (!cell.GetEntitiesFromCell().IsCellOccupied())
                    {
                        cell.AddEntityToCell(entity);
                    }
                    else
                    {
                        Debug.LogError("Tried to place entity in occupied cell\n" +
                                   $"Coordinates: x:{entityCoordinates.x} " +
                                                 $"y:{entityCoordinates.y} " +
                                                 $"z:{entityCoordinates.z} ");

                        return false;
                    }
                }
                catch
                {
                    Debug.LogError("Tried to place asset out of build range\n" +
                                   $"Coordinates: x:{entityCoordinates.x} " +
                                                 $"y:{entityCoordinates.y} " +
                                                 $"z:{entityCoordinates.z} ");

                    return false;
                }
            }

            return true;
        }

        public bool TryPlaceWall(IShape3D shape, Direction direction)
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
                    Debug.LogError("Tried to place asset out of build range\n" +
                                   $"Coordinates: x:{wallCoordinates.x} " +
                                                 $"y:{wallCoordinates.y} " +
                                                 $"z:{wallCoordinates.z} ");
                    return false;
                }
            }

            return true;
        }

        public void RemoveTiles(IShape3D shape)
        {
            foreach (var cellCoordinates in shape.GetDefinedGeometry())
            {
                try
                {
                    _levelModel[cellCoordinates.x, cellCoordinates.y, cellCoordinates.z]
                        .SetWall(WallType.None, Direction.Down);
                }
                catch
                {
                    Debug.LogError("Tried to remove asset out of build range\n" +
                                   $"Coordinates: x:{cellCoordinates.x} " +
                                                 $"y:{cellCoordinates.y} " +
                                                 $"z:{cellCoordinates.z} ");
                }

                try
                {
                    _levelModel[cellCoordinates.x, cellCoordinates.y - 1, cellCoordinates.z]
                        .SetWall(WallType.None, Direction.Up);
                }
                catch
                {
                    Debug.LogError("Tried to remove asset out of build range\n" +
                                   $"Coordinates: x:{cellCoordinates.x} " +
                                                 $"y:{cellCoordinates.y - 1} " +
                                                 $"z:{cellCoordinates.z} ");
                }
            }
        }

        public void RemoveEntity(IShape3D shape, string entityId)
        {
            foreach (var entityCoordinates in shape.GetDefinedGeometry())
            {
                var entities = _levelModel[entityCoordinates.x, entityCoordinates.y, entityCoordinates.z]
                    .GetEntitiesFromCell();

                var foundEntity = entities.FirstOrDefault(ent => ent.Identifier == entityId);

                if (foundEntity == null)
                {
                    Debug.LogError("Cannot delete entity with \n" +
                                   $"Coordinates: x:{entityCoordinates.x} " +
                                                 $"y:{entityCoordinates.y} " +
                                                 $"z:{entityCoordinates.z} \n" +
                                   $"and ID {entityId}");
                }
            }
        }

        public void RemoveWall(IShape3D shape, Direction direction)
        {
            foreach (var wallCoordinates in shape.GetDefinedGeometry())
            {
                try
                {
                    _levelModel[wallCoordinates.x, wallCoordinates.y, wallCoordinates.z]
                        .SetWall(WallType.None, direction);
                }
                catch
                {
                    Debug.LogError("Tried to place remove out of build range\n" +
                                   $"Coordinates: x:{wallCoordinates.x} " +
                                                 $"z:{wallCoordinates.z} " +
                                                 $"y:{wallCoordinates.y} ");
                }
            }
        }
    }
}