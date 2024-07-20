using UnityEngine;
using UnityEngine.UIElements;

namespace AncientGlyph.GameScripts.UserInterface
{
    public static class UiVisualElementExtensions
    {
        /// <summary>
        /// Create icon with absolute position relative to anchorElement
        /// </summary>
        /// <param name="anchorElement">Element to attach icon</param>
        /// <param name="sprite">Sprite of icon</param>
        /// <param name="placementRect">Relative position to anchorElement and size of icon</param>
        /// <param name="iconName">Name of icon</param>
        /// <returns></returns>
        public static VisualElement AttachIcon(this VisualElement anchorElement, 
            Sprite sprite, Rect placementRect, string iconName = "")
        {
            VisualElement iconElement = new()
            {
                name = iconName,
                style =
                {
                    height = placementRect.height,
                    width = placementRect.width,
                    top = placementRect.y - anchorElement.worldBound.y,
                    left = placementRect.x - anchorElement.worldBound.x,
                    backgroundImage = new StyleBackground(sprite),
                    position = new StyleEnum<Position>(Position.Absolute),
                },
                pickingMode = PickingMode.Ignore,
            };

            anchorElement.Add(iconElement);
            return iconElement;
        }
        
        /// <summary>
        /// Remove visual element from UI
        /// </summary>
        /// <param name="visualElement"></param>
        public static void Dispose(this VisualElement visualElement)
        {
            if (visualElement == null)
            {
                return;
            }

            VisualElement elementParent = visualElement.parent;
            elementParent?.Remove(visualElement);
        }

        public static Vector2 ToScreenSpace(this Vector2 panelSpacePosition)
        {
            Vector2 screenSpacePosition = panelSpacePosition;
            // Camera and panel space has inverted Y coordinate
            screenSpacePosition.y = Screen.height - screenSpacePosition.y;
            return screenSpacePosition;
        }
    }
}