using Microsoft.AspNetCore.Components;
using PersonalWebsite.Shared.Enums;
using System;
using System.Threading.Tasks;
using TTimer = System.Threading.Timer;

namespace PersonalWebsite.Client.Shared
{
    public partial class Timer : IDisposable
    {
        private PlaySpeed speed;
        private bool active;
        private double[] playSpeeds = new[] { 5, 2.5, 1.5, 1.0, 0.5 };
        private TTimer timer;
        
        [Parameter]
        public PlaySpeed Speed { get => speed; set => OnSpeedChanged(value); }
        [Parameter]
        public bool Active { get => active; set => OnActiveStateChanged(value); }
        [Parameter]
        public EventCallback<bool> OnTimerTick { get; set; }


        private void OnSpeedChanged(PlaySpeed newValue)
        {
            speed = newValue;
            InitialiseTimer();
        }

        private void OnActiveStateChanged(bool newValue)
        {
            active = newValue;
            InitialiseTimer();
        }

        private void InitialiseTimer()
        {
            timer?.Dispose();

            if (active)
            {
                timer = new TTimer
                (
                    async (e) => await Tick(),
                    null,
                    TimeSpan.FromSeconds(playSpeeds[(int)speed]),
                    TimeSpan.FromSeconds(playSpeeds[(int)speed])
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
