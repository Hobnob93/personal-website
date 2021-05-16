using System;
using System.Collections.Generic;
using PersonalWebsite.Shared.Enums;
using PersonalWebsite.Shared.Interfaces;
using PersonalWebsite.Shared.Models;

namespace PersonalWebsite.Shared.Services
{
    public class BoardStatItemFactory : IBoardStatItemFactory
    {
        public IEnumerable<BoardStatItem> GetStatItemsForBoardType(BoardType type)
        {
            return type switch
            {
                BoardType.Automata => GetAutomataStatItems(),
                _ => Array.Empty<BoardStatItem>()
            };
        }

        private IEnumerable<BoardStatItem> GetAutomataStatItems()
        {
            return new[]
            {
                new BoardStatItem { Name = "Generation", Stat = BoardStatistic.Generation },
                new BoardStatItem { Name = "Currently Alive", Stat = BoardStatistic.Living },
                new BoardStatItem { Name = "Total Lived", Stat = BoardStatistic.TotalLived },
                new BoardStatItem { Name = "Total Dead", Stat = BoardStatistic.TotalDied }
            };
        }
    }
}