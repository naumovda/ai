    # def get_boolean(self, fields):
    #     digit = lambda value: "1" if value in self.itemset else "0"
    #     return [digit(item) for item in fields]

fields = ['A','B','C','D','E','F']   
t1 = frozenset(['A','B','C'])

# print(*fields)

t = tuple(['A','B','C','D','E','F'])

print(t)

for e in t:
    print(e)

# # A B C D E F
# 0 1 1 1 0 0 0