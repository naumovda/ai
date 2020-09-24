from xo import state_xo
from minimax import bestmove

if __name__ == "__main__":
    s = state_xo()
    level = 4
    player, opponent = "X", state_xo.opponent["X"]    
    
    move = bestmove(s, level, player, opponent)

    print(f"Best move is: ", move)