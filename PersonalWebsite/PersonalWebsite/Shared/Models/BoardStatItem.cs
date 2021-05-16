using PersonalWebsite.Shared.Enums;

namespace PersonalWebsite.Shared.Models
{
    public record BoardStatItem
    {
        public string Name { get; init; }
        public BoardStatistic Stat { get; init; }
    }
}