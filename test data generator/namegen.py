# Take in single names and generate random first and second names

single_name = {}
with open("singleName.txt") as f:
    count = 0
    for line in f:
        single_name[count] = line.strip()

print(single_name)