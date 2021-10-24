// noinspection JSUnusedGlobalSymbols

let nullProcessor = {
    tick: function() {
        console.error("This is a null processor - something went wrong!");
    }
}

let boardProcessor = {
    processor: nullProcessor,
    isPlaying: false,
    playSpeed: 2,
    doEdgeWrap: false,
    tickInterval: null,
    
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
        if (this.isPlaying)
            this.stop();
        
        this.doEdgeWrap = doEdgeWrap;
    },
    
    setPlaySpeed: function(playSpeedType) {
        this.playSpeed = playSpeedType;
        if (this.isPlaying) {
            this.stop();
            this.start();
        }
    },
    
    stop: function() {
        this.isPlaying = false;
        clearInterval(this.tickInterval);
    },
    
    start: function() {
        this.isPlaying = true;
        this.tickInterval = window.setInterval(function() {
            if (boardProcessor.isPlaying) {
                boardProcessor.tick();
            }
        }, playbackSpeeds[this.playSpeed]);
    },
    
    tick: function() {
        this.processor.tick();
    }
};
