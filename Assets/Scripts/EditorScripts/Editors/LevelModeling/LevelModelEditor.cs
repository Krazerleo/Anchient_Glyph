using System.Linq;

using AncientGlyph.GameScripts.Enums;
using AncientGlyph.GameScripts.Geometry.Shapes.Interfaces;
using AncientGlyph.GameScripts.Interactors.Extentions;
using AncientGlyph.EditorScripts.Editors.LevelModeling.LevelFileEditing;
using AncientGlyph.GameScripts.Interactors;

using UnityEngine;

namespace AncientGlyph.EditorScripts.Editors
{
    public class LevelModelEditor
    {
        public bool TryPlaceTile(IShape3D shape)
        {
            var levelModel = LevelModelData.GetLevelModel();

            foreach (var cellCoordinates in shape.GetDefinedGeometry())
            {
                try
                {
                    levelModel[cellCoordinates.x, cellCoordinates.y, cellCoordinates.z]
                        .SetWall(WallType.Whole, Direction.Down);
                }
                catch
                {
                    Debug.LogError("Tried to place asset out of build range\n"+
                                   $"Cooordinates: x:{cellCoordinates.x} " +
                                                 $"y:{cellCoordinates.y} " +
                                                 $"z:{cellCoordinates.z} ");

                    return false;
                }

                try
                {
                    levelModel[cellCoordinates.x, cellCoordinates.y - 1, cellCoordinates.z]
                        .SetWall(WallType.Whole, Direction.Up);
                }
                catch
                {
                    Debug.LogError("Tried to place asset out of build range\n" +
                                   $"Cooordinates: x:{cellCoordinates.x} " +
                                                 $"y:{cellCoordinates.y - 1} " +
                                                 $"z:{cellCoordinates.z} ");
                    return false;
                }
            }

            return true;
        }

        public bool TryPlaceEntity(IShape3D shape, IEntityModel entity)
        {
            var levelModel = LevelModelData.GetLevelModel();

            foreach (var entityCoordinates in shape.GetDefinedGeometry())
            {
                try
                {
                    var cell = levelModel[entityCoordinates.x, entityCoordinates.y, entityCoordinates.z];

                    if (!cell.GetEntitiesFromCell().IsCellOccupied())
                    {
                        cell.AddEntityToCell(entity);
                    }
                    else
                    {
                        Debug.LogError("Tried to place entity in occupied cell\n" +
                                   $"Cooordinates: x:{entityCoordinates.x} " +
                                                 $"y:{entityCoordinates.y} " +
                                                 $"z:{entityCoordinates.z} ");

                        return false;
                    }
                }
                catch
                {
                    Debug.LogError("Tried to place asset out of build range\n" +
                                   $"Cooordinates: x:{entityCoordinates.x} " +
                                                 $"y:{entityCoordinates.y} " +
                                                 $"z:{entityCoordinates.z} ");

                    return false;
                }
            }

            return true;
        }

        public bool TryPlaceWall(IShape3D shape, Direction direction)
        {
            var levelModel = LevelModelData.GetLevelModel();

            foreach (var wallCoordinates in shape.GetDefinedGeometry())
            {
                try
                {
                    levelModel[wallCoordinates.x, wallCoordinates.y, wallCoordinates.z]
                        .SetWall(WallType.Whole, direction);
                }
                catch
                {
                    Debug.LogError("Tried to place asset out of build range\n" +
                                   $"Cooordinates: x:{wallCoordinates.x} " +
                                                 $"y:{wallCoordinates.y} " +
                                                 $"z:{wallCoordinates.z} ");
                    return false;
                }
            }

            return true;
        }

        public void RemoveTiles(IShape3D shape)
        {
            var levelModel = LevelModelData.GetLevelModel();

            foreach (var cellCoordinates in shape.GetDefinedGeometry())
            {
                try
                {
                    levelModel[cellCoordinates.x, cellCoordinates.y, cellCoordinates.z]
                        .SetWall(WallType.None, Direction.Down);
                }
                catch
                {
                    Debug.LogError("Tried to remove asset out of build range\n" +
                                   $"Cooordinates: x:{cellCoordinates.x} " +
                                                 $"y:{cellCoordinates.y} " +
                                                 $"z:{cellCoordinates.z} ");
                }

                try
                {
                    levelModel[cellCoordinates.x, cellCoordinates.y - 1, cellCoordinates.z]
                        .SetWall(WallType.None, Direction.Up);
                }
                catch
                {
                    Debug.LogError("Tried to remove asset out of build range\n" +
                                   $"Cooordinates: x:{cellCoordinates.x} " +
                                                 $"y:{cellCoordinates.y - 1} " +
                                                 $"z:{cellCoordinates.z} ");
                }
            }
        }

        public void RemoveEntity(IShape3D shape, string entityId)
        {
            var levelModel = LevelModelData.GetLevelModel();

            foreach (var entityCoordinates in shape.GetDefinedGeometry())
            {
                var entities = levelModel[entityCoordinates.x, entityCoordinates.y, entityCoordinates.z]
                    .GetEntitiesFromCell();

                var findedEntity = entities.FirstOrDefault(ent => ent.Identifier == entityId);

                if (findedEntity == null)
                {
                    Debug.LogError("Cannot delete entity with \n" +
                                   $"Cooordinates: x:{entityCoordinates.x} " +
                                                 $"y:{entityCoordinates.y} " +
                                                 $"z:{entityCoordinates.z} \n" +
                                   $"and ID {entityId}");
                }
            }
        }

        public void RemoveWall(IShape3D shape, Direction direction)
        {
            var levelModel = LevelModelData.GetLevelModel();

            foreach (var wallCoordinates in shape.GetDefinedGeometry())
            {
                try
                {
                    levelModel[wallCoordinates.x, wallCoordinates.y, wallCoordinates.z]
                        .SetWall(WallType.None, direction);
                }
                catch
                {
                    Debug.LogError("Tried to place remove out of build range\n" +
                                   $"Cooordinates: x:{wallCoordinates.x} " +
                                                 $"z:{wallCoordinates.z} " +
                                                 $"y:{wallCoordinates.y} ");
                }
            }
        }
    }
}