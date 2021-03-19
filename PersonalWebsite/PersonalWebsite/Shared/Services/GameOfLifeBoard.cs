using PersonalWebsite.Shared.Enums;
using PersonalWebsite.Shared.Interfaces;
using PersonalWebsite.Shared.Models;
using System.Collections.Generic;

namespace PersonalWebsite.Shared.Services
{
    public class GameOfLifeBoard : IBoardService
    {
        private readonly IBoardFactory boardFactory;
        private Dictionary<BoardStatistic, int> statistics;

        public Board Board { get; private set; }


        public GameOfLifeBoard(IBoardFactory boardFactory)
        {
            this.boardFactory = boardFactory;

            statistics = new Dictionary<BoardStatistic, int>();
        }


        public void Initialise(int height, int width, bool wrapEdge)
        {
            Board = boardFactory.BuildBoard(BoardType.Automata, height, width, wrapEdge);
            statistics.Clear();
            DecorateCells();
        }

        public void OnGridSizeChanged(int height, int width, bool wrapEdge)
        {
            Board = boardFactory.BuildBoard(BoardType.Automata, height, width, wrapEdge);
            statistics.Clear();
            DecorateCells();
        }

        public void Reset(bool clear, bool edgeWrap)
        {
            Board = boardFactory.RecycleBoard(BoardType.Automata, Board, clear, edgeWrap);

            if (clear)
            {
                statistics.Clear();
                DecorateCells();
            }
        }

        public void OnCellInteracted(int hPos, int wPos)
        {
            var cell = Board.Cells[Board.Width * hPos + wPos];
            ChangeCellType(cell, BoardCellType.Goal);
        }


        /*
          The universe of the Game of Life is an infinite, two-dimensional orthogonal grid of square cells, each of which is in one of two possible states, 
          live or dead, (or populated and unpopulated, respectively). Every cell interacts with its eight neighbours, which are the cells that are horizontally,
          vertically, or diagonally adjacent. At each step in time, the following transitions occur:

            Any live cell with fewer than two live neighbours dies, as if by underpopulation.
            Any live cell with two or three live neighbours lives on to the next generation.
            Any live cell with more than three live neighbours dies, as if by overpopulation.
            Any dead cell with exactly three live neighbours becomes a live cell, as if by reproduction.

          These rules, which compare the behavior of the automaton to real life, can be condensed into the following:

            Any live cell with two or three live neighbours survives.
            Any dead cell with three live neighbours becomes a live cell.
            All other live cells die in the next generation. Similarly, all other dead cells stay dead.
        */
        public void Tick()
        {
            AddStatistic(BoardStatistic.Generation, 1);

            var newCellStats = Board.Cells.Clone();

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

        private void DecorateCells()
        {
            //var cells = Board.Cells;
            //var rand = new Random(DateTime.Now.Millisecond);
            //var randCount = rand.Next(30, 50);
            //
            //for (int i = 0; i < randCount; i++)
            //{
            //    var randomCell = cells[rand.Next(0, cells.Length)];
            //    ChangeCellType(randomCell, BoardCellType.Goal);
            //}
        }

        private void ChangeCellType(BoardCell cell, BoardCellType toType)
        {
            var curType = cell.Type;
            if (curType == BoardCellType.Normal && toType == BoardCellType.Goal)
            {
                AddStatistic(BoardStatistic.Living, 1);
                AddStatistic(BoardStatistic.TotalLived, 1);
            }
            else if (curType == BoardCellType.Goal && toType == BoardCellType.Normal)
            {
                AddStatistic(BoardStatistic.Living, -1);
                AddStatistic(BoardStatistic.TotalDied, 1);
            }

            cell.Type = toType;
        }
    }
}
