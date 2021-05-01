using PersonalWebsite.Shared.Enums;
using PersonalWebsite.Shared.Models;

namespace PersonalWebsite.Client.Store
{
    public record BoardState
    {
        public Board Board { get; init; }
        public GridSize GridSize { get; init; }
        public bool DoEdgeWrap { get; init; }
        public bool IsAutoPlaying { get; init; }
        public PlaySpeed PlaySpeed { get; init; }

        public (int height, int width) GetBoardDimensions()
        {
            return GridSize switch
            {
                GridSize.ExtraExtraSmall => (12, 40),
                GridSize.ExtraSmall => (15, 45),
                GridSize.Small => (18, 50),
                GridSize.Medium => (21, 55),
                GridSize.Large => (24, 60),
                GridSize.ExtraLarge => (27, 65),
                GridSize.ExtraExtraLarge => (30, 70),
                _ => (21, 55)
            };
        }
    }
}