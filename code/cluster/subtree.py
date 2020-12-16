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
    if not edge in subtree:
        return None
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
    
    if not subtree:
        subtree = set()
        subtree.add((vertex, vertex))
    return subtree

def cut_edge(tree, edge):
    if not edge in tree:
        return [tree]
    v0, v1 = edge
    sub_tree1 = get_subtree(tree, edge, v0)
    sub_tree2 = get_subtree(tree, edge, v1)
    return [sub_tree1, sub_tree2]

def cut_edges(tree, edge_list):
    forest = [tree]
    for edge in edge_list:
        new_forest = []
        for item in forest:
            new_forest.extend(cut_edge(item, edge))
        forest = new_forest
    return forest

def get_clusters(forest):
    clusters = []
    for tree in forest:
        s = set()
        for v0, v1 in tree:
            s.add(v0)
            s.add(v1)
        clusters.append(s)
    return clusters

def main():
    edge_list = [(4, 5), (1, 2)]

    result = cut_edges(tree, edge_list)

    print(f'source tree ={tree}')
    print(f'cut edges: {edge_list}')
    print(f'trees list={result}')
    print(f'clusters={get_clusters(result)}')

if __name__ == "__main__":
    main()