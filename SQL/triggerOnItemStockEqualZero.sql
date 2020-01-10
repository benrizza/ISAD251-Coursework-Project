CREATE TRIGGER TakeOffSale ON PubItems
AFTER UPDATE
AS
BEGIN
    IF (SELECT ItemStock FROM inserted) = 0
        BEGIN
            UPDATE dbo.PubItems
            SET PubItems.ItemOnSale = 0
            FROM inserted
            WHERE PubItems.ItemID = inserted.ItemID
        END
END;