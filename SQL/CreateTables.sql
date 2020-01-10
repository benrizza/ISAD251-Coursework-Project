CREATE TABLE PubOrders
(
	OrderID INT NOT NULL,
	UserID INT NOT NULL,
	OrderDate DATE NOT NULL,
	CONSTRAINT PK_PubOrders PRIMARY KEY (OrderID)
);

CREATE TABLE PubOrderItems
(
	OrderID INT NOT NULL,
	ItemID INT NOT NULL,
	ItemOrderPrice DECIMAL(10,2) NOT NULL,
	ItemQuantity INT NOT NULL,
		CHECK(NOT (ItemQuantity <= 0)),
		CHECK(ItemQuantity <= 100), -- 100 quantity of a single item per order max
	CONSTRAINT PK_PubOrderItems PRIMARY KEY (OrderID, ItemID)
);

CREATE TABLE PubSessions -- each session has a unique entry in database - removed when exit?
(
	SessionID NVARCHAR(40) NOT NULL,
	UserID INT, --session may not have a user
	OrderBasketID INT NOT NULL,
	CONSTRAINT PK_PubLoggedInUsers PRIMARY KEY (SessionID)
);

CREATE TABLE PubOrderBasketItems --WORK ON
(
	OrderBasketID INT NOT NULL,
	ItemID INT NOT NULL,
	ItemQuantity INT NOT NULL,
		CHECK(ItemQuantity >= 0),
		CHECK(ItemQuantity <= 100), -- 100 quantity of a single item per order max
	CONSTRAINT PK_PubOrderBasketItems PRIMARY KEY (OrderBasketID, ItemID)
);

CREATE TABLE PubItems
(
	ItemID INT NOT NULL,
	ItemName NVARCHAR(100) NOT NULL,
	ItemType VARCHAR(5), -- either snack or drink therefore cannot be more than 5 letters and dosen't need to be unicode
	ItemPrice DECIMAL(10,2) NOT NULL, --allow 2 digits to the right of the decimal and 10 to the left 000000000.00
	ItemImagePath NVARCHAR(MAX),
	ItemDescription NVARCHAR(MAX),
	ItemStock INT,
        CHECK(ItemStock >= 0),
	ItemOnSale BIT, --default off sale
	CONSTRAINT PK_PubItems PRIMARY KEY (ItemID)
);

CREATE TABLE PubUsers
(
	UserID INT NOT NULL, --USER ID IS ALSO THE 'USERNAME' 
	UserFirstName NVARCHAR(60) NOT NULL,
	UserLastName NVARCHAR(80) NOT NULL,
	UserAccessRank VARCHAR(20) NOT NULL, --different access levels for users
	UserPassword NVARCHAR(MAX) NOT NULL, --attempt to hash
	UserOrderBasketID INT, --user can have a basket in progress
	CONSTRAINT PK_PubUsers PRIMARY KEY (UserID)
);

ALTER TABLE PubOrderItems
	ADD CONSTRAINT ItemIDFK FOREIGN KEY (ItemID)
	REFERENCES PubItems (ItemID)
	ON DELETE CASCADE
	ON UPDATE CASCADE

ALTER TABLE PubOrderItems
	ADD CONSTRAINT OrderIDFK FOREIGN KEY (OrderID)
	REFERENCES PubOrders (OrderID)
	ON DELETE CASCADE
	ON UPDATE CASCADE

ALTER TABLE PubOrders
	ADD CONSTRAINT UserIDFK FOREIGN KEY (UserID)
	REFERENCES PubUsers (UserID)
	ON DELETE CASCADE
	ON UPDATE CASCADE

ALTER TABLE PubOrderBasket
	ADD CONSTRAINT SessionIDFK FOREIGN KEY (SessionID)
	REFERENCES PubSessions (SessionID)
	ON DELETE CASCADE
	ON UPDATE CASCADE

ALTER TABLE PubOrderBasketItems
	ADD CONSTRAINT OrderBasketItemID FOREIGN KEY (ItemID)
	REFERENCES PubItems (ItemID)
	ON DELETE CASCADE
	ON UPDATE CASCADE