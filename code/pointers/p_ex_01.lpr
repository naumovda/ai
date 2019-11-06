program p_ex_01;

var
  i: integer;
  p: ^integer;

begin
  i := 10;
  p := @i;

  writeln('i = ', i);

  //writeln('p = ', p);

  //writeln('size of i = ', sizeof(i));
  //writeln('size of p = ', sizeof(p));

  p^ := 11;

  writeln('i = ', i);

  readln;
end.

