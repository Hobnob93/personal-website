using System;
using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;
using PersonalWebsite.Client.Actions;
using PersonalWebsite.Client.Store;
using PersonalWebsite.Shared.Enums;
using PersonalWebsite.Shared.Extensions;
using PersonalWebsite.Shared.Interfaces;
using PersonalWebsite.Shared.Models;

namespace PersonalWebsite.Client.Shared
{
    public partial class BoardToolbar
    {
        [Inject] public IBoardService BoardService { get; set; }
        [Inject] public IState<BoardState> BoardState { get; set; }
        [Inject] public IDispatcher Dispatcher { get; set; }

        private int PlaySpeedRaw { get; set; } = 2;
        private PlaySpeed PlaySpeed => BoardState.Value.PlaySpeed;
        private string PlaySpeedTooltip => $"Play Speed: {PlaySpeed.GetDescription()}";

        private MudMenu GridMenu { get; set; }
        private int GridSizeRaw { get; set; } = 2;
        private GridSize GridSize => BoardState.Value.GridSize;
        private string GridSizeTooltip => $"Grid Size: {GridSize.GetDescription()}";

        private bool IsPlaying => BoardState.Value.IsAutoPlaying;
        private bool DoEdgeWrap => BoardState.Value.DoEdgeWrap;
        private BoardCellType CurrentPen => BoardState.Value.CurrentPen;
        
        
        protected override void OnInitialized()
        {
            BoardState.StateChanged += BoardStateChanged;
            OnPenChanged(BoardCellType.Goal);
        }
        
        private async void BoardStateChanged(object obj, BoardState state)
        {
            StateHasChanged();
        }

        private void ClearBoard()
        {
            Dispatcher.Dispatch(new ClearBoardAction());
        }

        private void ToggleWrapEdge()
        {
            Dispatcher.Dispatch(new ToggleEdgeWrapAction());
        }

        private void OnSpeedChanged(int newValue)
        {
            PlaySpeedRaw = newValue;
            Dispatcher.Dispatch(new ChangePlaySpeedAction
            {
                NewPlaySpeed = (PlaySpeed)newValue
            });
        }

        private void OnGridSizeChanged(int newValue)
        {
            GridSizeRaw = newValue;
            Dispatcher.Dispatch(new ChangeGridSizeAction
            {
                NewGridSize = (GridSize)newValue
            });
        }

        private void Play()
        {
            Dispatcher.Dispatch(new SetAutoPlayAction
            {
                IsAutoPlaying = true
            });
        }

        private void Stop()
        {
            Dispatcher.Dispatch(new SetAutoPlayAction
            {
                IsAutoPlaying = false
            });
        }

        private void NextFrame(bool userClicked = false)
        {
            if (userClicked)
            {
                Dispatcher.Dispatch(new SetAutoPlayAction
                {
                    IsAutoPlaying = false
                });
                
                Dispatcher.Dispatch(new TickBoardAction());
            }
            else if (IsPlaying)
            {
                Dispatcher.Dispatch(new TickBoardAction());
            }
        }

        private void OnPenChanged(BoardCellType newPen)
        {
            Dispatcher.Dispatch(new ChangePenAction
            {
                NewPen = newPen
            });
        }
        
        public new void Dispose()
        {
            base.Dispose();
            BoardState.StateChanged -= BoardStateChanged;
        }
    }
}