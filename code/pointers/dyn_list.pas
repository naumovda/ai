program dyn_list;

//константа, погрешность для сравнения вещественных чисел
const
  eps = 1e-6;

type
  //тип данных - данные элемента списка
  TData = real;

  //указатель на элемент списка
  PElement = ^TElement;

  //элемент списка
  TElement = record
    Data: TData;    // данные
    Next: PElement; // указатель на следующий элемент списка
  end;

//функция проверки, что список пуст
//  List: PElement - список
//возвращаемое значение:
//  True - если список пуст (List = nil)
//  False - если список не пуст (List <> nil)
function IsEmpty(const List: PElement):boolean;
begin
  IsEmpty := List = nil;
end;

//функция печати списка
//  List: PElement - список
//возвращаемое значение: нет
procedure PrintList(const List: PElement);
var
  p: PElement; //"счетчик" списка, указатель на текущий элемент
begin
  writeln;
  if IsEmpty(List) then
    //для пустого списка выводим информацию об этом
    write('List is empty')
  else
    begin
      //начинаем с начала списка
      p := List;
      while p <> nil do //пока не достигнут конец списка
        begin
          write(p^.Data:6:2, ' '); //вывфодим данные
          p := p^.Next; //перемещаемся к следующему элементу списка
        end;
    end;
  writeln;
end;

//функция удаления первого элемента списка
//  List: PElement - список
//возвращаемое значение:
//  List: PElement - список, указатель на первый элемент
//  Data: TData - данные из первого элемента
//  ErrorCode: integer - признак ошибки
//    -1 - если список пуст
//     0 - если ошибки нет
procedure Pop(var List: PElement; var Data: TData; var ErrorCode: integer);
var
  p: PElement;
begin
  if IsEmpty(List) then
    ErrorCode := -1 //устанавливаем признак ошибки, если список пуст
  else
    begin
      ErrorCode := 0; //нет ошибки
      Data := List^.Data; //возвращаем данные первого элемента
      p := List; //сохраняем указатель на первый элемент списка
      List := List^.next; //сдвигаем указатель на следующий элемент списка
      Dispose(p); //освобождаем память, занимаемую первым элементом
    end;
end;

//функция удаления последнего элемента списка
//  List: PElement - список
//возвращаемое значение:
//  List: PElement - список, указатель на первый элемент
//  Data: TData - данные из первого элемента
//  ErrorCode: integer - признак ошибки
//    -1 - если список пуст
//     0 - если ошибки нет
procedure Tail(var List: PElement; var Data: TData; var ErrorCode: integer);
var
  p: PElement;
begin
  if IsEmpty(List) then
    ErrorCode := -1 //если список пуст, устанавливаем принзак ошибки
  else
    begin
      ErrorCode := 0; //если список не пуст, ошибки нет

      if List^.Next = nil then
        //если список состоит из одного элемента
        begin
          Data := List^.Data; //получаем данные элемента
          Dispose(List); //освобождаем память
          List := nil; //список теперь пуст
        end
      else
        //если список содержит более одного элемента
        begin
          //устанавливаем указатель на певый элемент списка
          p := List;
          //пока следующий элемент не является последним
          while p^.Next^.Next <> nil do
            p := p^.Next; //перемещаем указатель дальше по списку
          Data := p^.Next^.Data; //возвращаем данные последнего элемента
          Dispose(p^.Next); //освобождаем память
          p^.Next := nil; //устанавливаем ссылку последнего элемента на nil
        end;
    end;
end;

//функция добавления элемента в начало списка
//  List: PElement - список
//  Data: TData - данные элемента
//возвращаемое значение:
//  List: PElement - список, указатель на первый элемент
procedure Append(var List: PElement; const Data: TData);
var
  p: PElement; //указатель на элемент списка
begin
  New(p); //выделяем память под новый элемент списка
  p^.Data := Data; //устанавливаем данные
  p^.Next := List; //новый элемент ссылает на последний элемент списка
  List := p; //смещаем указатель списка
end;

//функция добавления элемента в список после заданного
//  List: PElement - список
//  Elem: PElement - элемент, после которого добавляем элемент
//  Data: TData - данные элемента
//возвращаемое значение:
//  List: PElement - список, указатель на первый элемент
procedure InsertAfter(var List, Elem: PElement; const Data: TData);
var
  p: PElement;
begin
  if (Elem = nil) and (List <> nil) then
    //если элемент пустой, а список - нет, то ничего не делаем
    exit;

  //если список пуст
  if List = nil then
    //то добавляем в начало списка
    Append(List, Data)
  else
    begin
      new(p); //выделяем память
      p^.Data := Data; //записываем данные
      p^.Next := Elem^.Next; //устанавливаем связи
      Elem^.Next := p;
    end;
end;

//функция удаления элемента из списка
//  List: PElement - список
//  Elem: PElement - указатель на удаляемый элемент
//возвращаемое значение:
//  List: PElement - список, указатель на первый элемент
//  Data: TData - данные удаляемого элемента
//  ErrorCode: integer - признак ошибки
procedure Delete(var List, Elem: PElement; var Data: TData;
  var ErrorCode: integer);
var
  p: PElement;
begin
  if Elem = nil then
    //если элемент указывает на nil, ошибка
    ErrorCode := -1
  else
    if List = Elem then
      //если удаляется первый элемент
      Pop(List, Data, ErrorCode)
    else
      begin
        ErrorCode := 0; //устанавливаем признак ошибки

        //начинаем с начала списка
        p := List;
        //пока не найден предшествующий элемент
        while p^.Next <> Elem do
          p := p^.Next;
        //меняяем связи
        p^.Next := Elem^.Next;
        Data := Elem^.Data;
        //освобождаем память
        Dispose(Elem);
      end;
end;

//функция удаления всех элементов списка
//  List: PElement - список
//возвращаемое значение:
//  List: PElement - список, указатель на первый элемент
procedure ClearList(var List: PElement);
var
  Data: TData;
  ErrorCode: integer;
begin
  //пока список не будет пуст
  while not IsEmpty(List) do
    Pop(List, Data, ErrorCode); //удаляем по одному элементу
end;

//функция инициализации списка
//  List: PElement - список
//возвращаемое значение:
//  List: PElement - список, указатель на первый элемент
procedure CreateList(var List: PElement);
begin
  ClearList(List); //очищаем список
  List := nil; //устанавливаем указатель начала списка на nil
end;

//функция удаления элемента из списка
//  List: PElement - список
//  Data: TData - данные удаляемого элемента
//возвращаемое значение:
//  Find: PElement - указатель на найденный элемент
function Find(const List:PElement; const Data: TData):PElement;
var
  p: PElement;
begin
  p := List; //начинаем с начала списка

  //пока не дошли до конца списка и пока не нашли элемент
  while (p <> nil) and (abs(p^.Data-Data) > eps) do
    p := p^.Next; //переходим к следующему элементу

  //возвращаем значение
  Find := p;
end;

var
  List, Elem: PElement;

  Data: TData;

  ErrorCode: integer;
begin
  CreateList(List);

  PrintList(List);

  Append(List, 13.1);
  Append(List, 12.1);
  Append(List, -4.2);

  PrintList(List);

  Append(List, 0);

  PrintList(List);

  writeln;

  Elem := Find(List, 12.1);

  if Elem <> nil then
    begin
      writeln('find 12.1');

      Delete(List, Elem, Data, ErrorCode);

      if ErrorCode = 0 then
        writeln('deletion done')
      else
        writeln('deletion fail');
    end
  else
    begin
      writeln('not find 12.1');
    end;

  PrintList(List);

  writeln;

  Elem := Find(List, 0);

  if Elem <> nil then
    begin
      writeln('find 0 at top');

      Delete(List, Elem, Data, ErrorCode);

      if ErrorCode = 0 then
        writeln('deletion done')
      else
        writeln('deletion fail');
    end
  else
    begin
      writeln('not find 12.1');
    end;

  PrintList(List);

  writeln;

  Elem := Find(List, 13.10);

  if Elem <> nil then
    begin
      writeln('find 13.1 at tail');

      InsertAfter(List, Elem, 5.1);

      writeln('insert 5.1 after 13.1');
      PrintList(List);
      writeln;

      Delete(List, Elem, Data, ErrorCode);

      if ErrorCode = 0 then
        writeln('deletion done')
      else
        writeln('deletion fail');

      writeln('deleted 13.1');
      PrintList(List);
      writeln;

      Tail(List, Data, ErrorCode);

      if ErrorCode = 0 then
        writeln('deletion done')
      else
        writeln('deletion fail');

      writeln('deleted tail');
      PrintList(List);
      writeln;
    end
  else
    begin
      writeln('not find 13.1');
    end;

  ClearList(List);

  PrintList(List);

  readln;
end.

