program bool;

type
  TPByte = ^byte;

var
  b: boolean;
  p: pointer;
  bt : byte;
begin
  writeln('size of boolean = ', sizeof(b));
  writeln('size of pointer = ', sizeof(p));

  p := @b;

  b := True;
  bt := TPByte(p)^;
  writeln('true is ', bt);

  b := False;
  bt := TPByte(p)^;
  writeln('false is ', bt);
end.

