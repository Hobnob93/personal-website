using System.Linq;
using Fluxor;
using PersonalWebsite.Client.Actions;
using PersonalWebsite.Client.Store;
using PersonalWebsite.Shared.Models;
// ReSharper disable UnusedType.Global UnusedMember.Global

namespace PersonalWebsite.Client.Reducers
{
    public static class BoardReducer
    {
        [ReducerMethod]
        public static BoardState OnToggleAutoPlay(BoardState state, SetAutoPlayAction action)
        {
            var function = action.IsAutoPlaying ? "play" : "stop";
        
            return state with
            {
                IsAutoPlaying = action.IsAutoPlaying,
                JsCalls = state.JsCalls.Append(new JsBoardCall
                {
                    Function = function,
                    Value = null
                }).ToList()
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
                    Function = "setEdgeWrap",
                    Value = !state.DoEdgeWrap
                }).ToList()
            };
        }

        [ReducerMethod(typeof(TickBoardAction))]
        public static BoardState OnBoardShouldTick(BoardState state)
        {
            return state with
            {
                JsCalls = state.JsCalls.Append(new JsBoardCall
                {
                    Function = "nextFrame",
                    Value = null
                }).ToList()
            };
        }
        
        [ReducerMethod]
        public static BoardState OnGridSizeChanged(BoardState state, ChangeGridSizeAction action)
        {
            return state with
            {
                Dirty = true,
                GridSize = action.NewGridSize
            };
        }
        
        [ReducerMethod]
        public static BoardState OnPlaySpeedChanged(BoardState state, ChangePlaySpeedAction action)
        {
            return state with
            {
                PlaySpeed = action.NewPlaySpeed,
                JsCalls = state.JsCalls.Append(new JsBoardCall
                {
                    Function = "setPlaySpeed",
                    Value = action.NewPlaySpeed
                }).ToList()
            };
        }

        [ReducerMethod]
        public static BoardState OnUpdateBoard(BoardState state, UpdateBoardAction action)
        {
            return state with
            {
                Dirty = true,
                Board = action.NewBoard,
                JsCalls = state.JsCalls.Append(new JsBoardCall
                {
                    Function = "setBoard",
                    Value = action.NewBoard
                }).ToList()
            };
        }

        [ReducerMethod]
        public static BoardState OnBoardUpdated(BoardState state, BoardUpdatedAction action)
        {
            return state with
            {
                JsCalls = state.JsCalls.Append(new JsBoardCall
                {
                    Function = "setBoard",
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
                    Function = "setPenType",
                    Value = action.NewPen
                }).ToList()
            };
        }

        [ReducerMethod(typeof(SaveBoardAction))]
        public static BoardState OnSaveBoard(BoardState state)
        {
            return state with
            {
                JsCalls = state.JsCalls.Append(new JsBoardCall
                {
                    Function = "save",
                    Value = null
                }).ToList()
            };
        }
        
        [ReducerMethod(typeof(LoadBoardAction))]
        public static BoardState OnLoadBoard(BoardState state)
        {
            return state with
            {
                JsCalls = state.JsCalls.Append(new JsBoardCall
                {
                    Function = "load",
                    Value = null
                }).ToList()
            };
        }
    }
}