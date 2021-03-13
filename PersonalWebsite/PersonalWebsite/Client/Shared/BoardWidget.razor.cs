using Microsoft.AspNetCore.Components;
using PersonalWebsite.Shared.Enums;
using PersonalWebsite.Shared.Interfaces;
using PersonalWebsite.Shared.Models;

namespace PersonalWebsite.Client.Shared
{
    public partial class BoardWidget
    {
        [Inject]
        public IBoardFactory BoardFactory { get; set; }

        [Parameter]
        public BoardType Type { get; set; }
        [Parameter]
        public int Height { get; set; }
        [Parameter]
        public int Width { get; set; }

        public Board Board { get; set; }


        protected override void OnInitialized()
        {
            Board = BoardFactory.BuildBoard(Type, Height, Width);
        }
    }
}
