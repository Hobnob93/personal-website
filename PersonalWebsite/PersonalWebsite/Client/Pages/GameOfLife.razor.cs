using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PersonalWebsite.Shared.Enums;
using PersonalWebsite.Shared.Models;

namespace PersonalWebsite.Client.Pages
{
    public partial class GameOfLife
    {
        [Inject] public IJSRuntime JsRuntime { get; set; }
        public BoardType BoardType => BoardType.Automata;

        private IJSObjectReference processorModule;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);
            
            if (firstRender)
                processorModule = await JsRuntime.InvokeAsync<IJSObjectReference>("import", "./scripts/boardProcessor.js");
        }

        public async ValueTask DisposeAsync()
        {
            await processorModule.DisposeAsync();
        }
    }
}
