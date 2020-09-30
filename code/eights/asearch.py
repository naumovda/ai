from queue import PriorityQueue

# Fair Evaluator ::= P(h)
#   P(h) - сумма манхеттенских расстояний между ячейками текущего состояния и целевым
def fair_evaluator(state, goal):
    sum = 0
    for dice in range(1, state.size**2):
        x1, y1 = state.places[state.data[dice]]
        x2, y2 = state.places[goal.data[dice]]
        sum += abs(x2-x1) + abs(y2-y1)
    return sum       

# Good Evaluator ::= P(h)+3*S(h)
#   P(h) - сумма манхеттенских расстояний между всеми ячейками
#   S(h) - для каждой плитки, 
#             0 - если за ней идет корректный приемник
#             2 - в противном случае 
#             1 - если это плитка в центре 
def good_evaluator(state, goal):
    cells = [(0,1), (1, 2), (2, 5), (5, 8), (8, 7), (7, 6), (6, 3), (3, 0)]
    anc = lambda x: 1 if x == 8 else x+1

    s = 0
    # для каждой кости
    for dice in range(1, state.size**2):
        if not (state.data[dice], state.data[anc(dice)]) in cells: 
            s += 2        
    if state.data[state.space] != 4: 
        s += 1
    return s*3 + fair_evaluator(state, goal)

# Weak Evaluator ::= N(h)
#   N(h) - количество плиток не на своем месте
def weak_evaluator(state, goal):
    # count = 0
    # for dice in range(1, state.size**2):
    #     if state.data[dice] != goal.data[dice]: 
    #         count += 1
    # return count
    return sum([1 for dice in range(1, state.size**2) if state.data[dice] != goal.data[dice]]) 

# Bad Evaluator ::= |D(h) - 16|
#   D(h) - сумма разностей значений противоположных (относительно центра) плиток 
#          для текущего состояния
#   G(h) - сумма разностей значений противоположных (относительно центра) плиток 
#          для целевого состояния     
def bad_evaluator(state, goal):
    f = lambda d: d[3]+d[6]+d[7]+d[8]-d[5]-d[2]-d[1]-d[0] 
    d = [0 for _ in range(state.size**2)]
    for dice in range(1, state.size**2):
        d[state.data[dice]] = dice 
    print(d)
    g = [goal.data[dice]  for dice in range(0, state.size**2)]
    print(g)
    return abs(f(d)-f(g))

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
        _, current = open_states.get()

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
    from eight import state 
    from timeit import Timer

    initial = state(None, [8,1,3,0,4,5,2,7,6])   
    goal = state(None, [1,2,3,8,0,4,7,6,5])

    print('fair =', fair_evaluator(initial, goal))
    print('good =', good_evaluator(initial, goal))    
    print('weak =', weak_evaluator(initial, goal))
    print('bad  =', bad_evaluator(initial, goal))

    f = lambda: a_search(initial, goal, fair_evaluator)
    # f = lambda: a_search(initial, goal, good_evaluator)
    # f = lambda: a_search(initial, goal, weak_evaluator)
    # f = lambda: a_search(initial, goal, bad_evaluator)

    res = f()        
    print('has decision  :', res['solved'])    
    print('open states   :', res['openstates'])    
    print('closed states :', res['closedstates'])
    print('result path   :')    
    for m in res['path']:
        print(m)
        
    # t = Timer(f)
    # print("Time = ", t.timeit(number=1))
