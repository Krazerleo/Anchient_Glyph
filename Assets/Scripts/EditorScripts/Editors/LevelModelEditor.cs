using AncientGlyph.GameScripts.GameWorldModel;
using AncientGlyph.GameScripts.Geometry.Shapes.Interfaces;

namespace AncientGlyph.EditorScripts.Editors
{
    public class LevelModelEditor
    {
        #region Public Fields

        public readonly LevelModel LevelModel;

        #endregion Public Fields

        #region Public Constructors

        public LevelModelEditor(LevelModel levelModel)
        {
            LevelModel = levelModel;
        }

        #endregion Public Constructors

        #region Public Methods

        public void PlaceTile(IShape shape)
        {
            foreach (var cellCoordinates in shape.GetDefinedGeometry())
            {
                LevelModel[cellCoordinates.x, cellCoordinates.z, cellCoordinates.y]
                    .SetWall(GameScripts.Enums.WallType.Whole, GameScripts.Enums.Direction.Down);
            }
        }

        #endregion Public Methods
    }
}