# Test data generation

# To install lorem package
# python -m pip install lorem --user
import math
import random
import lorem

# Number of accounts generated
ACCNUM = 500


class User:
    def __init__(self, firstName, lastName):
        # userID, password, name, email, phoneNumber, accountType, description, status
        self.firstName = (str)(firstName)
        self.lastName = (str)(lastName)
        self.accType = "user"
        self.userID = self.userIDGen()
        self.email = self.emailGen()
        self.phoneNumber = self.phoneNumberGen()
        self.password = self.passwordGen()
        self.descript = lorem.sentence()
        self.status = 0

    def emailGen(self):
        emailExtension = ["hotmail.com", "gmail.com", "yahoo.com",
                          "outlook.com", "iCloud.com", "aol.com", "mail.com"]
        return self.userID + "@{0}".format(random.choice(emailExtension))

    def passwordGen(self):
        pwlen = random.randint(8, 20)
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
        return (int)(random.choice("987") + (str)(random.randint(1, 9999999)))

    def insert_statement(self):
        pattern = ["N\'", "\', "]
        string = "INSERT INTO [dbo].[users] "
        string += "([userID], [password], [name], [email], [phoneNumber], [accountType], [description], [status]) "
        string += "VALUES ("
        string += "{0[0]}{1}{0[1]}".format(pattern, self.userID)
        string += "{0[0]}{1}{0[1]}".format(pattern, self.password)
        string += "{0[0]}{1} {2}{0[1]}".format(pattern,
                                               self.firstName, self.lastName)
        string += "{0[0]}{1}{0[1]}".format(pattern, self.email)
        string += "{0}, ".format(self.phoneNumber)
        string += "{0[0]}{1}{0[1]}".format(pattern, self.accType)
        string += "{0[0]}{1}{0[1]}".format(pattern, self.descript)
        string += "{0});".format(self.status)

        return string


count = 0
single_name = {}
user_accounts = []


def single_name_get(count):
    return single_name.get(random.randint(0, count))


if __name__ == '__main__':
    with open("singleName.txt") as names:
        for line in names:
            single_name[count] = line.strip()
            count += 1

    # Files
    user_account_SQL = open("userAccSQLGen.sql", "w+")
    user_account_manual = open("user_pw.csv", "w+")

    for i in range(0, ACCNUM):
        # Make account and save to list for future code upgrade
        user_accounts.append(
            User(single_name_get(count), single_name_get(count)))
        # Write account into insert statements
        user_account_SQL.write(user_accounts[i].insert_statement() + "\n")
        # Write account into user_pw.csv file
        user_account_manual.write(
            "{0.userID}, {0.password}\n".format(user_accounts[i]))

    user_account_SQL.close()
    user_account_manual.close()

    print("Program Complete.")
