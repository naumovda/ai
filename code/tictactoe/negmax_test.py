from negmax import *

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

def test_bestmove():
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

def test_time(level):
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

def test_plot():
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
    # test_bestmove()
    # test_time(4)
    test_plot()
