program p_ex_03;

type
  TData = char;

  PElem = ^TElem;
  TElem = record
    Data: TData;
    Next: PElem;
  end;

function IsOpenBracet(c: char): boolean;
begin
  Result := c in ['(', '[', '{']
end;

function IsCloseBracet(c: char): boolean;
begin
  Result := c in [')', ']', '}']
end;

function IsComplimentary(c1, c2: char): boolean;
var
  c3: char;
begin
  case c1 of
   '(': c3:= ')';
   '[': c3:= ']';
   '{': c3:= '}';
  else
    c3 := ' ';
  end;

  Result := IsOpenBracet(c1)
    and IsCloseBracet(c2)
    and (c3 = c2);
end;

procedure Create(var p: PElem);
begin
  p := nil;
end;

function IsEmpty(const p: PElem): boolean;
begin
  IsEmpty := (p = nil);
end;

procedure Push(const Data: TData; var p: PElem);
var
  q: PElem;
begin
  new(q);
  q^.Data := Data;
  q^.Next := p;
  p := q;
end;

function Pop(var p: PElem; var ErrorCode: integer):TData;
var
  q: PElem;
begin
  if IsEmpty(p) then
  begin
    ErrorCode := -1;
    exit;
  end;

  ErrorCode := 0;

  Result := p^.Data;
  q := p;
  p := p^.next;
  dispose(q);
end;

procedure Print(const p: PElem);
var
  q: PElem;
begin
  q := p;

  if IsEmpty(q) then
    writeln('Stack is empty')
  else
    while q<>nil do
    begin
      write(q^.Data:0, ' ');

      q := q^.next;
    end;

  writeln;
end;

var
  head: PElem;

  i, ErrorCode: integer;
  c: TData;

  s: string;

  balance: boolean;
begin
  Create(head);

  write('Input expression =');
  readln(s);

  balance := true;
  i := 1;
  while balance and (i <= length(s)) do
  begin
    if IsOpenBracet(s[i]) then
      Push(s[i], head)
    else
      if IsCloseBracet(s[i]) then
        if IsEmpty(head) then
          balance := false
        else
        begin
          c := Pop(head, ErrorCode);

          if not IsComplimentary(c, s[i]) then
            balance := false;
        end;

    i := i + 1;
  end;

  balance := balance and IsEmpty(head);

  writeln('Is balanced? ', balance);

  readln;
end.

