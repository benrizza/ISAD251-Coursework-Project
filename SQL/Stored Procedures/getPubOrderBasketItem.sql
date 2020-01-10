CREATE PROCEDURE Get_PubOrderBasketItem(@OrderBasketID INT, @ItemID INT)
AS
BEGIN
    SELECT *
    FROM PubOrderBasketItems
    WHERE PubOrderBasketItems.OrderBasketID = @OrderBasketID AND PubOrderBasketItems.ItemID = @ItemID
END;
GO