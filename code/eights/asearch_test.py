from eight import state, fair_evaluator, good_evaluator, weak_evaluator, bad_evaluator
from asearch import a_search
from timeit import Timer

initial = state(None, [8,1,3,0,4,5,2,7,6], 0)   
goal = state(None, [1,2,3,8,0,4,7,6,5])

print('fair =', fair_evaluator(initial, goal))
print('good =', good_evaluator(initial, goal))    
print('weak =', weak_evaluator(initial, goal))
# print('bad  =', bad_evaluator(initial, goal))

f = lambda: a_search(initial, goal, fair_evaluator)
# f = lambda: a_search(initial, goal, good_evaluator)
# f = lambda: a_search(initial, goal, weak_evaluator)
# f = lambda: a_search(initial, goal, bad_evaluator)

res = f()        
print('has decision  :', res['solved'])    
print('open states   :', res['openstates'])    
print('closed states :', res['closedstates'])
print('result path   :')    
for m in res['path']:
    print(m)
    
t = Timer(f)
print("Time = ", t.timeit(number=1))
