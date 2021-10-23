// noinspection JSUnusedGlobalSymbols

const boardTypes = {
    AUTOMATA: 0,
    PATHFINDING: 1,
    SEARCHING: 2,
    SORTING: 3
};

const cellTypes = {
    NORMAL: 0,              // A standard cell
    EMPTY: 1,               // An empty cell, used as a wall in pathfinding
    HIGH_COST: 2,           // A non-blocking tile used for pathfinding - adds penalty to movement
    LOW_COST: 3,            // A non-blocking tile used for pathfinding - deducts cost from movement
    START: 4,               // An initial node
    GOAL: 5,                // The target node for pathfinding/searching or an alive cell in automata
    SUB_GOAL: 6,            // The initial goal before the final goal when pathfinding
    TOUCHED: 7,             // A "touched" cell has been looked at by an algorithm
    PATH: 8                 // Cell is used as part of a path when pathfinding
};

const neighborhoodTypes = {
    ADJACENT: 0,            // Four neighbors per cell: north, east, south, west
    MOORE: 1,               // Eight neighbors per cell: north, north-east, east, south-east, south, south-west, west, north-west
    NEUMANN: 2              // Eight neighbors per cell: north, north-north, east, east-east, south, south-south, west, west-west
};

const neighborDirections = {
    NORTH: 0,               // Directly above
    NORTH_NORTH: 1,         // Two cells above
    NORTH_EAST: 2,          // Diagonally up and right
    EAST: 3,                // Directly right
    EAST_EAST: 4,           // Two cells to the right
    SOUTH_EAST: 5,          // Diagonally down and right
    SOUTH: 6,               // Directly down
    SOUTH_SOUTH: 7,         // Two cells down
    SOUTH_WEST: 8,          // Diagonally down and left
    WEST: 9,                // Directly left
    WEST_WEST: 10,          // Two cells left
    NORTH_WEST: 11          // Diagonally up and left
};

const adjacentNeighbors = [
    neighborDirections.NORTH,
    neighborDirections.EAST,
    neighborDirections.SOUTH,
    neighborDirections.WEST
];

const mooreNeighbors = [
    neighborDirections.NORTH,
    neighborDirections.NORTH_EAST,
    neighborDirections.EAST,
    neighborDirections.SOUTH_EAST,
    neighborDirections.SOUTH,
    neighborDirections.SOUTH_WEST,
    neighborDirections.WEST,
    neighborDirections.NORTH_WEST
];

const neumannNeighbors = [
    neighborDirections.NORTH,
    neighborDirections.NORTH_NORTH,
    neighborDirections.WEST,
    neighborDirections.WEST_WEST,
    neighborDirections.SOUTH,
    neighborDirections.SOUTH_SOUTH,
    neighborDirections.EAST,
    neighborDirections.EAST_EAST
];

const playbackSpeeds = [
    1000, 
    500, 
    300, 
    150, 
    0
];
