def bfs(initial, goal):
    """breadth search function

    Keyword arguments:
    initial -- inital state, start of the search tree
    goal -- final state, we want to find path

    Return: a tuple (is_find, path, open_state_count, close_state_count)
    is_find -- does path has been found
    path -- a path to final state, a solution 
    open_state_count -- count of elements in open states list
    close_state_count -- count of elements in close states list
    """
    if initial == goal: # начальное состояние равно целевому
        return (True, [], 0, 0)

    # список открытых состояний
    initial._depth = 0
    open_states = [initial]
    
    # список закрытых состояний
    closed_states = []

    while open_states != []:
        # извлекаем первый элемент из (очереди) списка открытых состояний
        current = open_states.pop(0)
        # добавляем его в закрытые
        closed_states.append(current)

        # генерируем список возможных ходов
        for m in current.get_moves():
            # get_moves() это делает, но вдруг забыли реализовать
            m._depth = current._depth + 1

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
                open_states.append(m)
    return (False, [], -1, -1) # нет решения, возвращаем пустой список
