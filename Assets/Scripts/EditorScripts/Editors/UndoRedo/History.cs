using System.Collections.Generic;

using AncientGlyph.EditorScripts.Editors.UndoRedo.Interfaces;

namespace AncientGlyph.EditorScripts.Editors.UndoRedo
{
    public class History
    {
        private int currentIndex = -1;
        private readonly List<IUndoRedoAction> _history = new();
        private static History _historyInstance;

        public bool CanUndo => currentIndex >= 0;
        public bool CanRedo => _history.Count > 0 && currentIndex < _history.Count - 1;

        private History()
        {

        }

        public static History GetHistoryInstance => _historyInstance ??= new History();

        public void AddAction(IUndoRedoAction action)
        {
            CutOffHistory();
            _history.Add(action);
            currentIndex++;
        }

        public void Redo()
        {
            if (!CanRedo)
            {
                return;
            }

            currentIndex++;
            _history[currentIndex].Redo();
        }

        public void Undo()
        {
            if (!CanUndo)
            {
                return;
            }

            _history[currentIndex].Undo();
            currentIndex--;
        }

        private void CutOffHistory()
        {
            int index = currentIndex + 1;

            if (index < _history.Count)
            {
                _history.RemoveRange(index, _history.Count - index);
            }
        }
    }
}