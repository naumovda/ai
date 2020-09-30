from copy import deepcopy

class state:
    space = 0 # код для обозначения пустого поля
    size = 3  # размер поля

    def __init__(self, parent, data, free, depth=0):
        self._parent = parent
        self._data = data
        self._free = free
        self._depth = depth
  
    def __hash__(self):
        k = 0
        d = 1
        for item in self._data:
            k += d * item
            d *= 10 # size*size
        return k

    def __str__(self):
        return str(self._data)
    
    def __eq__(self, other):
        if other == None:
            return False
        return self._data == other._data

    def __lt__(self, other):
        if other == None:
            return False
        return self._depth < other._depth

    @property
    def depth(self):
        return self._depth

    @depth.setter
    def set_depth(self, value):
        if value < 0:
            self._depth = 0
        else:
            self._depth = value

    def swap_cells(self, cell1, cell2, depth):
        m = deepcopy(self._data)
        m[cell1] = self._data[cell2]
        m[cell2] = self._data[cell1]
        if self._free == cell1:
            return state(self, m, cell2, depth)
        else:
            return state(self, m, cell1, depth)

    def swap_up(self, depth): 
        return self.swap_cells(self._free, self._free-3, depth)

    def swap_down(self, depth):
        return self.swap_cells(self._free, self._free+3, depth)

    def swap_left(self, depth):
        return self.swap_cells(self._free, self._free-1, depth)  

    def swap_right(self, depth):
        return self.swap_cells(self._free, self._free+1, depth)        

    def get_moves(self):
        moves = []  
        if not self._free in [0,1,2]:
            moves.append(self.swap_up(self._depth + 1))
        if not self._free in [0,3,6]:
            moves.append(self.swap_left(self._depth + 1))
        if not self._free in [2,5,8]:
            moves.append(self.swap_right(self._depth + 1))
        if not self._free in [6,7,8]:
            moves.append(self.swap_down(self._depth + 1))
        return moves
