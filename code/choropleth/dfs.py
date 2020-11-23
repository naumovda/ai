def dfs(initial, max_depth):
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
    if initial.is_goal():  # начальное состояние равно целевому
        return (True, initial, 0, 0)

    # список открытых состояний
    initial._depth = 0
    open_states = [initial]

    # список закрытых состояний
    closed_states = []

    i = 0

    while open_states != []:
        # извлекаем последний элемент из списка открытых состояний
        current = open_states.pop()

        i += 1

        if i % 100 == 0:
            print(f"d={current.depth}, open={len(open_states)}",
                  f"close={len(closed_states)}")
        # if current._depth > max_depth:
        #     max_depth = current._depth
        #     print(f'd={current._depth}')

        # добавляем его в закрытые
        closed_states.append(current)
        # генерируем список возможных ходов
        for m in current.get_moves():
            # если этот ход не в списке закрытых состояний
            if m not in closed_states:
                if m.is_goal():
                    return (True, m, len(open_states), len(closed_states))
                # if current._depth <= max_depth:
                m._depth = current._depth + 1
                open_states.append(m)
    return (False, None, -1, -1)  # нет решения, возвращаем пустой список


if __name__ == '__main__':
    from state import MapState
    import graphs

    initial = MapState(graphs.USA_CUT)

    _, goal_state, open_count, closed_count = dfs(initial, 50)
    print(goal_state.map)
    print(f'open states = {open_count} closed = {closed_count}')
