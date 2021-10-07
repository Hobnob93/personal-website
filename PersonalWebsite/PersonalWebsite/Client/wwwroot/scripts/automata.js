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
            let numLiveNeighbours = 0;
            for (let n = 0; n < cell.neighbours.length; n++)
            {
                let neighbour = oldCells[cell.neighbours[n]];
                if (neighbour === cellTypes.GOAL)
                    numLiveNeighbours++;
            }
            
            if (oldState === cellTypes.GOAL)
                this.changeCellType(oc, cell, this.liveCellNewState(numLiveNeighbours));
            else
                this.changeCellType(oc, cell, this.deadCellNewState(numLiveNeighbours));
        }
    },
    
    liveCellNewState: function(numLiveNeighbours) {
        switch(numLiveNeighbours) {
            case 2:
            case 3:
                return cellTypes.GOAL;
            default:
                return cellTypes.NORMAL;
        }
    },

    deadCellNewState: function(numLiveNeighbours) {
        switch(numLiveNeighbours) {
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
