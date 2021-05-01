using System;
using Fluxor;
using PersonalWebsite.Client.Store;
using PersonalWebsite.Shared.Enums;
using PersonalWebsite.Shared.Models;

namespace PersonalWebsite.Client.Features
{
    public class BoardStateFeature : Feature<BoardState>
    {
        public override string GetName() => nameof(BoardState);

        protected override BoardState GetInitialState()
        {
            return new BoardState()
            {
                Board = new Board
                {
                    Cells = Array.Empty<BoardCell>()
                },
                GridSize = GridSize.Small,
                PlaySpeed = PlaySpeed.Normal,
                DoEdgeWrap = false,
                IsAutoPlaying = false
            };
        }
    }
}