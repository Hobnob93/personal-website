using PersonalWebsite.Shared.Enums;
using PersonalWebsite.Shared.Extensions;
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
                .Select(i => new BoardCell())
                .ToArray();

            InitialiseCells(cells, height, width, false, wrapEdge);

            return cells;
        }

        private void InitialiseCells(BoardCell[] cells, int height, int width, bool clear, bool wrapEdge)
        {
            for (var h = 0; h < height; h++)
            {
                for (var w = 0; w < width; w++)
                {
                    var i = width * h + w;
                    var cell = cells[i];

                    cell.H = h;
                    cell.W = w;
                    cell.Neighbors = Array.Empty<int>();

                    if (clear)
                        cell.Type = BoardCellType.Normal;
                }
            }
        }
    }
}
