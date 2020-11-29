from base import Transaction, Database, AbstractAlgorithm
from rules import Rule


class AprioriAlgorithm(AbstractAlgorithm):
    '''Apriori algorithm for generating association rules

    database -- transaction database
    min_support -- minimum support
    min_confidence -- minimum confidence
    rules -- list of rules
    '''

    def __init__(self, database, min_support, min_confidence):
        '''Init algorithm object for generating association rules

        database -- transaction database
        min_support -- minimum support
        min_confidence -- minimum confidence
        rules -- list of rules
        '''

        super().__init__(database, min_support, min_confidence)
        self.itemsets = []
        self.support = {}
        self.rules = []

    def filter(self, items, support):
        '''Get itemsetst with support > min_support'''

        return [item for item in items if support[item] >= self.min_support]

    def step_0(self):
        '''First step of algorithm: generate one-element itemsets'''

        self.itemsets = []
        self.support = {frozenset():-1.0}
        self.rules = []        
        
        # generate one-element itemsets
        items = [frozenset([item]) for item in self.database.fields]        
        
        # calc support for one-element itemsets
        self.database.calc_support(items, self.support)
        self.itemsets.append(self.filter(items, self.support))

        return self.itemsets[-1] != []
        
    def step_k(self):
        '''K-step of algorithm: generate k-element itemsets'''

        if self.itemsets[-1] == []:
            return False

        items = []
        for e1 in self.itemsets[0]:
            for e2 in self.itemsets[-1]:
                if not e1 <= e2:                    
                    s = frozenset(e2.union(e1))  
                    if not s in items:
                        items.append(s)
        
        self.database.calc_support(items, self.support)        
        self.itemsets.append(self.filter(items, self.support))
        
        return self.itemsets[-1] != []

    def generate_rules(self):
        '''Generate rules from frequent itemsets'''
        
        # start from 2-elements itemsets
        for level in self.itemsets[1:]:
            for itemset in level:
                subsets = Rule.get_subsets(itemset)
                for antecedent in subsets: 
                    consequent = itemset.difference(antecedent)
                    if antecedent and consequent:
                        rule_support = self.support[itemset]
                        rule_confidence = rule_support / self.support[antecedent]
                        if rule_confidence > self.min_confidence:
                            self.rules.append(Rule(antecedent, consequent, rule_support, rule_confidence))
        self.rules.sort(key=lambda rule: rule.confidence)
     
    def run(self, debug=False):
        '''Run Apriory algorithm'''

        if debug: print('Runnung Apriori:')        

        if self.step_0():
            if debug: print('.')
            
            while self.step_k():
                if debug: print('.')
            self.generate_rules()
            
            if debug: print('.done!')  

    def print_itemsets(self):
        '''Print frequent itemsets'''

        k = 0
        for level in self.itemsets:        
            print(f'level {k}')
            for item in level:
                print(list(item))
            k += 1
        print()

    def itemset_count(self):
        '''Calc itemsets count for all levels'''

        return sum([len(item) for item in self.itemsets])

    def print_support(self):
        '''Print support for frequent itemsets'''

        for key in self.support.keys():
            print(list(key), f"{self.support[key]:.2f}")
        print()
   
if __name__ == "__main__":
    # Init database and field list
    db = Database(['A','B','C','D','E','F'])
    
    # Create transactions
    t1 = Transaction(0, frozenset(['A','B','C']))
    t2 = Transaction(1, frozenset(['A','C']))
    t3 = Transaction(2, frozenset(['A','D']))
    t4 = Transaction(3, frozenset(['B','E','F']))

    # Add transactions to database
    db.add_transaction(t1)
    db.add_transaction(t2)
    db.add_transaction(t3)
    db.add_transaction(t4)

    # Output database
    db.print_as_set()
    db.print_as_boolean()

    # Init and run Apriory
    alg = AprioriAlgorithm(db, 0.30, 0.50)
    alg.run()

    # Print results 
    print('Support of frequent itemsets:')
    alg.print_support()

    print('Find rules:')
    alg.print_rules(top=10)
