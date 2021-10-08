// noinspection JSUnusedGlobalSymbols,JSUnresolvedVariable

let boardData = {
    penType: cellTypes.GOAL,
    board: null,
    savedBoard: null,
    initialised: false,
    redrawCheckInterval: null,

    setBoard: function(board) {
        this.setBoardLoader(true);
        
        // Need to do a deep clone of the board (please see save method below)
        this.board = JSON.parse(JSON.stringify(board));
        
        this.initialised = false;
        this.redrawCheckInterval = window.setInterval(function() {
            boardData.checkBoardIsDrawn();
        }, 500);
    },

    checkBoardIsDrawn: function() {
        let board = this.board;
        if (board == null || this.initialised === true) {
            return;
        }

        let drawnCells = $('.board-cell');
        if (drawnCells.length !== (board.height * board.width)) {
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
        let boardCells = $('.board-cell');

        boardCells.off();

        boardCells.on('mousedown mousemove dragenter', function(e) {
            if (e.buttons === 1) {
                let h = Number($(this).attr('data-h'));
                let w = Number($(this).attr('data-w'));
                boardData.cellInteracted(h, w);
            }
        });

        this.initialised = true;
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
    },
    
    changeCellType: function(cellId, newType) {
        let cell = this.board.cells[cellId];
        cell.type = newType;

        let cellElement = $(`#cell-${cell.h}-${cell.w}`);
        let curType = cellElement.attr('data-t');

        cellElement.removeClass(`type-${curType}`);
        cellElement.addClass(`type-${cell.type}`);
        cellElement.attr('data-t', cell.type);
    },
    
    save: function() {
        // Use JSON to create a deep copy of the board object, otherwise changes to the board will also change the saved board!
        this.savedBoard = JSON.parse(JSON.stringify(this.board));
    },
    
    load: function() {
        if (this.savedBoard != null) {
            this.setBoard(this.savedBoard);
        }
    }
};
