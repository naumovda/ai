class state:
    space = 0 # код для обозначения пустого поля
    size = 3
    
    # соответствие номеров ячеек и их координат
    places = {
        1:(0,0), 2:(0,1), 3:(0,2), 
        4:(1,0), 5:(1,1), 6:(1,2), 
        7:(2,0), 8:(2,1), 9:(2,2)  
        }

    # список допустимых ходов для ячейки с заданным номером
    moves = {
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

    def __init__(self, parent, data, depth=0):        
        if isinstance(data, dict):
            self.data = data
        else:
            if isinstance(data, list):
                self.data = {}
                for idx, cell in enumerate(data):
                    self.data[cell] = idx+1
        self.parent = parent
        self.depth = depth
  
    def __hash__(self):
        k = 0
        d = 1
        for key in sorted(self.data.keys()):
            k += d * self.data[key]
            d *= 10        
        return k

    def __str__(self):
        s = ""
        l = [[0,0,0],[0,0,0],[0,0,0]]
        for key, item in self.data.items():
            row, col = self.places[item]
            l[row][col] = key
        for row in l:
            s += f"{row}"            
        return s
    
    def __eq__(self, other):
        return self.data == other.data

    def get_moves(self):
        d = self.data # для сокращения записи 
        new_moves = []
        for position in self.moves[d[0]]: # получаем список ходов для пустого поля
            key = state.get_key(d, position)
            new_state = d.copy()
            new_state[0] = d[key] 
            new_state[key] = d[0]
            new_moves.append(state(self, new_state, self.depth+1))
        return new_moves
