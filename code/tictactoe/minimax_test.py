from xo import state_xo
from minimax import bestmove

# начальное состояние (пустое)
s = state_xo() 

# на три хода (на шесть полуходов) вперед
level = 6 

# первым ходит "X", вторым - "0"
player, opponent = "X", state_xo.opponent["X"]    

# получаем лучший ход
move = bestmove(s, level, player, opponent)

print(f"Best move is: ", move)   
     