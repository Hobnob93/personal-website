namespace PersonalWebsite.Shared.Enums
{
    public enum BoardCellType
    {
        Empty,              // An empty cell
        Full,               // A wall/living cell
        FullPenalty,        // A non-blocking tile used for pathfinding - adds penalty to movement
        FullPreferred,      // A non-blocking tile used for pathfinding - deducts cost from movement
        Start,              // An initial node
        Goal,               // The target node for pathfinding/searching
        SubGoal,            // The initial goal before the final goal when pathfinding
        Touched,            // A "touched" cell has been looked at by an algorithm
        Path                // Cell is used as part of a path when pathfinding
    }
}
