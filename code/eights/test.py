from eight_dict import state
# initial = state([[8,1,3],[2,4,5],[state.space,7,6]], 0)

# goal = state([[1,2,3],[8,state.space,4],[7,6,5]])
# 123 456 789

if __name__ == "__main__":
    initial = state([[8,1,3],[2,4,5],[state.space,7,6]], 0)
    
    for idx in range(1, 5):
        print(idx)

    print(initial)
    print(initial.__hash__())

    print(1*2*3*4*5*6*7*8*9)
