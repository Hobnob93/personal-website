using System.Threading.Tasks;
using Fluxor;
using PersonalWebsite.Client.Actions;
using PersonalWebsite.Client.Store;
using PersonalWebsite.Shared.Interfaces;

namespace PersonalWebsite.Client.Effects
{
    public class BoardEffects
    {
        private readonly IBoardService boardService;
        private readonly IState<BoardState> boardState;

        public BoardEffects(IBoardService boardService, IState<BoardState> boardState)
        {
            this.boardService = boardService;
            this.boardState = boardState;
        }

        [EffectMethod(typeof(InitializeBoardAction))]
        public Task OnInitializeBoard(IDispatcher dispatcher)
        {
            return Task.Run(() =>
            {
                var (height, width) = boardState.Value.GetBoardDimensions();
                var board = boardService.Initialise(height, width, boardState.Value.DoEdgeWrap);
                
                dispatcher.Dispatch(new UpdateBoardAction
                {
                    NewBoard = board
                });
            });
        }

        [EffectMethod(typeof(ChangeGridSizeAction))]
        public Task OnChangeGridSize(IDispatcher dispatcher)
        {
            return Task.Run(() =>
            {
                dispatcher.Dispatch(new InitializeBoardAction());
            });
        }
        
        [EffectMethod(typeof(ClearBoardAction))]
        public Task OnClearBoard(IDispatcher dispatcher)
        {
            dispatcher.Dispatch(new SetAutoPlayAction
            {
                IsAutoPlaying = false
            });
            
            return Task.Run(() =>
            {
                dispatcher.Dispatch(new UpdateBoardAction
                {
                    NewBoard = boardService.Reset(boardState.Value.Board, true, boardState.Value.DoEdgeWrap)
                });
            });
        }
        
        [EffectMethod(typeof(ToggleEdgeWrapAction))]
        public Task OnToggleEdgeWrap(IDispatcher dispatcher)
        {
            return Task.Run(() =>
            {
                dispatcher.Dispatch(new UpdateBoardAction
                {
                    NewBoard = boardService.Reset(boardState.Value.Board, false, boardState.Value.DoEdgeWrap)
                });
            });
        }

        [EffectMethod(typeof(TickBoardAction))]
        public Task OnTickBoard(IDispatcher dispatcher)
        {
            return Task.Run(() =>
            {
                dispatcher.Dispatch(new UpdateBoardAction
                {
                    NewBoard = boardService.Tick(boardState.Value.Board)
                });
            });
        }

        [EffectMethod]
        public Task OnCellClicked(CellClickedAction action, IDispatcher dispatcher)
        {
            return Task.Run(() =>
            {
                dispatcher.Dispatch(new UpdateBoardAction
                {
                    NewBoard = boardService.CellInteracted(boardState.Value.Board, action.H, action.W)
                });
            });
        }
    }
}