using System;
using System.Collections.Generic;
using AncientGlyph.GameScripts.Constants;
using AncientGlyph.GameScripts.GameSystems.ItemSystem;

using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace AncientGlyph.EditorScripts.Editors.GameItemTools
{
    [CustomEditor(typeof(GameItem), true)]
    public class GameItemEditor : Editor
    {
        private const string SlotName = "InventorySlot";
        private const string IconFieldName = "IconField";
        private const string ItemGeometryPickerName = "ItemGeometryPicker";
        private const int ExpectedSlotsCount = GameConstants.MaxItemSizeY * GameConstants.MaxItemSizeX;

        private GameItem _inspectedGameItem;

        private List<VisualElement> _slots;

        [SerializeField]
        private VisualTreeAsset _editorUxmlAsset;

        [SerializeField]
        private StyleSheet _freeCellStyleSheet;

        private VisualElement _root;

        public override VisualElement CreateInspectorGUI()
        {
            _inspectedGameItem = target as GameItem;

            _root = new VisualElement();
            _editorUxmlAsset.CloneTree(_root);

            BindCellGrid();
            RestoreGameItemCells();
            ObserveOnSpriteChange();

            return _root;
        }

        public void OnDisable()
        {
            _slots.ForEach(v => v.UnregisterCallback<MouseUpEvent, Vector2Int>(OnSlotClick));
            SaveChanges();
        }

        private void RestoreGameItemCells()
        {
            foreach (var cell in _inspectedGameItem.CellSet.GetDefinedGeometry())
            {
                _slots[cell.x + cell.y * GameConstants.MaxItemSizeX]
                    .styleSheets.Add(_freeCellStyleSheet);
            }
        }

        private void BindCellGrid()
        {
            _slots = _root.Query(SlotName).ToList();

            if (_slots.Count != ExpectedSlotsCount)
            {
                Debug.LogError("Unexpected number of slots in UI Editor\n" +
                    $"Expected: {ExpectedSlotsCount} / Actual {_slots.Count}");

                return;
            }

            for (int y = 0; y < GameConstants.MaxItemSizeY; y++)
            {
                for (int x = 0; x < GameConstants.MaxItemSizeX; x++)
                {
                    _slots[x + y*GameConstants.MaxItemSizeX]
                        .RegisterCallback<MouseUpEvent, Vector2Int>(OnSlotClick, new(x, y));
                }
            }
        }

        private void OnSlotClick(MouseUpEvent clickEvent, Vector2Int position)
        {
            if (_inspectedGameItem.CellSet.Contains(position))
            {
                _inspectedGameItem.CellSet.Remove(position);
                _slots[position.x + position.y * GameConstants.MaxItemSizeX]
                    .styleSheets.Remove(_freeCellStyleSheet);
            }
            else
            {
                _inspectedGameItem.CellSet.Add(position);
                _slots[position.x + position.y * GameConstants.MaxItemSizeX]
                    .styleSheets.Add(_freeCellStyleSheet);
            }
        }

        private void ObserveOnSpriteChange()
        {
            var spriteField = _root.Query<PropertyField>(IconFieldName).First();
            spriteField.RegisterValueChangeCallback(OnSpriteChange);
        }

        private void OnSpriteChange(SerializedPropertyChangeEvent propertyChangedEvent)
        {
            if (_inspectedGameItem.Icon == null)
            {
                return;
            }

            var container = _root.Query(ItemGeometryPickerName).First();
            container.style.backgroundImage = new StyleBackground(_inspectedGameItem.Icon);
        }
    }
}