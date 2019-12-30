from queue import PriorityQueue

prio_queue = PriorityQueue()

prio_queue.put((2, 0, 'super blah'))
prio_queue.put((1, 0, 'Some thing'))
prio_queue.put((3, 0, 'This thing would come after Some Thing if we sorted by this text entry'))
prio_queue.put((5, 0, 'blah'))

item = (1, 0, 'Some thing')
# prio_queue.

if item in prio_queue.queue:
    print('yes')
else:
    print('no')

while not prio_queue.empty():
    item = prio_queue.get()
    print('%s.%s - %s' % item)