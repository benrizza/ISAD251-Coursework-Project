CREATE PROCEDURE Get_PubItem(@ItemID INT)
AS
BEGIN
    SELECT PubItems.ItemID, PubItems.ItemName, PubItems.ItemType, PubItems.ItemPrice, PubItems.ItemImagePath, PubItems.ItemDescription, PubItems.ItemStock, PubItems.ItemOnSale
    FROM PubItems
    WHERE PubItems.ItemID = @ItemID
END;