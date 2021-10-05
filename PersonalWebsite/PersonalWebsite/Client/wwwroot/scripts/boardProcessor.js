// noinspection JSUnusedGlobalSymbols

let nullProcessor = {
    tick: function() {
        console.error("This is a null processor - something went wrong!");
    }
}

let boardProcessor = {
    processor: null,
    isPlaying: false,
    playSpeed: 2,
    doEdgeWrap: false,
    tickInterval: nullProcessor,
    
    loadFromBoard: function(board) {
        switch (board.type) {
            case boardTypes.AUTOMATA:
                this.processor = automataProcessor;
                break;
            default:
                this.processor = nullProcessor;
                break;
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
        this.processor.tick();
    }
};