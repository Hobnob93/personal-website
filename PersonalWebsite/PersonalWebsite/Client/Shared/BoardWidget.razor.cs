using System;
using Microsoft.AspNetCore.Components;
using PersonalWebsite.Shared.Enums;
using PersonalWebsite.Shared.Interfaces;
using System.Threading.Tasks;
using Fluxor;
using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using MudBlazor.Extensions;
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
        
        private IJSObjectReference BoardModule { get; set; }
        private Board Board => BoardState.Value.Board;


        protected override async Task OnInitializedAsync()
        {
            BoardState.StateChanged += BoardStateChanged;
            Dispatcher.Dispatch(new InitializeBoardAction());
        }

        private async void BoardStateChanged(object obj, BoardState state)
        {
            try
            {
                await CheckBoardModule();
                
                foreach (var jsCall in state.JsCalls)
                    await BoardModule.InvokeVoidAsync(jsCall.Function, jsCall.Value);

                state.JsCalls.Clear();

                if (!state.Dirty) 
                    return;
                
                state.Dirty = false;
                StateHasChanged();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        private async Task CheckBoardModule()
        {
            BoardModule ??= await JsRuntime.InvokeAsync<IJSObjectReference>("import", "./scripts/board.js");
        }

        public void Dispose()
        {
            BoardState.StateChanged -= BoardStateChanged;
        }
    }
}
