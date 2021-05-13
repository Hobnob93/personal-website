using PersonalWebsite.Shared.Enums;
using PersonalWebsite.Shared.Models;

namespace PersonalWebsite.Shared.Interfaces
{
    public interface IBoardService
    {
        Board Initialise(int height, int width, bool wrapEdge);
        Board Tick(Board board);
        Board Reset(Board board, bool clear, bool wrapEdge);
        Board CellInteracted(Board board, int hPos, int wPos);
        int GetStatistic(BoardStatistic stat);
    }
}
