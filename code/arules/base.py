class Transaction:
    '''Transaction in database

    tid -- transaction id: int
    itemset -- itemset of elements in transaction
    '''

    def __init__(self, tid, itemset):
        '''Init transaction object

        tid -- identifier
        itemset -- itemset of transaction
        '''

        self.tid = tid
        self.itemset = frozenset(itemset)

    def get_list(self):
        '''Get tranaction as list'''
        
        return list(self.itemset)

    def get_boolean(self, fields):
        '''Get tranaction in boolean presentation'''

        digit = lambda value: "1" if value in self.itemset else "0"
        return [digit(item) for item in fields]

class Database:
    '''Database

    fields -- a list of database fields 
    transactions -- a list of database fields
    '''

    def __init__(self, fields, transactions=None):
        self.fields = fields
        self.transactions = transactions if transactions is not None else []

    def add_transaction(self, item):
        '''Add transaction to database'''

        self.transactions.append(item)

    def print_as_set(self):
        '''Print all transactions as set'''

        for transaction in self.transactions:
            print(transaction.tid, " ".join(transaction.get_list()))
        print()

    def print_as_boolean(self):
        '''Print all transactions as boolean table'''

        print("#", *self.fields)
        for transaction in self.transactions:
            print(transaction.tid, *transaction.get_boolean(self.fields))
        print()

    def calc_support(self, itemsets, support):
        '''Calculate support of itemsets in list'''

        count = {itemset:0 for itemset in itemsets}
        for transaction in self.transactions:
            for itemset in itemsets: 
                if itemset <= transaction.itemset:
                    count[itemset] += 1
        total = len(self.transactions)   
        for itemset in itemsets: 
            support[itemset] = count[itemset] / total      

    def load(self, file_name):
        '''Load database from external file'''
        raise NotImplementedError

class AbstractAlgorithm:
    '''Abstract algorithm for generating association rules

    database -- transaction database
    min_support -- minimum support
    min_confidence -- minimum confidence
    rules -- list of rules
    '''

    def __init__(self, database, min_support, min_confidence):
        '''Init algorithm object

        database -- transaction database
        min_support -- minimum support
        min_confidence -- minimum confidence
        rules -- list of rules
        '''

        self.database = database
        self.min_support = min_support
        self.min_confidence = min_confidence
        self.rules = []
    
    def run(self, debug=False):
        '''Run association rules calculation'''

        raise NotImplementedError    

    def print_rules(self, top=0):
        '''Print association rules'''

        for item in self.rules[:top]:
            print(item, f"supp = {item.support:.2f} conf = {item.confidence:.2f}")

    def print_description(self, top=0, names=None):
        '''Print association rules with description'''        
        for item in self.rules[:top]:
            print(item.description(names), 
                  f"supp = {item.support:.2f} conf = {item.confidence:.2f}")    

    def print_itemsets(self):
        '''Print frequent itemsets'''                

        raise NotImplementedError    

    def print_support(self):
        '''Print itemsets support'''                        

        raise NotImplementedError
