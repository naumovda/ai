from alpha_beta import bestmove

def calc_nodes(state, level, player, opponent):
    ''' Расчет количества сгенерированных узлов
        - state - начальное состояние
        - level - максимальная глубина рекрсии (количество полуходов)
        - player - игрок
        - opponent - оппонент
    '''     
    _, _, nodes = bestmove(state, level, player, opponent)
    return nodes 
     
def test_count():
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
    plt.xlabel('alpha-beta')
    plt.grid(True)
    plt.show() 

def test_play():
    from xo import state_xo

    s = state_xo()
    level = 4
    player, opponent = "X", state_xo.opponent["X"]  
    
    print('start!')
    
    step = 1
    while not (s.is_win(player) or s.is_win(opponent)):        
        move, _, _ = bestmove(s, level, player, opponent)
        if move == None:
            print('finish... draw')
            break        
        print(f'Step {step}: {move}')
        
        s.do_move(move)
        print(f'{s}')

        if s.is_win(player):
            print(f'Player {player} win!')
            break
        
        step += 1
        player, opponent = opponent, player
    
    print('the end')

if __name__ == "__main__":
    test_count()
    # test_play()