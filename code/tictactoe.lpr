program tictactoe;

const
  FileName = 'tictac.txt';

  INF_P = 100;
  INF_M = -INF_P;

  X_SIGN = 'X';
  O_SIGN = '0';
  E_SIGN = #32;

  E_FIELD = #32+#32+#32+#32+#32+#32+#32+#32+#32;

  LINES: array[1..8,1..3] of byte = (
    (1,2,3),
    (4,5,6),
    (7,8,9),
    (1,4,7),
    (2,5,8),
    (3,6,9),
    (1,5,9),
    (3,5,7)
  );

//[1][2][3]
//[4][5][6]
//[7][8][9]

var
  F: Text;

type
  //описание игровой ситуации
  TGameSituation = record
    Field: string[9]; // поле 3*3
    Turn: boolean;    // чей ход: true - крестик
  end;

  TGameSituationList = array of TGameSituation;

  //описание хода в игре
  TGameMove = record
    IsNone: boolean;  //признак "пустого" хода
    Index: 1..9;      //куда будет поставлен значок
    Turn: boolean;    // чей ход: true - крестик
  end;

  TGameMoveList = array of TGameMove;

  TTreeNode = record
    GS: TGameSituation;
    ParentId: integer;
    //Level: integer;
    Scope: integer;
    IsGenMove: boolean;
  end;

  TTree = array of TTreeNode;

procedure ClearField(var gs: TGameSituation);
begin
  gs.Field := E_FIELD;
end;

procedure WriteField(var gs: TGameSituation);
var
  i: integer;
begin
  for i := 1 to 9 do
  begin
    write(F, '[', gs.Field[i], ']');
    if i mod 3 = 0 then writeln(F);
  end;
end;

function GenMoves(const gs: TGameSituation):TGameSituationList;
var
  i, count: integer;
  gsl: TGameSituationList;
begin
  count := 0;

  SetLength(gsl, 9);

  for i := 1 to 9 do
    if gs.Field[i] = #32 then
    begin
      gsl[count].Turn := not gs.Turn;
      gsl[count].Field := gs.Field;

      if gs.Turn then
        gsl[count].Field[i] := X_SIGN
      else
        gsl[count].Field[i] := O_SIGN;

      count := count + 1;
    end;

  if count = 0 then
    gsl := NULL
  else
    SetLength(gsl, count);

  Result := gsl;
end;

function nc(gs:TGameSituation; turn:boolean): integer;
var
  i: byte;
  count: byte;
  sign: char;
begin
  count := 0;

  //sign - "нехороший знак"
  if turn then
    sign := O_SIGN
  else
    sign := X_SIGN;

  for i := 1 to 8 do
    if (gs.Field[Lines[i,1]] <> sign) and
       (gs.Field[Lines[i,2]] <> sign) and
       (gs.Field[Lines[i,3]] <> sign) then
      count := count + 1;

  Result := count;
end;

function is_win(gs:TGameSituation; player:boolean): boolean;
var
  i: byte;
  sign: char;
begin
  Result := false;

  //sign - "нехороший знак"
  if player then
    sign := X_SIGN
  else
    sign := O_SIGN;

  for i := 1 to 8 do
    if (gs.Field[Lines[i,1]] = sign) and
       (gs.Field[Lines[i,2]] = sign) and
       (gs.Field[Lines[i,3]] = sign) then
    begin
      Result := true;

      break;
    end;
end;


function is_win_x(gs:TGameSituation): boolean;
begin
  Result := is_win(gs, true);
end;

function is_win_o(gs:TGameSituation): boolean;
begin
  Result := is_win(gs, false);
end;

function scope(gs:TGameSituation; player:boolean):integer;
begin
  if is_win(gs, player) then
    Result := INF_P
  else
    if is_win(gs, not player) then
      Result := INF_M
    else
      Result := nc(gs, player) - nc(gs, not player);
end;

procedure write_node(var node: TTreeNode);
begin
  writeln(F, 'ParentId = ', node.ParentId);
  writeln(F, 'IsGenMove= ', node.IsGenMove);
  WriteField(node.GS);
end;

procedure write_tree(var tree: TTree);
var
  i, n: integer;
begin
  n := Length(tree);

  for i := 0 to n-1 do
  begin
    writeln(F, 'Id = ', i);
    write_node(tree[i]);
  end;
end;

procedure gen_node(var tree: TTree; node_idx: integer);
var
  gsl :TGameSituationList;
  i, count, n: integer;
begin
  //если неправильный индекс
  if (node_idx > length(tree)-1) or (node_idx < 0) then
    exit;

  //если уже генерировали ходы
  if tree[node_idx].IsGenMove then
    exit;

  tree[node_idx].IsGenMove := true;

  //проверить, что ситуация не конечная
  if (scope(tree[node_idx].GS, true) = INF_P) or
    (scope(tree[node_idx].GS, false) = INF_P) then
    exit;

  n := length(tree);

  gsl := GenMoves(tree[node_idx].GS);

  count := length(gsl);

  SetLength(tree, n + count);

  for i := 0 to count-1 do
  begin
    tree[n+i].GS := gsl[i];
    tree[n+i].IsGenMove := false;
    tree[n+i].ParentId := node_idx;
  end;
end;

procedure gen_all(var tree: TTree);
var
  i, n: integer;
begin
  n := length(tree);

  for i := 0 to n-1 do
    if not tree[i].IsGenMove then
      gen_node(tree, i);
end;

procedure do_move(const gm: TGameMove; var gs: TGameSituation);
begin
  if gm.Turn then //ход крестика
    gs.Field[gm.Index] := X_SIGN
  else
    gs.Field[gm.Index] := O_SIGN;

  gs.Turn := not gs.Turn; //передаем ход противнику
end;

procedure undo_move(const gm: TGameMove; var gs: TGameSituation);
begin
  gs.Field[gm.Index] := E_SIGN;

  gs.Turn := not gs.Turn; //отменяем ход
end;

function get_moves_list(const gs: TGameSituation):TGameMoveList;
var
  i, count: integer;

  gm: TGameMoveList;
begin
  count := 0;

  SetLength(gm, 9);

  for i := 1 to 9 do
    if gs.Field[i] = #32 then
    begin
      gm[count].IsNone := false;
      gm[count].Turn := gs.Turn;
      gm[count].Index := i;

      count := count + 1;
    end;

  if count = 0 then
    gm := NULL
  else
    SetLength(gm, count);

  Result := gm;
end;

//расчет лучшего хода по алгоритму MINIMAX
procedure minimax(gs: TGameSituation; player: boolean; deep: integer;
  var gm: TGameMove; var score: integer);
var
  gml: TGameMoveList;
  i: integer;

  move, best_move: TGameMove;
  best_score: integer;

begin
  best_move.IsNone := true;
  best_score := 0;

  gml := nil;

  //генерируем ходы игрока
  if deep <> 0 then
    gml := get_moves_list(gs);

  //если достигли максимальной глубины просмотра
  //или нет допустимых ходов
  if (deep = 0) or (gml = nil) then
  begin
    gm.IsNone := true;
    score := nc(gs, gs.Turn);
    exit;
  end;

  for i := 0 to Length(gml)-1 do
  begin
    gm := gml[i];

    //выполняем ход
    do_move(gm, gs);

    //выполняем расчет минимакса для сделанного хода
    minimax(gs, not player, deep-1, move, score);

    //отменяем ход
    undo_move(gm, gs);

    if (best_move.IsNone) or                               //первый найденый ход
       ((gs.Turn = player) and (score > best_score)) or   //уровень MAX-игрока
       ((gs.Turn = not player) and (score < best_score))  //уровень MIN-игрока
      then
    begin
        best_score := score;
        best_move := gm;
    end;
  end;

  gm := best_move;
  score := best_score;
end;

//получение лучшего хода
procedure best_move(const gs: TGameSituation; var gm: TGameMove;
  var score: integer);
const
  MINIMAX_DEEP = 4;
begin
  minimax(gs, gs.Turn, MINIMAX_DEEP, gm, score);
end;

var
  gs:TGameSituation;
  gsl, gsl2:TGameSituationList;

  root, node: TTreeNode;
  tree: TTree;

  i, level: integer;

  gm: TGameMove;
  score: integer;

begin
  //Assign(F, FileName);
  //Rewrite(F);

  //gs.Turn := true; //первый ходит крестик
  //ClearField(gs);  //на пустое поле

  //gs.Field := 'XX 00    ';

  //writeln(F, 'Started situation:');
  //WriteField(gs);
  //gsl := GenMoves(gs);
  {
  for i := 0 to Length(gsl)-1 do
  begin
    writeln(F, 'Move# :', i);
    WriteField(gsl[i]);
  end;
  }
  //gsl2 := GenMoves(gsl[8]);
  {
  for i := 0 to Length(gsl2)-1 do
  begin
    writeln(F, 'Move# :', i);
    WriteField(gsl2[i]);
  end;
  }

  //gsl2[4].Field := 'X X   0 0';
  //writeln(F, 'Move# :', 4);
  //WriteField(gsl2[4]);

  //writeln(F, 'nc(X) = ', nc(gsl2[4], true));
  //writeln(F, 'nc(O) = ', nc(gsl2[4], false));
  //writeln(F, 'Scope = ', scope(gsl2[4], false));


  //TTreeNode = record
  //    GS: TGameSituation;
  //    ParentId: integer;
  //    Scope: integer;
  //end;

  //TTree = array of TTreeNode;

  //root.GS := gs;
  //root.ParentId := -1;
  //root.IsGenMove := false;

  //SetLength(tree, 1);
  //tree[0] := root;

  //writeln(F, 'Tree:');
  //write_tree(tree);

  //writeln(F, 'Tree with level 1:');

  //gen_node(tree, 0);
  //write_tree(tree);

  //for i := 1 to 9 do
  //  gen_all(tree);

  //writeln(F, 'Tree with all levels:');
  //write_tree(tree);

  //Close(F);

  //writeln('Result is in file ', FileName);

  gs.Turn := true; //первый ходит крестик
  ClearField(gs);   //на пустое поле

  best_move(gs, gm, score);
  writeln('move X to ', gm.Index);
  writeln('score is ', score);

  writeln('Press Enter to quit...');
  readln;
end.


