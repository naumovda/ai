from xo import state_xo

# количество узлов для расчета статистики
nodes = 0

def alpha_beta(state, level, player, opponent, low, high):
    ''' Алгоритм AlphaBeta-отсечения (на основе negmax)
        - state - начальное состояние
        - level - максимальная глубина рекрсии (количество полуходов)
        - player - текущий игрок
        - opponent - оппонент
        - low - нижняя граница для отсечения         
        - high - верхняя граница для отсечения         
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
        # выполняем ход
        state.do_move(m)

        # вызываем рекурсивно NegMax,
        # уменьшая уровень на 1
        # и меняя местами игрока и оппонента          
        _, score = alpha_beta(state, level-1, opponent, player, -high, -low)

        # отменяем ход
        state.undo_move(m)

        # если текущий ход лучше best_score:
        if best_score == None or -1*score > best_score:
            # устанавливаем новое значение нижней границы
            low = -1*score
            # лучшим ходом считаем текущий
            best_move, best_score =  m, -1*score

        # если выполняется условие для AlphaBeta-отсечения
        if low >= high:
            return best_move, best_score        

    return best_move, best_score  

def bestmove(state, level, player, opponent):
    ''' Вызов функции AlphaBet с начальными значениями
        - state - начальное состояние
        - level - максимальная глубина рекрсии (количество полуходов)
        - player - игрок
        - opponent - оппонент
        - low = -infinity
        - high = +infinity
    '''     
    return alpha_beta(state, level, player, opponent, \
        -state_xo.infinity, state_xo.infinity)  
