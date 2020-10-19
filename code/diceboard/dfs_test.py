from dice import state, goal
from dfs import *
from timeit import Timer

initial = state(None, [], 0)

print(f"Initial state: {initial}")

f = lambda: dfs(initial, goal, 10)          

res = f()
print('has decision  :', res[0])    
print('open states   :', res[2])    
print('closed states :', res[3])        
print('result path   :')    
for m in res[1]:
    print(m)
    
t = Timer(f)
print("Time = ", t.timeit(number=1))