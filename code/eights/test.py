from eight_dict import state
# initial = state([[8,1,3],[2,4,5],[state.space,7,6]], 0)

# goal = state([[1,2,3],[8,state.space,4],[7,6,5]])
# 123 456 789

if __name__ == "__main__":
    initial = state([[8,1,3],[2,4,5],[state.space,7,6]], 0)
    
    print(initial)
    print(initial.__hash__())
