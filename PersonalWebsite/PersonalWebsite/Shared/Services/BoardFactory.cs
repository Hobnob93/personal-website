using PersonalWebsite.Shared.Enums;
using PersonalWebsite.Shared.Interfaces;
using PersonalWebsite.Shared.Models;
using System.Linq;

namespace PersonalWebsite.Shared.Services
{
    public class BoardFactory : IBoardFactory
    {
        public Board BuildBoard(BoardType type, int height, int width)
        {
            return new Board
            {
                Type = type,
                Height = height,
                Width = width,
                Cells = DecorateCells(type, CreateCells(height, width))
            };
        }

        private BoardCell[] CreateCells(int height, int width)
        {
            var cells = Enumerable.Repeat(new BoardCell(), height * width)
                .ToArray();

            DetermineCellNeighbours(cells, height, width);

            return cells;
        }

        private void DetermineCellNeighbours(BoardCell[] cells, int height, int width)
        {
            for (int h = 0; h < height; h++)
            {
                for (int w = 0; w < width; w++)
                {

                }
            }
        }

        private BoardCell[] DecorateCells(BoardType type, BoardCell[] cells)
        {
            return cells;
        }

    }
}
