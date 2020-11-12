class Cell:
    def __init__(self, value):
        if value is Cell:
            self.value = value.value
        else:
            self.value = value

    def get_str(self, line):
        if line == 0:
            return " "*3
        elif line == 1:
            return f" {self.value} "
        else:
            return " "*3

    def weight(self):
        return 0

class RightCell():
    def __init__(self, value):    
        if value is Cell:
            self.value = value.value
        else:
            self.value = value

    def get_str(self, line):
        if line == 0:
            return "***"
        elif line == 1:
            return f" {self.value}*"
        else:
            return "***"

    def weight(self):
        return 1 

class TopCell():
    def __init__(self, value):    
        if value is Cell:
            self.value = value.value
        else:
            self.value = value

    def get_str(self, line):
        if line == 0:
            return "***"
        elif line == 1:
            return f"*{self.value}*"
        else:
            return "* *"
    
    def weight(self):
        return 2   

class LeftCell():
    def __init__(self, value):    
        if value is Cell:
            self.value = value.value
        else:
            self.value = value

    def get_str(self, line):
        if line == 0:
            return "***"
        elif line == 1:
            return f"*{self.value} "
        else:
            return "***"

    def weight(self):
        return 3

class BottomCell():
    def __init__(self, value):    
        if value is Cell:
            self.value = value.value
        else:
            self.value = value

    def get_str(self, line):    
        if line == 0:
            return "* *"
        elif line == 1:
            return f"*{self.value}*"
        else:
            return "***"

    def weight(self):
        return 4

def get_cell_types(r1, c1, r2, c2):
    if c1 < c2:
        return [LeftCell, RightCell]
    elif c1 > c2:
        return [RightCell, LeftCell]
    elif r1 < r2:
        return [TopCell, BottomCell]
    else:
        return [BottomCell, TopCell]

class state:
    # board = [
    #     [1, 0, 0, 0, 0],
    #     [3, 1, 2, 2, 3],
    #     [2, 3, 0, 2, 1],
    #     [2, 3, 1, 3, 1]
    # ]
    # all_dices = [
    #     (0, 0), (0, 1), (0, 2), (0, 3), 
    #     (1, 1), (1, 2), (1, 3),
    #     (2, 2), (2, 3),
    #     (3, 3)
    # ]
    # rows = 4
    # cols = 5

    board = [
        [0, 0, 0, 1],
        [0, 2, 2, 1],
        [1, 2, 2, 1]
    ]
    all_dices = [
        (0, 0), (0, 1), (0, 2),
        (1, 1), (1, 2), 
        (2, 2)
    ]
    rows = 3
    cols = 4

    def __init__(self, parent, dice_places, depth):
        self.parent = parent
        self.dice_places = dice_places
        self.depth = depth        
        self.cells = []
        self.all_dices = state.all_dices.copy()

        for row in range(state.rows):
            cell_row = []
            for col in range(state.cols):
                c = Cell(state.board[row][col])
                cell_row.append(c)
            self.cells.append(cell_row)

        for place in self.dice_places:
            r1, c1, r2, c2 = place
            types = get_cell_types(r1, c1, r2, c2)            
            self.cells[r1][c1] = types[0](state.board[r1][c1])
            self.cells[r2][c2] = types[1](state.board[r2][c2])

            v1 = min(state.board[r1][c1], state.board[r2][c2])
            v2 = max(state.board[r1][c1], state.board[r2][c2])
            self.all_dices.remove((v1, v2))

    def __hash__(self):
        d = 0
        for row in self.cells:
            for cell in row:
                d *= 10
                d += cell.weight()
        return d

    def __str__(self):
        s = ''
        for row in self.cells:
            for line in range(3):
                for cell in row:
                    s += cell.get_str(line)
                s += "\n"
        return s

    def __lt__(self, other):
        return self.__hash__() < other.__hash__()

    def __eq__(self, other):
        return self.__hash__() == other.__hash__()

    def print(self):
        for row in self.cells:
            for line in range(3):
                s = ''
                for cell in row:
                    s += cell.get_str(line)
                print(s)

    def is_a_free_cell(self, row, col):
        return isinstance(self.cells[row][col], Cell)

    def is_exist(self, row, col):
        if row < 0 or row >= state.rows:
            return False
        if col < 0 or col >= state.cols:
            return False
        return True

    def is_dice_exist(self, v1, v2):
        v1, v2 = min(v1, v2), max(v1, v2)
        return (v1, v2) in self.all_dices

    def get_move(self, r1, c1, r2, c2):
        v1 = self.board[r1][c1]
        if self.is_exist(r2, c2):
            v2 = self.board[r2][c2]            
            if self.is_a_free_cell(r2, c2):
                if self.is_dice_exist(v1, v2):
                    dp = self.dice_places.copy()
                    dp.append((r1, c1, r2, c2))
                    return state(self, dp, self.depth+1)
        return None

    def append_move(self, moves, move):
        if move:
            if not move in moves:
                moves.append(move)

    def get_moves(self):
        moves = []
        for row in range(state.rows):
            for col in range(state.cols):
                if self.is_a_free_cell(row, col):
                    self.append_move(moves, self.get_move(row, col, row-1, col))
                    self.append_move(moves, self.get_move(row, col, row+1, col))
                    self.append_move(moves, self.get_move(row, col, row, col-1))
                    self.append_move(moves, self.get_move(row, col, row, col+1))
        return moves

    def score(self):
        return sum([1 for v1, v2 in self.all_dices if v1==v2])

def goal(state):
    for row in state.cells:
        for cell in row:
            if cell.weight() == 0:
                return False
    return True

def evaluator(state, goal):
   return sum([1 for v1, v2 in state.all_dices if v1==v2])

if __name__ == "__main__":
<<<<<<< HEAD
    s = state([])
=======
    s = state(None, [(0, 0, 1, 0), (0, 4, 0, 3)], 0)
>>>>>>> 56f024cd1fd86b83fa518e9518ad255a221740cc
    
    s.print()
    print('---')
    moves = s.get_moves()
    moves.sort()
    for move in moves:
        print(move)
        print(f's = {move.score()}')
        print(f's = {move.__hash__()}')        
        print('---')

    print(len(moves))
    # h = [m.__hash__() for m in moves]
    # h.sort()

    # print(h)