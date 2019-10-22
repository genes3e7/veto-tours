import random

def randLetter():
    ascii_letters = 'abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ'
    return random.choice(ascii_letters)
def rand():
    return random.randint(0,9)

#Aaaaaa0
def create_password():
    pw = str(randLetter()).upper()
    for i in range(0,6):
        pw += str(randLetter()).lower()
    pw += str(rand())
    return pw

passwords = []
for i in range(0,500):
    passwords.append(create_password())

f = open("password.txt","w+")
for i in passwords:
    f.write(str(i) + "\n")
    #print(i)