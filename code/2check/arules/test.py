# items = [1, 2 , 3, 4]
# items.sort(key=lambda item: -1 * item)
# print(items[:0])

v1 = {'v':1, 'childs':[]}
v2 = {'v':2, 'childs':[]}
v3 = {'v':3, 'childs':[]}
v4 = {'v':4, 'childs':[]}
v5 = {'v':5, 'childs':[]}
v7 = {'v':7, 'childs':[v1, v2, v3]}
v8 = {'v':8, 'childs':[v4, v5]}
root = {'v':0, 'childs':[v7, v8]}

#     0
#   ------ 
#   7    8
# ----- ---
# 1 2 3 4 5
# 123456789

def width(root):
    if root['childs'] == []:
        return 1
    return sum([width(child) for child in root['childs']])+len(root['childs'])-1

def print_tree(root):    
    print(f"{root['v']}")

print(width(root))