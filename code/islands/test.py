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
    def __init__(self, key, cell, road_count, roads):
        self.key = key
        self.cell = cell
        self.road_count = road_count
        self.roads = roads

    def __hash__(self):
        return self.key

    # def is_neighbor(self, other):
    #     return self.x==other.x or self.y == other.y

road1 = (0, 3), (6, 3)
road2 = (7, 0), (7, 9)
print(intersept(road1, road2))