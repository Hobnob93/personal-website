using Microsoft.AspNetCore.Components;
using PersonalWebsite.Shared.Enums;
using PersonalWebsite.Shared.Interfaces;
using PersonalWebsite.Shared.Models;
using System;

namespace PersonalWebsite.Client.Shared
{
    public partial class BoardWidget
    {
        private GridSize size;

        [Inject]
        public IBoardFactory BoardFactory { get; set; }

        [Parameter]
        public BoardType Type { get; set; }
        [Parameter]
        public GridSize Size { get => size; set => OnGridSizeChanged(value); }
        
        public int Height { get; set; }
        public int Width { get; set; }
        public Board Board { get; set; }


        protected override void OnInitialized()
        {
            InitialiseBoard();
        }

        protected void InitialiseBoard()
        {
            (Height, Width) = Size switch
            {
                GridSize.ExtraExtraSmall => (11, 40),
                GridSize.ExtraSmall => (15, 50),
                GridSize.Small => (20, 60),
                GridSize.Medium => (25, 70),
                GridSize.Large => (30, 80),
                GridSize.ExtraLarge => (35, 90),
                GridSize.ExtraExtraLarge => (40, 100)
            };

            Board = BoardFactory.BuildBoard(Type, Height, Width);
        }

        protected void OnGridSizeChanged(GridSize size)
        {
            if (this.size == size)
                return;

            this.size = size;
            InitialiseBoard();
        }

        public void ClearBoard()
        {
            Board = BoardFactory.RecycleBoard(Type, Board);
        }

        public void CellClicked(int hPos, int wPos)
        {
            var cell = Board.Cells[Width * hPos + wPos];
            cell.Type = BoardCellType.Full;

            foreach (var c in cell.Neighbours)
                if (c != null)
                    c.Type = BoardCellType.Full;
        }
    }
}
