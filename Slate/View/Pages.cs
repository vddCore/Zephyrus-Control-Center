namespace Slate.View
{
    public abstract record PageMarker;
    
    public sealed record MainMenuPageMarker : PageMarker;
    public sealed record FansPageMarker : PageMarker;
    public sealed record GraphicsAndDisplayPageMarker : PageMarker;
    public sealed record PowerManagementPageMarker : PageMarker;
    public sealed record KeyboardPageMarker : PageMarker;
    public sealed record AniMeMatrixPageMarker : PageMarker;
    public sealed record ApplicationPageMarker : PageMarker;

    public static class Pages
    {
        public static MainMenuPageMarker MainMenu = new();
        public static FansPageMarker Fans = new();
        public static GraphicsAndDisplayPageMarker GraphicsAndDisplay = new();
        public static PowerManagementPageMarker PowerManagement = new();
        public static KeyboardPageMarker Keyboard = new();
        public static AniMeMatrixPageMarker AniMeMatrix = new();
        public static ApplicationPageMarker Application = new();
    }
}