class state:
    def __init__(self, value):
        raise NotImplementedError

    def get_moves(self, player):
        raise NotImplementedError        

    def do_move(self, move):
        raise NotImplementedError                

    def undo_move(self, move):
        raise NotImplementedError                

    def is_win(self, player):
        raise NotImplementedError                

    def score(self, player):
        raise NotImplementedError                
