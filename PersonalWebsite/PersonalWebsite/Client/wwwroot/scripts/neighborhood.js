let neighborhood = {
    
    findNeighbors: function(cells, boardWidth, neighborhoodType, doEdgeWrap) {
        switch (neighborhoodType) {
            case neighborhoodTypes.ADJACENT:
                this.addNeighborsToCells(cells, boardWidth, doEdgeWrap, adjacentNeighbors)
                break;
            case neighborhoodTypes.MOORE:
                this.addNeighborsToCells(cells, boardWidth, doEdgeWrap, mooreNeighbors)
                break;
            case neighborhoodTypes.NEUMANN:
                this.addNeighborsToCells(cells, boardWidth, doEdgeWrap, neumannNeighbors)
                break;
        }
    },
    
    addNeighborsToCells: function(cells, boardWidth, doEdgeWrap, directions) {
        cells.forEach((cell, index) => {
            cell.neighbors = [];
            for (let i = 0; i < directions.length; i++) {
                this.tryAddNeighborToCell(cell, cells.length, boardWidth, index, directions[i], doEdgeWrap);
            }
        }, this);
    },
    
    tryAddNeighborToCell: function(cell, cellsCount, boardWidth, index, direction, doEdgeWrap) {
        let neighborIndex = this.indexOfNeighbor(boardWidth, index, direction);

        let cellIsFarLeft = index % boardWidth === 0;
        let cellIsFarRight = index % boardWidth === (boardWidth - 1);

        if (doEdgeWrap)
        {
            if (this.directionIsNorthern(direction) && neighborIndex < 0)
                neighborIndex += cellsCount;

            if (this.directionIsSouthern(direction) && neighborIndex >= cellsCount)
                neighborIndex -= cellsCount;

            if (this.directionIsWestern(direction) && cellIsFarLeft)
                neighborIndex += boardWidth;                    // cell is on left side, neighbour needs to be same row but on far right

            if (this.directionIsEastern(direction) && cellIsFarRight)
                neighborIndex -= boardWidth;                    // cell is on right side, neighbour needs to be same row but on far left
        }
        else
        {
            if (this.directionIsWestern(direction) && cellIsFarLeft)
                neighborIndex = -1;                             // cell is on left side, but neighbour index is on previous row on far right side

            if (this.directionIsEastern(direction) && cellIsFarRight)
                neighborIndex = -1;                             // cell is on right side, but neighbour index is on next row on far left side
        }
        
        if (neighborIndex >= 0 && neighborIndex < cellsCount) {
            cell.neighbors.push(neighborIndex);
        }
    },
    
    indexOfNeighbor: function(boardWidth, index, direction) {
        switch (direction) {
            case neighborDirections.NORTH:
                return index - boardWidth;
            case neighborDirections.NORTH_NORTH:
                return index - (boardWidth * 2);
            case neighborDirections.NORTH_EAST:
                return index - boardWidth + 1;
            case neighborDirections.EAST:
                return index + 1;
            case neighborDirections.EAST_EAST:
                return index + 2;
            case neighborDirections.SOUTH_EAST:
                return index + boardWidth + 1;
            case neighborDirections.SOUTH:
                return index + boardWidth;
            case neighborDirections.SOUTH_SOUTH:
                return index + (boardWidth * 2);
            case neighborDirections.SOUTH_WEST:
                return index + boardWidth - 1;
            case neighborDirections.WEST:
                return index - 1;
            case neighborDirections.WEST_WEST:
                return index - 2;
            case neighborDirections.NORTH_WEST:
                return index - boardWidth - 1;
        }
    },
    
    directionIsNorthern: function(direction) {
        return direction === neighborDirections.NORTH ||
            direction === neighborDirections.NORTH_NORTH ||
            direction === neighborDirections.NORTH_EAST ||
            direction === neighborDirections.NORTH_WEST;
    },
    
    directionIsEastern: function(direction) {
        return direction === neighborDirections.EAST ||
            direction === neighborDirections.EAST_EAST ||
            direction === neighborDirections.NORTH_EAST ||
            direction === neighborDirections.SOUTH_EAST;
    },
    
    directionIsSouthern: function(direction) {
        return direction === neighborDirections.SOUTH ||
            direction === neighborDirections.SOUTH_SOUTH ||
            direction === neighborDirections.SOUTH_EAST ||
            direction === neighborDirections.SOUTH_WEST;
    },
    
    directionIsWestern: function(direction) {
        return direction === neighborDirections.WEST ||
            direction === neighborDirections.WEST_WEST ||
            direction === neighborDirections.SOUTH_WEST ||
            direction === neighborDirections.NORTH_WEST;
    }
    
};