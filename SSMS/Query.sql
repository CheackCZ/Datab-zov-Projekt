CREATE TABLE Authors (
    id        int primary key not null identity(1,1),
    name      VARCHAR(20) not null,
    lastname  VARCHAR(20) not null,
    email     VARCHAR(30) unique not null,
	portfolio Varchar(50) unique not null
);

CREATE TABLE Bank_Transfers (
    id              int primary key not null identity(1,1),
    variable_symbol VARCHAR(10) not null,
    iban            VARCHAR(34) unique not null
);

CREATE TABLE Credit_Cards (
    id				int primary key not null identity(1,1),
    card_number     VARCHAR(16) unique NOT NULL,
    expiration_date DATETIME NOT NULL,
    cvv             VARCHAR(3) NOT NULL
);

CREATE TABLE Customers (
    id		 int primary key not null identity(1,1),
    name     VARCHAR(20) not null,
    lastname VARCHAR(20) not null,
    email    VARCHAR(30) unique not null,
    phone    VARCHAR(12) not null,
	password VARCHAR(60) not null
);

CREATE TABLE Items (
    id			  int primary key not null identity(1,1),
    orders_id     int not null foreign key references Orders(id),
    template_id   int not null foreign key references Templates(id),
    quantity      int not null,
    price_of_item float not null
);

CREATE TABLE Orders (
    id			 int primary key not null identity(1,1),
    customer_id  int not null foreign key references Customers(id),
    payment_id   int not null foreign key references Payments(id),
    order_number int unique not null,
    "Date"       DATETIME not null,
    order_price  float not null
);

CREATE TABLE Payments (
    id				 int primary key not null identity(1,1),
    bank_transfer_id int foreign key references Bank_Transfers(id),
    credit_card_id   int foreign key references Credit_Cards(id),
    description      VARCHAR(50),
	CHECK ( 
			( ( bank_transfer_id IS NOT NULL ) AND ( credit_card_id IS NULL ) )
			OR 
			( ( credit_card_id IS NOT NULL ) AND ( bank_transfer_id IS NULL ) )
		  )
);

CREATE TABLE Templates (
    id		  int primary key not null identity(1,1),
    author_id int not null foreign key references Authors(id),
    typ_id    int not null foreign key references Types(id),
    name      VARCHAR(30) not null,
    priced    bit not null,
    price     float
);

CREATE TABLE Types (
    id	 int primary key not null identity(1,1),
    nazev  VARCHAR(20) not null
); 