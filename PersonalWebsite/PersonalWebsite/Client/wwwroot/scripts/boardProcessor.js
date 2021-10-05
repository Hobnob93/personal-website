// noinspection JSUnusedGlobalSymbols

let boardProcessor = {
    processor: null,
    isPlaying: false,
    playSpeed: 2,
    doEdgeWrap: false,
    
    loadFromBoard: function(board) {
        switch (board.type) {
            default:
                this.processor = null;
        }
    },
    
    setEdgeWrap: function(doEdgeWrap) {
        this.doEdgeWrap = doEdgeWrap;
    },
    
    setPlaySpeed: function(playSpeedType) {
      this.playSpeed = playSpeedType;  
    },
    
    stop: function() {
        this.isPlaying = false;
    },
    
    start: function() {
        this.isPlaying = true;
    },
    
    tick: function() {
        // Do next frame
    }
};