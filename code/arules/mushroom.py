import csv
from base import apriori, database, rule, transaction

cap_shape = {'b':'bell', 'c':'conical', 'x':'convex', 'f':'flat', 'k':'knobbed', 's':'sunken'}
cap_surface = {'f':'fibrous', 'g':'grooves', 'y':'scaly', 's':'smooth'}
cap_color = {'n':'brown', 'b':'buff', 'c':'cinnamon','g':'gray','r':'green','p':'pink','u':'purple','e':'red','w':'white','y':'yellow'}
bruises = {'t':'bruises','f':'no'}
odor = {'a':'almond', 'l':'anise','c':'creosote','y':'fishy','f':'foul','m':'musty','n':'none','p':'pungent','s':'spicy'}
gill_attachment = {'a':'attached','d':'descending','f':'free','n':'notched'}
gill_spacing = {'c':'close', 'w':'crowded', 'd':'distant'}
gill_size = {'b':'broad', 'n':'narrow'}
gill_color = {'k':'black','n':'brown','b':'buff','h':'chocolate','g':'gray', 'r':'green','o':'orange','p':'pink', 'u':'purple','e':'red', 'w':'white','y':'yellow'}
stalk_shape = {'e':'enlarging','t':'tapering'}
stalk_root = {'b':'bulbous','c':'club','u':'cup','e':'equal', 'z':'rhizomorphs','r':'rooted', '':'missing'}
stalk_surface_above_ring = {'f':'fibrous','y':'scaly','k':'silky','s':'smooth'}
stalk_surface_below_ring = {'f':'fibrous','y':'scaly','k':'silky','s':'smooth'}
stalk_color_above_ring = {'n':'brown', 'b':'buff', 'c':'cinnamon','g':'gray','o':'orange','p':'pink','e':'red','w':'white','y':'yellow'}
stalk_color_below_ring = {'n':'brown', 'b':'buff', 'c':'cinnamon','g':'gray','o':'orange','p':'pink','e':'red','w':'white','y':'yellow'}
veil_type = {'p':'partial', 'u':'universal'}
veil_color = {'n':'brown', 'o':'orange', 'w':'white','y':'yellow'}
ring_number = {'n':'none','o':'one', 't':'two'}
ring_type = {'c':'cobwebby','e':'evanescent','f':'flaring','l':'large', 'n':'none','p':'pendant','s':'sheathing','z':'zone'}
spore_print_color = {'k':'black', 'n':'brown', 'b':'buff', 'h':'chocolate', 'r':'green', 'o':'orange', 'u':'purple', 'w':'white', 'y':'yellow'}
population = {'a':'abundant','c':'clustered','n':'numerous', 's':'scattered','v':'several','y':'solitary'}
habitat = {'g':'grasses','l':'leaves','m':'meadows','p':'paths', 'u':'urban','w':'waste','d':'woods'}
mushroom_class = {'e':'editable', 'p':'poisonous'}

dicts = [
    ('cap-shape', cap_shape),
    ('cap-surface', cap_surface),
    ('cap-color', cap_color),
    ('bruises', bruises),
    ('odor', odor),
    ('gill-attachment', gill_attachment),
    ('gill-spacing', gill_spacing),
    ('gill-size', gill_size),
    ('gill-color', gill_color),
    ('stalk-shape', stalk_shape), 
    ('stalk-root', stalk_root), 
    ('stalk-surface-above-ring', stalk_surface_above_ring),
    ('stalk-surface-below-ring', stalk_surface_below_ring),
    ('stalk-color-above-ring', stalk_color_above_ring),
    ('stalk-color-below-ring', stalk_color_below_ring),
    ('veil-type', veil_type),
    ('veil-color', veil_color),
    ('ring-number', ring_number),
    ('ring-type', ring_type),
    ('spore-print-color', spore_print_color),
    ('population', population),
    ('habitat', habitat),
    ('class', mushroom_class)
]

universal = []
indexes = {}  
names = {}

index = 0
for d in dicts:
    name = d[0]
    elems = d[1]
    for item in sorted(elems.keys()):
        indexes[f"{name}-{elems[item]}"] = index
        names[index] = f"{name}-{elems[item]}"
        universal.append(index)
        index += 1

class mushroom_database(database):
    def create_transaction(self, tid, field_names, row):
        s = list()
        k = 0
        for field in field_names:            
            title = f"{field}-{dicts[k][1][row[field]]}"            
            s.append(str(indexes[title]))
            k += 1
        return transaction(tid, frozenset(s))

    def load(self, file_name):
        with open(file_name, newline='') as csvfile:
            reader = csv.DictReader(csvfile)
            self.field_names = reader.fieldnames
            self.fields = [str(key) for key in sorted(names.keys())]
            self.items = []
            tid = 0       
            for row in reader:
                trans = self.create_transaction(tid, self.field_names, row)
                self.add_transaction(trans)
                tid += 1

if __name__ == "__main__":
    # print(universal)
    # print(indexes)
    # print(names)

    # загружаем файл данных
    db = mushroom_database()
    db.load('mushroom_csv.csv')

    # db.print_as_set()
    
    # инициализируем и запускаем алгоритм Apriori
    alg = apriori(db, 0.30, 0.50)

    alg.run(debug=True)

    # печатаем результаты
    alg.print_itemsets()    
    alg.print_support()
    alg.print_rules()