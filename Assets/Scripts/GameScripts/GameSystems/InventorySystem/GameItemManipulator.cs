using AncientGlyph.GameScripts.Constants;
using AncientGlyph.GameScripts.GameSystems.ItemSystem;
using AncientGlyph.GameScripts.UserInterface;
using UnityEngine;
using UnityEngine.UIElements;

namespace AncientGlyph.GameScripts.GameSystems.InventorySystem
{
    public class GameItemManipulator
    {
        public const string ItemIconName = "ItemIcon";
        private VisualElement _itemIcon;
        
        public GameItem GameItem { get; private set; }
        public int Rotations { get; private set; } = 0;
        
        private readonly Camera _camera;
        private readonly VisualElement _iconSpace;
        
        public GameItemManipulator(Camera camera, VisualElement iconSpace)
        {
            _camera = camera;
            _iconSpace = iconSpace;
        }

        public void HideItemIcon()
        {
            if (GameItem != null)
            {
                _itemIcon.visible = false;
            }
        }
        
        public void ShowItemIcon()
        {
            if (GameItem != null)
            {
                _itemIcon.visible = true;
            }
        }

        public void MoveItemIcon(Vector2 screenCoordinates)
        {
            if (GameItem == null)
            {
                return;
            }

            _itemIcon.style.top = screenCoordinates.y;
            _itemIcon.style.left = screenCoordinates.x;
        }
        
        
        public void PickItemFromScene(Vector2 screenCoordinates)
        {
            Ray cameraRay = _camera.ScreenPointToRay(screenCoordinates);

            if (Physics.Raycast(cameraRay, out RaycastHit hitPoint) == false)
            {
                return;
            }
            
            GameObject clickedObject = hitPoint.collider.gameObject;
            clickedObject = clickedObject.transform.parent.gameObject;

            if (clickedObject.TryGetComponent(out GameItemView itemView) == false)
            {
                return;
            }

            GameItem = itemView.GameItem;
            _itemIcon = _iconSpace.AttachIcon(GameItem.Icon,
                                              new Rect(screenCoordinates.x, screenCoordinates.y,
                                                       FileConstants.ItemImageCellSize * 4,
                                                       FileConstants.ItemImageCellSize * 4),
                                              ItemIconName);
        }

        public void RotateItem()
        {
            if (GameItem != null)
            {
                Rotations = (Rotations + 1) % 4;
            }
            
            //TODO: Add visual element rotation
        }

        public void DropItem()
        {
            //TODO: Implement item dropping
        }

        public void ThrowAwayItem(Vector2 mousePosition)
        {
            //TODO: Implement item throwing
        }
    }
}