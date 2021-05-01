using PersonalWebsite.Client.Shared;
using PersonalWebsite.Shared.Enums;

namespace PersonalWebsite.Client.Pages
{
    public partial class GameOfLife
    {
        public BoardType BoardType => BoardType.Automata;
        public BoardWidget Board { get; set; }
        public BoardToolbar BoardToolbar { get; set; }

        public int Generation => Board.BoardService.GetStatistic(BoardStatistic.Generation);
        public int Alive => Board.BoardService.GetStatistic(BoardStatistic.Living);
        public int Dead => Board.BoardService.GetStatistic(BoardStatistic.TotalDied);
        public int Lived => Board.BoardService.GetStatistic(BoardStatistic.TotalLived);
    }
}
