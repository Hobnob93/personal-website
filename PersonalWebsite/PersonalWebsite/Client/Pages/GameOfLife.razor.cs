using PersonalWebsite.Client.Shared;
using PersonalWebsite.Shared.Enums;
using PersonalWebsite.Shared.Extensions;

namespace PersonalWebsite.Client.Pages
{
    public partial class GameOfLife
    {
        public BoardType BoardType { get; } = BoardType.Automata;
        public BoardWidget Board { get; set; }

        public int PlaySpeedRaw { get; set; } = 2;
        public PlaySpeed PlaySpeed => (PlaySpeed)PlaySpeedRaw;
        public string PlaySpeedTooltip => $"Play Speed: {PlaySpeed.GetDescription()}";

        public int GridSizeRaw { get; set; } = 2;
        public GridSize GridSize => (GridSize)GridSizeRaw;
        public string GridSizeTooltip => $"Grid Size: {GridSize.GetDescription()}";

        public bool IsPlaying { get; set; }
        public bool DoEdgeWrap { get; set; }
        public int Generation { get; set; }
        public int Alive { get; set; }
        public int Dead { get; set; }
        public int Lived { get; set; }


        public void ClearBoard()
        {
            Board.ClearBoard(DoEdgeWrap);
        }

        public void ToggleWrapEdge()
        {
            DoEdgeWrap = !DoEdgeWrap;
            Board.OnWrapEdgeChanged(DoEdgeWrap);
        }

        public void OnSpeedChanged(int newValue)
        {
            PlaySpeedRaw = newValue;
        }

        public void OnGridSizeChanged(int newValue)
        {
            GridSizeRaw = newValue;
        }

        public void Play()
        {
            IsPlaying = true;
        }

        public void Stop()
        {
            IsPlaying = false;
        }

        public void NextFrame()
        {
            IsPlaying = false;
        }
    }
}
