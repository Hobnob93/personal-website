 // noinspection JSUnusedGlobalSymbols

 $(function() {
     $('.board-cell').on('mousedown mousemove dragenter', function(e) {
         if (e.buttons == 1) {
             let h = $(this).data('h');
             let w = $(this).data('w');
             boardData.cellClicked(h, w);
         }
     });
 });

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
}

export var boardData = {
    penType: cellTypes.GOAL,
    board: {},
    
    setBoard: function(board) {
        this.board = board;
    },
    
    cellClicked : function(h, w) {
        let cellId = this.board.width * h + w;
        let boardCell = this.board.cells[cellId];
        
        let cellElement = $(`#cell-${h}-${w}`);
        cellElement.removeClass(`type-${boardCell.type}`);
        
        boardCell.type = this.penType;
        cellElement.addClass(`type-${boardCell.type}`);
    }
 };