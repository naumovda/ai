def dfs(initial, goal, max_depth):
    """Deep search function

    Keyword arguments:
        initial -- inital state, start of the search tree
        goal -- final state, we want to find path
        max_depth -- maximum of search tree level

    Return: a tuple (is_find, path, open_state_count, close_state_count)
        is_find -- does path has been found
        path -- a path to final state, a solution
        open_state_count -- count of elements in open states list
        close_state_count -- count of elements in close states list
    """
    if initial == goal: # начальное состояние равно целевому
        return (True, [], 0, 0)

    # список открытых состояний
    initial.depth = 0
    open_states = [initial]
    
    # список закрытых состояний
    closed_states = []

    while open_states != []:
        # извлекаем последний элемент из списка открытых состояний
        current = open_states.pop()
        # добавляем его в закрытые
        closed_states.append(current)
        # генерируем список возможных ходов
        for m in current.get_moves():
            # если этот ход не в списке закрытых состояний
            if not m in closed_states:                 
                if m == goal:
                    # сформировать список ходов до текущего
                    path = [m]
                    s = m.parent
                    while s:
                        path.append(s)
                        s = s.parent
                    path.reverse()                            
                    return (True, path, len(open_states), len(closed_states))
                if current.depth <= max_depth:                    
                    open_states.append(m)
    return (False, [], -1, -1) # нет решения, возвращаем пустой список

if __name__ == '__main__':
    from eight import state
    from timeit import Timer

    initial = state(None, [[8,1,3],[2,4,5],[state.space,7,6]], 0)
    goal = state(None, [[1,2,3],[8,state.space,4],[7,6,5]])

    print(f"Initial state: {initial}")
    print(f"Goal state: {goal}")

    f = lambda: dfs(initial, goal, 20)          

    res = f()        
    print('has decision  :', res[0])    
    print('open states   :', res[2])    
    print('closed states :', res[3])        
    print('result path   :')    
    for m in res[1]:
        print(m)
        
    t = Timer(f)
    print("Time = ", t.timeit(number=1))
