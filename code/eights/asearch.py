from queue import PriorityQueue
from eight_parent import state 
from timeit import Timer

# Fair Evaluator ::= P(h)
#   P(h) - сумма манхеттенских расстояний между ячейками текущего состояния и целевым
def fair_evaluator(state, goal):
    size = state.size    
    src = [(0,0) for i in range(size*size)]
    dst = [(0,0) for i in range(size*size)]

    for row in range(size):
        for col in range(size):
            src[state._data[row*size+col]] = (row, col)
            dst[goal._data[row*size+col]] = (row, col)

    dist = 0
    for idx in range(1, len(src)):
        dist += abs(src[idx][0]-dst[idx][0]) + abs(src[idx][1]-dst[idx][1])

    return dist

# Good Evaluator ::= P(h)+3*S(h)
#   P(h) - сумма манхеттенских расстояний между всеми ячейками
#   S(h) - для каждой плитки, 
#             0 - если за ней идет корректный приемник
#             2 - в противном случае 
#             1 - если это плитка в центре 
def good_evaluator(state, goal):
    cells = [(0,1), (1, 2), (2, 5), (5, 8), (8, 7), (7, 6), (6, 3), (3, 0)]
    s = 0
    for first, second in cells:
        if state._data[first] == 8:
            if state._data[second] != 1:
                s += 2
        elif state._data[first] != 0:
            if state._data[first] + 1 != state._data[second]:
                s += 2
    if state._data[4] != state.space:
        s += 1
    return fair_evaluator(state, goal) + 3*s

# Weak Evaluator ::= N(h)
#   N(h) - количество плиток не на своем месте
def weak_evaluator(state, goal):
    count = 0
    for k in range(len(state._data)):
        if state._data[k] != goal._data[k] and state._data[k] != 0:
            count += 1
    return count 

# Bad Evaluator ::= |D(h) - 16|
#   D(h) - сумма разностей значений противоположных (относительно центра) плиток для текущего состояния
#   G(h) - сумма разностей значений противоположных (относительно центра) плиток для целевого состояния      
def bad_evaluator(state, goal):
    d = state._data[3] - state._data[5] \
        + state._data[6] - state._data[2] \
        + state._data[7] - state._data[1] \
        + state._data[8] - state._data[0]
    g = goal._data[3] - goal._data[5] \
        + goal._data[6] - goal._data[2] \
        + goal._data[7] - goal._data[1] \
        + goal._data[8] - goal._data[0]
    # print(d)
    # print(g)
    # sumDistance += node.cell(1, 0) - node.cell(1, 2); 3 - 5
	# sumDistance += node.cell(2, 0) - node.cell(0, 2); 6 - 2
	# sumDistance += node.cell(2, 1) - node.cell(0, 1); 7 - 1
	# sumDistance += node.cell(2, 2) - node.cell(0, 0); 8 - 0
    return abs(d-g)

def a_search(initial, goal, evaluator):
    """A*search algorythm function

    Keyword arguments:
    initial -- inital state, start of the search tree
    goal -- final state, we want to find path
    evaluator -- a function to calculate priority for queue

    Return: a dict (solved, path, open state count, close_state_count)
    is_find -- does path has been found
    path -- a path to final state, a solution 
    open_state_count -- count of elements in open states list
    close_state_count -- count of elements in close states list
    """
    if initial == goal: # начальное состояние равно целевому
        return {'solved':True, 'path': [initial], 'openstates':1, 'closedstates':0}
    
    initial._depth = 0
    
    # список открытых состояний      
    open_states = PriorityQueue()
    open_states.put((evaluator(initial, goal), initial))
    
    # список закрытых состояний
    closed_states = set()

    while not open_states.empty():
        # извлекаем первый элемент из (очереди) списка открытых состояний
        value, current = open_states.get()

        print(current, ' v = ', value, 'd = ', current._depth)

        # добавляем его в закрытые
        closed_states.add(current)

        if current == goal:
            # сформировать список ходов до текущего
            item = current
            path = [item]
            while item._parent != None:
                item = item._parent
                path.append(item)                
            path.reverse()
            return {'solved':True, 
                'path': path, 
                'openstates':len(open_states.queue), 
                'closedstates':len(closed_states)
                }

        # генерируем список возможных ходов
        for move in current.get_moves():
            # если этот ход не в списке закрытых состояний
            if not move in closed_states:               
                move._depth = current._depth + 1
                # по алгоритму надо проверить, есть ли move 
                # в очереди открытых состояний
                # извлечь его и сравнить с приоритетом 
                # текущего состояния move
                # если приоритет move меньше,
                # то заменить в очереди состояние на move
                # но если не искать и не удалять, что мы просто потом 
                # извлекая очередное состояние из очереди будем видеть,
                # что оно уже есть в закрытых
                open_states.put((evaluator(move, goal) + move.depth, move))

    # нет решения, возвращаем пустой список
    return {'solved':False, 
        'path': [], 
        'openstates':len(open_states.queue), 
        'closedstates':len(closed_states)
        }

if __name__ == '__main__':
    # initial = state(None, [2,1,6,4,0,8,7,5,3], 4, 0)
    # initial = state(None, [1,4,8,7,3,0,6,5,2], 5, 0)
    initial = state(None, [8,1,3,0,4,5,2,7,6], 3, 0)
    goal = state(None, [1,2,3,8,0,4,7,6,5], 4)

    # 1,4,8
    # 7,3,0
    # 6,5,2
    # 1 +2
    # 4 +2
    # 8 +0
    # 

    print('fair =', fair_evaluator(initial, goal))
    print('good =', good_evaluator(initial, goal))    
    print('weak =', weak_evaluator(initial, goal))
    print('bad  =', bad_evaluator(initial, goal))

    # fair_evaluator
    # good_evaluator
    # weak_evaluator
    # bad_evaluator
    # f = lambda: a_search(initial, goal, fair_evaluator)
    # f = lambda: a_search(initial, goal, good_evaluator)
    f = lambda: a_search(initial, goal, weak_evaluator)
    # f = lambda: a_search(initial, goal, bad_evaluator)

    res = f()        
    print('has decision  :', res['solved'])    
    print('open states   :', res['openstates'])    
    print('closed states :', res['closedstates'])
    print('result path   :')    
    for m in res['path']:
        print(m)
        
    t = Timer(f)
    print("Time = ", t.timeit(number=1))
