from queue import PriorityQueue
from eight_list import state

st1 = state([8,1,3,2,4,5,0,7,6], 6, 0)
st2 = state([1,2,3,8,0,4,7,6,5], 4, 0)
st3 = state([1,0,3,8,2,4,7,6,5], 1, 0)

prio_queue = PriorityQueue()

prio_queue.put((2, st1))
prio_queue.put((1, st2))
prio_queue.put((2, st3))

while not prio_queue.empty():
    item = prio_queue.get()
    print('%s - %s' % item)