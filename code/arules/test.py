import matplotlib.pyplot as plt
import numpy as np
from timeit import Timer

from base import apriori
from mushroom import mushroom_database as mdb 

db = mdb()
db.load('mushroom_csv.csv')

confidence = 0.80

def itemset_count(support):
    alg = apriori(db, support, confidence)
    alg.run(debug=False)
    return alg.itemset_count()

def time(support): 
    alg = apriori(db, support, confidence)
    t = Timer(lambda: alg.run(debug=False))   
    return t.timeit(number=1)

def rules_count(confidence): 
    support = 0.75
    alg = apriori(db, support, confidence)
    alg.run(debug=False)
    return len(alg.rules)

lag = 0.01
# x = np.arange(0.30, 1.00, lag)
x = np.arange(0.75, 1.01, lag)
# y = np.array([itemset_count(support) for support in x])
# y = np.array([time(support) for support in x])
y = np.array([rules_count(confidence) for confidence in x])

fig = plt.figure()
plt.plot(x, y)

# plt.title('Count of itemsets')
# plt.title('Time of calc')
# plt.title('Association Rules')

# plt.ylabel('time, sec')
# plt.ylabel('itemsets, count')
plt.ylabel('rules, count')

# plt.xlabel('support')
plt.xlabel('confidence')

plt.grid(True)

# plt.savefig('{}.{}'.format('itemset_count', 'png'))
# plt.savefig('{}.{}'.format('time_support', 'png'))

plt.show()