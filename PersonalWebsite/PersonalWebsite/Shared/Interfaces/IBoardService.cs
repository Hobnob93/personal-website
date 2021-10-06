using PersonalWebsite.Shared.Enums;
using PersonalWebsite.Shared.Models;

namespace PersonalWebsite.Shared.Interfaces
{
    public interface IBoardService
    {
        Board Initialise(GridSize size, bool wrapEdge);
        Board Reset(Board board, bool clear, bool wrapEdge);
        int GetStatistic(BoardStatistic stat);
    }
}
