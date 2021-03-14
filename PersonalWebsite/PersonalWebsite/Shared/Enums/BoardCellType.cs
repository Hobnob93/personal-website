namespace PersonalWebsite.Shared.Enums
{
    public enum BoardCellType
    {
        Empty = 0,              // An empty cell
        Full = 1,               // A wall/living cell
        HighCost = 2,           // A non-blocking tile used for pathfinding - adds penalty to movement
        LowCost = 3,            // A non-blocking tile used for pathfinding - deducts cost from movement
        Start = 4,              // An initial node
        Goal = 5,               // The target node for pathfinding/searching
        SubGoal = 6,            // The initial goal before the final goal when pathfinding
        Touched = 7,            // A "touched" cell has been looked at by an algorithm
        Path = 8                // Cell is used as part of a path when pathfinding
    }
}
