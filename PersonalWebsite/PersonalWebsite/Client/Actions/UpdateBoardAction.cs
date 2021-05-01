using PersonalWebsite.Shared.Models;

namespace PersonalWebsite.Client.Actions
{
    public record UpdateBoardAction
    {
        public Board NewBoard { get; init; }
    }
}