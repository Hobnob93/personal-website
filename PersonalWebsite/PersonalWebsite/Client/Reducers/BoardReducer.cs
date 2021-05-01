using Fluxor;
using PersonalWebsite.Client.Actions;
using PersonalWebsite.Client.Store;

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
                DoEdgeWrap = !state.DoEdgeWrap
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
        public static BoardState OnBoardUpdated(BoardState state, UpdateBoardAction action)
        {
            return state with
            {
                Board = action.NewBoard
            };
        }
    }
}