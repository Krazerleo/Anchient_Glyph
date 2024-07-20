using System.Collections.Generic;
using AncientGlyph.GameScripts.Constants;
using AncientGlyph.GameScripts.GameSystems.ItemSystem;
using AncientGlyph.GameScripts.Geometry.Shapes.PlanarShapes;
using AncientGlyph.GameScripts.Services.SaveDataService;
using AncientGlyph.GameScripts.UserInterface;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using Zenject;

namespace AncientGlyph.GameScripts.GameSystems.InventorySystem
{
    public partial class InventoryView : MonoBehaviour
    {
        private const string InventoryMainName = "MainInventory";
        private const string InventoryLeftName = "LeftInventory";
        private const string InventoryRightName = "RightInventory";
        private const string InventoryName = "Inventory";
        private const string IconSpaceName = "IconSpace";
        private const string SideWindowName = "SideWindow";

        private Storage _inventoryMain;
        private Storage _inventoryLeft;
        private Storage _inventoryRight;

        private VisualElement _inventoryUi;
        private VisualElement _inventoryIconVisualElement;
        private GameItemManipulator _manipulator;

        [SerializeField]
        private UIDocument _playerSideWindowUiDocument;

        [SerializeField]
        private InputActionReference _onCloseInventory;

        [SerializeField]
        private InputActionReference _onRotateItem;

        [Inject]
        public void Construct(ISaveDataService saveDataService, Camera mainCamera)
        {
            _inventoryMain = saveDataService.PlayerInfo.InventoryInfo.MainInventory;
            _inventoryLeft = saveDataService.PlayerInfo.InventoryInfo.ExtraInventoryLeft;
            _inventoryRight = saveDataService.PlayerInfo.InventoryInfo.ExtraInventoryRight;
            _inventoryUi = _playerSideWindowUiDocument.rootVisualElement.Q(InventoryName);

            VisualElement iconSpace = _playerSideWindowUiDocument.rootVisualElement.Q(IconSpaceName);
            _manipulator = new GameItemManipulator(mainCamera, iconSpace);

            AddBindings();
            ToggleInventory(default);
        }

        private void OnDisable()
        {
            RemoveBindings();
        }

        private void AddBindings()
        {
            AddInventorySpaceInteraction();
            AddIconSpaceInteraction();
            AddInventoryToggle();
            AddRotateAction();

            AddBindingsToInventory(_inventoryMain,  InventoryMainName);
            AddBindingsToInventory(_inventoryLeft,  InventoryLeftName);
            AddBindingsToInventory(_inventoryRight, InventoryRightName);
        }

        private void RemoveBindings()
        {
            _onRotateItem.action.performed -= RotateItem;
            _onCloseInventory.action.performed -= ToggleInventory;
        }

        private void AddInventoryToggle()
            => _onCloseInventory.action.performed += ToggleInventory;

        private void AddRotateAction()
            => _onRotateItem.action.performed += RotateItem;

        private void RotateItem(InputAction.CallbackContext context)
        {
            _manipulator.RotateItem();

            if (_inventoryIconVisualElement != null)
            {
                _inventoryIconVisualElement.style.rotate = new StyleRotate(new Rotate(-90 * _manipulator.Rotations));
            }
        }

        private void ToggleInventory(InputAction.CallbackContext context)
        {
            VisualElement sideWindow = _playerSideWindowUiDocument.rootVisualElement.Q(SideWindowName);
            sideWindow.visible = !sideWindow.visible;
        }

        private void AddIconSpaceInteraction()
        {
            VisualElement rootIconSpace = _playerSideWindowUiDocument.rootVisualElement;

            if (rootIconSpace == null)
            {
                Debug.LogError($"Not found IconSpace element: should be named {IconSpaceName}");
                return;
            }

            rootIconSpace.RegisterCallback<MouseDownEvent>(OnMouseDownInIconSpace);
            rootIconSpace.RegisterCallback<MouseUpEvent>(OnMouseUpInIconSpace);
            rootIconSpace.RegisterCallback<MouseMoveEvent>(OnMouseMoveInIconSpace);
        }

        private void AddInventorySpaceInteraction()
        {
            VisualElement rootAllInventoryElement = _playerSideWindowUiDocument.rootVisualElement.Q(InventoryName);

            if (rootAllInventoryElement == null)
            {
                Debug.LogError($"Not found UI inventory: should be named {InventoryName}");
                return;
            }

            rootAllInventoryElement.RegisterCallback<MouseEnterEvent>(OnMouseEnterInventorySpace);
            rootAllInventoryElement.RegisterCallback<MouseLeaveEvent>(OnMouseLeaveInventorySpace);
        }

        private void OnMouseEnterInventorySpace(MouseEnterEvent mouseEvent) => _manipulator.HideItemIcon();

        private void OnMouseLeaveInventorySpace(MouseLeaveEvent mouseEvent) => _manipulator.ShowItemIcon();

        private void AddBindingsToInventory(Storage inventory, string inventoryViewName)
        {
            VisualElement rootInventoryElement = _playerSideWindowUiDocument.rootVisualElement.Q(inventoryViewName);

            if (rootInventoryElement == null)
            {
                Debug.LogError($"Not found UI inventory: should be named {inventoryViewName}");
                return;
            }

            List<VisualElement> slots = rootInventoryElement.Query("Slot").ToList();

            if (slots.Count != inventory.SlotsCount)
            {
                Debug.LogError("UI and Model slots count are different");
                return;
            }

            for (int j = 0; j < inventory.Height; j++)
            {
                for (int i = 0; i < inventory.Width; i++)
                {
                    VisualElement slot = slots[i + j * inventory.Width];
                    InventoryPosition inventoryPosition = new(new Vector2Int(i, j), inventory);

                    slot.RegisterCallback<MouseEnterEvent, InventoryPosition>(OnMouseEnterItemSlot, inventoryPosition);
                    slot.RegisterCallback<MouseLeaveEvent, InventoryPosition>(OnMouseLeaveItemSlot, inventoryPosition);
                    slot.RegisterCallback<MouseUpEvent, InventoryPosition>(OnMouseUpInItemSlot, inventoryPosition);
                }
            }
        }

        private void OnMouseEnterItemSlot(MouseEnterEvent mouseEvent, InventoryPosition inventoryPosition)
        {
            VisualElement slotVisualElement = (mouseEvent.currentTarget as VisualElement)!;
            GameItem gameItem = _manipulator.GameItem;

            if (gameItem == null)
            {
                return;
            }

            Vector2Int pivotCell = CalculateModelOffset(inventoryPosition, gameItem);

            if (false == inventoryPosition.ParentInventory.CanItemBePlaced(pivotCell, gameItem, _manipulator.Rotations))
            {
                return;
            }

            Vector2 iconCenterOffset = gameItem.CellSet.FindCenterOfBoundingBox() * UiConstants.CellSlotSize;

            _inventoryIconVisualElement = _inventoryUi
                .AttachIcon(gameItem.InventoryIcon,
                            new Rect(slotVisualElement.worldBound.x - (int)(iconCenterOffset.x / 64f) * 64,
                                     slotVisualElement.worldBound.y - (int)(iconCenterOffset.y / 64f) * 64,
                                     FileConstants.ItemImageCellSize * 4,
                                     FileConstants.ItemImageCellSize * 4),
                            gameItem.Name);

            _inventoryIconVisualElement.style.transformOrigin
                = new StyleTransformOrigin(new TransformOrigin(iconCenterOffset.x, iconCenterOffset.y));
            _inventoryIconVisualElement.style.rotate = new StyleRotate(new Rotate(-90 * _manipulator.Rotations));
        }

        private static Vector2Int CalculateModelOffset(InventoryPosition inventoryPosition, GameItem gameItem)
        {
            Vector2Int selectedCell = inventoryPosition.SlotModelPosition;
            RectInt cellSetBoundingBox = gameItem.CellSet.GetBoundingBox();
            Vector2Int pivotCell = new(selectedCell.x - cellSetBoundingBox.width / 2,
                                       selectedCell.y - cellSetBoundingBox.height / 2);
            return pivotCell;
        }

        private void OnMouseLeaveItemSlot(MouseLeaveEvent mouseEvent, InventoryPosition inventoryPosition)
        {
            if (_manipulator.GameItem != null)
            {
                _inventoryIconVisualElement.Dispose();
            }
        }

        private void OnMouseUpInItemSlot(MouseUpEvent mouseEvent, InventoryPosition inventoryPosition)
        {
            GameItem gameItem = _manipulator.GameItem;

            if (gameItem == null)
            {
                // TODO : Implement taking item from inventory grid
                return;
            }

            _inventoryIconVisualElement.Dispose();

            Vector2Int pivotCell = CalculateModelOffset(inventoryPosition, gameItem);

            if (inventoryPosition.ParentInventory.TryPlaceItemToPosition(pivotCell, gameItem, _manipulator.Rotations))
            {
                VisualElement slotVisualElement = (mouseEvent.currentTarget as VisualElement)!;
                Vector2 iconCenterOffset = gameItem.CellSet.FindCenterOfBoundingBox() * UiConstants.CellSlotSize;
                VisualElement newIcon = _inventoryUi
                    .AttachIcon(gameItem.MiniIcon,
                                new Rect(slotVisualElement.worldBound.x - (int)(iconCenterOffset.x / 64f) * 64,
                                         slotVisualElement.worldBound.y - (int)(iconCenterOffset.y / 64f) * 64,
                                         FileConstants.ItemImageCellSize * 4,
                                         FileConstants.ItemImageCellSize * 4),
                                GameItemManipulator.ItemIconName);

                newIcon.style.transformOrigin
                    = new StyleTransformOrigin(new TransformOrigin(iconCenterOffset.x, iconCenterOffset.y));
                newIcon.style.rotate = new StyleRotate(new Rotate(-90 * _manipulator.Rotations));

                _manipulator.ReleaseItem();
                return;
            }

            _manipulator.DropItem();
        }

        private void OnMouseUpInIconSpace(MouseUpEvent mouseUpEvent) =>
            _manipulator.ThrowAwayItem(mouseUpEvent.mousePosition.ToScreenSpace());

        private void OnMouseMoveInIconSpace(MouseMoveEvent mouseMoveEvent) =>
            _manipulator.MoveItemIcon(mouseMoveEvent.mousePosition);

        private void OnMouseDownInIconSpace(MouseDownEvent mouseDownEvent) =>
            _manipulator.PickItemFromScene(mouseDownEvent.mousePosition);
    }
}