from copy import deepcopy

players = ["X", "0"]

def swap_player(player):
    return ({"X":"0", "0":"X"})[player]

def get_lines():
    lines = [
        [(0,0), (0,1), (0,2)],
        [(1,0), (1,1), (1,2)],
        [(2,0), (2,1), (2,2)],
        [(0,0), (1,0), (2,0)],
        [(0,1), (1,1), (2,1)],
        [(0,2), (1,2), (2,2)],
        [(0,0), (1,1), (2,2)],
        [(0,2), (1,1), (2,0)],
    ]
    return lines

class state:
    def __init__(self, player, value=None):
        if not player in players:
            raise Exception

        self.player = player

        if value != None:
            self.value = value
        else:
            self.value = [
                [None, None, None], 
                [None, None, None], 
                [None, None, None]]                

    def __str__(self):
        return str(self.value)

    def get_moves(self):
        states = []
        for row in range(3):
            for col in range(3):
                if self.value[row][col] == None:
                    new_state = deepcopy(self.value)
                    new_state[row][col] = self.player
                    states.append(state(swap_player(self.player), new_state))
        return states    

    def is_win(self, player):
        lines = get_lines()    
        for line in lines:        
            is_win = True
            for i, j in line:
                is_win = is_win and (self.value[i][j]==player)
            if is_win:
                return True
        return False        

if __name__ == "__main__":
    s = state("X")
    print(s)

    for item in s.get_moves():
        print(item)

    print(s.get_moves()[0].get_moves()[0])


