namespace Slate.Infrastructure.PowerManagement
{
    /**
     * Weisdjafjdsfj why not use a boolean?
     * IDK, looks nicer. More expressive I guess.
     *
     * Memory-efficiency? That's so 1980s.
     */
    public enum PowerMode : byte /* Here, takes just 1 instead of 4 now. (: */
    {
        AC,
        DC
    }
}