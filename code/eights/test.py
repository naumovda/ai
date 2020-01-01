# initial = state([[8,1,3],[2,4,5],[state.space,7,6]], 0)

# goal = state([[1,2,3],[8,state.space,4],[7,6,5]])
# 123 456 789

pl = {\
    1:(0,0), 2:(0,1), 3:(0,2), \
    4:(1,0), 5:(1,1), 6:(1,2), \
    7:(2,0), 8:(2,1), 9:(2,2)  \
    }

moves = {\
    1: [2, 4],
    2: [1,3,5],
    3: [2, 6],
    4: [1, 5, 7],
    5: [2, 4, 6, 8],
    6: [3, 5, 9],
    7: [4, 8],
    8: [5, 7, 9],
    9: [6, 8]
    }

d = {1:2, 2:4, 3:3, 4:5, 5:6, 6:9, 7:8, 8:1, 0:7}

def get_key(items, elem):
    for key, item in items.items():
        if item == elem:
            return key
    return None

def print_dict(d):
    l = [[0,0,0],[0,0,0],[0,0,0]]
    for key, item in d.items():
        row, col = pl[item]
        l[row][col] = key
    for row in l:
        print(row)
    print()

def get_moves(d):
    new_moves = []
    for position in moves[d[0]]: # получаем список ходов для пустого поля
        key = get_key(d, position)
        new_state = d.copy()
        new_state[0] = d[key] 
        new_state[key] = d[0]
        new_moves.append(new_state)
    return new_moves

if __name__ == "__main__":
    print_dict(d)

    moves = get_moves(d)

    for move in moves:
        print_dict(move)