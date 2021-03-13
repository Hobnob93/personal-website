using PersonalWebsite.Shared.Enums;

namespace PersonalWebsite.Shared.Models
{
    public class BoardCell
    {
        public Board Board { get; set; }
        public int H { get; set; }      // The cell's position on the height
        public int W { get; set; }      // The cell's position on the width
        public BoardCell[] Neighbours { get; set; }
        public BoardCellType Type { get; set; }
    }
}
