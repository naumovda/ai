program pointers;

function ByteToString(b: byte): string;
var
  s: string;
begin
  s := '';

  while b <> 0 do
  begin
    if b mod 2 = 0 then
      s := '0' + s
    else
      s := '1' + s;
    b := b div 2;
  end;

  while length(s) < 8 do
    s := '0' + s;

  ByteToString := s;
end;

type
  TPLongint = ^longword;
  TPByte = ^byte;

var
  p: pointer;
  r: single;
  i: longword;
  s: string;

  pb: TPByte;
begin
  r := 0.2;
  p := @r;

  writeln('size of pointer = ', sizeof(p));

  i := TPLongint(p)^;

  writeln('size of single = ', sizeof(r));
  writeln('size of longword = ', sizeof(i));
  writeln('longword interpretation = ', i);

  pb := TPByte(p);

  s := '';
  for i := 0 to 3 do
  begin
    writeln('byte[', i, '] = ', ByteToString(pb^), ' (', pb^, ')');
    s := ByteToString(pb^) + s;
    inc(pb);
  end;

  writeln('binary code = ', s);

end.

