//массив создается статически
//каждый элемент массива создается статически

program record_pointer_03;

const
  MAX = 2;

type
  //структура данных
  TData = record
    code: integer;
    name: string[15];
    price: real;
  end;

  //массив данных
  TArray = array[1..MAX] of TData;

var
  //массив данных
  v: TArray;

  //переменная для информационной структуры данных
  d: TData;

  //счетчики
  i, j: integer;

  //переменные для ввода
  code: integer;
  name: string[15];
  price, avg: real;

begin
  //введем значения элементов данных
  for i := 1 to MAX do
  begin
    writeln('enter ', i, ' element:');
    write('code=');
    readln(code);
    write('name=');
    readln(name);
    write('price=');
    readln(price);

    v[i].code := code;
    v[i].name := name;
    v[i].price := price;
  end;

  //выведем массив
  writeln('source:');
  writeln('N':3, 'Code': 8, 'Name':15, 'Price':6);
  for i := 1 to MAX do
    writeln(i:3, v[i].code:8, v[i].name:15, v[i].price:6:2);
  writeln;

  //рассчитаем среднюю цену
  avg := 0;
  for i := 1 to MAX do
    avg := avg + v[i].price;
  avg := avg / MAX;
  writeln('average price=', avg:6:2);

  //выполним сортировку данных по возрастанию цены
  for i := 1 to MAX-1 do
    for j := i+1 to MAX do
      if v[i].price > v[j].price then
      begin
        d := v[i];
        v[i] := v[j];
        v[j] := d;
      end;

  //выведем отсортированный массив
  writeln('sorted:');
  writeln('N':3, 'Code': 8, 'Name':15, 'Price':6);
  for i := 1 to MAX do
    writeln(i:3, v[i].code:8, v[i].name:15, v[i].price:6:2);
  writeln;
end.
