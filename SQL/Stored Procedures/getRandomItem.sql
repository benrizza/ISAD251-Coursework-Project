CREATE PROCEDURE [dbo].[Get_RandomPubItem](@ItemOnSale BIT = 1, @ItemType VARCHAR(5) = NULL)
AS
BEGIN
    IF @ItemType IS NULL
    BEGIN
        SELECT TOP 1 * FROM dbo.PubItems
        WHERE PubItems.ItemOnSale = @ItemOnSale
        ORDER BY NEWID()
    END;
    ELSE
    BEGIN
        SELECT TOP 1 * FROM dbo.PubItems
        WHERE PubItems.ItemType = @ItemType AND PubItems.ItemOnSale = @ItemOnSale
        ORDER BY NEWID()
    END;
END;