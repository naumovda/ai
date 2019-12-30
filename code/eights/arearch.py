from queue import PriorityQueue
from timeit import Timer
from eight import state

def weak_evaluator(state, goal):
    count = 0
    for i in range(state.size):
        for j in range(state.size):
            if state._data[i][j] != goal._data[i][j]:
                count += 1
    return state._depth + count 

def a_search(initial, goal, priority):
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
    open_states.put(initial, priority(initial))
    
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
        for m in current.get_moves():
            # если этот ход не в списке закрытых состояний
            if m in closed_states:                 
                # ищем, есть ли в open_states состояние m
                if m in open_states:
                    
                # сравниваем приоритеты

            else:
                # m._depth = current._depth + 1
                open_states.put(m, priority(m))

    return (False, [], -1, -1) # нет решения, возвращаем пустой список

if __name__ == '__main__':
    initial = state([[8,1,3],[2,4,5],[state.space,7,6]], 0)
    goal = state([[1,2,3],[8,state.space,4],[7,6,5]])

    f = lambda: bfs_set(initial, goal)          

    res = f()        
    print('has decision  :', res[0])    
    print('open states   :', res[2])    
    print('closed states :', res[3])        
    print('result path   :')    
    for m in res[1]:
        print(m)
        
    t = Timer(f)
    print("Time = ", t.timeit(number=100))
