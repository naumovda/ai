program p_ex_02;

function f(n: integer):integer;
begin
  writeln(n);
  f := f(n+1);
end;

type
  ta = array[1..8096] of integer;
var
  i: integer;
  p: ^ta;
begin
  i := 0;
  //writeln(f(0));
  while true do
  begin
    i := i + 1;
    writeln(i);
    new(p);
  end;

  readln;
end.

