namespace Slate.Model
{
    public struct KeyBind
    {
        public KeyBindMode Mode { get; set; } = KeyBindMode.Default;
        public MediaKey MediaKey { get; set; } = MediaKey.PlayPause;
        
        public string? Command { get; set; }

        public KeyBind()
        {
        }

        public KeyBind(KeyBindMode mode)
        {
            Mode = mode;
        }
    }
}