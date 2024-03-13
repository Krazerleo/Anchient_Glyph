namespace AncientGlyph.GameScripts.Services.LoggingService
{
    public interface ILoggingService
    {
        void LogError(string errorMessage);
        void LogWarning(string warningMessage);
        void Log(string message);
    }
}