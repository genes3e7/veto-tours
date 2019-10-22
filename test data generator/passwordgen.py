import random

def randLetter():
    ascii_letters = 'abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ'
    return random.choice(ascii_letters)
def rand():
    return random.randint(0,10)

#Aaaaaa0
def create_password():
    pw = str(randLetter).upper() + str(randLetter).lower() * 6 + rand()
    return pw

print(create_password())