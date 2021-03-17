using PersonalWebsite.Shared.Enums;
using PersonalWebsite.Shared.Interfaces;
using PersonalWebsite.Shared.Models;
using System;
using System.Linq;

namespace PersonalWebsite.Shared.Services
{
    public class BoardFactory : IBoardFactory
    {
        public Board BuildBoard(BoardType type, int height, int width, bool wrapEdge)
        {
            var board = new Board
            {
                Type = type,
                Height = height,
                Width = width
            };

            board.Cells = CreateCells(board, height, width, wrapEdge);

            return board;
        }

        public Board RecycleBoard(BoardType type, Board board, bool clear, bool wrapEdge)
        {
            if (board == null) throw new ArgumentNullException(nameof(board));

            InitialiseCells(board.Cells, board.Height, board.Width, clear, wrapEdge);

            return board;
        }

        private BoardCell[] CreateCells(Board board, int height, int width, bool wrapEdge)
        {
            var cells = Enumerable.Range(1, height * width)
                .Select(i => new BoardCell { Board = board })
                .ToArray();

            InitialiseCells(cells, height, width, false, wrapEdge);

            return cells;
        }

        private void InitialiseCells(BoardCell[] cells, int height, int width, bool clear, bool wrapEdge)
        {
            for (int h = 0; h < height; h++)
            {
                for (int w = 0; w < width; w++)
                {
                    var i = width * h + w;
                    var cell = cells[i];

                    cell.H = h;
                    cell.W = w;
                    cell.Neighbours = new BoardCell[4]
                    {
                        FindNeighbour(NeighbourDirection.North, cells, i, width, wrapEdge),
                        FindNeighbour(NeighbourDirection.East, cells, i, width, wrapEdge),
                        FindNeighbour(NeighbourDirection.South, cells, i, width, wrapEdge),
                        FindNeighbour(NeighbourDirection.West, cells, i, width, wrapEdge),
                    };

                    if (clear)
                        cell.Type = BoardCellType.Normal;
                }
            }
        }

        private BoardCell FindNeighbour(NeighbourDirection dir, BoardCell[] cells, int curIndex, int width, bool wrapEdge)
        {
            var neighbourIndex = dir switch
            {
                NeighbourDirection.North => curIndex - width,
                NeighbourDirection.East => curIndex + 1,
                NeighbourDirection.South => curIndex + width,
                NeighbourDirection.West => curIndex - 1,
                _ => -1
            };

            var isFarLeft = curIndex % width == 0;
            var isFarRight = curIndex % width == (width - 1);

            if (wrapEdge)
            {
                if (dir == NeighbourDirection.North && neighbourIndex < 0)
                    neighbourIndex += cells.Length;

                if (dir == NeighbourDirection.South && neighbourIndex >= cells.Length)
                    neighbourIndex -= cells.Length;

                if (dir == NeighbourDirection.West && isFarLeft)
                    neighbourIndex += width;           // cell is on left side, neighbour needs to be same row but on far right

                if (wrapEdge && dir == NeighbourDirection.East && isFarRight)
                    neighbourIndex -= width;           // cell is on right side, neighbour needs to be same row but on far left

                if (neighbourIndex < 0 || neighbourIndex >= cells.Length)
                    return null;                        // neighbour index is out of range
            }
            else
            {
                if (neighbourIndex < 0 || neighbourIndex >= cells.Length)
                    return null;                        // neighbour index is out of range

                if (dir == NeighbourDirection.West && isFarLeft)
                    return null;                        // cell is on left side, but neighbour index is on previous row on far right side

                if (dir == NeighbourDirection.East && isFarRight)
                    return null;                        // cell is on right side, but neighbour index is on next row on far left side
            }

            return cells[neighbourIndex];
        }
    }
}
