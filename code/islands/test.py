from base import base_state

def mult(ax,ay,bx,by):
    return ax*by-bx*ay

def intersept(road1, road2):
    (x1, y1), (x2, y2) = road1
    (x3, y3), (x4, y4) = road2

    v1 = mult(x4-x3, y4-y3, x1-x3, y1-y3)
    v2 = mult(x4-x3, y4-y3, x2-x3, y2-y3)
    v3 = mult(x2-x1, y2-y1, x3-x1, y3-y1)
    v4 = mult(x2-x1, y2-y1, x4-x1, y4-y1)
    
    return (v1*v2 < 0) and (v3*v4 < 0)

class island:
    def __init__(self, x, y, roads, count=0):
        self.x, self.y = x, y
        self.roads = roads
        self.count = count

    def __neq__(self, other):
        return self.x != other.x or self.y != other.y

    def neighbor(self, other):
        return self.x==other.x or self.y==other.y

    def between(self, other, rest):
        return self.neighbor(other) and self.neighbor(rest) \
            and rest.neighbor(other) and (self.x<=rest.x<=other.x or self.x>=rest.x>=other.x)\
            and (self.y<=rest.y<=other.y or self.y>=rest.y>=other.y)

class state(base_state):
    islands = [
        island(0, 0, 1),
        island(0, 1, 4),
        island(0, 2, 4),
        island(1, 0, 2),
        island(1, 1, 4),
        island(1, 2, 2),
        island(2, 1, 1)
    ]

    neighbors = {key:[] for key in range(len(islands))}
    for key, island in enumerate(islands): 
        for key_other, other in enumerate(islands):
            if key != key_other and island.neighbor(other):                
                flag = False
                for key_rest, rest in enumerate(islands):
                    if key_rest != key and key_rest != key_other \
                        and island.neighbor(rest) and other.neighbor(rest) \
                            and island.between(other, rest):
                        flag = True 
                        break
                if not flag:
                    neighbors[key].append(key_other)

    def __init__(self, parent=None, slots=None, edges=None):
        self.parent = parent
        self.edges = edges if edges else []
        if slots:
            self.slots = slots
        else:
            self.slots = [item.roads for item in state.islands]

    def __eq__(self, other):
        return self.slots == other.slots

    def __hash__(self):
        return hash(tuple(self.slots))

    def __str__(self):
        return str(self.slots)

    def get_moves(self):
        moves = []
        for key, _ in enumerate([item for item in self.slots if item]):
            # по списку соседей
            for item in state.neighbors[key]:
                # если еще есть свободные слоты           
                if self.slots[item]:                               
                    # todo: проверить, нет ли пересечений с ребрами

                    # добавить в список ходов дорогу,
                    # изменить количество слотов в соединенных городах                  
                    slot = self.slots.copy()
                    slot[key] -= 1
                    slot[item] -=1
                    edges = self.edges.copy()
                    edges.append((key, item))                    
                    s = state(self, slot, edges)
                    if not s in moves:
                        moves.append(s)
        return moves

def evaluator(state, goal):
    return sum([1 for key, item in enumerate(state.slots) if state.slots[key]!=goal.slots[key]])

if __name__ == "__main__":
    # road1 = (0, 3), (6, 3)
    # road2 = (7, 0), (7, 9)
    # print(intersept(road1, road2))

    s = state()
    print(s)
    print(f"n = {s.neighbors}")
    print('---')

    goal = state()
    goal.slots = [0 for _ in range(len(goal.islands))]

    for m in s.get_moves():
        print(f"move: {m}, score: {evaluator(m, goal)}")
