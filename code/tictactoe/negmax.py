def negmax(state, level, player, opponent):
    best_move, best_score = None, None
    moves = state.get_moves(player)

    if level == 0 or moves == []:
        return None, state.score(player)

    for m in moves:
        state.do_move(m)
        _, score = negmax(state, level-1, opponent, player)
        state.undo_move(m)
        if best_score == None or (-1)*score > best_score:
            best_move, best_score =  m, (-1)*score
    return best_move, best_score

def bestmove(state, level, player, opponent):
    return negmax(state, level, player, opponent)