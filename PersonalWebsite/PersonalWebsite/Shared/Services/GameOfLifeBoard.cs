using PersonalWebsite.Shared.Enums;
using PersonalWebsite.Shared.Interfaces;
using PersonalWebsite.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public void Tick()
        {
            AddStatistic(BoardStatistic.Generation, 1);

            var oldCellStates = Board.Cells
                .Select(c => c.Type)
                .ToArray();

            for (var i = 0; i < oldCellStates.Length; i++)
            {
                var cell = Board.Cells[i];

                var numLiveNeighbours = 0;
                for (var n = 0; n < cell.Neighbours.Length; n++)
                {
                    var neighbour = oldCellStates[cell.Neighbours[n]];
                    if (neighbour == BoardCellType.Goal)
                        numLiveNeighbours++;
                }

                var oldState = oldCellStates[i];
                if (oldState == BoardCellType.Goal)
                    ChangeCellType(cell, LiveNewCellState(numLiveNeighbours));
                else
                    ChangeCellType(cell, DeadCellNewState(numLiveNeighbours));
            }
        }

        private BoardCellType LiveNewCellState(int numLiveNeighbours)
        {
            return numLiveNeighbours switch
            {
                2 => BoardCellType.Goal,
                3 => BoardCellType.Goal,
                _ => BoardCellType.Normal
            };
        }

        private BoardCellType DeadCellNewState(int numLiveNeighbours)
        {
            return numLiveNeighbours switch
            {
                3 => BoardCellType.Goal,
                _ => BoardCellType.Normal
            };
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
