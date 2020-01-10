CREATE PROCEDURE Get_PubOrder(@OrderID as INT)
AS
BEGIN
    SELECT PubOrders.OrderID, PubOrders.UserID, PubOrders.OrderDate, SUM(POI.ItemQuantity * POI.ItemOrderPrice) as OrderTotalCost
    FROM PubOrders
    JOIN PubOrderItems as POI ON PubOrders.OrderID = POI.OrderID
    WHERE PubOrders.OrderID = @OrderID
    GROUP BY PubOrders.OrderID, PubOrders.UserID, PubOrders.OrderDate
	RETURN
END;
GO


