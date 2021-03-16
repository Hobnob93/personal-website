using PersonalWebsite.Shared.Enums;
using PersonalWebsite.Shared.Interfaces;
using PersonalWebsite.Shared.Models;
using System;

namespace PersonalWebsite.Shared.Services
{
    public class GameOfLifeBoard : IBoardService
    {
        private readonly IBoardFactory boardFactory;

        public Board Board { get; private set; }


        public GameOfLifeBoard(IBoardFactory boardFactory)
        {
            this.boardFactory = boardFactory;
        }


        public void Initialise(int height, int width, bool wrapEdge)
        {
            Board = boardFactory.BuildBoard(BoardType.Automata, height, width, wrapEdge);
            DecorateCells();
        }

        public void OnGridSizeChanged(int height, int width, bool wrapEdge)
        {
            Board = boardFactory.BuildBoard(BoardType.Automata, height, width, wrapEdge);
            DecorateCells();
        }

        public void Reset(bool clear, bool edgeWrap)
        {
            Board = boardFactory.RecycleBoard(BoardType.Automata, Board, clear, edgeWrap);

            if (clear)
                DecorateCells();
        }

        public void OnCellInteracted(int hPos, int wPos)
        {
            var cell = Board.Cells[Board.Width * hPos + wPos];
            cell.Type = BoardCellType.Goal;

            foreach (var c in cell.Neighbours)
                if (c != null)
                    c.Type = BoardCellType.Goal;
        }

        public void Tick()
        {
            Console.WriteLine("TICK");
        }

        private void DecorateCells()
        {
            var cells = Board.Cells;
            var rand = new Random(DateTime.Now.Millisecond);
            var randCount = rand.Next(30, 50);

            for (int i = 0; i < randCount; i++)
            {
                var randomCell = cells[rand.Next(0, cells.Length)];
                randomCell.Type = BoardCellType.Goal;
            }
        }
    }
}
