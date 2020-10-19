field = [
    ['0', '0', '0', '0', '0'],
    ['0', 'B', '0', '0', '0'],
    ['0', '0', '0', '0', '0'],
    ['0', 'B', '0', 'B', '0'],
    ['0', '0', 'W', '0', '0']
]

# move is a list of changes - []
# change is row, col, old_state, new_state
move = [
    ((2, 4), 'W', '0'),
    ((4, 2), '0', 'W'),
    ((3, 3), 'B', '0'),
]

player = ['W', 'B']
opponent = {'W':'B', 'B':'W', '0': None}

def do_move(field, move):
    result = field.copy()
    for (row, col), _, new_state in move:
        result[row][col] = new_state
    return result

def in_field(row, col):
    return row > 0 and col >0 and row < 5 and col < 5

def is_opponent(field, player, cell):
    row, col = cell
    return in_field(row, col) and field[row][col] == opponent[player]   

def append_move(moves, field, cell, dr, dc):
    row, col = cell
    player = field[row][col]
    if in_field(row+dr, col+dc) and is_opponent(field, player, cell):
        move = []
        move.append(((row, col), player, '0'))
        move.append(((row+dr*2, col+dr*2), '0', player))
        move.append(((row+dr, col+dr), opponent[player], '0'))
        moves.append(move)

def get_move1(field, cell):
    moves = []
    append_move(moves, field, cell, -1, -1)
    append_move(moves, field, cell,  1, -1)
    append_move(moves, field, cell, -1,  1)
    append_move(moves, field, cell,  1,  1)
    return moves

print(get_move1(field, (2, 4)))