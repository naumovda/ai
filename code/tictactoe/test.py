from xo import state_xo
# from minimax import bestmove
from negmax import bestmove

if __name__ == "__main__":
    s = state_xo()
    level = 6
    player, opponent = "X", state_xo.opponent["X"]    
    
    move = bestmove(s, level, player, opponent)

    print(f"Best move is: ", move)