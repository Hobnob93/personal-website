using System.Threading.Tasks;
using PersonalWebsite.Shared.Enums;
using PersonalWebsite.Shared.Models;

namespace PersonalWebsite.Shared.Interfaces
{
    public interface IBoardService
    {
        Board Board { get; }

        void Initialise(int height, int width, bool wrapEdge);
        void OnGridSizeChanged(int height, int width, bool wrapEdge);
        void Tick();
        void Reset(bool clear, bool wrapEdge);
        void OnCellInteracted(int hPos, int wPos);
        int GetStatistic(BoardStatistic stat);
    }
}
