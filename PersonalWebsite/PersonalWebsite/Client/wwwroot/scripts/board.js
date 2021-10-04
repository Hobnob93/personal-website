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
}

export var boardData = {
    doEdgeWrap: false,
    penType: cellTypes.GOAL,
    board: null,
    eventsHandled: false,
    redrawCheckInterval: null,
    
    setBoard: function(board) {
        this.setBoardLoader(true);
        this.board = board;
        this.eventsHandled = false;
        this.redrawCheckInterval = window.setInterval(function() {
            boardData.checkBoardIsDrawn(); 
        }, 500);
    },
    
    checkBoardIsDrawn: function() {
        let board = this.board;
        if (board == null || this.eventsHandled == true) {
            return;
        }
        
        let drawnCells = $('.board-cell');
        if (drawnCells.length != (board.height * board.width)) {
            return;
        }
        
        clearInterval(this.redrawCheckInterval);
        
        for (let h = 0; h < board.height; h++) {
            for (let w = 0; w < board.width; w++) {
                let cellId = board.width * h + w;
                let cell = board.cells[cellId];

                let cellElement = $(`#cell-${h}-${w}`);
                let curType = cellElement.attr('data-t');

                cellElement.removeClass(`type-${curType}`);
                cellElement.addClass(`type-${cell.type}`);
                cellElement.attr('data-t', cell.type);
            }
        }
        
        this.createEvents();
    },
    
    createEvents: function() {
        $('.board-cell').off();
        
        $('.board-cell').on('mousedown mousemove dragenter', function(e) {
            if (e.buttons == 1) {
                let h = Number($(this).attr('data-h'));
                let w = Number($(this).attr('data-w'));
                boardData.cellInteracted(h, w);
            }
        });

        this.eventsHandled = true;
        this.setBoardLoader(false);
    },
    
    cellInteracted: function(h, w) {
        let cellId = this.board.width * h + w;
        let boardCell = this.board.cells[cellId];
        
        let cellElement = $(`#cell-${h}-${w}`);
        cellElement.removeClass(`type-${boardCell.type}`);
        
        boardCell.type = this.penType;
        cellElement.addClass(`type-${boardCell.type}`);
        cellElement.attr('data-t', boardCell.type);
    },
    
    setPenType: function (penType) {
        this.penType = penType;
    },
    
    setEdgeWrap: function(doEdgeWrap) {
        this.doEdgeWrap = doEdgeWrap;
    },
    
    setBoardLoader: function(isLoading) {
        let loader = $('#loading-body');
        let table = $('#table-board');
        if (isLoading) {
            loader.removeClass('done');
            table.attr('hidden', '');
        } else {
            loader.addClass('done');
            table.removeAttr('hidden')
        }
    }
 };