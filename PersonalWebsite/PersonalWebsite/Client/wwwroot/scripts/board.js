 // noinspection JSUnusedGlobalSymbols

export function setBoard(board) {
    boardData.setBoard(board);
    boardProcessor.loadFromBoard(board);
}

export function setPenType(penType) {
    boardData.setPenType(penType);
}

export function setEdgeWrap(doEdgeWrap) {
    boardProcessor.setEdgeWrap(doEdgeWrap);
    boardData.updateNeighbors();
}

export function setPlaySpeed(playSpeed) {
    boardProcessor.setPlaySpeed(playSpeed);
}

export function stop() {
    boardProcessor.stop();
}

export function play() {
    boardProcessor.start();
}

export function nextFrame() {
    boardProcessor.tick();
}

export function save() {
    boardData.save();
}

export function load() {
    boardData.load();
}
