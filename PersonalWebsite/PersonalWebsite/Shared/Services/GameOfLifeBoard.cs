using PersonalWebsite.Shared.Enums;
using PersonalWebsite.Shared.Interfaces;
using PersonalWebsite.Shared.Models;
using System.Collections.Generic;
using PersonalWebsite.Shared.Extensions;

namespace PersonalWebsite.Shared.Services
{
    public class GameOfLifeBoard : IBoardService
    {
        private readonly IBoardFactory boardFactory;
        private Dictionary<BoardStatistic, int> statistics;

        public GameOfLifeBoard(IBoardFactory boardFactory)
        {
            this.boardFactory = boardFactory;

            statistics = new Dictionary<BoardStatistic, int>();
        }


        public Board Initialise(GridSize size, bool wrapEdge)
        {
            var (height, width) = size.GetDimensions();
            
            var board = boardFactory.BuildBoard(BoardType.Automata, height, width, wrapEdge);
            statistics.Clear();

            return board;
        }

        public Board Reset(Board board, bool clear, bool edgeWrap)
        {
            board = boardFactory.RecycleBoard(BoardType.Automata, board, clear, edgeWrap);

            if (!clear) 
                return board;
            
            statistics.Clear();

            return board;
        }

        public int GetStatistic(BoardStatistic stat)
        {
            return statistics.GetValueOrDefault(stat, 0);
        }

        private void AddStatistic(BoardStatistic stat, int val)
        {
            var curVal = statistics.GetValueOrDefault(stat, 0);
            statistics[stat] = curVal + val;
        }

        private void SetStatistic(BoardStatistic stat, int val)
        {
            statistics[stat] = val;
        }
    }
}
