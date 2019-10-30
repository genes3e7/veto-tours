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

class User:
    emailExtension = ["@hotmail.com", "@gmail.com", "@yahoo.com"]
    def __init__(self, firstName, lastName):
        self.firstName = (str)(firstName)
        self.lastName = (str)(lastName)
        self.accType = "user"
        self.userID = self.userIDGen()
        self.email = self.emailGen()
        self.phoneNumber = self.phoneNumberGen()
        self.password = self.passwordGen()
        self.descript = lorem.sentence()
        self.status = "0"

    def emailGen(self):
        return self.userID + random.choice(self.emailExtension)
    def passwordGen(self):
        pwlen = random.randint(8,20)
        ascii_letters = 'abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ'
        specialSymbols = "@%+/!#$^?:{([~-_.])}"
        password = ""
        for i in range(0, pwlen - 2):
            password += str(random.choice(ascii_letters))
        password += str(random.randint(0, 99))
        password += str(random.choice(specialSymbols))

        return ''.join(random.sample(password, len(password)))
    def userIDGen(self):
        return self.firstName + (str)(random.randint(1, 999))
    def phoneNumberGen(self):
        return random.choice("987") + (str)(random.randint(1,9999999))

    def insert_statement(self):
        string = "INSERT INTO [dbo].[users] ([userID], [password], [name], [email], [phoneNumber], [accountType], [description], [status]) VALUES ("
        string += "N\'" + self.userID + "\', "
        string += "N\'" + self.password + "\', "
        string += "N\'" + self.firstName + " " + self.lastName + "\', "
        string += "N\'" + self.email + "\', "
        string += self.phoneNumber + ", "
        string += "N\'" + self.accType + "\', "
        string += "N\'" + self.descript + "\', "
        string += self.status + ");"
        return string
        
count = 0
single_name = {}
user_accounts = []

def single_name_get(count):
    return single_name.get(random.randint(0, count))

if __name__== '__main__':
    with open("singleName.txt") as names:
        for line in names:
            single_name[count] = line.strip()
            count += 1

    # Make Account
    user_account_SQL = open("userAccSQLGen.sql", "w+")
    user_account_manual = open("user_pw.csv", "w+")

    for i in range(0, ACCNUM):
        user_accounts.append(User(single_name_get(count), single_name_get(count)))
        user_account_SQL.write(user_accounts[i].insert_statement() + "\n")
        user_account_manual.write(user_accounts[i].userID + ", " + user_accounts[i].password + "\n")
        print(user_accounts[i].insert_statement() + "\n")
    
    user_account_SQL.close()
    user_account_manual.close()