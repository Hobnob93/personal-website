using Microsoft.AspNetCore.Components;
using PersonalWebsite.Shared.Enums;
using PersonalWebsite.Shared.Interfaces;

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


        protected override void OnInitialized()
        {
            InitialiseBoard();
        }

        protected void InitialiseBoard()
        {
            var (height, width) = Size switch
            {
                GridSize.ExtraExtraSmall => (11, 40),
                GridSize.ExtraSmall => (15, 50),
                GridSize.Small => (20, 60),
                GridSize.Medium => (25, 70),
                GridSize.Large => (30, 80),
                GridSize.ExtraLarge => (35, 90),
                GridSize.ExtraExtraLarge => (40, 100)
            };

            BoardService.Initialise(height, width, EdgeWrap);
        }

        protected void OnGridSizeChanged(GridSize size)
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

        public void CellInteracted(int hPos, int wPos)
        {
            BoardService.OnCellInteracted(hPos, wPos);
        }

        public void Tick()
        {
            BoardService.Tick();
        }
    }
}
