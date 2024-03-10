-- Creates Authors Table
CREATE TABLE Authors (
    id        int primary key not null,
    name      VARCHAR(20) not null,
    lastname  VARCHAR(20) not null,
    email     VARCHAR(30) unique not null,
	portfolio Varchar(50) unique
);
GO

-- Create trigger for automatic ID generation
CREATE TRIGGER trgAuthor_AutoID
ON Authors
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @InsertedID INT;

    -- Get the inserted ID value
    SELECT @InsertedID = id
    FROM inserted;

    -- If the inserted ID is 0, generate a new ID
    IF @InsertedID = 0
    BEGIN
        DECLARE @NewID INT;

        -- Generate new ID
        SELECT @NewID = ISNULL(MAX(id), 0) + 1
        FROM Authors;

        -- Update inserted rows with the new ID value
        UPDATE Authors
        SET id = @NewID
        WHERE id IN (SELECT id FROM inserted);
    END;
END;

-- Create Bank Transfers Table
CREATE TABLE Bank_Transfers (
    id              int primary key not null,
    variable_symbol VARCHAR(10) not null,
    iban            VARCHAR(34) unique not null
);
GO

-- Create trigger for automatic ID generation
CREATE TRIGGER trgBankTransfer_AutoID
ON Bank_Transfers
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @InsertedID INT;

    -- Get the inserted ID value
    SELECT @InsertedID = id
    FROM inserted;

    -- If the inserted ID is 0, generate a new ID
    IF @InsertedID = 0
    BEGIN
        DECLARE @NewID INT;

        -- Generate new ID
        SELECT @NewID = ISNULL(MAX(id), 0) + 1
        FROM Bank_Transfers;

        -- Update inserted rows with the new ID value
        UPDATE Bank_Transfers
        SET id = @NewID
        WHERE id IN (SELECT id FROM inserted);
    END;
END;

-- Create Credit Cards Table
CREATE TABLE Credit_Cards (
    id				int primary key not null,
    card_number     VARCHAR(16) unique NOT NULL,
    expiration_date DATETIME NOT NULL,
    cvv             VARCHAR(3) NOT NULL
);
GO

-- Create trigger for automatic ID generation
CREATE TRIGGER trgCreditCard_AutoID
ON Credit_Cards
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @InsertedID INT;

    -- Get the inserted ID value
    SELECT @InsertedID = id
    FROM inserted;

    -- If the inserted ID is 0, generate a new ID
    IF @InsertedID = 0
    BEGIN
        DECLARE @NewID INT;

        -- Generate new ID
        SELECT @NewID = ISNULL(MAX(id), 0) + 1
        FROM Credit_Cards;

        -- Update inserted rows with the new ID value
        UPDATE Credit_Cards
        SET id = @NewID
        WHERE id IN (SELECT id FROM inserted);
    END;
END;

-- Create Customers Table
CREATE TABLE Customers (
    id		 int primary key not null,
    name     VARCHAR(20) not null,
    lastname VARCHAR(20) not null,
    email    VARCHAR(30) unique not null,
    phone    VARCHAR(12) not null,
	password VARCHAR(60) not null
);
GO

-- Create trigger for automatic ID generation
CREATE TRIGGER trgCustomer_AutoID
ON Customers
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @InsertedID INT;

    -- Get the inserted ID value
    SELECT @InsertedID = Id
    FROM inserted;

    -- If the inserted ID is 0, generate a new ID
    IF @InsertedID = 0
    BEGIN
        DECLARE @NewID INT;

        -- Generate new ID
        SELECT @NewID = ISNULL(MAX(Id), 0) + 1
        FROM Customers;

        -- Update inserted rows with the new ID value
        UPDATE Customers
        SET Id = @NewID
        WHERE Id IN (SELECT Id FROM inserted);
    END;
END;

-- Create Items Table
CREATE TABLE Items (
    id			  int primary key not null,
    orders_id     int not null foreign key references Orders(id),
    template_id   int not null foreign key references Templates(id),
    quantity      int not null,
    price_of_item float not null
);
GO

-- Create trigger for automatic ID generation
CREATE TRIGGER trgItem_AutoID
ON Items
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @InsertedID INT;

    -- Get the inserted ID value
    SELECT @InsertedID = id
    FROM inserted;

    -- If the inserted ID is 0, generate a new ID
    IF @InsertedID = 0
    BEGIN
        DECLARE @NewID INT;

        -- Generate new ID
        SELECT @NewID = ISNULL(MAX(id), 0) + 1
        FROM Items;

        -- Update inserted rows with the new ID value
        UPDATE Items
        SET id = @NewID
        WHERE id IN (SELECT id FROM inserted);
    END;
END;

-- Create Orders Table
CREATE TABLE Orders (
    id			 int primary key not null,
    customer_id  int not null foreign key references Customers(id),
    payment_id   int not null foreign key references Payments(id),
    order_number int unique not null,
    "Date"       DATETIME not null,
    order_price  float not null
);
GO

-- Create trigger for automatic ID generation
CREATE TRIGGER trgOrder_AutoID
ON Orders
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @InsertedID INT;

    -- Get the inserted ID value
    SELECT @InsertedID = id
    FROM inserted;

    -- If the inserted ID is 0, generate a new ID
    IF @InsertedID = 0
    BEGIN
        DECLARE @NewID INT;

        -- Generate new ID
        SELECT @NewID = ISNULL(MAX(id), 0) + 1
        FROM Orders;

        -- Update inserted rows with the new ID value
        UPDATE Orders
        SET id = @NewID
        WHERE id IN (SELECT id FROM inserted);
    END;
END;

-- Create Payments Table
CREATE TABLE Payments (
    id				 int primary key not null,
    bank_transfer_id int foreign key references Bank_Transfers(id),
    credit_card_id   int foreign key references Credit_Cards(id),
    description      VARCHAR(50),
	CHECK ( 
			( ( bank_transfer_id IS NOT NULL ) AND ( credit_card_id IS NULL ) )
			OR 
			( ( credit_card_id IS NOT NULL ) AND ( bank_transfer_id IS NULL ) )
		  )
);
GO

-- Create trigger for automatic ID generation
CREATE TRIGGER trgPayment_AutoID
ON Payments
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @InsertedID INT;

    -- Get the inserted ID value
    SELECT @InsertedID = id
    FROM inserted;

    -- If the inserted ID is 0, generate a new ID
    IF @InsertedID = 0
    BEGIN
        DECLARE @NewID INT;

        -- Generate new ID
        SELECT @NewID = ISNULL(MAX(id), 0) + 1
        FROM Payments;

        -- Update inserted rows with the new ID value
        UPDATE Payments
        SET id = @NewID
        WHERE id IN (SELECT id FROM inserted);
    END;
END;

-- Create Templates Table
CREATE TABLE Templates (
    id		  int primary key not null,
    author_id int not null foreign key references Authors(id),
    typ_id    int not null foreign key references Types(id),
    name      VARCHAR(30) not null,
    priced    bit not null,
    price     float
);
GO

-- Create trigger for automatic ID generation
CREATE TRIGGER trgTemplate_AutoID
ON Templates
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @InsertedID INT;

    -- Get the inserted ID value
    SELECT @InsertedID = id
    FROM inserted;

    -- If the inserted ID is 0, generate a new ID
    IF @InsertedID = 0
    BEGIN
        DECLARE @NewID INT;

        -- Generate new ID
        SELECT @NewID = ISNULL(MAX(id), 0) + 1
        FROM Templates;

        -- Update inserted rows with the new ID value
        UPDATE Templates
        SET id = @NewID
        WHERE id IN (SELECT id FROM inserted);
    END;
END;

-- Create Types Table
CREATE TABLE Types (
    id	 int primary key not null,
    nazev  VARCHAR(20) not null
); 
GO

-- Create trigger for automatic ID generation
CREATE TRIGGER trgType_AutoID
ON Types
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @InsertedID INT;

    -- Get the inserted ID value
    SELECT @InsertedID = id
    FROM inserted;

    -- If the inserted ID is 0, generate a new ID
    IF @InsertedID = 0
    BEGIN
        DECLARE @NewID INT;

        -- Generate new ID
        SELECT @NewID = ISNULL(MAX(id), 0) + 1
        FROM Types;

        -- Update inserted rows with the new ID value
        UPDATE Types
        SET id = @NewID
        WHERE id IN (SELECT id FROM inserted);
    END;
END;