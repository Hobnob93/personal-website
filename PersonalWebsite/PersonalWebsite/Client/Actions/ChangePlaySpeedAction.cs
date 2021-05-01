using PersonalWebsite.Shared.Enums;

namespace PersonalWebsite.Client.Actions
{
    public record ChangePlaySpeedAction
    {
        public PlaySpeed NewPlaySpeed { get; init; }
    }
}