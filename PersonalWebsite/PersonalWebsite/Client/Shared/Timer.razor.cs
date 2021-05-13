using Microsoft.AspNetCore.Components;
using PersonalWebsite.Shared.Enums;
using System;
using System.Threading;
using System.Threading.Tasks;
using Fluxor;
using PersonalWebsite.Client.Store;
using TTimer = System.Threading.Timer;

namespace PersonalWebsite.Client.Shared
{
    public partial class Timer : IDisposable
    {
        private double[] playSpeeds = new[] { 2.0, 1.0, 0.75, 0.5, 0.2 };
        private TTimer timer;
        
        [Inject] public IState<BoardState> BoardState { get; set; }
        [Inject] public IDispatcher Dispatcher { get; set; }
        
        [Parameter] public PlaySpeed Speed { get; set; }
        [Parameter] public bool Active { get; set; }
        [Parameter] public EventCallback<bool> OnTimerTick { get; set; }


        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            InitialiseTimer();
        }

        private void InitialiseTimer()
        {
            Console.WriteLine("Timer init");
            
            timer?.Change(Timeout.Infinite, Timeout.Infinite);
            timer?.Dispose();
            timer = null;

            if (Active)
            {
                timer = new TTimer
                (
                    async (e) => await Tick(),
                    null,
                    TimeSpan.FromSeconds(playSpeeds[(int)Speed]),
                    TimeSpan.FromSeconds(playSpeeds[(int)Speed])
                );
            }
        }

        private async Task Tick()
        {
            await OnTimerTick.InvokeAsync(false);
        }

        public void Dispose()
        {
            timer?.Dispose();
        }
    }
}
