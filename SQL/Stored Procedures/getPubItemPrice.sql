CREATE PROCEDURE Get_PubItemPrice(@ItemID INT, @ItemOnSale BIT)
AS
BEGIN
    SELECT PubItems.ItemID, PubItems.ItemPrice
    FROM PubItems
    WHERE PubItems.ItemID = @ItemID AND PubItems.ItemOnSale = @ItemOnSale
END;