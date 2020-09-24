def minimax (state, level, original, player, opponent):
    best_move, best_score = None, None
    moves = state.get_moves(player)

    if level == 0 or moves == []:
        return None, state.score(player)

    for m in moves:
        state.do_move(m)
        _, score = minimax(state, level-1, original, opponent, player)
        state.undo_move(m)
        if player == original:
            if best_score == None or score > best_score:
                best_move, best_score =  m, score
        else:
            if best_score == None or score < best_score:
                best_move, best_score =  m, score
    return best_move, best_score

def bestmove(state, level, player, opponent):
    return minimax(state, level, player, player, opponent)