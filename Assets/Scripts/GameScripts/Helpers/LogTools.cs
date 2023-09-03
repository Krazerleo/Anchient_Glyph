using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace AncientGlyph.GameScripts.Helpers
{
    public class LogTools
    {
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void LogTodo(object obj, string message = null)
        {
            var st = new StackTrace();
            var sf = st.GetFrame(1);

            UnityEngine.Debug.LogWarning($"TODO :: {obj.GetType()} :: {sf.GetMethod().Name} :: {message ?? ""}");
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void LogError(object obj, string message = null)
        {
            var st = new StackTrace();
            var sf = st.GetFrame(1);

            UnityEngine.Debug.LogWarning($"Error Occured :: {obj.GetType()} :: {sf.GetMethod().Name} :: {message ?? ""}");
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void LogWarning(object obj, string message = null)
        {
            var st = new StackTrace();
            var sf = st.GetFrame(1);

            UnityEngine.Debug.LogWarning($"Warning :: {obj.GetType()} :: {sf.GetMethod().Name} :: {message ?? ""}");
        }
    }
}