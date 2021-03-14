using PersonalWebsite.Shared.Enums;
using PersonalWebsite.Shared.Interfaces;
using PersonalWebsite.Shared.Models;
using System;
using System.Linq;

namespace PersonalWebsite.Shared.Services
{
    public class BoardFactory : IBoardFactory
    {
        public Board BuildBoard(BoardType type, int height, int width)
        {
            var board = new Board
            {
                Type = type,
                Height = height,
                Width = width
            };

            board.Cells = DecorateCells(type, CreateCells(board, height, width));

            return board;
        }

        public Board RecycleBoard(BoardType type, Board board)
        {
            if (board == null) throw new ArgumentNullException(nameof(board));

            InitialiseCells(board.Cells, board.Height, board.Width);
            board.Cells = DecorateCells(type, board.Cells);

            return board;
        }

        private BoardCell[] CreateCells(Board board, int height, int width)
        {
            var cells = Enumerable.Range(1, height * width)
                .Select(i => new BoardCell { Board = board })
                .ToArray();

            InitialiseCells(cells, height, width);

            return cells;
        }

        private void InitialiseCells(BoardCell[] cells, int height, int width)
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
                        FindNeighbour(NeighbourDirection.North, cells, i, width),
                        FindNeighbour(NeighbourDirection.East, cells, i, width),
                        FindNeighbour(NeighbourDirection.South, cells, i, width),
                        FindNeighbour(NeighbourDirection.West, cells, i, width),
                    };

                    cell.Type = BoardCellType.Empty;
                }
            }
        }

        private BoardCell FindNeighbour(NeighbourDirection dir, BoardCell[] cells, int curIndex, int width)
        {
            var neighbourIndex = dir switch
            {
                NeighbourDirection.North => curIndex - width,
                NeighbourDirection.East => curIndex + 1,
                NeighbourDirection.South => curIndex + width,
                NeighbourDirection.West => curIndex - 1
            };

            if (neighbourIndex < 0 || neighbourIndex >= cells.Length)
                return null;        // neighbour index is out of range

            if (dir == NeighbourDirection.West && curIndex % width == 0)
                return null;        // cell is on left side, but neighbour index is on previous row on far right side

            if (dir == NeighbourDirection.East && curIndex % width == (width - 1))
                return null;        // cell is on right side, but neighbour index is on next row on far left side

            return cells[neighbourIndex];
        }

        private BoardCell[] DecorateCells(BoardType type, BoardCell[] cells)
        {
            var rand = new Random(DateTime.Now.Millisecond);
            var randCount = rand.Next(30, 50);
            for (int i = 0; i < randCount; i++)
            {
                var randomCell = cells[rand.Next(0, cells.Length)];
                randomCell.Type = BoardCellType.Full;
            }

            return cells;
        }
    }
}
