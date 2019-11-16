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

# Numbers to generate (CAN CHANGE)
ACCNUM = 500  # Accounts
TOURSNUM = 200  # created tours
BOOKINGSNUM = 10000  # no of bookings
RATINGSNUM = 10000  # no of ratings
CHATNUM = 10000  # no of dialogs sent

# Filenames
SINGLE_NAME_INPUT = "singleName.txt"
USER_ACC_SQL = "userAccSQL.sql"
USER_ACC_DATA = "userAccData.csv"
TOUR_SQL = "tourSQL.sql"
TOUR_DATA = "tourData.csv"
BOOKING_SQL = "bookingSQL.sql"
BOOKING_DATA = "bookingData.csv"
RATING_SQL = "ratingSQL.sql"
RATING_DATA = "ratingData.csv"
CHAT_SQL = "chatSQL.sql"
CHAT_DATA = "chatData.csv"

ALL_SQL = "all_sql.sql"

# Stored memory
user_accounts = []
single_name = {}
tour_list = []
booking_list = []
ratings_list = []
chat_list = []


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
# Create tours
# Pick 1k times


def pick_acc():
    return random.choice(user_accounts)


def write_tour_list():
    # Write account into insert statements
    tour_sql_writer = open(TOUR_SQL, "w+")

    for i in range(0, len(tour_list)):
        tour_sql_writer.write(tour_list[i].insert_statement() + "\n")

    tour_sql_writer.close()

    # Keep data in csv file
    with open(TOUR_DATA, mode='w+', newline='') as tours_manual:
        fieldnames = tour_list[0].data.keys()
        writer = csv.DictWriter(tours_manual, fieldnames=fieldnames)
        writer.writeheader()

        for i in range(0, TOURSNUM):
            writer.writerow(tour_list[i].data)


def tour_generator():
    for i in range(0, TOURSNUM):
        tourguide = pick_acc()
        tour_list.append(Tours(tourguide.userID))

    write_tour_list()

##############################################################################
# Create bookings
# Pick 1000 times random user check not booking own tour and book


def pick_Tour():
    return random.choice(tour_list)


def write_booking_list():
    # Write account into insert statements
    booking_sql_writer = open(BOOKING_SQL, "w+")

    for i in range(0, len(booking_list)):
        booking_sql_writer.write(booking_list[i].insert_statement() + "\n")

    booking_sql_writer.close()

    # Keep data in csv file
    with open(BOOKING_DATA, mode='w+', newline='') as booking_manual:
        fieldnames = booking_list[0].data.keys()
        writer = csv.DictWriter(booking_manual, fieldnames=fieldnames)
        writer.writeheader()

        for i in range(0, BOOKINGSNUM):
            writer.writerow(booking_list[i].data)


def booking_generator():
    for i in range(0, BOOKINGSNUM):
        tourist = pick_acc()
        while True:
            tour = pick_Tour()
            if tour.userID != tourist.userID:
                break
        booking_list.append(
            Bookings(tourist.userID, tour.userID, tour.tourName))

    write_booking_list()

##############################################################################
# Create ratings
# Pick random 1k times user rate user. Must be unique


def write_ratings_list():
    # Write account into insert statements
    ratings_sql_writer = open(RATING_SQL, "w+")

    for i in range(0, len(ratings_list)):
        ratings_sql_writer.write(ratings_list[i].insert_statement() + "\n")

    ratings_sql_writer.close()

    # Keep data in csv file
    with open(RATING_DATA, mode='w+', newline='') as rating_manual:
        fieldnames = ratings_list[0].data.keys()
        writer = csv.DictWriter(rating_manual, fieldnames=fieldnames)
        writer.writeheader()

        for i in range(0, RATINGSNUM):
            writer.writerow(ratings_list[i].data)


def setState():
    if random.randint(0, 1) == 0:
        return "tourist"
    else:
        return "tourguide"


def ratings_generator():
    for i in range(0, RATINGSNUM):
        while True:
            userA, userB = pick_acc(), pick_acc()
            if userA != userB:
                rate = Ratings(userA.userID, userB.userID, setState())
            check = False
            for i in ratings_list:
                if rate == i:
                    check = True
            if check == False:
                break

        ratings_list.append(rate)

    write_ratings_list()

##############################################################################
# Create chats


def write_chat_list():
    # Write account into insert statements
    chat_sql_writer = open(CHAT_SQL, "w+")

    for i in range(0, len(chat_list)):
        chat_sql_writer.write(chat_list[i].insert_statement() + "\n")

    chat_sql_writer.close()

    # Keep data in csv file
    with open(CHAT_DATA, mode='w+', newline='') as chat_manual:
        fieldnames = chat_list[0].data.keys()
        writer = csv.DictWriter(chat_manual, fieldnames=fieldnames)
        writer.writeheader()

        for i in range(0, CHATNUM):
            writer.writerow(chat_list[i].data)


def chat_generator():
    for i in range(0, CHATNUM):
        while True:
            userA, userB = pick_acc(), pick_acc()
            if userA != userB:
                break
        chat_list.append(Chat(userA.userID, userB.userID))

    write_chat_list()


##############################################################################

def final_sql():
    writer = open(ALL_SQL, "w+")
    # writer .write(
    #    "DELETE FROM [dbo].[chat];\nDELETE FROM [dbo].[ratings];\nDELETE FROM [dbo].[bookings];\nDELETE FROM [dbo].[tours];\nDELETE FROM [dbo].[users];\n")
    for i in range(0, len(user_accounts)):
        writer.write(user_accounts[i].insert_statement() + "\n")
    for i in range(0, len(tour_list)):
        writer.write(tour_list[i].insert_statement() + "\n")
    for i in range(0, len(booking_list)):
        writer.write(booking_list[i].insert_statement() + "\n")
    for i in range(0, len(ratings_list)):
        writer.write(ratings_list[i].insert_statement() + "\n")
    for i in range(0, len(chat_list)):
        writer.write(chat_list[i].insert_statement() + "\n")
    writer.close()


if __name__ == '__main__':
    user_acc_generator()
    tour_generator()
    booking_generator()
    ratings_generator()
    chat_generator()
    final_sql()

    print("Program Complete.")
