# Test data generation

# To install lorem package
# python -m pip install lorem --user
import math
import random
import lorem
import csv

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
        self.descript = lorem.paragraph()
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
        return random.choice([9, 8, 7]) * 10000000 + random.randint(1, 9999999)

    def insert_statement(self):
        string = "INSERT INTO [dbo].[users] "
        string += "([userID], [password], [name], [email], [phoneNumber], [accountType], [description], [status]) "
        string += "VALUES ("
        string += "N\'{0}\', ".format(self.userID)
        string += "N\'{0}\', ".format(self.password)
        string += "N\'{0} {1}\', ".format(self.firstName, self.lastName)
        string += "N\'{0}\', ".format(self.email)
        string += "{0}, ".format(self.phoneNumber)
        string += "N\'{0}\', ".format(self.accType)
        string += "N\'{0}\', ".format(self.descript)
        string += "{0});".format(self.status)

        return string

    def csv_header(self):
        return ["userID", "password", "firstName", "lastName", "email", "phoneNumber", "accType", "descript", "status"]

    def csv_content(self):
        array = [self.userID, self.password, self.firstName, self.lastName,
                 self.email, self.phoneNumber, self.accType, self.descript, self.status]

        return array


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

    user_account_SQL = open("userAccSQLGen.sql", "w+")

    for i in range(0, ACCNUM):
        # Make account and save to list for future code upgrade
        user_accounts.append(
            User(single_name_get(count), single_name_get(count)))

        # Write account into insert statements
        user_account_SQL.write(user_accounts[i].insert_statement() + "\n")

    user_account_SQL.close()

    # keep data
    with open('userAccData.csv', mode='w+', newline='') as user_account_manual:
        writer = csv.writer(user_account_manual, delimiter=',',
                            quotechar='"', quoting=csv.QUOTE_MINIMAL)

        writer.writerow(user_accounts[0].csv_header())
        for i in user_accounts:
            writer.writerow(i.csv_content())
            
    print("Program Complete.")
