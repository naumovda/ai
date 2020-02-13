class vertex:
    def __init__(self, item, support):
        self.item = item
        self.support = support
        self.childs = []

    def get_item(self, item):
        for child in self.childs:
            if child.item == item:
                return child
        v = vertex(item, 0)
        self.childs.append(v)
        return v

    # def add_child(self, child):
    #     item = self.get_item(child)
    #     item.support += 1

class fptree:
    def __init__(self):
        self.root = vertex(None, 0) # корень fp-дерева
        self.support = {} # словарь поддержки одноэлементных наборов
        
    def add_list(self, items):
        v = self.root
        for item in items:
            v = v.get_item(item)
            v.support += 1

    def add_itemset(self, itemset):
        items = list(itemset)
        items.sort(key=lambda item: self.support[item])
        self.add_list(items)
        
if __name__ == "__main__":
    fpt = fptree()

    fs1 = frozenset([1, 2, 3])
    fs2 = frozenset([1, 3])
    fs3 = frozenset([3, 4])
