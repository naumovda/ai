from copy import deepcopy

# move is a list of changes - []
# change is row, col, old_state, new_state, is active cell
# move = [
#     ((2, 4), 'W', '0', False),
#     ((3, 3), 'B', '0', False),
#     ((4, 2), '0', 'W', True)
# ]

# players' label's
player = ['W', 'B']

# opponets for each player
opponent = {'W':'B', 'B':'W', '0': None}

# Apply a move on a field and return a new field
def do_move(field, move):
    result = deepcopy(field) 
    # change the state on each cell in move
    for (row, col), _, new_state, _ in move:
        result[row][col] = new_state
    return result

# Check on row, col in a field
def in_field(row, col):
    return (0<=row<5) and (0<=col<5)

# Check that a cell is an opponent's 
def is_opponent(field, player, cell):
    row, col = cell
    return in_field(row, col) and field[row][col] == opponent[player]   

# Append one move to list  of moves
#   from cell
#   to direction dr, dc
#   on field
def append_move(moves, field, cell, dr, dc):
    row, col = cell
    player = field[row][col]
    if in_field(row+2*dr, col+2*dc):
        if is_opponent(field, player, (row+dr, col+dc)):
            move = []
            # remove checker
            move.append(((row, col), player, '0', False))                      
            # eat opponent
            move.append(((row+dr, col+dc), opponent[player], '0', False))            
            # move here
            move.append(((row+dr*2, col+dc*2), '0', player, True))                        
            moves.append(move)

# Get one-step moves list
def get_move1(field, cell):
    moves = []
    append_move(moves, field, cell, -1, -1)
    append_move(moves, field, cell,  1, -1)
    append_move(moves, field, cell, -1,  1)
    append_move(moves, field, cell,  1,  1)
    return moves

# Get all moves from cell on field
def get_moves(field, cell):    
    # list of closed moves
    close_states = [] 
    # list of moves to view
    open_states = []
    open_states.extend(get_move1(field, cell))        
    while open_states:
        # get new move
        move = open_states.pop()
        # get active cell from move
        new_cell = move[-1][0]
        # calc new field
        new_field = do_move(field, move)
        # get new moves from new state
        new_moves = get_move1(new_field, new_cell)
        if new_moves:
            # for each move append it
            # to open states
            for new_move in new_moves:
                new_move1 = deepcopy(move)
                new_move1.extend(new_move)
                open_states.append(new_move1)
        else:
            # no new moves, append to close
            close_states.append(move)
    return close_states

field = [
    ['0', '0', '0', '0', '0'],
    ['0', 'B', '0', '0', '0'],
    ['0', '0', '0', '0', '0'],
    ['0', 'B', '0', 'B', '0'],
    ['0', '0', 'W', '0', '0']
]

for move in get_moves(field, (4, 2)):
    new_field = do_move(field, move)
    print('-'*40)
    for row in new_field:
        print(row)