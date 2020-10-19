from math import sin, pi

class Individual:
    MIN = 0
    MAX = 5
    N = 2 ** 16

    def __init__(self, value):
        self.value = value
        self.set_gen()

    def set_gen(self):
        '''Преобразование фенотипа в генотип'''
        x = int((self.value - Individual.MIN) \
            / (Individual.MAX - Individual.MIN) * Individual.N)
        s = ''
        while x > 0:
            s += str(x % 2)
            x //= 2
        
        for _ in range(16 - len(s)):
            s = '0' + s 
        self.gen = s
        
    def set_pheno(self):
        '''Преобразование генотипа в фенотипа'''
        pass
        t = 0
        p = 1
        for bit in self.gen:
            if bit == 1:
                t += p
            p *= 2
        return Individual.MIN + t*(Individual.MAX-Individual.MIN)\
            / Individual.N

    # Фитнесс-функция
    # f(t)=(1.5t+0.9)sin(πt+1.1) 
    def finness(self):
        return (1.5 * self.value + 0.9)*sin(pi*self.value + 1.1)

# const
#   Pm = 4e-1; //вероятность мутации
#   RUN_COUNT = 20; //количество поколений
#   P_COUNT = 20; //кол-во особей в популяции

# type
#   //сама популяция
#   TPopulation = array[1..2*P_COUNT] of string;


# function Mutation(s: string): string;
# var
#   n: integer;
#   s1: string;
# begin
#   s1 := s;

#   //номер гена для мутации
#   n := round(length(s)*random)+1;

#   if s1[n] = '1' then
#     s1[n] := '0'
#   else
#     s1[n] := '1';

#   Mutation := s1;
# end;


# //определение макисмума функции приспособленности
# function MaxF(var P:TPopulation):real;
# var
#   i: integer;
#   tmax: real;
# begin
#   tmax := f(GenToPheno(P[1], MIN, MAX));

#   for i := 2 to P_COUNT do
#     if f(GenToPheno(P[i], MIN, MAX)) > tmax then
#       tmax := f(GenToPheno(P[i], MIN, MAX));

#   Result := tmax;
# end;

# //определение среднего функции приспособленности
# function AvgF(var P:TPopulation):real;
# var
#   i: integer;
#   tavg: real;
# begin
#   tavg := 0;

#   for i := 1 to P_COUNT do
#      tavg := tavg + f(GenToPheno(P[i], MIN, MAX));

#   Result := tavg / P_COUNT;
# end;

# //вывод популяции
# procedure WriteP(var P:TPopulation; Count: integer);
# var
#   i: integer;
# begin
#   writeln('Population:');
#   for i := 1 to Count do
#   begin
#     write('i = ');
#     write(P[i], ' ');
#     write(GenToPheno(P[i], MIN, MAX):0:4, ' ');
#     write(f(GenToPheno(P[i], MIN, MAX)):0:4, ' ');
#     writeln;
#   end;
#   writeln;
# end;

# var
#   //популяция
#   Population: TPopulation;

#   //новое поколение + старое
#   NewPopulation: TPopulation;

#   //всего новых особей
#   NewCount: integer;

#   i, j, p, k: integer;
#   x, rnd: real;

#   tmp_s: string;
#   f1, f2: real;

# begin
#   //PhenoToGen(1, 0, 5);
#   //writeln(GenToPheno('0011001100110011', 0, 5));
#   randomize;

#   //формируем начальную популяцию
#   for i := 1 to P_COUNT do
#   begin
#     x := random * (MAX-MIN) + MIN;

#     //получаем генотип, сохраняем в Population
#     Population[i] := PhenoToGen(x, MIN, MAX);
#   end;

#   writeln('START');
#   writeln('Max fitness = ', MaxF(Population));
#   writeln('Avg fitness = ', AvgF(Population));

#   // запуск эволюции!
#   for k := 1 to RUN_COUNT do
#   begin
#     WriteP(Population, P_COUNT);

#     //применяем оператор мутации
#     NewCount := 0;
#     for i := 1 to P_COUNT do
#     begin
#       if random < Pm then //если мутация сработала
#       begin
#         //добавляем в NewPopulation
#         NewCount := NewCount + 1;

#         NewPopulation[NewCount] := Mutation(Population[i]);
#       end;
#     end;

#     //добавляем старое поколение
#     for i := 1 to P_COUNT do
#     begin
#       NewCount := NewCount + 1;
#       NewPopulation[NewCount] := Population[i];
#     end;

#     WriteP(NewPopulation, NewCount);

#     //сортируем популяцию
#     for i := 1 to NewCount-1 do
#       for j := i+1 to NewCount do
#       begin
#         f1 := f(GenToPheno(NewPopulation[i], MIN, MAX));
#         f2 := f(GenToPheno(NewPopulation[j], MIN, MAX));

#         if f1 < f2 then
#         begin
#           tmp_s := NewPopulation[i];
#           NewPopulation[i] := NewPopulation[j];
#           NewPopulation[j] := tmp_s;
#         end;
#       end;

#     //отбираем только лучшие P_COUNT особей
#     for i := 1 to P_COUNT do
#       Population[i] := NewPopulation[i];

#     //выводим статистику
#     writeln('Generation ', k);
#     writeln('Max fitness = ', MaxF(Population));
#     writeln('Avg fitness = ', AvgF(Population));
#   end;

#   writeln('Press enter');
#   readln;
# end.



