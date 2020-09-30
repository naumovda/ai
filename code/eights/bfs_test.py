from eight import state
from bfs import *
from timeit import Timer

initial = state(None, [[8,1,3],[2,4,5],[state.space,7,6]], 0)
goal = state(None, [[1,2,3],[8,state.space,4],[7,6,5]])

print(initial)

f = lambda: bfs(initial, goal)          

res = f()        
print('has decision  :', res[0])    
print('open states   :', res[2])    
print('closed states :', res[3])        
print('result path   :')    
for m in res[1]:
    print(m)
    
t = Timer(f)
print("Time = ", t.timeit(number=100))
