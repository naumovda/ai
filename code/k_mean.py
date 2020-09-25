import pandas as pd
import os

cd = os.getcwd()

df = pd.read_csv(cd + "\\code\\kmean_test_01.csv", \
    header=None, delimiter=';')

class cluster:
    def __init__(x1, x2):
        self.x1 = x1
        self.x2 = x2
        self.items = []

    def add(self, item):
        self.items.append(item)
        pass

    def calc_center(self):
        
        for i in item:

    


