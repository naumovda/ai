from eight import state

s = state(None, [1, 2, 3, 4, 5, 6, 7, 8, 0])    
print(f"Initial state:{s}")

moves = s.get_moves()
for move in moves:
    print(f"Possible moves: {move}")