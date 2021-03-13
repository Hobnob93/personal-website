using PersonalWebsite.Shared.Enums;

namespace PersonalWebsite.Shared.Models
{
    public class Board
    {
        public BoardType Type { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public BoardCell[] Cells { get; set; }
    }
}
