using AncientGlyph.EditorScripts.Editors.UndoRedo.Interfaces;
using AncientGlyph.GameScripts.Geometry.Shapes.Interfaces;

namespace AncientGlyph.EditorScripts.Editors.UndoRedo
{
    public class AddTileAction : IUndoRedoAction
    {
        private readonly IShape3D _shape;
        private readonly LevelModelEditor _levelModelEditor = new();

        public AddTileAction(IShape3D shape)
        {
            _shape = shape;
        }

        public void Redo()
        {
            _levelModelEditor.TryPlaceTile(_shape);
        }

        public void Undo()
        {
            _levelModelEditor.RemoveTiles(_shape);
        }
    }

    public class RemoveTileAction : IUndoRedoAction
    {
        private readonly AddTileAction _addTileAction;

        public RemoveTileAction(IShape3D shape)
        {
           _addTileAction = new AddTileAction(shape);
        }

        public void Redo()
        {
            _addTileAction.Undo();
        }

        public void Undo()
        {
            _addTileAction.Redo();
        }
    }
}