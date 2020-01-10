CREATE PROCEDURE Get_PubOrderItems(@OrderID as INT)
AS
BEGIN
	SELECT PubItems.ItemID, PubItems.ItemName, PubItems.ItemType, PubOrderItems.ItemOrderPrice, PubItems.ItemImagePath, PubItems.ItemDescription, PubOrderItems.ItemQuantity
	FROM PubItems, PubOrderItems
	WHERE PubOrderItems.OrderID = @OrderID AND PubItems.ItemID = PubOrderItems.ItemID
	RETURN
END;
GO