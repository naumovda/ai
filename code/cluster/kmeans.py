import numpy as np
import matplotlib.pyplot as plt
from random import choice, random


def distance(p1, p2):
    return np.linalg.norm(p2 - p1)


def points_mean(cluster):
    return np.mean(cluster, axis=0)


def centers(points, k):
    cnts = []
    cnts.append(choice(points))
    for i in range(k-1):
        ds = np.array([])
        for p in points:
            md = min([distance(p, c) for c in cnts])
            ds = np.append(ds, md)
        probs = np.asarray([d**2 / np.sum(ds**2) for d in ds])
        cnts.append(points[np.random.choice(len(points), p=probs)])
    return cnts


def assign_point(clusters, centers, point):
    ds = np.array([distance(point, cnt) for cnt in centers])
    clusters[ds.argmin()].append(point)
    return clusters


def kmeans(points, k):
    clusters_prev = []
    cnts = centers(points, k)

    ITER_COUNT = 50
    while ITER_COUNT != 0:
        clusters = [[] for _ in range(k)]
        for p in points:
            clusters = assign_point(clusters, cnts, p)
        for i, _ in enumerate(cnts):
            if len(clusters[i]) > 0:
                cnts[i] = points_mean(clusters[i])

        print(f"it = {ITER_COUNT}")
        if (clusters == clusters_prev) or (ITER_COUNT == 0):
            return clusters, cnts

        clusters_prev = clusters
        ITER_COUNT -= 1


if __name__ == "__main__":
    data = [
        (1, 1), (1, 2), (1, 3), (2, 2), (2, 3), (3, 1), (3, 2), (2, 5), (2, 6), (3, 6),
        (3, 7), (4, 5), (4, 6), (5, 3), (5, 4), (6, 2), (6, 4), (7, 2), (7, 3), (7, 4)
    ]
    points = [np.asarray(p) for p in data]

    # plt.scatter(*zip(*points))
    # plt.grid(True)
    # plt.show()

    clusters, _ = kmeans(points, 3)

    print(clusters)

    fig, ax = plt.subplots()
    for clust in clusters:
        xs = [p[0] for p in clust]
        ys = [p[1] for p in clust]
        ax.scatter(xs, ys)

    plt.show()
