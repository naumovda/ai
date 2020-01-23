# d = {'c':'color', 'r':'radius', 'w':'width', 'a':'alpha'}
# for key in sorted(d.keys()):
#     print(key, d[key])

s1 = set([1, 2, 3])

s2 = set([2, 4])

# s2.update(s1)

s3 = set()
s3 += s2
s3 += s1

print(s1)
print(s2)
print(s3)