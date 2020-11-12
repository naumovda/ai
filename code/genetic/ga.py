from math import sin, pi
import random 
import numpy as np
import matplotlib.pyplot as plt

class Species:
    MIN = 0
    MAX = 5
    N = 2 ** 16
    MUT_PROBABILITY = 0.15 # вероятность мутации

    def __init__(self, value = None):
        if value:
            self.value = value
        else:
            self.value = random.randint(Species.MIN, Species.MAX)            
        self.set_gen()

    def __lt__(self, value):
        return self.fitness() < value.fitness()

    def set_gen(self):
        '''Преобразование фенотипа в генотип'''
        x = int((self.value - Species.MIN) / (Species.MAX - Species.MIN) * Species.N)
        s = ''
        while x > 0:
            s += str(x % 2)
            x //= 2
        
        for _ in range(16 - len(s)):
            s = '0' + s 
        self.gen = s

    @classmethod    
    def get_pheno(cls, gen):
        '''Получение фенотипа из генотипа'''
        t = 0
        p = 1
        for bit in gen:
            if bit == '1':
                t += p
            p *= 2
        return Species.MIN + t*(Species.MAX-Species.MIN) / Species.N

    def pheno(self):
        return Species.get_pheno(self.gen)

    def fitness(self):
        '''Фитнесс-функция'''
        # f(t)=(1.5t+0.9)sin(πt+1.1) 
        return (1.5 * self.value + 0.9)*sin(pi*self.value + 1.1)

    @staticmethod
    def inverse(bit):
        return '1' if bit == '0' else '0'

    # Функция мутации
    def mutation(self):
        if random.random() < Species.MUT_PROBABILITY:
            mutation_index = random.randint(0, len(self.gen)-1)      
            new_gen = self.gen[:mutation_index] + Species.inverse(self.gen[mutation_index]) + self.gen[mutation_index+1:]
            new_species = Species(Species.get_pheno(new_gen))
            return new_species
        else:
            return None

class Population:
    SPC_COUNT = 40 # кол-во особей в популяции

    def __init__(self):
        self.species = [Species() for _ in range(Population.SPC_COUNT+1)]

    def max_fitness(self):
        '''определение максимума функции приспособленности'''
        return max([item.fitness() for item in self.species])

    def avg_fitness(self):
        '''определение среднего функции приспособленности'''
        return sum([item.fitness() for item in self.species]) / len(self.species)

    def __str__(self):
        return f"max = {self.max_fitness():.2f} avg = {self.avg_fitness():.2f}"

    def mutation(self):
        '''добавление в популяцию особей с мутацией'''
        new_population = []
        for species in self.species:
            new_species = species.mutation()
            if new_species:
                new_population.append(new_species)
        self.species.extend(new_population)

    def selection(self):        
        self.species.sort(reverse=True) # сортируем популяцию b         
        self.species = self.species[:Population.SPC_COUNT+1] # отбираем только лучшие SPC_COUNT особей

class Evolution: 
    def __init__(self, gen_count):        
        self.gen_count = gen_count
        self.generation = None
        self.population = None
        self.max = []
        self.avg = []

    def add_statistic(self):
        self.max.append(self.max_fitness())  
        self.avg.append(self.avg_fitness())  

    def start(self):        
        self.max = []
        self.avg = []        
        self.generation = 0
        self.population = Population()       
        self.add_statistic()

    def next(self):
        self.generation += 1
        # применяем оператор мутации
        self.population.mutation()        
        # выполняем xотбор
        self.population.selection()
        self.add_statistic()

    def run(self, debug=False):
        self.start()
        if debug: print(f"start generation: {self.population}")      

        for k in range(self.gen_count):            
            self.next()
            if debug: print(f"generation {self.generation}: {self.population}")

    def max_fitness(self):
        return self.population.max_fitness()

    def avg_fitness(self):
        return self.population.avg_fitness()

def test_runonce():
    gen_count = 50
    e = Evolution(gen_count)
    e.run(debug=True)
    print(f'After {gen_count} generation:')
    print(e.population)
    print(f'The best species is {e.population.species[0].value} ({e.population.species[0].gen}) with fitness: {e.population.species[0].fitness():.2f}')

def test_function():
    fitness = lambda x: (1.5*x+0.9)*sin(pi*x+1.1)
    x = np.arange(0, 5, 0.1)
    y = np.array([fitness(elem) for elem in x])
    _ = plt.figure()
    plt.plot(x, y)
    plt.title('Fitness functions')
    plt.ylabel('y')
    plt.xlabel('x')
    plt.grid(True)
    plt.show() 

def test_picture():
    gen_count = 140
    e = Evolution(gen_count)
    e.run()     
    
    x = np.arange(0, gen_count+1, 1)    
    y1 = np.array([int(elem) for elem in e.avg])
    y2 = np.array([int(elem) for elem in e.max])

    plt.title('Fitness')
    plt.plot(x, y1, 'r', x, y2, 'g') 
    plt.xlabel("generations")
    plt.ylabel("fitness")
    plt.title("Fitness(generation number)")
    plt.legend(['AVG fitness', 'MAX fitness'])
    plt.grid()
    plt.show()    
   
if __name__ == "__main__":
    # test_function()
    # test_runonce()
    test_picture()