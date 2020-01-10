CREATE PROCEDURE Get_PubUserOrderItems(@OrderID as INT)
AS
BEGIN
	SELECT TOP 30 PubItems.ItemID, PubItems.ItemName
	FROM PubItems, PubOrderItems
	WHERE PubOrderItems.OrderID = @OrderID AND PubItems.ItemID = PubOrderItems.ItemID
	RETURN
END;
GO