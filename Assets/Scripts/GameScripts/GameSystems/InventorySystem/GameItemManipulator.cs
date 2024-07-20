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
        private VisualElement _itemIconVisualElement;
        private Vector2 _iconCenterOffset;

        public GameItem GameItem { get; private set; }
        public int Rotations { get; private set; }

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
                _itemIconVisualElement.visible = false;
            }
        }

        public void ShowItemIcon()
        {
            if (GameItem != null)
            {
                _itemIconVisualElement.visible = true;
            }
        }

        public void PickItemFromScene(Vector2 panelCoordinates)
        {
            Ray cameraRay = _camera.ScreenPointToRay(panelCoordinates.ToScreenSpace());

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

            AcceptItem(itemView.GameItem, panelCoordinates);
        }

        public void MoveItemIcon(Vector2 screenCoordinates)
        {
            if (GameItem == null)
            {
                return;
            }

            SetIconPosition(screenCoordinates);
        }

        public void RotateItem()
        {
            if (GameItem != null)
            {
                Rotations = (Rotations + 1) % 4;
            }

            _itemIconVisualElement.style.rotate = new StyleRotate(new Rotate(-90 * Rotations));
        }

        public void DropItem()
        {
            //TODO: Implement item dropping
            ReleaseItem();
        }

        public void ThrowAwayItem(Vector2 mousePosition)
        {
            //TODO: Implement item throwing
            ReleaseItem();
        }

        public void AcceptItem(GameItem item, Vector2 panelCoordinates, bool isVisible = true)
        {
            GameItem = item;
            _iconCenterOffset = GameItem.CellSet.FindCenterOfBoundingBox() * UiConstants.CellSlotSize;
            _itemIconVisualElement = _iconSpace.AttachIcon(GameItem.MiniIcon,
                                                           new Rect(panelCoordinates.x - _iconCenterOffset.x,
                                                                    panelCoordinates.y - _iconCenterOffset.y,
                                                                    FileConstants.ItemImageCellSize * 4,
                                                                    FileConstants.ItemImageCellSize * 4),
                                                           ItemIconName);
            
            _itemIconVisualElement.style.transformOrigin =
                new StyleTransformOrigin(new TransformOrigin(_iconCenterOffset.x, _iconCenterOffset.y));

            if (isVisible == false)
            {
                HideItemIcon();
            }
        }
        
        public void ReleaseItem()
        {
            Rotations = 0;
            _itemIconVisualElement.Dispose();
            _itemIconVisualElement = null;
            GameItem = null;
        }

        private void SetIconPosition(Vector2 screenCoordinates)
        {
            _itemIconVisualElement.style.left = screenCoordinates.x - _iconCenterOffset.x;
            _itemIconVisualElement.style.top = screenCoordinates.y - _iconCenterOffset.y;
        }
    }
}