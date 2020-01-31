# class itemset:
#     '''
#     Элемент
#         Атрибуты:
#             items - множество элементов, входящих в набор
#         Методы:
#             support - поддержка набора в базе данных 
#     '''
#     def __init__(self, itemset=set()):
#         self.itemset = itemset
#         self.count = 0
#         self.total = 0

#     def add(self, value):
#         self.itemset.add(value)

#     def union(self, other):
#         result = set()
#         result.update(self)
#         result.update(other)
#         return result

#     def support(self):
#         if self.total == 0:
#             return 0        
#         return self.count/self.total

#     def __eq__(self, value):
#         return self.itemset == value.itemset

#     def __str__(self):
#         return f"set={self.itemset}, support={self.support()}"

class transaction:
    '''
    Транзакция, имеющая идентификатор и содержащая набор элементов 
    '''
    def __init__(self, tid, itemset=frozenset()):
        self.tid = tid
        self.itemset = itemset

    def get_list(self, fields):
        return sorted(list(self.itemset))

    def get_boolean(self, fields):
        digit = lambda value: "1" if value in self.itemset else "0"
        return [digit(item) for item in fields]

class database:
    '''
    База данных, состоящая из транзакций
    '''
    def __init__(self, fields=[], transactions=[]):
        self.fields = fields
        self.transactions = transactions

    def add_transaction(self, item):
        self.transactions.append(item)

    def print_as_set(self):
        for t in self.transactions:
            print(t.tid, " ".join(t.get_list(self.fields)))
        print()

    def print_as_boolean(self):
        print("#", " ".join(self.fields))
        for t in self.transactions:
            print(t.tid, " ".join(t.get_boolean(self.fields)))
        print()

    def calc_support(self, itemsets, support):
        count = {itemset:0 for itemset in itemsets}
        for transaction in self.transactions:
            for itemset in itemsets: 
                if itemset <= transaction.itemset:
                    count[itemset] += 1
        total = len(self.transactions)   
        for itemset in itemsets: 
            support[itemset] = count[itemset] / total      

    def load(self, file_name):
        pass
  
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

class apriori:
    def __init__(self, min_support, min_confidence):
        self.min_support = min_support
        self.min_confidence = min_confidence
        self.itemsets = []
        self.support = {}

    def filter(self, items, support):
        # формируем список наборов с поддержкой, большей min_support
        return [item for item in items if support[item] >= self.min_support]

    def step_0(self, db):
        self.itemsets = []
        self.support = {}
        # формируем одноэлементные наборы 
        items = [frozenset([item]) for item in db.fields]        
        # рассчтываем поддержку одноэлементных наборов
        db.calc_support(items, self.support)
        # добавляем в список
        self.itemsets.append(self.filter(items, self.support))
        
    def step_k(self, db):
        if self.itemsets[-1] == []:
            return False
        items = []
        for e1 in self.filter(self.itemsets[0], self.min_support):
            for e2 in self.itemsets[-1]:
                if not e1.itemset <= e2.itemset:                    
                    s = frozenset(e2.union(e1))  
                    if not s in items:
                        items.append(e)
        # рассчтываем поддержку наборов
        db.calc_support(items, self.support)
        # добавляем в список
        self.itemsets.append(self.filter(items, self.support))
        # возвращаем признак того, что можно продолжать
        return self.itemsets[-1] != []:

# class rule:
#     def __init__(self, antecedent, consequent, support, confidence):
#         self.antecedent = antecedent
#         self.consequent = consequent
#         self.support = support
#         self.confidence = confidence   
   
if __name__ == "__main__":
    # загружаем файл данных
    db = database(['A','B','C','D','E','F'])
    
    t1 = transaction(0, frozenset(['A','B','C']))
    t2 = transaction(1, frozenset(['A','C']))
    t3 = transaction(2, frozenset(['A','D']))
    t4 = transaction(3, frozenset(['B','E','F']))

    db.add_transaction(t1)
    db.add_transaction(t2)
    db.add_transaction(t3)
    db.add_transaction(t4)

    db.print_as_set()
    db.print_as_boolean()

    alg = apriori(0.40, 0.50)
    alg.step_0(db)
    for key in alg.support.keys():
        print(list(key), alg.support[key])

    # while alg.step_k(db):
    #     pass

    # k = 0
    # for level in alg.itemsets:        
    #     print('level', k)
    #     for item in level:
    #         print(item)
    #     k += 1
    # db.load('mushroom_csv.csv')
    
    # формируем одноэлементные наборы 
    # itemsetlist = []
    # for item in universal:        
    #     itemsetlist.append(itemset(set([item])))

    # calc_support(itemsetlist, db)
    
    # c1 = []
    # for item in itemsetlist:
    #     if item.support() > min_support:
    #         c1.append(item)

    # itemsetlist = []
    # for e1 in c1:
    #     for e2 in c1:
    #         if not e2.itemset <= e1.itemset:
    #             s = set()
    #             s.update(e1.itemset)
    #             s.update(e2.itemset)
    #             e = itemset(s)
    #             itemsetlist.append(e)
    
    # calc_support(itemsetlist, db)
    
    # c2 = []
    # for item in itemsetlist:
    #     if item.support() > min_support:
    #         c2.append(item)

    # itemsetlist = []
    # for e1 in c2:
    #     for e2 in c1:
    #         if not e2.itemset <= e1.itemset:
    #             s = set()
    #             s.update(e1.itemset)
    #             s.update(e2.itemset)
    #             e = itemset(s)
    #             itemsetlist.append(e)
    
    # calc_support(itemsetlist, db)
    
    # c3 = []
    # for item in itemsetlist:
    #     if item.support() > min_support:
    #         c3.append(item)

    # for item in c3:
    #     print_set(item.itemset)
    #     print(item.support())
    
