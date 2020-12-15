tree = {
    (1, 2),
    (2, 3),
    (2, 4),
    (4, 5),
    (5, 6),
    (5, 7)    
}

def get_subtree(tree, edge, vertex):
    subtree = tree.copy()
    subtree.remove(edge)
    
    # find all vertexes, connected to vertex
    open_v = [vertex]
    close_v = []

    while open_v:
        v = open_v.pop()        
        close_v.append(v) 
        # find edges with v
        for v0, v1 in subtree:
            if v0 == v:
                if v1 not in close_v:
                    open_v.append(v1)
            if v1 == v:
                if v0 not in close_v:
                    open_v.append(v0)
            
    # remove edges
    for v0, v1 in subtree.copy():
        if v0 not in close_v or v1 not in close_v:
            subtree.remove((v0, v1))      
    
    return subtree

def cut_edge(tree, edge):
    v0, v1 = edge
    sub_tree1 = get_subtree(tree, edge, v0)
    sub_tree2 = get_subtree(tree, edge, v1)
    return sub_tree1, sub_tree2

edge = (4, 5)
st0, st1 = cut_edge(tree, edge)

print(f'source tree ={tree}')
print(f'cut edge {edge}')
print(f'tree 1={st0}')
print(f'tree 2={st1}')
