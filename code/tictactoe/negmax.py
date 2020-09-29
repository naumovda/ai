nodes = 0 # переменная для расчета статистики

def negmax(state, level, player, opponent):
    ''' Алгоритм поиска лучше хода NegMax
        - state - начальное состояние
        - level - максимальная глубина рекрсии (количество полуходов)
        - original - исходный игрок, для которого считается
                     дерево
        - player - текущий игрок
        - opponent - оппонент
    '''
    
    # инициализируем лучший ход и оценку 
    best_move, best_score = None, None
    
    # получаем список возможных ходов
    moves = state.get_moves(player)

    # накапливаем количество сгенерированных ходов
    global nodes 
    nodes += len(moves)

    # если достигнута максимальная глубина дерева
    # или ходов нет, то рассчитываем оценку
    # при помощи оценочной функции
    if level == 0 or moves == []:
        return None, state.score(player)

    # перебираем последовательно все возможные ходы
    for m in moves:
        state.do_move(m) # выполняем ход
        # вызываем рекурсивно NegMax,
        # уменьшая уровень на 1
        # и меняя местами игрока и оппонента        
        _, score = negmax(state, level-1, opponent, player)
        state.undo_move(m) # отменяем ход

        # меняем знак оценочной функции на противоположный
        if best_score == None or (-1)*score > best_score:
            best_move, best_score =  m, (-1)*score

    return best_move, best_score

def bestmove(state, level, player, opponent):
    ''' Вызов функции MiniMax с начальными значениями
        - state - начальное состояние
        - level - максимальная глубина рекрсии (количество полуходов)
        - player - игрок
        - opponent - оппонент
    '''    
    return negmax(state, level, player, opponent)
