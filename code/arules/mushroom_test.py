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
    support = 0.80
    alg = apriori(db, support, confidence)
    alg.run(debug=False)
    return len(alg.rules)

def pic_itemset_support():
    lag = 0.01
    x = np.arange(0.30, 1.00, lag)
    y = np.array([itemset_count(support) for support in x])
    fig = plt.figure()
    plt.plot(x, y)
    plt.title('Count of itemsets')
    plt.ylabel('itemsets, count')
    plt.xlabel('support')
    plt.grid(True)
    plt.show()    

def pic_time_support():
    lag = 0.01
    x = np.arange(0.30, 1.00, lag)
    y = np.array([time(support) for support in x])
    fig = plt.figure()
    plt.plot(x, y)
    plt.title('Time of calc')
    plt.ylabel('time, sec')
    plt.xlabel('support')
    plt.grid(True)
    plt.show()    

def pic_rules_confidence():
    lag = 0.01
    x = np.arange(0.80, 1.00, lag)
    y = np.array([rules_count(confidence) for confidence in x])
    fig = plt.figure()
    plt.plot(x, y)
    plt.title('Association Rules')
    plt.ylabel('rules, count')
    plt.xlabel('confidence')
    plt.grid(True)
    plt.show()

if __name__ == "__main__":
    pic_itemset_support()
    # pic_time_support()
    # pic_rules_confidence()
