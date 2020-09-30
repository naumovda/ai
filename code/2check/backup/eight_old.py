from copy import deepcopy

class state:
    space = 0 # код для обозначения пустого поля
    size = 3  # размер поля

    def __init__(self, data, depth=0):
        self._data = data
        self._depth = depth

        #ищем пустое поле
        self._row = None
        self._col = None
        for i in range(state.size):
            for j in range(state.size):
                if self._data[i][j] == state.space:
                    self._row = i
                    self._col = j
                    break
  
    def __hash__(self):
        k = self._data[0][0] + 10*self._data[0][1] + 100*self._data[0][2] \
            + 1000*self._data[1][0] + 10000*self._data[1][1] + 100000*self._data[1][2] \
            + 1000000*self._data[2][0] + 10000000*self._data[2][1] + 100000000*self._data[2][2]
        return k

    def __str__(self):
        s = ""
        for row in self._data:
            s += f"{row}"
            # s += f"{row}\n" # для вывода построчно
        return s
    
    def __eq__(self, other):
        return self._data == other._data

    @property
    def depth(self):
        return self._depth

    @depth.setter
    def set_depth(self, value):
        if value < 0:
            self._depth = 0
        else:
            self._depth = value

    def swap_cells(self, row1, col1, row2, col2, depth):
        m = deepcopy(self._data)
        m[row1][col1], m[row2][col2] = m[row2][col2], m[row1][col1]
        return state(m, depth)

    def swap_up(self, depth): 
        return self.swap_cells(self._row, self._col, self._row-1, self._col, depth)

    def swap_down(self, depth):
        return self.swap_cells(self._row, self._col, self._row+1, self._col, depth)

    def swap_left(self, depth):
        return self.swap_cells(self._row, self._col, self._row, self._col-1, depth)  

    def swap_right(self, depth):
        return self.swap_cells(self._row, self._col, self._row, self._col+1, depth)        

    def get_moves(self):
        moves = []     
       
        # если можно двигать фишку сверху
        if self._row != 0:
            moves.append(self.swap_up(self._depth + 1))

        # если можно двигать фишку слева
        if self._col != 0:
            moves.append(self.swap_left(self._depth + 1))

        # если можно двигать фишку справа
        if self._col != state.size-1:
            moves.append(self.swap_right(self._depth + 1))

        # если можно двигать фишку снизу
        if self._row != state.size-1:
            moves.append(self.swap_down(self._depth + 1))

        return moves
