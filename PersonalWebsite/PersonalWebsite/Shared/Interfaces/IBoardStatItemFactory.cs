using System.Collections.Generic;
using PersonalWebsite.Shared.Enums;
using PersonalWebsite.Shared.Models;

namespace PersonalWebsite.Shared.Interfaces
{
    public interface IBoardStatItemFactory
    {
        IEnumerable<BoardStatItem> GetStatItemsForBoardType(BoardType type);
    }
}