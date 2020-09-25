from base import state

# Класс для описания состояния - 
#   игровой ситуации при игре в крестики-нолики 3х3. 
class state_xo(state):
    '''Значение бесконечности для оценочной функции'''
    infinity = 100

    '''Возможный набор координат столбцов и строк'''
    lines = [
        [(0,0), (0,1), (0,2)], [(1,0), (1,1), (1,2)],
        [(2,0), (2,1), (2,2)], [(0,0), (1,0), (2,0)],
        [(0,1), (1,1), (2,1)], [(0,2), (1,2), (2,2)],
        [(0,0), (1,1), (2,2)], [(0,2), (1,1), (2,0)]]

    '''список игроков - крестик и нолик'''
    players = ["X", "0"]
    
    '''противники'''
    opponent = {"X":"0", "0":"X"}

    '''инициализация игрового состояния'''    
    def __init__(self, value=None):
        # если value==None, то генерируем
        # пустое поле
        if value:
            self.value = value
        else: 
            # иначе создаем список 
            # [[None, None, None]
            #  [None, None, None]
            #  [None, None, None]]
            self.value = [[None for _ in range(3)] for _ in range(3)]              

    '''представления состояния в виде строки'''
    def __str__(self):
        return str(self.value)

    '''выполнение хода'''    
    def do_move(self, move):
        # ход представляет собой кортеж
        # (row, col, player)
        # где
        # - row, col - координаты клетки, 
        #              в которую ставится символ
        # - player - игрок, который делает ход
        row, col, player = move
        self.value[row][col] = player        

    '''отмена хода'''    
    def undo_move(self, move):
        # ход представляет собой кортеж
        # (row, col, player)
        # где
        # - row, col - координаты клетки, 
        #              в которую ставится символ
        # - player - игрок, который делает ход        
        row, col, _ = move
        # при отмене хода мы ставим в ячейку
        # пустое значение
        self.value[row][col] = None

    '''проверка на то, что ситуация выигрышная''' 
    '''player - игрок, которого проверяем'''
    def is_win(self, player):
        # по всем строкам, столбцам и диагоналям
        for line in state_xo.lines:                                
            is_win = True
            # проверяем, может ли игрок
            # заполнить линию
            for i, j in line:
                is_win = is_win and (self.value[i][j]==player)
            if is_win:
                return True
        return False  

    '''получить список ходов''' 
    def get_moves(self, player):
        # если ситуация выигрышная или проигрышная
        # то ходов нет
        if self.is_win(player) or self.is_win(self.opponent[player]):
            return []
        
        moves = []
        for row in range(3):
            for col in range(3):
                # добавляем в список ход 
                # для каждой пустой клетки
                if self.value[row][col] == None:
                    moves.append((row, col, player))
        return moves

    '''вспомогательная функция для расчета оценочной функции'''
    '''подсчет количества строк, столбцов и диагоналей'''
    '''которые может заполнить игрок'''
    def nc(self, player):
        count = 0
        for (r1,c1),(r2,c2),(r3,c3) in state_xo.lines:
            if self.value[r1][c1] != state_xo.opponent[player] \
                and self.value[r2][c2] != state_xo.opponent[player] \
                and self.value[r3][c3] != state_xo.opponent[player]:
                count += 1
        return count

    '''оценочная функция'''
    def score(self, player):
        oppenent = state_xo.opponent[player]
        # если выиграл игрок, то +бесконечность
        if self.is_win(player):
            return state_xo.infinity
        # если игрок проиграл, то -бесконечность            
        elif self.is_win(oppenent):
            return (-1)*state_xo.infinity 
        else:
            # инача расчет разности функций nc
            return self.nc(player) - self.nc(oppenent)
