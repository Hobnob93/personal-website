using PersonalWebsite.Client.Shared;
using PersonalWebsite.Shared.Enums;

namespace PersonalWebsite.Client.Pages
{
    public partial class GameOfLife
    {
        public BoardType BoardType { get; } = BoardType.Automata;
        public BoardWidget Board { get; set; }


        public void ClearBoard()
        {
            Board.ClearBoard();
        }
    }
}
