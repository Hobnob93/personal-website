using System;
using Microsoft.AspNetCore.Components;
using PersonalWebsite.Shared.Enums;
using PersonalWebsite.Shared.Interfaces;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Web;

namespace PersonalWebsite.Client.Shared
{
    public partial class BoardWidget
    {
        private GridSize size;

        [Inject]
        public IBoardService BoardService { get; set; }

        [Parameter]
        public BoardType Type { get; set; }
        [Parameter]
        public GridSize Size { get => size; set => OnGridSizeChanged(value); }
        [Parameter]
        public bool EdgeWrap { get; set; }
        [Parameter]
        public EventCallback OnCellInteracted { get; set; }


        protected override void OnInitialized()
        {
            InitialiseBoard();
        }

        private void InitialiseBoard()
        {
            var (height, width) = Size switch
            {
                GridSize.ExtraExtraSmall => (12, 40),
                GridSize.ExtraSmall => (15, 45),
                GridSize.Small => (18, 50),
                GridSize.Medium => (21, 55),
                GridSize.Large => (24, 60),
                GridSize.ExtraLarge => (27, 65),
                GridSize.ExtraExtraLarge => (30, 70),
                _ => (21, 55)
            };

            BoardService.Initialise(height, width, EdgeWrap);
        }

        private void OnGridSizeChanged(GridSize size)
        {
            if (this.size == size)
                return;

            this.size = size;
            InitialiseBoard();
        }

        public void ClearBoard(bool wrapEdge)
        {
            BoardService.Reset(true, wrapEdge);
        }

        public void OnWrapEdgeChanged(bool wrapEdge)
        {
            BoardService.Reset(false, wrapEdge);
        }

        private void CellInteracted(int hPos, int wPos)
        {
            BoardService.OnCellInteracted(hPos, wPos);
        }

        public void Tick()
        {
            BoardService.Tick();
        }
    }
}
