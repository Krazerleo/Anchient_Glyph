using System.Collections.Generic;

using AncientGlyph.GameScripts.Services.Interfaces;

using UnityEngine;

namespace AncientGlyph.GameScripts.Core.Services
{
    public class ComponentLocatorService : IComponentLocatorService
    {
        private readonly Dictionary<System.Type, MonoBehaviour> ComponentCache = new Dictionary<System.Type, MonoBehaviour>();

        #region Public Methods
        public TComponent FindComponent<TComponent>() where TComponent : MonoBehaviour
        {
            var type = typeof(TComponent);
            var contatinCache = ComponentCache.ContainsKey(type);
            if (contatinCache)
            {
                return (TComponent) ComponentCache[type];
            }
            else
            {
                var component = Object.FindFirstObjectByType<TComponent>();
                if (component != null)
                {
                    ComponentCache[type] = component;
                }
                return component;
            }
        }
        #endregion
    }
}