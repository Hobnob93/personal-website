using PersonalWebsite.Shared.Enums;
using PersonalWebsite.Shared.Models;

namespace PersonalWebsite.Shared.Interfaces
{
    public interface IBoardService
    {
        Board Initialise(GridSize size, bool wrapEdge);
        Board Tick(Board board);
        Board Reset(Board board, bool clear, bool wrapEdge);
        Board CellInteracted(Board board, int hPos, int wPos, BoardCellType type);
        int GetStatistic(BoardStatistic stat);
    }
}
