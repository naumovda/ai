import matplotlib.pyplot as plt
import csv
import numpy as np
from kmeans import kmeans
from trees import make_clusters, Graph
from sklearn.datasets import make_blobs


def test_kmeans():
    dots = []
    with open("s1.txt", "r", encoding="utf-8") as f:
        reader = csv.reader(f, delimiter=" ", skipinitialspace=True)
        for row in reader:
            dots.append(np.asarray(row, dtype=np.float32))

    # plt.scatter(*zip(*dots), s=5)
    # plt.show()

    colors = ["#FF6347", "#CD5C5C", "#FF8C00", "#FFD700", "#DAA520", "#BDB76B",
              "#9ACD32", "#32CD32", "#90EE90", "#00FA9A", "#66CDAA", "#20B2AA",
              "#5F9EA0", "#6495ED", "#7B68EE", "#DB7093", "#F4A460", "#B0C4DE"]
    clusters, centers = kmeans(dots, 15)

    fig, ax = plt.subplots()
    for clust, col in zip(clusters, colors):
        xs = [p[0] for p in clust]
        ys = [p[1] for p in clust]
        ax.scatter(xs, ys, s=5, c=col)

    ax.plot(*zip(*centers), "r^")

    plt.show()


def test_trees():
    dots, _ = make_blobs(n_samples=250, n_features=2, centers=6,
                         cluster_std=0.2, random_state=41, center_box=(0, 6))
    plt.scatter(*zip(*dots), s=5)
    # plt.show()

    vertices = [i for i in range(len(dots))]
    edges = [(i, j) for i in range(len(dots)) for j in range(len(dots))]
    distances = np.zeros( shape=(len(vertices), len(vertices)) )
    for i in range(len(vertices)):
        for j in range(i+1, len(vertices)):
            d = np.linalg.norm(dots[i] - dots[j])
            distances[i, j] = d
            distances[j, i] = d
    graph = Graph(vertices, edges, distances)

    clusters = make_clusters(graph, 6)

    colors = ["#FF0000", "#0000FF", "#00FF00", "#FFD700", "#DAA520", "#BDB76B",
              "#9ACD32", "#32CD32", "#90EE90", "#00FA9A", "#66CDAA", "#20B2AA",
              "#5F9EA0", "#6495ED", "#7B68EE", "#DB7093", "#F4A460", "#B0C4DE"]
    print(len(clusters))
    for clust, col in zip(clusters, colors):
        c = list(clust)
        print(c)
        for p in c:
            plt.scatter(dots[p][0], dots[p][1], s=5, c=col)

    plt.show()


if __name__ == "__main__":
    # test_kmeans()
    test_trees()
