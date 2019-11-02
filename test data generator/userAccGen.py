# Test data generation

# To install lorem package
# python -m pip install lorem --user
import math
import random
import lorem
import csv

# Number of accounts generated
ACCNUM = 500

# Filenames
SINGLE_NAME_INPUT = "singleName.txt"
USER_ACC_SQL = "userAccSQLGen.sql"
USER_ACC_DATA = "userAccData.csv"
TRANSACTIONSQL = "transactionSQL.sql"
TRANSACTION_DATA = "transactionData.txt"

# Stored memory
user_accounts = []
single_name = {}


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
        self.data = {}
        self.createDict()

    def createDict(self):
        self.data["userID"] = self.userID
        self.data["password"] = self.password
        self.data["firstName"] = self.firstName
        self.data["lastName"] = self.lastName
        self.data["email"] = self.email
        self.data["phoneNumber"] = self.phoneNumber
        self.data["accType"] = self.accType
        self.data["descript"] = self.descript
        self.data["status"] = self.status

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


class Transaction:
    def __init__(self, toUser, fromUser):
        self.touser = touser
        self.fromUser = fromUser

#############################################################################
# create user accounts


def read_single_name():
    count = 0

    with open(SINGLE_NAME_INPUT) as names:
        for line in names:
            single_name[count] = line.strip()
            count += 1

    return count


def single_name_get(count):
    return single_name.get(random.randint(0, count))


def write_user_accounts():
    # Write account into insert statements
    user_account_SQL = open(USER_ACC_SQL, "w+")

    for i in range(0, len(user_accounts)):
        user_account_SQL.write(user_accounts[i].insert_statement() + "\n")

    user_account_SQL.close()

    # Keep data in csv file
    with open(USER_ACC_DATA, mode='w+', newline='') as user_account_manual:
        fieldnames = user_accounts[0].data.keys()
        writer = csv.DictWriter(user_account_manual, fieldnames=fieldnames)
        writer.writeheader()

        for i in range(0, ACCNUM):
            writer.writerow(user_accounts[i].data)


def user_acc_generator():
    count = read_single_name()

    for i in range(0, ACCNUM):
        # Make account and save to list for future code upgrade
        user_accounts.append(
            User(single_name_get(count), single_name_get(count)))

    write_user_accounts()

##############################################################################


if __name__ == '__main__':
    user_acc_generator()

    print("Program Complete.")
