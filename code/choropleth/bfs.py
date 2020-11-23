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
    if initial.is_goal():  # начальное состояние равно целевому
        return (True, initial, 0, 0)

    # список открытых состояний
    initial.depth = 0
    open_states = [initial]

    max_depth = 0

    # список закрытых состояний
    closed_states = []

    while open_states != []:
        # извлекаем первый элемент из (очереди) списка открытых состояний
        current = open_states.pop(0)

        if max_depth < current.depth:
            max_depth = current.depth
            print(f'd={current.depth}')

        # добавляем его в закрытые
        closed_states.append(current)

        # генерируем список возможных ходов
        for m in current.get_moves():
            # get_moves() это делает, но вдруг забыли реализовать
            m.depth = current.depth + 1

            # если этот ход не в списке закрытых состояний
            if m not in closed_states:
                if m.is_goal():
                    # сформировать список ходов до текущего
                    return (True, m, len(open_states), len(closed_states))
                open_states.append(m)
    return (False, initial, -1, -1)  # нет решения, возвращаем пустой список

if __name__ == '__main__':
    from state import MapState
    import graphs

    initial = MapState(graphs.TEST_CUT1)
    for move in initial.get_moves():
        print(move.map)

    # _, goal_state, open_count, closed_count = bfs(initial, None)
    # print(goal_state.map)
    # print(f'open states = {open_count} closed = {closed_count}')
