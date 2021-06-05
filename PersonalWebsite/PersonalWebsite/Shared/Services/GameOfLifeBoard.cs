using PersonalWebsite.Shared.Enums;
using PersonalWebsite.Shared.Interfaces;
using PersonalWebsite.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            DecorateCells(board);

            return board;
        }

        public Board Reset(Board board, bool clear, bool edgeWrap)
        {
            board = boardFactory.RecycleBoard(BoardType.Automata, board, clear, edgeWrap);

            if (clear)
            {
                statistics.Clear();
                DecorateCells(board);
            }

            return board;
        }

        public Board CellInteracted(Board board, int hPos, int wPos, BoardCellType type)
        {
            var cell = board.Cells[board.Width * hPos + wPos];
            ChangeCellType(cell, type);

            return board;
        }

        public Board Tick(Board board)
        {
            AddStatistic(BoardStatistic.Generation, 1);

            var oldCellStates = board.Cells
                .Select(c => c.Type)
                .ToArray();

            for (var i = 0; i < oldCellStates.Length; i++)
            {
                var cell = board.Cells[i];

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

            return board;
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

        private void DecorateCells(Board board)
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
