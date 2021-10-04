using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PersonalWebsite.Shared.Enums;

namespace PersonalWebsite.Client.Pages
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public partial class GameOfLife
    {
        [Inject] private IJSRuntime JsRuntime { get; set; }

        private BoardType BoardType => BoardType.Automata;

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
