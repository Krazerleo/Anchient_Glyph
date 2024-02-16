namespace AncientGlyph.GameScripts.Services.LoggingService
{
    public interface ILoggingService
    {
        void LogError(string errorMessage);
        void Log(string message);
    }
}