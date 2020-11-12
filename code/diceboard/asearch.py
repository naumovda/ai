from queue import PriorityQueue

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
    if goal(initial): # начальное состояние равно целевому
        return {'solved':True, 'path': [initial], 'openstates':1, 'closedstates':0}
    
    initial.depth = 0
    
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

        print(f'd: {current.depth} o: {len(open_states.queue)}, c: {len(closed_states)}, s:{evaluator(current, None)}')

        if goal(current):
            # сформировать список ходов до текущего
            item = current
            path = [item]
            while item.parent is not None:
                item = item.parent
                path.append(item)                
            path.reverse()

            return {
                'solved':True, 
                'path': path, 
                'openstates':len(open_states.queue), 
                'closedstates':len(closed_states)
                }

        # генерируем список возможных ходов
        for move in current.get_moves():
            # если этот ход не в списке закрытых состояний
            if not move in closed_states:               
                move.depth = current.depth + 1
                open_states.put((evaluator(move, goal) + move.depth, move))

    # нет решения, возвращаем пустой список
    return {
        'solved':False, 
        'path': [], 
        'openstates':len(open_states.queue), 
        'closedstates':len(closed_states)
        }
