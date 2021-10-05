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
