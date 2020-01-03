program p_ex_03;

type
  TData = char;

  PElem = ^TElem;
  TElem = record
    Data: TData;
    Next: PElem;
    Prev: PElem;
  end;

  TQue = record
    Head: PElem;
    Tail: PElem;
  end;

procedure Create(var q: TQue);
begin
  q.Head := nil;
  q.Tail := nil;
end;

function IsEmpty(const q: TQue): boolean;
begin
  IsEmpty := (q.Head = nil);
end;

procedure Enq(var q: TQue; const data: TData);
var
  p: TElem;
begin
  new(p);
  p^.Data := data;

  if IsEmpty(q) then
    q.Head := p
  else
  begin
    p^.next := q.Tail;
    q.Tail^.prev := p;
  end;

  q.Tail := p;
  p^.prev := nil;
end;

function Deq(var q: TQue; var ErrorCode: integer):TData;
var
  p: TElem;
begin
  if IsEmpty(q) then
  begin
    ErrorCode := -1;
    exit;
  end;

  ErrorCode := 0;
  Result := q.Head^.Data;

  p := q.Head;

  if q.Head = q.Tail then
  begin
    q.Head := nil;
    q.Tail := nil;
  end
  else
    q.Head := q.Head^.prev;

  Dispose(p);
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

begin
  Create(head);

  readln;
end.
