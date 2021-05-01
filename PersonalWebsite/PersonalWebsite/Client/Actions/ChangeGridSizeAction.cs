using PersonalWebsite.Shared.Enums;

namespace PersonalWebsite.Client.Actions
{
    public record ChangeGridSizeAction
    {
        public GridSize NewGridSize { get; init; }
    }
}