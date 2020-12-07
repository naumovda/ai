class Rule:
    ''' Association Rule Class'''

    def __init__(self, antecedent, consequent, support, confidence):
        self.antecedent = antecedent
        self.consequent = consequent
        self.support = support
        self.confidence = confidence  

    def __str__(self):
        '''String representation of rule'''

        return f"{list(self.antecedent)} => {list(self.consequent)}"
    
    def description(self, names):
        '''String representation of rule with description'''

        ant = [names[item] for item in list(self.antecedent)]
        cons = [names[item] for item in list(self.consequent)]
        return f"{ant} => {cons}"

    @staticmethod    
    def get_subsets(source):    
        '''Get subsets of source'''
        
        result = []
        for item in list(source):
            temp = source.difference(frozenset([item]))
            if temp != []:
                result.append(temp)
                for elem in Rule.get_subsets(temp):
                    if elem != [] and not elem in result:
                        result.append(elem)
        return result
