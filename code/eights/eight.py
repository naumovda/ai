from base import base_state

class state(base_state):
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
        field = [[0,0,0],[0,0,0],[0,0,0]]        
        for key, item in self.data.items():
            row, col = self.places[item]
            field[row][col] = key
        return str(field)
    
    def __eq__(self, other):
        return self.data == other.data

    def get_moves(self):
        d = self.data 
        new_moves = []
        # получаем список ходов для пустого поля:
        for position in self.moves[d[0]]: 
            key = state.get_key(d, position)
            new_state = d.copy()
            new_state[0] = d[key] 
            new_state[key] = d[0]
            new_moves.append(state(self, new_state, self.depth+1))
        return new_moves

# Fair Evaluator ::= P(h)
#   P(h) - сумма манхеттенских расстояний между ячейками текущего состояния и целевым
def fair_evaluator(state, goal):
    sum = 0
    for dice in range(1, state.size**2):
        x1, y1 = state.places[state.data[dice]]
        x2, y2 = state.places[goal.data[dice]]
        sum += abs(x2-x1) + abs(y2-y1)
    return sum       

# Good Evaluator ::= P(h)+3*S(h)
#   P(h) - сумма манхеттенских расстояний между всеми ячейками
#   S(h) - для каждой плитки, 
#             0 - если за ней идет корректный приемник
#             2 - в противном случае 
#             1 - если это плитка в центре 
def good_evaluator(state, goal):
    cells = [(0,1), (1, 2), (2, 5), (5, 8), (8, 7), (7, 6), (6, 3), (3, 0)]
    anc = lambda x: 1 if x == 8 else x+1

    s = 0
    # для каждой кости
    for dice in range(1, state.size**2):
        if not (state.data[dice], state.data[anc(dice)]) in cells: 
            s += 2        
    if state.data[state.space] != 4: 
        s += 1
    return s*3 + fair_evaluator(state, goal)

# Weak Evaluator ::= N(h)
#   N(h) - количество плиток не на своем месте
def weak_evaluator(state, goal):
    # count = 0
    # for dice in range(1, state.size**2):
    #     if state.data[dice] != goal.data[dice]: 
    #         count += 1
    # return count
    return sum([1 for dice in range(1, state.size**2) if state.data[dice] != goal.data[dice]]) 

# Bad Evaluator ::= |D(h) - 16|
#   D(h) - сумма разностей значений противоположных (относительно центра) плиток 
#          для текущего состояния
#   G(h) - сумма разностей значений противоположных (относительно центра) плиток 
#          для целевого состояния     
def bad_evaluator(state, goal):
    f = lambda d: d[3]+d[6]+d[7]+d[8]-d[5]-d[2]-d[1]-d[0] 
    d = [0 for _ in range(state.size**2)]
    for dice in range(1, state.size**2):
        d[state.data[dice]] = dice 
    print(d)
    g = [goal.data[dice]  for dice in range(0, state.size**2)]
    print(g)
    return abs(f(d)-f(g))
