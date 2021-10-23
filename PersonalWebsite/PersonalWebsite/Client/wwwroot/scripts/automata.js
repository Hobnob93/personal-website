// noinspection JSUnresolvedVariable

let automataProcessor = {
    tick: function() {
        let board = boardData.board;        
        let oldCells = $.map(board.cells, function(cell) {
           return cell.type;
        });
        
        for (let oc = 0; oc < oldCells.length; oc++) {
            let oldState = oldCells[oc];
            if (oldState === cellTypes.EMPTY) {
                continue;
            }
            
            let cell = board.cells[oc];
            let numLiveNeighbors = 0;
            for (let n = 0; n < cell.neighbors.length; n++)
            {
                let neighbor = oldCells[cell.neighbors[n]];
                if (neighbor === cellTypes.GOAL)
                    numLiveNeighbors++;
            }
            
            if (oldState === cellTypes.GOAL)
                this.changeCellType(oc, cell, this.liveCellNewState(numLiveNeighbors));
            else
                this.changeCellType(oc, cell, this.deadCellNewState(numLiveNeighbors));
        }
    },
    
    liveCellNewState: function(numLiveNeighbors) {
        switch(numLiveNeighbors) {
            case 2:
            case 3:
                return cellTypes.GOAL;
            default:
                return cellTypes.NORMAL;
        }
    },

    deadCellNewState: function(numLiveNeighbors) {
        switch(numLiveNeighbors) {
            case 3:
                return cellTypes.GOAL;
            default:
                return cellTypes.NORMAL;
        }
    },
    
    changeCellType: function(cellId, cell, toType) {
        let curType = cell.type;
        if (curType === cellTypes.NORMAL && toType === cellTypes.GOAL)
        {
            //AddStatistic(BoardStatistic.Living, 1);
            //AddStatistic(BoardStatistic.TotalLived, 1);
        }
        else if (curType === cellTypes.GOAL && toType === cellTypes.NORMAL)
        {
            //AddStatistic(BoardStatistic.Living, -1);
            //AddStatistic(BoardStatistic.TotalDied, 1);
        }

        boardData.changeCellType(cellId, toType);
    }
};
