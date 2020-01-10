CREATE PROCEDURE Get_PubOrderBasketItems(@OrderBasketID as INT = NULL)
AS
BEGIN
    DECLARE @ERROR VARCHAR(MAX);
    IF @OrderBasketID IS NULL
		BEGIN
			SET @ERROR = 'Error: An order basket ID was not given.'
			RAISERROR(@ERROR, 1, 0);
		END
	ELSE
		BEGIN
			SELECT PubItems.ItemID, PubItems.ItemName, PubItems.ItemType, PubItems.ItemPrice, PubItems.ItemImagePath, PubItems.ItemDescription, PubItems.ItemStock, PubItems.ItemOnSale, PubOrderBasketItems.ItemQuantity
			FROM PubItems, PubOrderBasketItems
			WHERE PubOrderBasketItems.OrderBasketID = @OrderBasketID AND PubItems.ItemID = PubOrderBasketItems.ItemID
			RETURN
		END
END;
GO