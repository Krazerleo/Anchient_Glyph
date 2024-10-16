using System;
using Cysharp.Threading.Tasks;

namespace AncientGlyph.GameScripts.EntityModel.Controller.PlayerControllerComponents
{
    public class InputRegistry
    {
        private readonly AutoResetUniTaskCompletionSource _completionSource = AutoResetUniTaskCompletionSource.Create();
        public event EventHandler EnableInputHandler;
        public event EventHandler DisableInputHandler;

        public UniTask ListenInput()
        {
            EnableInputHandler?.Invoke(this, null);
            return _completionSource.Task;
        }
        
        public bool RegisterInput()
        {
            DisableInputHandler?.Invoke(this, null);
            return _completionSource.TrySetResult();
        }
    }
}