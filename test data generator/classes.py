import math
import random
import lorem
import csv
import time
import datetime


class User:
    def __init__(self, firstName, lastName):
        # userID, password, name, email, phoneNumber, accountType, description, status
        self.firstName = (str)(firstName)
        self.lastName = (str)(lastName)
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
        string += "([userID], [password], [name], [email], [phoneNumber], [description], [status]) "
        string += "VALUES ("
        string += "N\'{0}\', ".format(self.userID)
        string += "N\'{0}\', ".format(self.password)
        string += "N\'{0} {1}\', ".format(self.firstName, self.lastName)
        string += "N\'{0}\', ".format(self.email)
        string += "{0}, ".format(self.phoneNumber)
        string += "N\'{0}\', ".format(self.descript)
        string += "{0});".format(self.status)

        return string


class Tours:
    def __init__(self, userID):
        # userID, tourName, capacity, location, description, startDate, endDate, price, status
        self.userID = userID
        self.tourName = lorem.sentence()
        self.capacity = random.randint(1, 20) * 10
        self.location = lorem.sentence()
        self.description = lorem.sentence()
        self.startDate, self.endDate = self.generateDates()
        self.price = random.random() * 10000
        if (random.random() > 0.25):
            self.status = 'open'
        else:
            self.status = 'close'
        self.data = {}
        self.createDict()

    def createDict(self):
        self.data["userID"] = self.userID
        self.data["tourName"] = self.tourName
        self.data["capacity"] = self.capacity
        self.data["location"] = self.location
        self.data["description"] = self.description
        self.data["startDate"] = self.startDate.strftime("%Y-%m-%d %H:%M:%S")
        self.data["endDate"] = self.endDate.strftime("%Y-%m-%d %H:%M:%S")
        self.data["price"] = self.price
        self.data["status"] = self.status

    def generateDates(self):
        now = datetime.datetime.now()
        year = datetime.timedelta(days=365)
        lower, upper = now - year, now + year

        start_time = lower + random.random() * (upper - lower)
        end_time = start_time + random.random() * (upper-start_time)

        return start_time, end_time

    def insert_statement(self):
        string = "INSERT INTO [dbo].[tours] "
        string += "([userID], [tourName], [capacity], [location], [description], [startDate], [endDate], [price], [status]) "
        string += "VALUES ("
        string += "N\'{0}\', ".format(self.userID)
        string += "N\'{0}\', ".format(self.tourName)
        string += "{0}, ".format(self.capacity)
        string += "N\'{0}\', ".format(self.location)
        string += "N\'{0}\', ".format(self.description)
        string += "N\'{0}\', ".format(
            self.startDate.strftime("%Y-%m-%d %H:%M:%S"))
        string += "N\'{0}\', ".format(
            self.endDate.strftime("%Y-%m-%d %H:%M:%S"))
        string += "CAST({0} AS MONEY), ".format(self.price)
        string += "N\'{0}\');".format(self.status)

        return string


class Ratings:
    def __init__(self, toUser, fromUser, state):
        # ratingTo, ratingFrom, stars, type
        self.toUser = toUser
        self.fromUser = fromUser
        self.state = state
        self.rating = random.randint(0, 5)
        self.data = {}
        self.createDict()

    def createDict(self):
        self.data["ratingTo"] = self.toUser
        self.data["ratingFrom"] = self.fromUser
        self.data["stars"] = self.rating
        self.data["type"] = self.state

    def insert_statement(self):
        string = "INSERT INTO [dbo].[ratings] "
        string += "([ratingTo], [ratingFrom], [stars], [type]) "
        string += "VALUES ("
        string += "N\'{0}\', ".format(self.toUser)
        string += "N\'{0}\', ".format(self.fromUser)
        string += "{0}, ".format(self.rating)
        string += "N\'{0}\');".format(self.state)

        return string


class Chat:
    def __init__(self, sender, recipient):
        # sender, recipient, subject, message, dateTime
        self.sender = sender
        self.recipient = recipient
        self.subject = lorem.sentence()
        self.message = lorem.paragraph()
        self.dateTime = self.generateDates()
        self.data = {}
        self.createDict()

    def generateDates(self):
        now = datetime.datetime.now()
        year = datetime.timedelta(days=365)
        lower, upper = now - year, now + year

        return lower + random.random() * (upper - lower)

    def createDict(self):
        self.data["sender"] = self.sender
        self.data["recipient"] = self.recipient
        self.data["subject"] = self.subject
        self.data["message"] = self.message
        self.data["dateTime"] = self.dateTime.strftime("%Y-%m-%d %H:%M:%S")

    def insert_statement(self):
        string = "INSERT INTO [dbo].[chat] "
        string += "([sender], [recipient], [subject], [message], [dateTime]) "
        string += "VALUES ("
        string += "N\'{0}\', ".format(self.sender)
        string += "N\'{0}\', ".format(self.recipient)
        string += "N\'{0}\', ".format(self.subject)
        string += "N\'{0}\', ".format(self.message)
        string += "N\'{0}\');".format(
            self.dateTime.strftime("%Y-%m-%d %H:%M:%S"))

        return string


class Bookings:
    def __init__(self, userID, tourID, tourName):
        # userID, tourID
        self.userID = userID
        self.tourID = tourID
        self.tourName = tourName
        self.data = {}
        self.createDict()

    def createDict(self):
        self.data["userID"] = self.userID
        self.data["tourID"] = self.tourID
        self.data["tourName"] = self.tourName

    def insert_statement(self):
        string = "INSERT INTO [dbo].[bookings] "
        string += "([userID], [tourID]) "
        string += "VALUES ("
        string += "N\'{0}\', ".format(self.userID)
        # SELECT tourID FROM [dbo].[tours] WHERE (userID = 'Ophelia392'AND tourName = 'Dolore porro dolorem amet dolore.');
        string += "(SELECT tourID FROM [dbo].[tours] WHERE (userID = \'{0}\' AND tourName = \'{1}\')));".format(
            self.tourID, self.tourName)
        #string += "N\'{0}\');".format(self.tourID)

        return string
