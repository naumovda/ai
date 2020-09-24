from base import state

class state_xo(state):
    infinity = 100

    lines = [
        [(0,0), (0,1), (0,2)], [(1,0), (1,1), (1,2)],
        [(2,0), (2,1), (2,2)], [(0,0), (1,0), (2,0)],
        [(0,1), (1,1), (2,1)], [(0,2), (1,2), (2,2)],
        [(0,0), (1,1), (2,2)], [(0,2), (1,1), (2,0)]]

    players = ["X", "0"]
    opponent = {"X":"0", "0":"X"}

    def __init__(self, value=None): # player
        if value:
            self.value = value
        else:
            self.value = [[None for _ in range(3)] for _ in range(3)]              

    def __str__(self):
        return str(self.value)

    def get_moves(self, player):
        moves = []
        for row in range(3):
            for col in range(3):
                if self.value[row][col] == None:
                    moves.append((row, col, player))
        return moves

    def do_move(self, move):
        row, col, player = move
        self.value[row][col] = player        

    def undo_move(self, move):
        row, col, _ = move
        self.value[row][col] = None

    def is_win(self, player):
        for line in state_xo.lines:                                
            is_win = True
            for i, j in line:
                is_win = is_win and (self.value[i][j]==player)
            if is_win:
                return True
        return False  

    def nc(self, player):
        count = 0
        for (r1,c1),(r2,c2),(r3,c3) in state_xo.lines:
            if self.value[r1][c1] != state_xo.opponent[player] \
                and self.value[r2][c2] != state_xo.opponent[player] \
                and self.value[r3][c3] != state_xo.opponent[player]:
                count += 1
        return count

    def score(self, player):
        oppenent = state_xo.opponent[player]
        if self.is_win(player):
            return state_xo.infinity
        elif self.is_win(oppenent):
            return (-1)*state_xo.infinity 
        else:
            return self.nc(player) - self.nc(oppenent)
