 // noinspection JSUnusedGlobalSymbols

 $(function() {
     $('.board-cell').on('click', function() {
         let h = $(this).data('h');
         let w = $(this).data('w');
         boardData.cellClicked(h, w);
     });
 });

const boardTypes = {
    AUTOMATA: 0,
    PATHFINDING: 1,
    SEARCHING: 2,
    SORTING: 3
 };

export var boardData = {
    board : {},
    
    setBoard: function(board) {
        this.board = board;
        console.log(this.board);
    },
    
    cellClicked : function(h, w) {
        console.log("Foo");
    }
 };