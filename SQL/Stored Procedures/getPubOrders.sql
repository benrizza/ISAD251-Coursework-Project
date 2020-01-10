CREATE PROCEDURE Get_PubOrders(@UserID as INT = 0, @PageNumber INT = 0, @OrdersPerPage INT)
AS
BEGIN
	DECLARE @RowOffset INT;
	DECLARE @NumberOfRows INT = 0;

	SET @RowOffset = @PageNumber * @OrdersPerPage;

    IF @UserID > 0
    BEGIN
        SELECT PubOrders.OrderID, PubOrders.UserID, PubOrders.OrderDate, SUM(POI.ItemQuantity * POI.ItemOrderPrice) as OrderTotalCost
        FROM PubOrders
        JOIN PubOrderItems as POI ON PubOrders.OrderID = POI.OrderID
        WHERE PubOrders.UserID = @UserID
        GROUP BY PubOrders.OrderID, PubOrders.UserID, PubOrders.OrderDate
        ORDER BY PubOrders.OrderID
        OFFSET @RowOffset ROWS
		FETCH NEXT @OrdersPerPage ROWS ONLY;
        SET @NumberOfRows = (SELECT COUNT(OrderID) FROM PubOrders WHERE PubOrders.UserID = @UserID)
		RETURN(@NumberOfRows)
    END
    ELSE
    BEGIN
        SELECT PubOrders.OrderID, PubOrders.UserID, PubOrders.OrderDate, SUM(POI.ItemQuantity * POI.ItemOrderPrice) as OrderTotalCost
        FROM PubOrders
        JOIN PubOrderItems as POI ON PubOrders.OrderID = POI.OrderID
        GROUP BY PubOrders.OrderID, PubOrders.UserID, PubOrders.OrderDate
        ORDER BY PubOrders.OrderID
        OFFSET @RowOffset ROWS
		FETCH NEXT @OrdersPerPage ROWS ONLY;
        SET @NumberOfRows = (SELECT COUNT(OrderID) FROM PubOrders)
		RETURN(@NumberOfRows)
    END
END;
GO