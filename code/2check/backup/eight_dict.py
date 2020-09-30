class state:
    space = 0 # код для обозначения пустого поля
    size = 3
    
    # соответствие номеров ячеек и их координат
    places = {\
        1:(0,0), 2:(0,1), 3:(0,2), \
        4:(1,0), 5:(1,1), 6:(1,2), \
        7:(2,0), 8:(2,1), 9:(2,2)  \
        }

    # список допустимых ходов для ячейки с заданным номером
    moves = {\
        1: [2, 4],
        2: [1,3,5],
        3: [2, 6],
        4: [1, 5, 7],
        5: [2, 4, 6, 8],
        6: [3, 5, 9],
        7: [4, 8],
        8: [5, 7, 9],
        9: [6, 8]
        }

    @staticmethod
    def get_key(items, elem):
        for key, item in items.items():
            if item == elem:
                return key
        return None

    def __init__(self, data, depth=0):        
        if isinstance(data, dict):
            self._data = data
        else:
            if isinstance(data, list):
                self._data = {}
                for row in range(self.size):
                    for col in range(self.size):
                        self._data[data[row][col]] = 3*row + col + 1

        self._depth = depth
  
    def __hash__(self):
        k = 0
        d = 1
        for key in sorted(self._data.keys()):
            k += d * self._data[key]
            d *= 10        
        return k

    def __str__(self):
        s = ""
        l = [[0,0,0],[0,0,0],[0,0,0]]
        for key, item in self._data.items():
            row, col = self.places[item]
            l[row][col] = key
        for row in l:
            s += f"{row}"            
        return s
    
    def __eq__(self, other):
        return self._data == other._data

    @property
    def depth(self):
        return self._depth

    @depth.setter
    def set_depth(self, value):
        self._depth = value if value >= 0 else 0

    def get_moves(self):
        d = self._data # для сокращения записи 
        new_moves = []
        for position in self.moves[d[0]]: # получаем список ходов для пустого поля
            key = state.get_key(d, position)
            new_state = d.copy()
            new_state[0] = d[key] 
            new_state[key] = d[0]
            new_moves.append(state(new_state, self.depth+1))
        return new_moves

if __name__ == "__main__":
    d = {1:2, 2:4, 3:3, 4:5, 5:6, 6:9, 7:8, 8:1, 0:7}    
    s = state(d, 0)    
    print(s)

    moves = s.get_moves()
    for move in moves:
        print(move)