namespace Slate.View.Control
{
    public abstract record KeyMacroViewMarker;

    public record EmptyKeyMacroView : KeyMacroViewMarker;

    public record MediaKeyMacroView : KeyMacroViewMarker;

    public record CommandKeyMacroView : KeyMacroViewMarker;

    public static class KeyMacroViewMarkers
    {
        public static readonly EmptyKeyMacroView Empty = new();
        public static readonly MediaKeyMacroView Media = new();
        public static readonly CommandKeyMacroView Command = new();
    }
}