CREATE TABLE [dbo].[admins] (
    [userID]   VARCHAR (50) NOT NULL,
    [password] VARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([userID] ASC)
);

CREATE TABLE [dbo].[users] (
    [userID]      VARCHAR (50)  NOT NULL,
    [password]    VARCHAR (50)  NOT NULL,
    [name]        VARCHAR (50)  NOT NULL,
    [email]       VARCHAR (50)  NOT NULL,
    [phoneNumber] INT           NOT NULL,
    [description] VARCHAR (MAX) NOT NULL,
    [status]      INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([userID] ASC)
);
INSERT INTO [dbo].[users] ([userID], [password], [name], [email], [phoneNumber], [description], [status]) VALUES (N'classUser1', N'password1', N'class user', N'test@email.com', 9888888, N'testing', 0);

CREATE TABLE [dbo].[tours] (
    [tourID]      INT          IDENTITY (1, 1) NOT NULL,
    [userID]      VARCHAR (50) NOT NULL,
    [tourName]    VARCHAR (50) NOT NULL,
    [capacity]    INT          NOT NULL,
    [location]    VARCHAR (50) NOT NULL,
    [description] VARCHAR (50) NOT NULL,
    [startDate]   DATETIME     NOT NULL,
    [endDate]     DATETIME     NOT NULL,
    [price]       MONEY        NOT NULL,
    [status]      VARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([tourID] ASC),
    CONSTRAINT [FK_userID_tours] FOREIGN KEY ([userID]) REFERENCES [dbo].[users] ([userID])
);
INSERT INTO [dbo].[tours] ([tourID], [userID], [tourName], [capacity], [location], [description], [startDate], [endDate], [price], [status]) VALUES (15, N'user1', N'Error Tour Check', 20, N'error check', N'error description check', N'2019-11-21 20:00:00', N'2019-11-21 21:10:00', CAST(30.0000 AS Money), N'open')

CREATE TABLE [dbo].[ratings] (
    [ratingID]   INT          IDENTITY (1, 1) NOT NULL,
    [ratingTo]   VARCHAR (50) NOT NULL,
    [ratingFrom] VARCHAR (50) NOT NULL,
    [stars]      INT          NOT NULL,
    [type]       VARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([ratingID] ASC),
    CONSTRAINT [FK_ratingsTo_user] FOREIGN KEY ([ratingTo]) REFERENCES [dbo].[users] ([userID]),
    CONSTRAINT [FK_ratingsFrom_user] FOREIGN KEY ([ratingFrom]) REFERENCES [dbo].[users] ([userID])
);
INSERT INTO [dbo].[ratings] ([ratingID], [ratingTo], [ratingFrom], [stars], [type]) VALUES (2, N'Ruth781', N'user2', 4, N'tourguide')

CREATE TABLE [dbo].[chat] (
    [chatID]    INT           IDENTITY (1, 1) NOT NULL,
    [sender]    VARCHAR (50)  NOT NULL,
    [recipient] VARCHAR (50)  NOT NULL,
    [subject]   VARCHAR (50)  NOT NULL,
    [message]   VARCHAR (MAX) NOT NULL,
    [dateTime]  DATETIME      NOT NULL,
    PRIMARY KEY CLUSTERED ([chatID] ASC),
    CONSTRAINT [FK_recipient_ToUser] FOREIGN KEY ([recipient]) REFERENCES [dbo].[users] ([userID]),
    CONSTRAINT [FK_sender_ToUser] FOREIGN KEY ([sender]) REFERENCES [dbo].[users] ([userID])
);
INSERT INTO [dbo].[chat] ([chatID], [sender], [recipient], [subject], [message], [dateTime]) VALUES (6, N'Ruth781', N'user1', N'Test Message', N'This is a test message', N'2019-08-11 22:02:17')

CREATE TABLE [dbo].[bookings] (
    [bookingID] INT          IDENTITY (1, 1) NOT NULL,
    [userID]    VARCHAR (50) NOT NULL,
    [tourID]    INT          NOT NULL,
    PRIMARY KEY CLUSTERED ([bookingID] ASC),
    CONSTRAINT [FK_userID_users] FOREIGN KEY ([userID]) REFERENCES [dbo].[users] ([userID]),
    CONSTRAINT [FK_tourID_tours] FOREIGN KEY ([tourID]) REFERENCES [dbo].[tours] ([tourID])
);
INSERT INTO [dbo].[bookings] ([bookingID], [userID], [tourID]) VALUES (9, N'user1', 7)
