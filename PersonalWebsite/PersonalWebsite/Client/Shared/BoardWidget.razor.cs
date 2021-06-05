using System;
using Microsoft.AspNetCore.Components;
using PersonalWebsite.Shared.Enums;
using PersonalWebsite.Shared.Interfaces;
using System.Threading.Tasks;
using Fluxor;
using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using PersonalWebsite.Client.Actions;
using PersonalWebsite.Client.Store;
using PersonalWebsite.Shared.Extensions;
using PersonalWebsite.Shared.Models;

namespace PersonalWebsite.Client.Shared
{
    public partial class BoardWidget
    {
        [Inject] public IJSRuntime JsRuntime { get; set; }
        [Inject] public IBoardService BoardService { get; set; }
        [Inject] public IState<BoardState> BoardState { get; set; }
        [Inject] public IDispatcher Dispatcher { get; set; }

        [Parameter] public BoardType BoardType { get; set; }
        
        public IJSObjectReference BoardModule { get; set; }
        public Board Board { get; set; }
        public GridSize Size => BoardState.Value.GridSize;
        public bool EdgeWrap => BoardState.Value.DoEdgeWrap;


        protected override async Task OnInitializedAsync()
        {
            BoardModule = await JsRuntime.InvokeAsync<IJSObjectReference>("import", "./scripts/board.js");
            
            Board = BoardService.Initialise(Size, EdgeWrap);
            await BoardModule.InvokeVoidAsync("boardData.setBoard", Board);
        }

        private void BoardStateChanged(object obj, BoardState state)
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
        }
    }
}
