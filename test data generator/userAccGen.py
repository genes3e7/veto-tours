# Test data generation

# To install lorem package
# python -m pip install lorem --user
from classes import User, Tours, Ratings, Chat, Bookings
import math
import random
import lorem
import csv
import time
import datetime

# Numbers to generate
ACCNUM = 500  # Accounts
TOURSNUM = 1000  # random 100 ppl approx 10 tours average
BOOKINGSNUM = 1000  # random bookings from 200 ppl 5 tours avg
RATINGSNUM = 1000  # All 500 ppl randomly rate 2 other ppl
CHATNUM = 1000  # random 200 ppl 5 chat avg

# Filenames
SINGLE_NAME_INPUT = "singleName.txt"
USER_ACC_SQL = "userAccSQLGen.sql"
USER_ACC_DATA = "userAccData.csv"
TRANSACTIONSQL = "transactionSQL.sql"
TRANSACTION_DATA = "transactionData.txt"

# Stored memory
user_accounts = []
single_name = {}


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
