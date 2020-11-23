import dfs
import bfs
from graphs import TEST_CUT
import state

geo = state.GeoMap(TEST_CUT)

initial = state.MapState(geo)

print(initial.painting)
print(f"hash = {hash(initial)}")

moves = initial.get_moves()
for move in moves:
    print(move.painting)
    print(f"hash = {hash(move)}")

# _, goal, _, _ = dfs.dfs(initial, None)
_, goal, _, _ = bfs.bfs(initial, None)

print(goal.painting)