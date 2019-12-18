import numpy as np
from copy import deepcopy

size = 3

def get_opponent(player):
    if player == 0:
        return 1
    elif player == 1 :
        return 0
    return None

def get_lines():
    lines = [
        [(0,0), (0,1), (0,2)],
        [(1,0), (1,1), (1,2)],
        [(2,0), (2,1), (2,2)],
        [(0,0), (1,0), (2,0)],
        [(0,1), (1,1), (2,1)],
        [(0,2), (1,2), (2,2)],
        [(0,0), (1,1), (2,2)],
        [(0,2), (1,1), (2,0)],
    ]
    return lines

def is_win(s, player):
    lines = get_lines()
    
    for line in lines:        
        is_win = True
        for i, j in line:
            is_win = is_win and (s[i][j]==player)
        if is_win:
            return True

    return False

def get_moves(s, player):
    moves = []

    if is_win(s, 1) or is_win(s, 0):
        return moves    

    for i in range(size):
        for j in range(size):
            if s[i][j] == None:
                move = deepcopy(s)
                move[i][j] = player
                moves.append(move)
    return moves

def inf():
    return 100

def nc(s, player):
    lines = get_lines()
    count = 0
    for line in lines:        
        is_free = True
        for i, j in line:
            is_free = is_free and (s[i][j]==player or s[i][j]==None)
        if is_free:
            count += 1
    return count

def get_score(s, player):
    if is_win(s, 1):
        return inf()
    if is_win(s, 0):
        return -inf()
    return nc(s, player) - nc(s, get_opponent(player))

def minimax(s, ply, player, opponent):
    if player == 1:
        best = (None, -inf())
    else:
        best = (None, inf())        

    moves = get_moves(s, player)

    if (ply == 0) or (moves == []):
        score = get_score(s, player)
        return (None, score)

    for move in moves:
        new_move, new_score = minimax(move, ply-1, opponent, player)
        if player == 1:
            if new_score > best[1]:
                best = (move, new_score)  
        else:
            if new_score < best[1]:
                best = (move, new_score)

    return best

def best_move(s, ply, player, opponent):
    return minimax(s, ply, player, opponent)

field = [[0, None, 0],[None, 1, None],[None, None, 1]]
print(field)

print("win 1 = ", is_win(field, 1))
print("win 0 = ", is_win(field, 0))
print("nc 1  = ", nc(field, 1))
print("nc 0  = ", nc(field, 0))
print("score = ", get_score(field, 1))

print("moves:")
moves = get_moves(field, 0)
for move in moves:
    print(move)

move = best_move(field, 3, 1, 0)

print("field:")
print(field)
print("best move:")
print(move)