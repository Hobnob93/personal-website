using PersonalWebsite.Shared.Enums;

namespace PersonalWebsite.Shared.Models
{
    public class BoardCell
    {
        public BoardCell[] Neighbours { get; set; }
        public BoardCellType Type { get; set; }
    }
}
