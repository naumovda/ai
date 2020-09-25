nodes = 0

def negmax(state, level, player, opponent):
    best_move, best_score = None, None
    moves = state.get_moves(player)

    global nodes 
    nodes += len(moves)

    if level == 0 or moves == []:
        return None, state.score(player)

    for m in moves:
        state.do_move(m)
        _, score = negmax(state, level-1, opponent, player)
        state.undo_move(m)

        if best_score == None or (-1)*score > best_score:
            best_move, best_score =  m, (-1)*score

    return best_move, best_score

def bestmove(state, level, player, opponent):
    return negmax(state, level, player, opponent)

def calc_nodes(state, level, player, opponent):
    global nodes 
    nodes = 0    
    _ = bestmove(state, level, player, opponent)
    return nodes

def test01():
    from xo import state_xo

    s = state_xo()
    level = 3
    player, opponent = "X", state_xo.opponent["X"]  
    
    nodes = 0
    move = bestmove(s, level, player, opponent)
    
    print(f"Best move is: {move}")    
    print(f"Node count {nodes}")

def test02():
    from xo import state_xo
    from timeit import Timer

    s = state_xo()
    level = 3
    player, opponent = "X", state_xo.opponent["X"] 

    f = lambda: calc_nodes(s, level, player, opponent)        
       
    t = Timer(f)
    print("Time = ", t.timeit(number=1))    

def test03():
    from xo import state_xo 
    import numpy as np
    import matplotlib.pyplot as plt

    s = state_xo()
    player, opponent = "X", state_xo.opponent["X"] 

    lag = 1
    x = np.arange(1, 8, lag)
    y = np.array([calc_nodes(s, level, player, opponent) for level in x])
    fig = plt.figure()
    plt.plot(x, y)
    plt.title('Count of nodes')
    plt.ylabel('nodes')
    plt.xlabel('negmax')
    plt.grid(True)
    plt.show()      

if __name__ == "__main__":
    # test01()
    test03()
