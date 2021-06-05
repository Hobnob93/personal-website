using System;
using Fluxor;
using Microsoft.AspNetCore.Components;
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
        
        public int PlaySpeedRaw { get; set; } = 2;
        public PlaySpeed PlaySpeed => BoardState.Value.PlaySpeed;
        public string PlaySpeedTooltip => $"Play Speed: {PlaySpeed.GetDescription()}";
        
        public int GridSizeRaw { get; set; } = 2;
        public GridSize GridSize => BoardState.Value.GridSize;
        public string GridSizeTooltip => $"Grid Size: {GridSize.GetDescription()}";

        public bool IsPlaying => BoardState.Value.IsAutoPlaying;
        public bool DoEdgeWrap => BoardState.Value.DoEdgeWrap;
        public BoardCellType CurrentPen => BoardState.Value.CurrentPen;
        public string SelectedCellHiddenState => IsPlaying ? "Hidden" : string.Empty;
        
        
        protected override void OnInitialized()
        {
            BoardState.StateChanged += BoardStateChanged;
            OnPenChanged(BoardCellType.Goal);
        }
        
        private void BoardStateChanged(object obj, BoardState state)
        {
            StateHasChanged();
        }
        
        public void ClearBoard()
        {
            Dispatcher.Dispatch(new ClearBoardAction());
        }
        
        public void ToggleWrapEdge()
        {
            Dispatcher.Dispatch(new ToggleEdgeWrapAction());
        }
        
        public void OnSpeedChanged(int newValue)
        {
            PlaySpeedRaw = newValue;
            Dispatcher.Dispatch(new ChangePlaySpeedAction
            {
                NewPlaySpeed = (PlaySpeed)newValue
            });
        }

        public void OnGridSizeChanged(int newValue)
        {
            GridSizeRaw = newValue;
            Dispatcher.Dispatch(new ChangeGridSizeAction
            {
                NewGridSize = (GridSize)newValue
            });
        }

        public void Play()
        {
            Dispatcher.Dispatch(new SetAutoPlayAction
            {
                IsAutoPlaying = true
            });
        }

        public void Stop()
        {
            Dispatcher.Dispatch(new SetAutoPlayAction
            {
                IsAutoPlaying = false
            });
        }
        
        public void NextFrame(bool userClicked = false)
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

        public void OnPenChanged(BoardCellType newPen)
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