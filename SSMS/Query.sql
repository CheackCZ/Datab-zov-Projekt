CREATE TABLE author (
    id       int primary key not null identity(1,1),
    name     VARCHAR(20) not null,
    lastname VARCHAR(20) not null,
    email    VARCHAR(30) unique not null
);
ALTER TABLE author ADD portfolio Varchar(50) unique not null;

CREATE TABLE bank_transfer (
    id              int primary key not null identity(1,1),
    variable_symbol VARCHAR(10) not null,
    iban            VARCHAR(34) unique not null
);

CREATE TABLE credit_card (
    id				int primary key not null identity(1,1),
    card_number     VARCHAR(16) unique NOT NULL,
    expiration_date DATE NOT NULL,
    cvv             VARCHAR(3) NOT NULL
);

CREATE TABLE customer (
    id		 int primary key not null identity(1,1),
    name     VARCHAR(20) not null,
    lastname VARCHAR(20) not null,
    email    VARCHAR(30) unique not null,
    phone    VARCHAR(12) not null
);
ALTER TABLE author ADD Heslo Varchar(32) unique not null;

CREATE TABLE item (
    id			  int primary key not null identity(1,1),
    orders_id     int not null foreign key references objednavka(id),
    template_id   int not null foreign key references template(id),
    quantity      int not null,
    price_of_item float not null
);

CREATE TABLE objednavka (
    id			 int primary key not null identity(1,1),
    customer_id  int not null foreign key references customer(id),
    payment_id   int not null foreign key references payment(id),
    order_number int unique not null,
    "Date"       DATE not null,
    order_price  float not null
);

CREATE TABLE payment (
    id				 int primary key not null identity(1,1),
    bank_transfer_id int not null foreign key references bank_transfer(id),
    credit_card_id   int not null foreign key references credit_card(id),
    description      VARCHAR(50) not null,
	CHECK ( 
		( ( bank_transfer_id IS NOT NULL ) AND ( credit_card_id IS NULL ) )
        OR 
		( ( credit_card_id IS NOT NULL ) AND ( bank_transfer_id IS NULL ) ) )
);

CREATE TABLE template (
    id		  int primary key not null identity(1,1),
    author_id int not null foreign key references author(id),
    typ_id    int not null foreign key references typ(id),
    name      VARCHAR(30) not null,
    priced    CHAR(1) not null,
    float     int not null
);
alter table template alter column float int price float;

CREATE TABLE typ (
    id	 int primary key not null identity(1,1),
    type  VARCHAR(20) not null
); 