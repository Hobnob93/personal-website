using PersonalWebsite.Shared.Enums;
using PersonalWebsite.Shared.Models;

namespace PersonalWebsite.Shared.Interfaces
{
    public interface IBoardFactory
    {
        Board BuildBoard(BoardType type, int height, int width, bool wrapEdge);
        Board RecycleBoard(BoardType type, Board board, bool clear, bool wrapEdge);
    }
}
