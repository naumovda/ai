def minimax (state, level, original, player, opponent):
    ''' Алгоритм поиска лучше хода MiniMax
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

    # если достигнута максимальная глубина дерева
    # или ходов нет, то рассчитываем оценку
    # при помощи оценочной функции
    if level == 0 or moves == []:
        return None, state.score(player)

    # перебираем последовательно все возможные ходы
    for m in moves:
        state.do_move(m) # выполняем ход
        # вызываем рекурсивно MiniMax,
        # уменьшая уровень на 1
        # и меняя местами игрока и оппонента
        _, score = minimax(state, level-1, original, opponent, player)        
        state.undo_move(m) # отменяем ход

        # для уровня Max выбираем узел с максимальной оценкой
        if player == original:
            if best_score == None or score > best_score:
                best_move, best_score =  m, score
        else:
        # для уровня Min выбираем узел с минимальной оценкой            
            if best_score == None or score < best_score:
                best_move, best_score =  m, score

    return best_move, best_score

def bestmove(state, level, player, opponent):
    ''' Вызов функции MiniMax с начальными значениями
        - state - начальное состояние
        - level - максимальная глубина рекрсии (количество полуходов)
        - player - игрок
        - opponent - оппонент
    '''    
    return minimax(state, level, player, player, opponent)
