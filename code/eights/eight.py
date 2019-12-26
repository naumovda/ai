from copy import deepcopy

class state:
    space = 0 # код для обозначения пустого поля
    size = 3  # размер поля

    def __init__(self, data, depth=0):
        self._data = data
        self._depth = depth
    
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

    def get_moves(self):
        moves = []
        
        #ищем пустое поле
        for i in range(state.size):
            for j in range(state.size):
                if self._data[i][j] == state.space:
                    si, sj = i, j
                    break
        
        # если можно двигать фишку сверху
        if si != 0:
            m = deepcopy(self._data)
            m[si][sj], m[si-1][sj] = m[si-1][sj], state.space            
            moves.append(state(m, self._depth + 1))
        
        # если можно двигать фишку снизу
        if si != state.size-1:
            m = deepcopy(self._data)
            m[si][sj], m[si+1][sj] = m[si+1][sj], state.space
            moves.append(state(m, self._depth + 1))

        # если можно двигать фишку слева
        if sj != 0:
            m = deepcopy(self._data)
            m[si][sj], m[si][sj-1] = m[si][sj-1], state.space
            moves.append(state(m, self._depth + 1))

        # если можно двигать фишку слева
        if sj != state.size-1:
            m = deepcopy(self._data)
            m[si][sj], m[si][sj+1] = m[si][sj+1], state.space
            moves.append(state(m, self._depth + 1))

        return moves
