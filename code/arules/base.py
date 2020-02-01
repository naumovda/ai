class transaction:
    '''
    Транзакция, имеющая идентификатор и содержащая набор элементов 
    '''
    def __init__(self, tid, itemset=frozenset()):
        self.tid = tid
        self.itemset = itemset

    def get_list(self, fields):
        # return sorted(list(self.itemset))
        return list(self.itemset)

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
    for item in list(source):
        temp = source.difference(frozenset([item]))
        if temp != []:
            result.append(temp)
            for elem in subsets(temp):
                if not elem in result:
                    result.append(elem)
    return result

class rule:
    def __init__(self, antecedent, consequent, support, confidence):
        self.antecedent = antecedent
        self.consequent = consequent
        self.support = support
        self.confidence = confidence  

    def __str__(self):
        return f"{list(self.antecedent)} => {list(self.consequent)}"

def get_subsets(source):    
    result = []
    for item in list(source):
        temp = source.difference(frozenset([item]))
        if temp != []:
            result.append(temp)
            for elem in subsets(temp):
                if elem != [] and not elem in result:
                    result.append(elem)
    return result

class apriori:
    def __init__(self, database, min_support, min_confidence):
        self.database = database
        self.min_support = min_support
        self.min_confidence = min_confidence
        self.itemsets = []
        self.support = {}
        self.rules = []

    def filter(self, items, support):
        # формируем список наборов с поддержкой, большей min_support
        return [item for item in items if support[item] >= self.min_support]

    def step_0(self):
        self.itemsets = []
        self.support = {frozenset():-1}
        self.rules = []        
        # формируем одноэлементные наборы 
        items = [frozenset([item]) for item in self.database.fields]        
        # рассчтываем поддержку одноэлементных наборов
        self.database.calc_support(items, self.support)
        # добавляем в список
        self.itemsets.append(self.filter(items, self.support))
        return self.itemsets[-1] != []
        
    def step_k(self):
        if self.itemsets[-1] == []:
            return False
        items = []
        for e1 in self.itemsets[0]:
            for e2 in self.itemsets[-1]:
                if not e1 <= e2:                    
                    s = frozenset(e2.union(e1))  
                    if not s in items:
                        items.append(s)
        # рассчтываем поддержку наборов
        self.database.calc_support(items, self.support)
        # добавляем в список
        self.itemsets.append(self.filter(items, self.support))
        # возвращаем признак того, что можно продолжать
        return self.itemsets[-1] != []

    def generate_rules(self):
        # перебрать все наборы из itemsets, начиная с двухэлементых
        for level in self.itemsets[1:]:
            for itemset in level:
                # генерируем все подмножества множества itemset
                subsets = get_subsets(itemset)
                # формируем посыл и следствие правила
                for antecedent in subsets: 
                    consequent = itemset.difference(antecedent)
                    # рассчитать поддержку и достоверность
                    rule_support = self.support[itemset]
                    rule_confidence = rule_support / self.support[antecedent]
                    # если достоверность больше пороговой, добавить в список
                    if rule_confidence > self.min_confidence:
                        self.rules.append(rule(antecedent, consequent, rule_support, rule_confidence))
     
    def run(self, debug=False):
        if debug:
            print('Runnung Apriori:')        
        if self.step_0():
            if debug:
                print('.')
            while self.step_k():
                if debug:
                    print('.')
            self.generate_rules()
            if debug:
                print('.done!')  

    def print_itemsets(self):
        k = 0
        for level in self.itemsets:        
            print('level', k)
            for item in level:
                print(list(item))
            k += 1
        print()

    def print_support(self):
        for key in self.support.keys():
            print(list(key), self.support[key])

    def print_rules(self):
        for item in self.rules:
            print(item, "supp = ", item.support, "conf = ", item.confidence)
   
if __name__ == "__main__":
    # инициализация объекта базы данных списком полей
    db = database(['A','B','C','D','E','F'])
    
    # создаем транзакции
    t1 = transaction(0, frozenset(['A','B','C']))
    t2 = transaction(1, frozenset(['A','C']))
    t3 = transaction(2, frozenset(['A','D']))
    t4 = transaction(3, frozenset(['B','E','F']))

    # добавляем транзакции в базу данных
    db.add_transaction(t1)
    db.add_transaction(t2)
    db.add_transaction(t3)
    db.add_transaction(t4)

    # выводим базу данных
    db.print_as_set()
    db.print_as_boolean()

    # инициализируем и запускаем алгоритм Apriori
    alg = apriori(db, 0.30, 0.50)
    alg.run()

    # печатаем результаты
    alg.print_itemsets()    
    alg.print_support()
    alg.print_rules()
