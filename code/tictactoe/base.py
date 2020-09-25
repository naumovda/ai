# Базовый класс для описания состояния - 
#   игровой ситуации. 
# Класс должен уметь:
# - создавать список ходов
# - выполнять ход, изменяя текущее состояние
# - отменять ход
# - проверять, является ли состояние выигрышным
# - рассчитывать оценочную функцию
class state:
    def __init__(self, value):
        '''Конструктор класса, инициализация полей'''
        raise NotImplementedError

    def get_moves(self, player):
        '''Получение списка ходов'''        
        raise NotImplementedError        

    def do_move(self, move):
        '''Выполнение хода'''
        raise NotImplementedError                

    def undo_move(self, move):
        '''Отмена хода'''
        raise NotImplementedError                

    def is_win(self, player):
        '''Проверка, что игрок player выиграл'''
        raise NotImplementedError                

    def score(self, player):
        '''Расчет оценочной функции'''
        raise NotImplementedError                
