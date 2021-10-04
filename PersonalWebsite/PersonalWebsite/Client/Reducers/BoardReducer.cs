using System.Linq;
using Fluxor;
using PersonalWebsite.Client.Actions;
using PersonalWebsite.Client.Store;
using PersonalWebsite.Shared.Models;

namespace PersonalWebsite.Client.Reducers
{
    public static class BoardReducer
    {
        [ReducerMethod]
        public static BoardState OnToggleAutoPlay(BoardState state, SetAutoPlayAction action)
        {
            return state with
            {
                IsAutoPlaying = action.IsAutoPlaying
            };
        }
        
        [ReducerMethod(typeof(ToggleEdgeWrapAction))]
        public static BoardState OnToggleEdgeWrap(BoardState state)
        {
            return state with
            {
                DoEdgeWrap = !state.DoEdgeWrap,
                JsCalls = state.JsCalls.Append(new JsBoardCall
                {
                    Function = "boardData.setEdgeWrap",
                    Value = !state.DoEdgeWrap
                }).ToList()
            };
        }
        
        [ReducerMethod]
        public static BoardState OnGridSizeChanged(BoardState state, ChangeGridSizeAction action)
        {
            return state with
            {
                GridSize = action.NewGridSize
            };
        }
        
        [ReducerMethod]
        public static BoardState OnPlaySpeedChanged(BoardState state, ChangePlaySpeedAction action)
        {
            return state with
            {
                PlaySpeed = action.NewPlaySpeed
            };
        }

        [ReducerMethod]
        public static BoardState OnUpdateBoard(BoardState state, UpdateBoardAction action)
        {
            return state with
            {
                Initializing = false,
                Board = action.NewBoard,
                JsCalls = state.JsCalls.Append(new JsBoardCall
                {
                    Function = "boardData.setBoard",
                    Value = action.NewBoard
                }).ToList()
            };
        }

        [ReducerMethod]
        public static BoardState OnBoardUpdated(BoardState state, BoardUpdatedAction action)
        {
            return state with
            {
                Initializing = false,
                JsCalls = state.JsCalls.Append(new JsBoardCall
                {
                    Function = "boardData.setBoard",
                    Value = state.Board
                }).ToList()
            };
        }

        [ReducerMethod]
        public static BoardState OnPenTypeChanged(BoardState state, ChangePenAction action)
        {
            return state with
            {
                CurrentPen = action.NewPen,
                JsCalls = state.JsCalls.Append(new JsBoardCall
                {
                    Function = "boardData.setPenType",
                    Value = action.NewPen
                }).ToList()
            };
        }
    }
}