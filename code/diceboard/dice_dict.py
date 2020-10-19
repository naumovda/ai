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
    board = [
        [1, 0, 0, 0, 0],
        [3, 1, 2, 2, 3],
        [2, 3, 0, 2, 1],
        [2, 3, 1, 3, 1]
    ]
    all_dices = [
        (0, 0), (0, 1), (0, 2), (0, 3), 
        (1, 1), (1, 2), (1, 3),
        (2, 2), (2, 3),
        (3, 3)
    ]
    rows = 4
    cols = 5

    LEFT = 0
    RIGHT = 1
    TOP = 2
    BOTTOM = 3

    def __init__(self, dices=None):
        # dice is a dict like this:
        # {
        #   (0, 0): (1, 2, True), 
        #   (0, 1): None
        #   ...
        # 
        # }
        # dice: dice place (row, col, direction)
        # direction: L, R, T, B
        if dices is None:
            self.dices = {dice: (None, None, None) for dice in state.all_dices}
        else:
            self.dices = dices

    @staticmethod
    def get_cell(r, c, direction):
        if direction == state.TOP:
            return r-1, c
        elif direction == state.BOTTOM:
            return r+1, c
        elif direction == state.LEFT:
            return r, c-1
        else:
            return r, c+1

    @staticmethod
    def get_direction(r1, c1, r2, c2):
        if r1 < r2:
            return state.BOTTOM
        elif r2 > r1:
            return state.TOP
        elif c1 < c2:
            return state.RIGHT
        else:
            return state.LEFT

    def get_cells(self):
        cells = []
        for row in range(state.rows):
            cell_row = []
            for col in range(state.cols):
                c = Cell(state.board[row][col])
                cell_row.append(c)
            cells.append(cell_row)

        for r1, c1, direction in self.dices.values():
            if direction:
                r2, c2 = self.get_cell(r1, c1, direction) 
                types = get_cell_types(r1, c1, r2, c2)            
                cells[r1][c1] = types[0](state.board[r1][c1])
                cells[r2][c2] = types[1](state.board[r2][c2])

        return cells

    def __str__(self):
        cells = self.get_cells()
        s = ''
        for row in cells:
            for line in range(3):
                for cell in row:
                    s += cell.get_str(line)
                s += "\n"
        return s

    def print(self):
        cells = self.get_cells()
        for row in cells:
            for line in range(3):
                s = ''
                for cell in row:
                    s += cell.get_str(line)
                print(s)

    def is_a_free_cell(self, row, col):
        if (row, col) in [(r, c) for (r, c, _) in self.dices.values()]:
            return False
        if (row, col) in [self.get_cell(r, c, d) for r, c, d in self.dices.values()]:
            return False
        return True

    def is_exist(self, row, col):
        if row < 0 or row >= state.rows:
            return False
        if col < 0 or col >= state.cols:
            return False
        return True

    def is_dice_exist(self, v1, v2):
        v1, v2 = min(v1, v2), max(v1, v2)
        return self.dices[(v1, v2)] is None

    def get_move(self, r1, c1, r2, c2):        
        if self.is_exist(r2, c2):
            v1 = self.board[r1][c1]
            v2 = self.board[r2][c2]            
            if self.is_a_free_cell(r2, c2):
                if self.is_dice_exist(v1, v2):                    
                    dp = self.dices.copy()
                    dp[(v1, v2)] = r1, c1, state.get_direction(r1, c1, r2, c2)
                    return state(dp)
        return None

    def append_move(self, moves, move):
        if move:
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

if __name__ == "__main__":
    s = state()
    
    s.print()
    print('---')

    moves = s.get_moves()
    for move in moves:
        print(move)
        print('---')
