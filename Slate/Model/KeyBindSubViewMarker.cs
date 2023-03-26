namespace Slate.View.Control
{
    public abstract record KeybindSubViewMarker;

    public record EmptyKeySubView : KeybindSubViewMarker;

    public record MediaKeySubView : KeybindSubViewMarker;

    public record CommandKeySubView : KeybindSubViewMarker;

    public static class KeybindSubViewMarkers
    {
        public static readonly EmptyKeySubView Empty = new();
        public static readonly MediaKeySubView Media = new();
        public static readonly CommandKeySubView Command = new();
    }
}