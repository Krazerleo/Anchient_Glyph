using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;

namespace AncientGlyph.GameScripts.Services.LoggingService
{
    [UsedImplicitly]
    public class DebugLoggingService : ILoggingService
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void LogFatal(string errorMessage)
        {
            Debug.LogError($"<color=red>Fatal Error: {errorMessage}</color>");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void LogError(string errorMessage)
        {
            Debug.LogError($"<color=red>Error: {errorMessage}</color>");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void LogWarning(string warningMessage)
        {
            Debug.LogWarning($"<color=orange>Warning: {warningMessage}</color>");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void LogDebug(string debugMessage)
        {
            Debug.Log($"<color=blue>Debug Info: {debugMessage}</color>");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void LogTrace(string message)
        {
            Debug.Log($"<color=gray>Trace Info: {message}</color>");
        }
    }
}