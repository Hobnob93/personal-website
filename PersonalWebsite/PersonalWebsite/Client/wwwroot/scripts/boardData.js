// noinspection JSUnusedGlobalSymbols

let boardData = {
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
        if (board == null || this.eventsHandled === true) {
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