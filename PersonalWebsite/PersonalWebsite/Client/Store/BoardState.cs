using System.Collections.Generic;
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
        public BoardCellType CurrentPen { get; init; }
        public List<JsBoardCall> JsCalls { get; init; }
    }
}