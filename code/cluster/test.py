from copy import deepcopy


class Graph:
    def __init__(self, vertexes, edges, distance):
        self.vertexes = vertexes
        self.edges = edges
        self.distance = distance

    def get_distance(self, vertex1, vertex2):
        if (vertex1, vertex2) in self.distance:
            return self.distance[(vertex1, vertex2)]
        elif (vertex2, vertex1) in self.distance:
            return self.distance[(vertex2, vertex1)]
        else:
            raise ValueError

    def get_sct(self):
        t = Graph(deepcopy(self.vertexes), [], deepcopy(self.distance))
        
        selected = []
        unselected = deepcopy(t.vertexes)

        selected.append(t.vertexes[4])
        unselected.remove(t.vertexes[4])

        while unselected:
            # find min distance between selected and unselected vertexes
            min_distance = None
            nearest_selected = None
            nearest_vertex = None

            for v1 in selected:
                for v2 in unselected:                    
                    if nearest_vertex:
                        d = t.get_distance(v1, v2)
                        if d < min_distance:
                            nearest_selected = v1
                            nearest_vertex = v2
                            min_distance = d
                    else:                 
                        nearest_selected = v1       
                        nearest_vertex = v2
                        min_distance = t.get_distance(v1, v2)
        
            selected.append(nearest_vertex)
            unselected.remove(nearest_vertex)
            t.edges.append((nearest_selected, nearest_vertex))
        
        return t

    def __str__(self):
        return f"v={self.vertexes} e={self.edges}"

class Cluster:
    def __init__(self, vertexes):
        self.vertexes = vertexes

    def union(self, cluster):
        self.vertexes.extend(cluster.vertexes)

    def __str__(self):
        return f"{self.vertexes}"

def get_clusters(graph):
    history = []

    clusters = []
    for v in graph.vertexes:
        clusters.append(Cluster([v]))

    history.append(deepcopy(clusters))

    # find shortest edge
    edges = deepcopy(graph.edges)
    
    while len(clusters) > 1:
        min_edge = None
        distance = None
        for (v1, v2) in edges:
            if min_edge is None:
                min_edge = (v1, v2)
                distance = graph.get_distance(v1, v2) 
            else:
                d = graph.get_distance(v1, v2) 
                if d < distance:
                    min_edge = (v1, v2)
                    distance = graph.get_distance(v1, v2) 

        v1, v2 = min_edge
        # find cluster with v1
        for c1 in clusters:
            if v1 in c1.vertexes:
                break

        # find cluster with v2
        for c2 in clusters:
            if v2 in c2.vertexes:
                break

        edges.remove(min_edge)
        
        # union c1 and c2
        c1.union(c2)
        clusters.remove(c2)
        history.append(deepcopy(clusters))
    
    return history


def _main():
    vertexes = [1, 2, 3, 4, 5]
    edges = []
    distance = {
        (1, 2):11,
        (1, 3):9,
        (1, 4):7,
        (1, 5):8,
        (2, 3):15,
        (2, 4):14,
        (2, 5):13,
        (3, 4):12,
        (3, 5):14,
        (4, 5):6
    } 

    g = Graph(vertexes, edges, distance)
    print("\nSource graph:", g)
    
    t = g.get_sct()
    print("\nSCT graph:", t)

    h = get_clusters(t)
    print("\nClusters:")
    for level in h:
        print('---')
        for c in level:
            print(c)


def _main1():
    vertexes = [1, 2, 3, 4, 5, 7]
    edges = [
        (1, 2),
        (2, 3),
        (2, 4),
        (4, 5),
        (4, 7),
        (5, 6)
    ]
    distance = {
        (1, 2):5,
        (1, 3):None,
        (1, 4):None,
        (1, 5):None,
        (1, 6):None,
        (1, 7):None,
        (2, 3):3,
        (2, 4):6,
        (2, 5):None,
        (2, 6):None,
        (2, 7):None,
        (3, 4):None,
        (3, 5):None,
        (3, 6):None,
        (3, 7):None,
        (4, 5):2,
        (4, 6):None,
        (4, 7):7,
        (5, 6):4,
        (5, 7):None,
        (6, 7):None
    } 

    g = Graph(vertexes, edges, distance)
    print("\nSource graph:", g)
    
    t = g.get_sct()
    print("\nSCT graph:", t)

    h = get_clusters(t)
    print("\nClusters:")
    for level in h:
        print('---')
        for c in level:
            print(c)


if __name__ == "__main__":
    _main()
