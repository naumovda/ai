class Matrix:
    def __init__(self, a11, a12, a21, a22):
        self.a11 = a11
        self.a12 = a12
        self.a21 = a21
        self.a22 = a22

    def det(self):
        return self.a11*self.a22 - self.a12*self.a21

    def __add__(self, other):
        return Matrix(self.a11+other.a11,self.a12+other.a12, self.a21+other.a21, self.a22+other.a22)

    def __str__(self):
        return f"\n[{self.a11}, {self.a12}]\n[{self.a21}, {self.a22}]"

    def __bool__(self):
        return self.a11 != 0 or self.a12 != 0 or self.a21 != 0 or self.a22 != 0

a = Matrix(2, -1, 1, 3)
b = Matrix(1,  0, 0, 1)
d = Matrix(-2, 1, -1, -3)
c = a + b
e = a + d
# [2 -1
#  1  3]

print(f"Matrix a = {a}")
print(f"Matrix b = {a}")
print(f"a[1][1] = {a.a11}")
print(f"det(A) = {a.det()}")
print(f"det(B) = {b.det()}")
print(f"Matrix c = {c}")
print(f"Matrix e = a+d = {e}")

if e:
    print("E is a not zero-matrix")
else:
    print("E is a zero-matrix")




