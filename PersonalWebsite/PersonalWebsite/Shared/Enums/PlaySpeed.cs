using System.ComponentModel;

namespace PersonalWebsite.Shared.Enums
{
    public enum PlaySpeed
    {
        [Description("Very Slow")]
        VerySlow,
        [Description("Slow")]
        Slow,
        [Description("Normal")]
        Normal,
        [Description("Fast")]
        Fast,
        [Description("Very Fast")]
        VeryFast
    }
}
