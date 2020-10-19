from dice import state, goal, evaluator
from asearch import a_search
from timeit import Timer

initial = state(parent=None, dice_places=[], depth=0)   

f = lambda: a_search(initial, goal, evaluator)

res = f()        
print('has decision  :', res['solved'])    
print('open states   :', res['openstates'])    
print('closed states :', res['closedstates'])
print('result path   :')    
for m in res['path']:
    print(m)
    
t = Timer(f)
print("Time = ", t.timeit(number=1))

print(m)
