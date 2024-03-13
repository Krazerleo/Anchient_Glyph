using AncientGlyph.GameScripts.Constants;
using AncientGlyph.GameScripts.GameSystems.ItemSystem;

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace AncientGlyph.GameScripts.GameSystems.InventorySystem
{
    public class InventoryView : MonoBehaviour
    {
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

        [SerializeField]
        private UIDocument _inventoryUiDocument;

        [SerializeField]
        private Camera _camera;

        [SerializeField]
        private InputActionReference _onCloseInventory;

        [SerializeField]
        private InputActionReference _onRotateItem;

        public InventoryView(InventoryModel inventoryMain,
            InventoryModel inventoryLeft, InventoryModel inventoryRight)
        {
            _inventoryMain = inventoryMain;
            _inventoryLeft = inventoryLeft;
            _inventoryRight = inventoryRight;

            AddBindings();
        }

        private void Awake()
        {
            _inventoryMain = new InventoryModel(6, 4);
            _inventoryLeft = new InventoryModel(3, 3);
            _inventoryRight = new InventoryModel(3, 3);

            AddBindings();
        }

        private void AddBindings()
        {
            AddInventorySpaceInteraction();
            AddIconSpaceInteraction();
            AddIconDragging();
            AddInventoryToggle();
            AddRotateAction();

            AddBindingsToInventory(_inventoryMain, InventoryMainName);
            AddBindingsToInventory(_inventoryLeft, InventoryLeftName);
            AddBindingsToInventory(_inventoryRight, InventoryRightName);
        }

        private void RemoveBindings()
        {
            // TODO
        }

        private void AddRotateAction()
            => _onRotateItem.action.performed += RotateItem;

        private void RotateItem(InputAction.CallbackContext context)
            => _rotations++;

        private void AddInventoryToggle()
            => _onCloseInventory.action.performed += CloseInventory;

        private void CloseInventory(InputAction.CallbackContext context)
        {
            var sideWindow = _inventoryUiDocument.rootVisualElement
                .Query(SideWindowName).First();

            sideWindow.visible = !sideWindow.visible;
        }

        private void AddIconDragging()
            => _inventoryUiDocument.rootVisualElement
                .RegisterCallback<MouseMoveEvent>(OnMouseMoveIconSpace);

        private void AddIconSpaceInteraction()
        {
            var rootIconSpace = _inventoryUiDocument.rootVisualElement
                .Query(IconSpaceName)
                .First();

            if (rootIconSpace == null)
            {
                Debug.LogError($"Not found IconSpace element: should be named {IconSpaceName}");
                return;
            }

            rootIconSpace.RegisterCallback<MouseDownEvent>(OnMouseDownIconSpace);
            rootIconSpace.RegisterCallback<MouseUpEvent>(OnMouseUpIconSpace);
        }

        private void AddInventorySpaceInteraction()
        {
            var rootAllInventoryElement = _inventoryUiDocument.rootVisualElement
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
            var rootInventoryElement = _inventoryUiDocument.rootVisualElement
                .Query(inventoryViewName)
                .First();

            if (rootInventoryElement == null)
            {
                Debug.LogError($"Not found UI inventory: should be named {inventoryViewName}");
                return;
            }

            var slots = rootInventoryElement.Query("Slot").ToList();

            if (slots.Count != inventory.SlotsCount)
            {
                Debug.LogError("UI and Model slots count are different");
                return;
            }

            for (int i = 0; i < inventory.Height; i++)
            {
                for (int j = 0; j < inventory.Width; j++)
                {
                    var slot = slots[j + i*inventory.Width];

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
                        OnMouseUpItemSlot,
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
                    position = new StyleEnum<Position>(Position.Absolute)
                },
                pickingMode = PickingMode.Ignore
            };

            _inventoryUiDocument.rootVisualElement.Add(iconElement);
        }

        private void DestroyItemIconOnUI(VisualElement itemIconElement)
        {
            if (itemIconElement == null)
            {
                return;
            }

            var iconParent = itemIconElement.parent;

            iconParent.Remove(itemIconElement);
        }

        private void OnMouseEnterItemSlot(MouseEnterEvent mouseEvent,
            InventoryPosition inventoryPosition)
        {
            if (inventoryPosition.CurrentInventory.CanItemPlaced(
                inventoryPosition.SlotModelPosition.x,
                inventoryPosition.SlotModelPosition.y,
                _capturedGameItem.GameItem))
            {
                InstantiateItemIconOnUI(_capturedGameItem.GameItem.GhostIcon,
                    new Rect(inventoryPosition.SlotViewPosition.x, inventoryPosition.SlotViewPosition.y,
                        FileConstants.ItemImageCellSize * 4, FileConstants.ItemImageCellSize * 4),
                    _capturedGameItem.GameItem.Name, out var iconElement);
            }
        }

        private void OnMouseLeaveItemSlot(MouseLeaveEvent mouseEvent,
            InventoryPosition inventoryPosition)
        {
            // TODO : Implement moving item in inventory grid
            //Debug.Log($"Mouse leave icon slot {inventoryPosition.SlotModelPosition}");
        }

        private void OnMouseUpItemSlot(MouseUpEvent mouseEvent,
            InventoryPosition inventoryPosition)
        {
            // TODO : Implement taking item from inventory grid
            //Debug.Log($"Mouse up icon slot {inventoryPosition.SlotModelPosition}");
        }

        private void OnMouseUpIconSpace(MouseUpEvent mouseUpEvent)
            => DestroyItemIconOnUI(_itemIconElement);

        private void OnMouseMoveIconSpace(MouseMoveEvent mouseMoveEvent)
        {
            if (_itemIconElement == null)
            {
                return;
            }

            var mousePosition = mouseMoveEvent.mousePosition;
            _itemIconElement.style.top = mousePosition.y;
            _itemIconElement.style.left = mousePosition.x;
        }

        private void OnMouseDownIconSpace(MouseDownEvent mouseDownEvent)
        {
            var mousePosition = mouseDownEvent.mousePosition;
            mousePosition.y = Screen.height - mousePosition.y;
            var cameraRay = _camera.ScreenPointToRay(mousePosition);

            if (Physics.Raycast(cameraRay, out var hitPoint))
            {
                var clickedObject = hitPoint.collider.gameObject;

                if (clickedObject.TryGetComponent<GameItemView>(out var itemView))
                {
                    _capturedGameItem = itemView;
                    InstantiateItemIconOnUI(
                        _capturedGameItem.GameItem.Icon,
                        new Rect(mousePosition.x, mousePosition.y,
                            FileConstants.ItemImageCellSize*4,
                            FileConstants.ItemImageCellSize*4),
                        ItemIconName, out var resultIcon);

                    _itemIconElement = resultIcon;
                }
            }
        }
    }
}