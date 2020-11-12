class Foo:
    Material = "Blax"

f1 = Foo()
print(f1.Material)

f1.Material = 1.0
print(f1.Material)

del f1.Material
print(f1.Material)
