nodes = 0 # переменная для расчета статистики

def negmax(state, level, player, opponent):
    ''' Алгоритм поиска лучше хода NegMax
        - state - начальное состояние
        - level - максимальная глубина рекрсии (количество полуходов)
        - original - исходный игрок, для которого считается
                     дерево
        - player - текущий игрок
        - opponent - оппонент
    '''
    
    # инициализируем лучший ход и оценку 
    best_move, best_score = None, None
    
    # получаем список возможных ходов
    moves = state.get_moves(player)

    # накапливаем количество сгенерированных ходов
    global nodes 
    nodes += len(moves)

    # если достигнута максимальная глубина дерева
    # или ходов нет, то рассчитываем оценку
    # при помощи оценочной функции
    if level == 0 or moves == []:
        return None, state.score(player)

    # перебираем последовательно все возможные ходы
    for m in moves:
        state.do_move(m) # выполняем ход
        # вызываем рекурсивно NegMax,
        # уменьшая уровень на 1
        # и меняя местами игрока и оппонента        
        _, score = negmax(state, level-1, opponent, player)
        state.undo_move(m) # отменяем ход

        # меняем знак оценочной функции на противоположный
        if best_score == None or (-1)*score > best_score:
            best_move, best_score =  m, (-1)*score

    return best_move, best_score

def bestmove(state, level, player, opponent):
    ''' Вызов функции MiniMax с начальными значениями
        - state - начальное состояние
        - level - максимальная глубина рекрсии (количество полуходов)
        - player - игрок
        - opponent - оппонент
    '''    
    return negmax(state, level, player, opponent)

def calc_nodes(state, level, player, opponent):
    ''' Расчет количества сгенерированных узлов
        - state - начальное состояние
        - level - максимальная глубина рекрсии (количество полуходов)
        - player - игрок
        - opponent - оппонент
    '''    
    global nodes 
    nodes = 0 # обнуляем статистику 
    # вызываем метод NegMax
    _ = bestmove(state, level, player, opponent)
    return nodes

def test01():
    from xo import state_xo

    # начальное состояние (пустое)
    s = state_xo()
    # на два хода (на четыре полухода) вперед
    level = 4
    # первым ходит "X", вторым - "0"
    player, opponent = "X", state_xo.opponent["X"]  
    
    # обнуляем статистику
    nodes = 0

    # получаем лучший ход
    move = bestmove(s, level, player, opponent)
    
    print(f"Best move is: {move}")    
    print(f"Node count {nodes}")

def test02(level):
    ''' Тестирование времени расчета
        level - количество полуходов, максимальная 
                глубина дерева
    '''

    from xo import state_xo
    from timeit import Timer

    s = state_xo()
    player, opponent = "X", state_xo.opponent["X"] 

    # lambda-функция расчета количества узлов
    f = lambda: calc_nodes(s, level, player, opponent)        
       
    # расчет времени выполнения
    t = Timer(f)
    print("Time = ", t.timeit(number=1))    

def test03():
    '''
    '''
    from xo import state_xo 
    import numpy as np
    import matplotlib.pyplot as plt

    s = state_xo()
    player, opponent = "X", state_xo.opponent["X"] 

    # правая граница для графика 
    depth = 6
    x = np.arange(1, depth, 1)
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
    # test02(4)
    test03()
