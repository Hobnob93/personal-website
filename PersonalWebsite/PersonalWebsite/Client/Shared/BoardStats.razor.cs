using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Fluxor;
using Microsoft.AspNetCore.Components;
using PersonalWebsite.Client.Store;
using PersonalWebsite.Shared.Enums;
using PersonalWebsite.Shared.Interfaces;
using PersonalWebsite.Shared.Models;

namespace PersonalWebsite.Client.Shared
{
    public partial class BoardStats
    {
        [Inject] public IBoardService BoardService { get; set; }
        [Inject] public IState<BoardState> BoardState { get; set; }
        [Inject] public HttpClient Http { get; set; }
        
        [Parameter] public BoardType BoardType { get; set; }

        public BoardStatItem[] StatItems { get; set; } = Array.Empty<BoardStatItem>();
        
        
        protected override void OnInitialized()
        {
            BoardState.StateChanged += BoardStateChanged;
        }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                StatItems = await Http.GetFromJsonAsync<BoardStatItem[]>($"api/StatisticItems?type={(int)BoardType}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                StatItems = Array.Empty<BoardStatItem>();
            }
        }

        private void BoardStateChanged(object? obj, BoardState state)
        {
            StateHasChanged();
        }
        
        public new void Dispose()
        {
            base.Dispose();
            BoardState.StateChanged -= BoardStateChanged;
        }
    }
}