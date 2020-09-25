from xo import state_xo

nodes = 0

def alpha_beta(state, level, player, opponent, low, high):
    best_move, best_score = None, None
    moves = state.get_moves(player)

    global nodes 
    nodes += len(moves)

    if level == 0 or moves == []:
        return None, state.score(player)

    for m in moves:
        state.do_move(m)
        _, score = alpha_beta(state, level-1, opponent, player, -high, -low)
        state.undo_move(m)

        if best_score == None or -1*score > best_score:
            low = -1*score
            best_move, best_score =  m, -1*score

        if low >= high:
            return best_move, best_score        

    return best_move, best_score

def bestmove(state, level, player, opponent):
    return alpha_beta(state, level, player, opponent, \
        -state_xo.infinity, state_xo.infinity)

def calc_nodes(state, level, player, opponent):
    global nodes 
    nodes = 0    
    _ = bestmove(state, level, player, opponent)
    return nodes
    
# if __name__ == "__main__":
#     s = state_xo()
#     level = 3
#     player, opponent = "X", state_xo.opponent["X"]    
    
#     move = bestmove(s, level, player, opponent)

#     print(f"Best move is: {move}")    
#     print(f"Node count {nodes}")
      
def test03():
    from xo import state_xo 
    import numpy as np
    import matplotlib.pyplot as plt

    s = state_xo()
    player, opponent = "X", state_xo.opponent["X"] 

    lag = 1
    x = np.arange(1, 8, lag)
    y = np.array([calc_nodes(s, level, player, opponent) for level in x])
    _ = plt.figure()
    plt.plot(x, y)
    plt.title('Count of nodes')
    plt.ylabel('nodes')
    plt.xlabel('negmax')
    plt.grid(True)
    plt.show()      

if __name__ == "__main__":
    # test01()
    # test02()
    test03()
