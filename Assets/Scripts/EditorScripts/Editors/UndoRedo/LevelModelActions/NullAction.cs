using AncientGlyph.EditorScripts.Editors.UndoRedo.Interfaces;

namespace AncientGlyph.EditorScripts.Editors.UndoRedo
{
    // Because model ando redo has strong coupling
    // with unity undo redo there is placeholder for
    // cases when need undo redo for scene only
    public class NullAction : IUndoRedoAction
    {
        public void Redo() { }

        public void Undo() { }
    }
}