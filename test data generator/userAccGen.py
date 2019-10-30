# User account consists of
# Keywords
# userID, password, name, email, phoneNumber, accountType, description, status

# userID generation
# first name
# password generation take from file
#

import math
import random
#pip install lorem --user
#python -m pip install lorem --user
import lorem

ACCNUM = 500

# Functions for name
def rand():
    return random.randint(0, 2000)
def single_name_get():
    return single_name.get(rand())

# Functions of password
def randLetter():
    ascii_letters = 'abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ'
    return random.choice(ascii_letters)
def randPW():
    return random.randint(0, 9)
def create_password():
    pw = str(randLetter()).upper()
    for i in range(0, 6):
        pw += str(randLetter()).lower()
    pw += str(randPW())
    return pw

# Stored
emailExtension = ["@hotmail.com", "@gmail.com", "@yahoo.com"]
userID = []
full_name = []
passwords = []
email = []

# Get single names
single_name = {}
with open("singleName.txt") as f:
    count = 0
    for line in f:
        single_name[count] = line.strip()
        count += 1

# Make full name
for i in range(0, ACCNUM):
    full_name.append((single_name_get(), single_name_get()))
    userID.append((str)(full_name[i][0]) + (str)(random.randint(1, 999)))
    email.append(userID[i] + random.choice(emailExtension))

# print(full_name[i][0])
# Aaaaaa0 
for i in range(0, ACCNUM):
    passwords.append(create_password())

# Form insert Statement
insert_statement = []
# 0
insert_statement.append("INSERT INTO [dbo].[users] ([userID], [password], [name], [email], [phoneNumber], [accountType], [description], [status]) VALUES (")
# 1
insert_statement.append("N\'")
# 2
insert_statement.append("\', ")
# 3
insert_statement.append(", ")
# 4
insert_statement.append("\', 0)")

# N'test-user', N'password1', N'Johnny', N'johnny@email.com', 98765432, N'user', N'I am looking for tours in SG'
f = open("userAccSQLGen.txt","w+")
for i in range(0, ACCNUM):
    string = insert_statement[0]
    string += insert_statement[1] + userID[i] + insert_statement[2]
    string += insert_statement[1] + passwords[i] + insert_statement[2]
    string += insert_statement[1] + (str)(full_name[i][0]) + " " + (str)(full_name[i][1]) + insert_statement[2]
    string += insert_statement[1] + email[i] + insert_statement[2]
    string += "9" + (str)(random.randint(1,8999999)) + insert_statement[3]
    string += insert_statement[1] + "user" + insert_statement[2]
    string += insert_statement[1] + lorem.sentence() + insert_statement[4]
    string += "\n"
    f.write(string)
    print(string)
f.close()
