// noinspection JSUnusedGlobalSymbols

const boardTypes = {
    AUTOMATA: 0,
    PATHFINDING: 1,
    SEARCHING: 2,
    SORTING: 3
};

const cellTypes = {
    NORMAL: 0,             // A standard cell
    EMPTY: 1,              // An empty cell, used as a wall in pathfinding
    HIGH_COST: 2,          // A non-blocking tile used for pathfinding - adds penalty to movement
    LOW_COST: 3,           // A non-blocking tile used for pathfinding - deducts cost from movement
    START: 4,              // An initial node
    GOAL: 5,               // The target node for pathfinding/searching or an alive cell in automata
    SUB_GOAL: 6,           // The initial goal before the final goal when pathfinding
    TOUCHED: 7,            // A "touched" cell has been looked at by an algorithm
    PATH: 8                // Cell is used as part of a path when pathfinding
};

const playbackSpeeds = [1000, 500, 300, 150, 0];
