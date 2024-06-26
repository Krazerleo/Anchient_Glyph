using System.Collections.Generic;
using AncientGlyph.GameScripts.Constants;
using AncientGlyph.GameScripts.GameSystems.ItemSystem;
using AncientGlyph.GameScripts.Services.SaveDataService;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using Zenject;

namespace AncientGlyph.GameScripts.GameSystems.InventorySystem
{
    public class InventoryView : MonoBehaviour
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
        private VisualElement _itemIconElement;
        private VisualElement _inventoryUiDocument;

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

            _inventoryUiDocument = _playerSideWindowUiDocument.rootVisualElement.Q(InventoryName); 
            
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

            for (int i = 0; i < inventory.Height; i++)
            {
                for (int j = 0; j < inventory.Width; j++)
                {
                    VisualElement slot = slots[j + i * inventory.Width];

                    slot.RegisterCallback<MouseEnterEvent, InventoryPosition>(
                        OnMouseEnterItemSlot,
                        new InventoryPosition(
                            new Vector2(slot.style.left.value.value,
                                        slot.style.top.value.value),
                            new Vector2Int(i, j), inventory));

                    slot.RegisterCallback<MouseLeaveEvent, InventoryPosition>(
                        OnMouseLeaveItemSlot,
                        new InventoryPosition(
                            new Vector2(slot.style.left.value.value,
                                        slot.style.top.value.value),
                            new Vector2Int(i, j), inventory));

                    slot.RegisterCallback<MouseUpEvent, InventoryPosition>(
                        OnMouseUpInItemSlot,
                        new InventoryPosition(
                            new Vector2(slot.style.left.value.value,
                                        slot.style.top.value.value),
                            new Vector2Int(i, j), inventory));
                }
            }
        }

        private void InstantiateItemIconOnUI(Sprite sprite, Rect size,
            string iconName, out VisualElement iconElement)
        {
            iconElement = new VisualElement
            {
                name = iconName,
                style =
                {
                    height = size.height,
                    width = size.width,
                    top = size.y,
                    left = size.x,
                    backgroundImage = new StyleBackground(sprite),
                    position = new StyleEnum<Position>(Position.Absolute),
                },
                pickingMode = PickingMode.Ignore,
            };

            _inventoryUiDocument.Add(iconElement);
        }

        private void DestroyItemIconOnUI(VisualElement itemIconElement)
        {
            if (itemIconElement == null)
            {
                return;
            }

            VisualElement iconParent = itemIconElement.parent;
            iconParent?.Remove(itemIconElement);
        }

        private void OnMouseEnterItemSlot(MouseEnterEvent mouseEvent,
            InventoryPosition inventoryPosition)
        {
            if (_capturedGameItem == null)
            {
                return;
            }

            if (inventoryPosition.CurrentInventory.CanItemPlaced(
                    inventoryPosition.SlotModelPosition.x,
                    inventoryPosition.SlotModelPosition.y,
                    _capturedGameItem.GameItem))
            {
                InstantiateItemIconOnUI(_capturedGameItem.GameItem.GhostIcon,
                                        new Rect(inventoryPosition.SlotViewPosition.x,
                                                 inventoryPosition.SlotViewPosition.y,
                                                 FileConstants.ItemImageCellSize * 4,
                                                 FileConstants.ItemImageCellSize * 4),
                                        _capturedGameItem.GameItem.Name, out VisualElement _);
            }
        }

        private void OnMouseLeaveItemSlot(MouseLeaveEvent mouseEvent,
            InventoryPosition inventoryPosition)
        {
            if (_capturedGameItem == null)
            {
                return;
            }
            // TODO : Implement moving item in inventory grid
            //Debug.Log($"Mouse leave icon slot {inventoryPosition.SlotModelPosition}");
        }

        private void OnMouseUpInItemSlot(MouseUpEvent mouseEvent,
            InventoryPosition inventoryPosition)
        {
            if (_capturedGameItem == null)
            {
                return;
            }
            
            DestroyItemIconOnUI(_itemIconElement);
            
            // TODO : Implement taking item from inventory grid
            //Debug.Log($"Mouse up icon slot {inventoryPosition.SlotModelPosition}");
        }

        private void OnMouseUpInIconSpace(MouseUpEvent mouseUpEvent)
        {
            DestroyItemIconOnUI(_itemIconElement);
        }

        private void OnMouseMoveInIconSpace(MouseMoveEvent mouseMoveEvent)
        {
            if (_itemIconElement == null)
            {
                return;
            }

            Vector2 mousePosition = mouseMoveEvent.mousePosition;
            _itemIconElement.style.top = mousePosition.y;
            _itemIconElement.style.left = mousePosition.x;
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

                if (clickedObject.TryGetComponent<GameItemView>(out var itemView))
                {
                    _capturedGameItem = itemView;
                    InstantiateItemIconOnUI(
                        _capturedGameItem.GameItem.Icon,
                        new Rect(mousePosition.x, mousePosition.y,
                                 FileConstants.ItemImageCellSize * 4,
                                 FileConstants.ItemImageCellSize * 4),
                        ItemIconName, out VisualElement resultIcon);

                    _itemIconElement = resultIcon;
                }
            }
        }

        private readonly struct InventoryPosition
        {
            public readonly Vector2Int SlotModelPosition;
            public readonly Vector2 SlotViewPosition;
            public readonly InventoryModel CurrentInventory;

            public InventoryPosition(Vector2 slotViewPosition,
                Vector2Int slotModelPosition,
                InventoryModel inventory)
            {
                SlotViewPosition = slotViewPosition;
                SlotModelPosition = slotModelPosition;
                CurrentInventory = inventory;
            }
        }
    }
}