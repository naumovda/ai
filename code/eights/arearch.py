from queue import PriorityQueue
from timeit import Timer
from eight_list import state

# Fair Evaluator ::= P(h)
#   P(h) - сумма манхеттенских расстояний между ячейками текущего состояния и целевым
def fair_evaluator(state, goal):
    size = state.size    
    src = [(0,0) for i in range(size)]
    dst = [(0,0) for i in range(size)]

    for row in range(size):
        for col in range(size):
            src[state._data[row*size+col]] = (row, col)
            dst[goal._data[row*size+col]] = (row, col)

    dist = 0
    for idx in range(len(src)):
        dist += abs(src[idx][0]-dst[idx][0]) + abs(src[idx][1]-dst[idx][1])

    return dist

# Good Evaluator ::= P(h)+3-S(h)
#   P(h) - сумма манхеттенских расстояний между всеми ячейками
#   S(h) - для каждой плитки, 
#             0 - если за ней идет корректный приемник
#             2 - в противном случае 
#             1 - если это плитка в центре 
def good_evaluator(state, goal):
    
    return fair_evaluator(state, goal) + 3

# Weak Evaluator ::= N(h)
#   N(h) - количество плиток не на своем месте
def weak_evaluator(state, goal):
    count = 0
    for k in range(len(state._data)):
        if state._data[k] != goal._data[k]:
            count += 1
    return count 

# Bad Evaluator ::= |D(h) - 16|
#   D(h) - сумма модулей разности значений противоположных (относительно центра) плиток для текущего состояния
#   G(h) - сумма модулей разности значений противоположных (относительно центра) плиток для целевого состояния      
def bad_evaluator(state, goal):
    d = abs(state[0] - state[9]) + abs(state[1] - state[8]) + abs(state[2] - state[7]) + abs(state[3] - state[6])
    g = abs(goal[0] - goal[9]) + abs(goal[1] - goal[8]) + abs(goal[2] - goal[7]) + abs(goal[3] - goal[6])
    return abs(d-g)

def a_search(initial, goal, evaluator):
    """A*search algorythm function

    Keyword arguments:
    initial -- inital state, start of the search tree
    goal -- final state, we want to find path
    priority -- a function to calculate priority for queue

    Return: a tuple (is_find, path, open_state_count, close_state_count)
    is_find -- does path has been found
    path -- a path to final state, a solution 
    open_state_count -- count of elements in open states list
    close_state_count -- count of elements in close states list
    """
    if initial == goal: # начальное состояние равно целевому
        return (True, [], 0, 0)
    
    initial._depth = 0
    
    # список открытых состояний      
    open_states = PriorityQueue()
    open_states.put(evaluator(initial), 0, initial)
    
    # список закрытых состояний
    closed_states = set()

    while not open_states.empty():
        # извлекаем первый элемент из (очереди) списка открытых состояний
        _, _, current = open_states.get()

        # добавляем его в закрытые
        closed_states.add(current)

        if current == goal:
            # сформировать список ходов до текущего
            path = [current]
            return (True, path, len(open_states), len(closed_states))

        # генерируем список возможных ходов
        for move in current.get_moves():
            # если этот ход не в списке закрытых состояний
            if not move in closed_states:               
                move.depth = current.depth + 1
                # по алгоритму надо проверить, есть ли move 
                # в очереди открытых состояний
                # извлечь его и сравнить с приоритетом 
                # текущего состояния move
                # если приоритет move меньше,
                # то заменить в очереди состояние на move
                # но если не искать и не удалять, что мы просто потом 
                # извлекая очередное состояние из очереди будем видеть,
                # что оно уже есть в закрытых
                open_states.put(evaluator(move), 0, move)

    return (False, [], -1, -1) # нет решения, возвращаем пустой список

if __name__ == '__main__':
    initial = state([[8,1,3],[2,4,5],[state.space,7,6]], 0)
    goal = state([[1,2,3],[8,state.space,4],[7,6,5]])

    f = lambda: a_search(initial, goal, weak_evaluator)          

    res = f()        
    print('has decision  :', res[0])    
    print('open states   :', res[2])    
    print('closed states :', res[3])        
    print('result path   :')    
    for m in res[1]:
        print(m)
        
    t = Timer(f)
    print("Time = ", t.timeit(number=100))
