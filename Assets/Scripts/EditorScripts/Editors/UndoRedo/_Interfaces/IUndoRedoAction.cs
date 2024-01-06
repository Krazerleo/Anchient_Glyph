namespace AncientGlyph.EditorScripts.Editors.UndoRedo.Interfaces
{
    public interface IUndoRedoAction
    {
        public void Undo();
        public void Redo();
    }
}