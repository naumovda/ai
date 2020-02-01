# d = {'c':'color', 'r':'radius', 'w':'width', 'a':'alpha'}
# for key in sorted(d.keys()):
#     print(key, d[key])

def subsets(source):    
    result = []
    for item in source:
        temp = source.copy()
        temp.remove(item)
        if temp != []:
            result.append(temp)
            for elem in subsets(temp):
                if not elem in result:
                    result.append(elem)             
    return result

def get_subsets(source):    
    # source = frozenset()
    result = []
    for item in list(source):
        temp = source.difference(frozenset([item]))
        if temp != []:
            result.append(temp)
            for elem in get_subsets(temp):
                if not elem in result:
                    result.append(elem)
    return result

d = {}
l1 = [1, 2, 3, 4, 5]
s1 = frozenset(l1)
# d[s1] = 1
# print(s1, d[s1])
# z = {item: 0 for item in l1}
# print(z)
print(get_subsets(s1))

# n =2**len(s1)

# print(n)

# def confidence(rule):
#     return 1.0

# def assoc_rules(rules, items, seq, min_confidence):
#     items = list()
#     seq = list()
#     for item in items:
#         new_items = items.copy()
#         new_items.remove(item)
#         seq.append(item)
#         if confidence(None) >= min_confidence:
            