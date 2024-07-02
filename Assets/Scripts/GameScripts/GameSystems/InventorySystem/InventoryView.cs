using System.Collections.Generic;
using AncientGlyph.GameScripts.Constants;
using AncientGlyph.GameScripts.GameSystems.ItemSystem;
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
        private const string ItemIconName = "ItemIcon";
        private const string SideWindowName = "SideWindow";

        private InventoryModel _inventoryMain;
        private InventoryModel _inventoryLeft;
        private InventoryModel _inventoryRight;

        private int _rotations;
        private GameItemView _capturedGameItem;
        private VisualElement _itemIcon;
        private VisualElement _inventoryUi;
        private VisualElement _iconSpace;
        private VisualElement _ghostIcon;

        [SerializeField]
        private UIDocument _playerSideWindowUiDocument;

        [SerializeField]
        private Camera _camera;

        [SerializeField]
        private InputActionReference _onCloseInventory;

        [SerializeField]
        private InputActionReference _onRotateItem;

        [Inject]
        public void Construct(ISaveDataService saveDataService)
        {
            _inventoryMain = saveDataService.PlayerInfo.InventoryInfo.MainInventory;
            _inventoryLeft = saveDataService.PlayerInfo.InventoryInfo.ExtraInventoryLeft;
            _inventoryRight = saveDataService.PlayerInfo.InventoryInfo.ExtraInventoryRight;

            _inventoryUi = _playerSideWindowUiDocument.rootVisualElement.Q(InventoryName);
            _iconSpace = _playerSideWindowUiDocument.rootVisualElement.Q(IconSpaceName);

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
            if (_capturedGameItem != null) { }
        }

        private void ToggleInventory(InputAction.CallbackContext context)
        {
            VisualElement sideWindow = _playerSideWindowUiDocument.rootVisualElement
                .Query(SideWindowName)
                .First();

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
            VisualElement rootAllInventoryElement = _playerSideWindowUiDocument.rootVisualElement
                .Query(InventoryName)
                .First();

            if (rootAllInventoryElement == null)
            {
                Debug.LogError($"Not found UI inventory: should be named {InventoryName}");
                return;
            }

            rootAllInventoryElement.RegisterCallback<MouseEnterEvent>(OnMouseEnterInventorySpace);
            rootAllInventoryElement.RegisterCallback<MouseLeaveEvent>(OnMouseLeaveInventorySpace);

            return;

            void OnMouseEnterInventorySpace(MouseEnterEvent mouseEvent)
            {
                if (_capturedGameItem == null)
                {
                    return;
                }

                _itemIcon.visible = false;
            }

            void OnMouseLeaveInventorySpace(MouseLeaveEvent mouseEvent)
            {
                if (_capturedGameItem == null)
                {
                    return;
                }

                _itemIcon.visible = true;
            }
        }

        private void AddBindingsToInventory(InventoryModel inventory,
            string inventoryViewName)
        {
            VisualElement rootInventoryElement = _playerSideWindowUiDocument.rootVisualElement
                .Query(inventoryViewName)
                .First();

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

        private void OnMouseEnterItemSlot(MouseEnterEvent mouseEvent,
            InventoryPosition inventoryPosition)
        {
            VisualElement slotVisualElement = (mouseEvent.currentTarget as VisualElement)!;

            if (_capturedGameItem == null)
            {
                return;
            }

            if (inventoryPosition.ParentInventory.CanItemBePlaced(
                    inventoryPosition.SlotModelPosition, _capturedGameItem.GameItem))
            {
                _ghostIcon = _inventoryUi.AttachIcon(_capturedGameItem.GameItem.GhostIcon,
                                                     new Rect(slotVisualElement.worldBound.x,
                                                              slotVisualElement.worldBound.y,
                                                              FileConstants.ItemImageCellSize * 4,
                                                              FileConstants.ItemImageCellSize * 4),
                                                     _capturedGameItem.GameItem.Name);
            }
        }

        private void OnMouseLeaveItemSlot(MouseLeaveEvent mouseEvent,
            InventoryPosition inventoryPosition)
        {
            if (_capturedGameItem == null)
            {
                return;
            }

            _ghostIcon.Dispose();
        }

        private void OnMouseUpInItemSlot(MouseUpEvent mouseEvent,
            InventoryPosition inventoryPosition)
        {
            if (_capturedGameItem == null)
            {
                // TODO : Implement taking item from inventory grid
                return;
            }

            if (inventoryPosition.ParentInventory.CanItemBePlaced(inventoryPosition.SlotModelPosition,
                                                                _capturedGameItem.GameItem))
            {
                inventoryPosition.ParentInventory.TryPlaceItemToPosition(inventoryPosition.SlotModelPosition,
                                                                         _capturedGameItem.GameItem);
                
                VisualElement slotVisualElement = (mouseEvent.currentTarget as VisualElement)!;
                _inventoryUi.AttachIcon(_capturedGameItem.GameItem.Icon,
                                       new Rect(slotVisualElement.worldBound.x,
                                                slotVisualElement.worldBound.y,
                                                FileConstants.ItemImageCellSize * 4,
                                                FileConstants.ItemImageCellSize * 4),
                                       ItemIconName);
                
                _ghostIcon.Dispose();
            }

            DropGameItem();
        }
        
        private void OnMouseUpInIconSpace(MouseUpEvent mouseUpEvent)
        {
            _itemIcon.Dispose();
            DropGameItem();
        }

        private void OnMouseMoveInIconSpace(MouseMoveEvent mouseMoveEvent)
        {
            if (_itemIcon == null)
            {
                return;
            }

            Vector2 mousePosition = mouseMoveEvent.mousePosition;
            _itemIcon.style.top = mousePosition.y;
            _itemIcon.style.left = mousePosition.x;
        }

        // Grabbed item from environment
        private void OnMouseDownInIconSpace(MouseDownEvent mouseDownEvent)
        {
            Vector2 mousePosition = mouseDownEvent.mousePosition;
            mousePosition.y = Screen.height - mousePosition.y;
            Ray cameraRay = _camera.ScreenPointToRay(mousePosition);

            if (Physics.Raycast(cameraRay, out RaycastHit hitPoint))
            {
                GameObject clickedObject = hitPoint.collider.gameObject;
                clickedObject = clickedObject.transform.parent.gameObject;

                if (clickedObject.TryGetComponent(out GameItemView itemView))
                {
                    _capturedGameItem = itemView;
                    _itemIcon = _iconSpace.AttachIcon(_capturedGameItem.GameItem.Icon,
                                                      new Rect(mousePosition.x, mousePosition.y,
                                                               FileConstants.ItemImageCellSize * 4,
                                                               FileConstants.ItemImageCellSize * 4),
                                                      ItemIconName);
                }
            }
        }
        
        private void DropGameItem()
        {
            _capturedGameItem = null;
            //TODO: Implement item dropping
        }
    }
}