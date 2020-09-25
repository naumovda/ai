program eight;

type
  TBase = 1..8;
  TField = array[1..3, 1..3] of TBase;

var
  field: TField;
  i, j: integer;

  s: set of TBase;
  n: integer;

  count: integer;

procedure WriteField(const f: TField);
var
  i, j: integer;
begin
  for i := 1 to 3 do
  begin
    for j := 1 to 3 do
      write('[', f[i,j],']');
    writeln;
  end;

end;

begin
  randomize;

  count := 0;

  s := [];

  for i := 1 to 3 do
    for j := 1 to 3 do
      begin
        repeat
          n := random(9);
          inc(count);
        until not (n in s);
        field[i, j] := n;
        s := s + [n];
      end;

  WriteField(field);

  Writeln('C = ',count);
  readln;
end.

