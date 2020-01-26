class itemset:
    '''
    Элемент
        Атрибуты:
            items - множество элементов, входящих в набор
        Методы:
            support - поддержка набора в базе данных 
    '''
    def __init__(self, itemset=set()):
        self.itemset = itemset
        self.count = 0
        self.total = 0

    def add(self, value):
        self.itemset.add(value)

    def union(self, other):
        result = set()
        result.update(self)
        result.update(other)
        return result

    def support(self):
        if self.total == 0:
            return 0        
        return self.count/self.total

    def __eq__(self, value):
        return self.itemset == value.itemset

    def __str__(self):
        return f"set={self.itemset}, support={self.support()}"

class transaction:
    '''
    Транзакция, имеющая идентификатор и содержащая набор элементов 
    '''
    cid = -1

    def __init__(self, itemset=set()):
        transaction.cid += 1
        self.tid = transaction.cid
        self.itemset = set()
        self.itemset.update(itemset)

    def add(self, item):
        self.itemset.add(item)
    
    def update(self, items):
        self.itemset.update(items)

    def to_list(self, fields):
        items = []
        for item in fields:
            if item in self.itemset:
                items.append(item)
        return items

    def to_boolean_list(self, fields):
        items = []
        for item in fields:
            if item in self.itemset:
                items.append('1')
            else:
                items.append('0')
        return items

class database:
    '''
    База данных, состоящая из транзакций
    '''
    def __init__(self, fields=[], transactions=[]):
        self.fields = fields
        self.transactions = transactions

    def add_transaction(self, item):
        self.transactions.append(item)

    def calc_support(self, itemsets):
        total = len(self.transactions)   
        for itemset in itemsets:
            itemset.count = 0
            itemset.total = total
        for transaction in self.transactions:
            for itemset in itemsets: 
                if itemset.itemset <= transaction.itemset:
                    itemset.count += 1

    def print_set(self):
        for t in self.transactions:
            print(t.tid, " ".join(t.to_list(self.fields)))
        print()

    def print_boolean(self):
        print("#", " ".join(self.fields))
        for t in self.transactions:
            print(t.tid, " ".join(t.to_boolean_list(self.fields)))
        print()

    def load(self, file_name):
        pass

class apriori:
    def __init__(self, min_support):
        self.min_support = min_support
        self.itemsets = []

    def append_itemsets(self, items):
        # формируем список наборов с поддержкой, большей min_support
        res = []
        for item in items:
            if item.support() >= self.min_support:
                res.append(item)
        self.itemsets.append(res)

    def step_0(self, db):
        self.itemsets = []
        # формируем одноэлементные наборы 
        items = []        
        for item in db.fields:
            items.append(itemset(set([item])))        
        # рассчтываем поддержку одноэлементных наборов
        db.calc_support(items)
        # добавляем в список
        self.append_itemsets(items)        
        
    def step_k(self, db):
        if self.itemsets[-1] == []:
            return False
        items = []
        for e1 in self.itemsets[0]:
            for e2 in self.itemsets[-1]:
                if not e1.itemset <= e2.itemset:
                    s = set()
                    s.update(e1.itemset)
                    s.update(e2.itemset)                    
                    e = itemset(s)
                    if not e in items:
                        items.append(e)
        # рассчтываем поддержку наборов
        db.calc_support(items)
        # добавляем в список
        self.append_itemsets(items)       
        # возвращаем признак того, что можно продолжать
        return True

class rule:
    def __init__(self, antecedent, consequent, support, confidence):
        self.antecedent = antecedent
        self.consequent = consequent
        self.support = support
        self.confidence = confidence   
   
if __name__ == "__main__":
    # загружаем файл данных
    db = database(['A','B','C','D','E','F'])
    
    t1 = transaction(set(['A','B','C']))
    t2 = transaction(set(['A','C']))
    t3 = transaction(set(['A','D']))
    t4 = transaction(set(['B','E','F']))

    db.add_transaction(t1)
    db.add_transaction(t2)
    db.add_transaction(t3)
    db.add_transaction(t4)

    db.print_set()
    db.print_boolean()

    alg = apriori(0.25)
    alg.step_0(db)

    while alg.step_k(db):
        pass

    k = 0
    for level in alg.itemsets:        
        print('level', k)
        for item in level:
            print(item)
        k += 1
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
    
