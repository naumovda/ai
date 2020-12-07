from copy import deepcopy

from base import Transaction, Database, AbstractAlgorithm
from rules import Rule


class Vertex:
    '''Vertex of FP-Tree

    item -- an item of vertex
    support -- support of itemset of path [vertex -> root]
    '''

    def __init__(self, parent, item, support):
        '''Init a vertex of FP-Tree

        parent -- parent vertex
        item -- data of vertex
        support -- support of item
        '''

        self.parent = parent
        self.item = item
        self.support = support
        self.childs = []
        self.mark = False

    def _parent_str(self):
        '''Get string presentation of parent vertex'''
        
        if self.parent:
            return f"p:{id(self.parent)}"
        else:
            return "root"

    def _name(self):
        '''Get string presentation of vertex name'''

        if self.item:
            return f"{list(self.item).pop()}"
        else:
            return f"*"

    def _mark(self):
        '''Get string presentation of vertex mark'''

        if self.mark:
            return "+"
        return "-"

    def __str__(self):
        '''Get string presentation of vertex'''

        return f"id:{id(self)} {self._name()}{self._mark()}:{self.support} {self._parent_str()}"

    def add_child(self, item):
        '''Add new child to vertex'''

        child = Vertex(self, item, 0)
        self.childs.append(child)
        return child

    def get_child(self, item):
        '''Get child vertex by item value'''

        for child in self.childs:
            if child.item == item:
                return child
        return None

    def grow(self, itemset, levels):
        '''Grow FP-tree by new itemset'''

        self.support += 1

        if not itemset:
            return        

        word = itemset.pop()
        child = self.get_child(word)
        if child is None:
            child = self.add_child(word)
            for level in levels:
                if level[0] == word:
                    level[1].append(child)
                    break

        child.grow(itemset, levels)

    def clear_mark(self):
        '''Clear all marks of vertex and sub-tree'''

        self.mark = False
        for child in self.childs:
            child.clear_mark()

    def set_mark(self):
        '''Set mark of vertex and parents'''

        self.mark = True
        if self.parent is not None:
            self.parent.set_mark()

    def update_support(self, increase):
        '''Update support of vertex and sub-tree'''

        if self.mark:
            self.support = increase
            self.mark = False
        else:
            self.support += increase

        if self.parent is not None:
            self.parent.update_support(increase)


class FPTree:
    '''FP-Tree

    database -- transaction database
    root -- root of FP-tree
    support -- dictionary of one-element itemsets support
    levels -- list of tuples (word, [vertex, ...])
    '''

    def __init__(self, database):
        self.database = database
        self.root = Vertex(None, None, 0)
        self.support = {}
        self.levels = []

    def build(self, min_support):
        # calc one-element itemset support
        itemsets = [frozenset(field) for field in self.database.fields]
        support = {}
        self.database.calc_support(itemsets, support)

        # generate levels of fp-tree in order of support decrease
        # for items with support >= MinSupp        
        self.levels = [(item, []) for item in support if support[item] >= min_support]
        self.levels.sort(key=lambda item: support[item[0]])

        for transaction in self.database.transactions:
            # generate a list of transaction elements
            # in order of their levels
            words = [level for level, _ in self.levels if level <= transaction.itemset]
            self.root.grow(words, self.levels)

    def print_vertex(self, vertex):
        '''Print vertex and child vertexes'''

        print(vertex)
        for v in vertex.childs:
            self.print_vertex(v)

    def print_tree(self):
        '''Print FP-tree'''

        self.print_vertex(self.root)
        print()

    def print_levels(self):
        '''Print FP-Tree level-by-level'''

        for word, vertexes in reversed(self.levels):
            s = str(word)
            s += ':'
            for vertex in vertexes:
                s += vertex._name() + ':' + str(vertex.support) + ' '
            print(s)
        print()

    def get_level(self, word):
        '''Find level by its frozenset representation'''

        for item in self.levels:
            if word == item[0]:
                return item
        return None

    def get_level_by_name(self, name):
        '''Find level by its field name'''

        return self.get_level(frozenset({name}))

    def clear_marks(self):
        '''Clear marks of all vertexes'''

        self.root.clear_mark()

    def set_marks(self, level):
        '''Set marks of all vertexes of level and their parents'''

        if level is None: return
        
        # set marks for all vertexes in level
        for vertex in level[1]:
            vertex.set_mark()

    def delete_unmarked(self, vertex):
        '''Delete all unmarked vertexes from tree'''

        for _, vertexes in self.levels:
            for vertex in vertexes:
                if not vertex.mark:
                    vertexes.remove(vertex)
                    if vertex.parent is not None:
                        vertex.parent.childs.remove(vertex)                        

    def remove_level(self, level):
        '''Remove level and all in vertexes from fp-tree'''

        if level is None: return
        
        # remove level vertexes from fp-tree
        for vertex in level[1]:
            if vertex.parent:
                vertex.parent.childs.remove(vertex)

        self.levels.remove(level)

    def calc_level_support(self, level):
        '''Calculate summary level suport'''

        return sum(vertex.support for vertex in level[1])

    def recalc_support(self, level):
        '''Update support for fp-tree from vertexes of level and up'''

        for vertex in level[1]:
            vertex.update_support(vertex.support)
                
    def get_condition_tree(self, word):
        '''Get FPC-Tree from FP-Tree by word'''  

        self.clear_marks()

        fpc = deepcopy(self)
        
        level = fpc.get_level(word)
        fpc.set_marks(level)
        fpc.delete_unmarked(fpc.root)        
        fpc.recalc_support(level)        
        fpc.remove_level(level)

        return fpc

    def find(self, min_support, itemset, itemset_list):
        '''Find all frequent itemsets 
            with support >= min_support and which contain itemset,
            and insert them into itemset_list.

        min_support -- value of minimal support
        itemset -- an itemset, a subset
        itemset_list -- a list to update with find itemsets
        '''

        for level in reversed(self.levels):
            word, _ = level
            level_support = self.calc_level_support(level)
            if level_support >= min_support * len(self.database.transactions):
                new_itemset = set(itemset)
                new_itemset.update(set(word))
                itemset_list.append(new_itemset)

                fpc = self.get_condition_tree(word)
                fpc.find(min_support, new_itemset, itemset_list)

    def get_itemsets(self, min_support):
        '''Get all itemsets with support > min_support'''

        itemset = set()
        itemset_list = []
        
        self.find(min_support, itemset, itemset_list)

        return itemset_list

    def calc_itemset_support(self, itemset):
        '''Calc support of itemset using FP-Tree'''

        itemset_list = list(itemset)
        itemset_list.sort(key=lambda item: self.calc_level_support(self.get_level_by_name(item)), 
                          reverse=True)
        
        result = 0

        word = itemset_list.pop()
        level = self.get_level_by_name(word)
        for vertex in level[1]:
            # get itemset from path to root            
            v = vertex
            path = set()
            while v is not None:
                if v.item:
                    path.update(v.item)
                v = v.parent

            # check that itemset is a subset of
            if itemset.issubset(path):
                result += vertex.support
        
        return result / len(self.database.transactions)

class FPGrowthAlgorithm(AbstractAlgorithm):
    '''FP-Growth algorithm for generating association rules

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
        '''

        super().__init__(database, min_support, min_confidence)
        
        self.itemsets = []
        self.support = {}
        self.rules = []
        self.fpt = None

    def get_support(self, itemset):
        '''Get (or calc if not calculated) support of itemset'''

        fs = frozenset(itemset)        
        if not fs in self.support:
            self.support[fs] = self.fpt.calc_itemset_support(itemset)        
        return self.support[fs]

    def get_rules(self):
        '''Generate association rules from frequent itemsets'''

        rules = []
        for itemset in self.itemsets:
            # if itemset is one-element, then antecedent or consequent
            # will be empty, so skip
            if len(itemset) > 1: 
                subsets = Rule.get_subsets(itemset)                
                for antecedent in subsets: 
                    consequent = itemset.difference(antecedent)
                    if antecedent and consequent:
                        rule_support = self.get_support(itemset)
                        rule_confidence = rule_support / self.get_support(antecedent)
                        if rule_confidence > self.min_confidence:
                            rules.append(Rule(antecedent, consequent, rule_support, rule_confidence))
        rules.sort(key=lambda rule: rule.confidence)
        return rules
     
    def run(self, debug=False):
        '''Run FP-Growth algorithm'''

        if debug: 
            print('Runnung FP-Growth...')        

        # init fields
        self.itemsets = []
        self.support = {}
        self.rules = []

        # create and build fp-tree
        self.fpt = FPTree(self.database)
        self.fpt.build(self.min_support)
        
        self.itemsets = self.fpt.get_itemsets(self.min_support)
        self.itemsets.sort(key=lambda itemset: self.fpt.calc_itemset_support(itemset))

        self.rules = self.get_rules()

        if debug: 
            print('FP-Growth done!')

    def print_itemsets(self):
        '''Print frequent itemsets'''

        for itemset in self.itemsets:        
            print(itemset)
        print()

    def print_support(self):
        '''Print support of frequent itemsets'''

        itemsets = list(self.support.keys())
        itemsets.sort(key=lambda item: self.support[item], reverse=True)

        for item in itemsets:
            print(f"{list(item)} supp = {self.support[item]:.2f}")
        print()

if __name__ == "__main__":
    # initialize database object with list of fields
    db = Database(['A','B','C','D','E','F', 'G']) 
    
    # add transactions to database
    db.add_transaction(Transaction(0, ['A', 'D', 'F']))
    db.add_transaction(Transaction(1, ['A', 'C', 'D', 'E']))
    db.add_transaction(Transaction(2, ['B', 'D']))
    db.add_transaction(Transaction(3, ['B', 'C', 'D']))
    db.add_transaction(Transaction(4, ['B', 'C']))
    db.add_transaction(Transaction(5, ['A', 'B', 'D']))
    db.add_transaction(Transaction(6, ['B', 'D', 'E']))
    db.add_transaction(Transaction(7, ['B', 'C', 'E', 'G']))
    db.add_transaction(Transaction(8, ['C', 'D', 'F']))
    db.add_transaction(Transaction(9, ['A', 'B', 'D']))

    # print database
    db.print_as_boolean()

    # create and build fp-tree
    fpt = FPTree(db)
    fpt.build(min_support=0.3)

    print("FP-Tree:")
    fpt.print_tree()

    print("FP-Tree levels:")
    fpt.print_levels()

    # create fpc-tree
    word_e = frozenset({'E'})
    fpct = fpt.get_condition_tree(word_e)

    print("FPC-Tree|E:")
    fpct.print_tree()

    print("FPC-Tree|E levels:")
    fpct.print_levels()

    print("Frequent itemsets (MinSupp >= 0.3):")
    itemsets = fpt.get_itemsets(min_support=0.3)
    print(itemsets)
    print()

    print('Support({A, D}):')
    print(fpt.calc_itemset_support({'A', 'D'}))
    print()

    # Init and run FP-Growth algorithm
    alg = FPGrowthAlgorithm(db, 0.30, 0.50)
    alg.run()

    # Print results 
    print('Support of frequent itemsets:')
    alg.print_support()

    print('Find rules:')
    alg.print_rules(top=10)
