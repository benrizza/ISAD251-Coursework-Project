CREATE TABLE PubOrders
(
	OrderID INT IDENTITY(1,1) NOT NULL,
	UserID INT NOT NULL,
	OrderDate DATE NOT NULL,
	CONSTRAINT PK_PubOrders PRIMARY KEY (OrderID)
);

CREATE TABLE PubOrderItems
(
	OrderID INT NOT NULL,
	ItemID INT NOT NULL,
	ItemQuantitiy INT NOT NULL,
		CHECK(ItemQuantity <> 0),
	CONSTRAINT PK_PubOrderItems PRIMARY KEY (OrderID, ItemID)
);

CREATE TABLE PubItems
(
	ItemID INT IDENTITY(1,1) NOT NULL,
	ItemName NVARCHAR(100) NOT NULL,
	ItemType VARCHAR(5), -- either snack or drink therefore cannot be more than 5 letters and dosen't need to be unicode
	ItemPrice DECIMAL(10,2) NOT NULL, --allow 2 digits to the right of the decimal and 10 to the left 000000000.00
	--ItemImagePath IMAGE ?????
	ItemDescription NVARCHAR(MAX),
	ItemStock INT,
	ItemOnSale BIT,
	CONSTRAINT PK_PubItems PRIMARY KEY (ItemID)
);

CREATE TABLE PubUsers
(
	UserID INT IDENTITY(1,1) NOT NULL,
	UserFirstName NVARCHAR(60),
	UserLastName NVARCHAR(80),
	UserAccessRank TINYINT, --different access levels for users
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

