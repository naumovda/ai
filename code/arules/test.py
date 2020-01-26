# d = {'c':'color', 'r':'radius', 'w':'width', 'a':'alpha'}
# for key in sorted(d.keys()):
#     print(key, d[key])

s1 = [1, 2, 3, 4, 5]

n =2**len(s1)

print(n)

def confidence(rule):
    return 1.0

def assoc_rules(rules, items, seq, min_confidence):
    items = list()
    seq = list()
    for item in items:
        new_items = items.copy()
        new_items.remove(item)
        seq.append(item)
        if confidence(None) >= min_confidence:
            