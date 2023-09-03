using UnityEngine;

namespace AncientGlyph.GameScripts.Services.Interfaces
{
    public interface IComponentLocatorService
    {
        public TComponent FindComponent<TComponent>() where TComponent : MonoBehaviour;
    }
}