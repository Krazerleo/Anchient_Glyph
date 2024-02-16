using UnityEngine;

namespace AncientGlyph.GameScripts.Services.ComponentLocatorService
{
    public interface IComponentLocatorService
    {
        public TComponent FindComponent<TComponent>() where TComponent : MonoBehaviour;

        public bool IsComponentExist<TComponent>(TComponent component) where TComponent: MonoBehaviour;
    }
}