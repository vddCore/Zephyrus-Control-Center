namespace Slate.View
{
    public abstract record PageMarker(double ViewHeight);

    public sealed record MainMenuPageMarker() : PageMarker(462.0);
    public sealed record FansPageMarker() : PageMarker(571.0);
    public sealed record GraphicsAndDisplayPageMarker() : PageMarker(180.0);
    public sealed record PowerManagementPageMarker() : PageMarker(571.0);
    public sealed record KeyboardPageMarker() : PageMarker(571.0);
    public sealed record AniMeMatrixPageMarker() : PageMarker(571.0);
    public sealed record ApplicationPageMarker() : PageMarker(571.0);

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