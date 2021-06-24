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
        [Inject] public IState<BoardState> BoardState { get; set; }
        [Inject] public IDispatcher Dispatcher { get; set; }

        [Parameter] public BoardType BoardType { get; set; }
        
        public IJSObjectReference BoardModule { get; set; }

        public bool Initializing => BoardState.Value.Initializing;
        public Board Board => BoardState.Value.Board;
        public GridSize Size => BoardState.Value.GridSize;
        public bool EdgeWrap => BoardState.Value.DoEdgeWrap;


        protected override async Task OnInitializedAsync()
        {
            BoardState.StateChanged += BoardStateChanged;
            
            BoardModule = await JsRuntime.InvokeAsync<IJSObjectReference>("import", "./scripts/board.js");
            
            Dispatcher.Dispatch(new InitializeBoardAction());
        }

        private async void BoardStateChanged(object obj, BoardState state)
        {
            try
            {
                if (BoardModule != null)
                {
                    foreach (var jsCall in state.JsCalls)
                    {
                        await BoardModule.InvokeVoidAsync(jsCall.Function, jsCall.Value);
                    }

                    state.JsCalls.Clear();
                }

                //StateHasChanged();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
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
