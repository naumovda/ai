edges = [
    ((1,2),(1,4)), 
    ((1,6),(1,8)), 
    ((3,0),(3,2)),
    ((1,4),(1,6)),
    ((1,2),(3,2))
    ]

def collapse(clusters):
    for i in range(len(clusters)):
        for j in range(i+1, len(clusters)):
            if clusters[i].intersection(clusters[j]):
                for c in clusters[j]:
                    clusters[i].add(c)
                del clusters[j]
                return True
    return False

def ultimate_collapse(clusters):
    while collapse(clusters):
        pass

clusters = []

for e in edges:
    (x1, y1), (x2, y2) = e
    find = False
    for c in clusters:
        if ((x1, y1) in c) or ((x2, y2) in c):
            c.add((x1,y1))
            c.add((x2,y2))
            find = True
            collapse(clusters)
            break
    if not find:
        c = set()
        c.add((x1, y1))
        c.add((x2, y2))
        clusters.append(c)

print(clusters)

def m(s, idx, fun):
    return fun(item[idx] for item in s)

def l(s):
    min_x = m(clusters[0], 0, min)
    max_x = m(clusters[0], 0, max)
    min_y = m(clusters[0], 1, min)
    max_y = m(clusters[0], 1, max)
    return min([9+min_x-max_x, 9+min_y-max_y])

def score_p(clusters):
    return min([l(cluster) for cluster in clusters])

for c in clusters:
    print(f"cluster = {c}")

print(f"score_p = {score_p(clusters)}")

#  0123456789
# 0---------- 
# 1--*******-
# 2--*-------
# 3***-------
# 4----------
# 5----------
# 6----------
# 7----------
# 8----------
# 9----------