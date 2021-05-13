namespace PersonalWebsite.Client.Actions
{
    public record CellClickedAction
    {
        public int H { get; init; }
        public int W { get; init; }
    }
}