using System;
using Microsoft.AspNetCore.Components;
using PersonalWebsite.Shared.Enums;
using PersonalWebsite.Shared.Interfaces;
using System.Threading.Tasks;
using Fluxor;
using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components.Web;
using PersonalWebsite.Client.Actions;
using PersonalWebsite.Client.Store;

namespace PersonalWebsite.Client.Shared
{
    public partial class BoardWidget
    {
        [Inject] public IBoardService BoardService { get; set; }
        [Inject] public IState<BoardState> BoardState { get; set; }
        [Inject] public IDispatcher Dispatcher { get; set; }
        

        [Parameter] public BoardType BoardType { get; set; }

        public GridSize Size => BoardState.Value.GridSize;
        public bool EdgeWrap => BoardState.Value.DoEdgeWrap;

        
        protected override void OnInitialized()
        {
            BoardState.StateChanged += BoardStateChanged;
            Dispatcher.Dispatch(new InitializeBoardAction());
        }

        private void BoardStateChanged(object? obj, BoardState state)
        {
            StateHasChanged();
        }

        private async Task CellInteracted(int hPos, int wPos)
        {
            await Task.Run(() =>
            {
                if (BoardState.Value.IsAutoPlaying)
                    return;
            
                Dispatcher.Dispatch(new CellClickedAction
                {
                    H = hPos,
                    W = wPos
                });
            });
        }

        public new void Dispose()
        {
            base.Dispose();
            BoardState.StateChanged -= BoardStateChanged;
        }
    }
}
