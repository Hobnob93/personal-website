using System;
using System.Collections.Generic;
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
            return new()
            {
                Board = new Board
                {
                    Cells = Array.Empty<BoardCell>()
                },
                GridSize = GridSize.Small,
                PlaySpeed = PlaySpeed.Normal,
                DoEdgeWrap = false,
                IsAutoPlaying = false,
                JsCalls = new List<JsBoardCall>()
            };
        }
    }
}