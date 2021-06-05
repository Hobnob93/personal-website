using PersonalWebsite.Shared.Enums;

namespace PersonalWebsite.Shared.Models
{
    public class BoardCell
    {
        public int H { get; set; }      // The cell's position on the height
        public int W { get; set; }      // The cell's position on the width
        public int[] Neighbours { get; set; }   // An array of indices for the neighbouring cells
        public BoardCellType Type { get; set; }
    }
}
