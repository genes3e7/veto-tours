# Take in single names and generate random first and second names
import math
import random

single_name = {}
with open("singleName.txt") as f:
    count = 0
    for line in f:
        single_name[count] = line.strip()
        count += 1

def rand():
    return random.randint(0,1999)
def single_name_get():
    return single_name.get(rand())

full_name = []
for i in range(0, 500):
    full_name.append((single_name_get(), single_name_get()))

f = open("full name.txt","w+")
for a, b in full_name:
    name = a + " " + b + "\n"
    f.write(name)
    print(a,b)

f.close()