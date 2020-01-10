CREATE PROCEDURE Update_PubItem(@ItemID INT, @ItemName as NVARCHAR(100), @ItemType VARCHAR(5), @ItemPrice DECIMAL(10,2),@ItemImagePath NVARCHAR(MAX) = NULL, @ItemDescription NVARCHAR(MAX), @ItemStock INT = 0, @ItemOnSale BIT = 0) 
AS
BEGIN
    BEGIN TRANSACTION
    DECLARE @Error NVARCHAR(Max);

    BEGIN TRY
        UPDATE dbo.PubItems 
        SET ItemName = @ItemName, ItemType = @ItemType, ItemPrice = @ItemPrice, ItemImagePath = @ItemImagePath, ItemDescription = @ItemDescription, ItemStock = @ItemStock, ItemOnSale = @ItemOnSale
        WHERE ItemID = @ItemID;

        IF @@TRANCOUNT > 0 
            PRINT N'Edited item: ' + CAST(@ItemID as varchar);
            COMMIT;
            RETURN(1)
    END TRY
    BEGIN CATCH
        SET @Error = 'An error was encountered : Could not edit the item details'; --set output error message
        IF @@TRANCOUNT > 0 --if anything was changed during the transaction before the error, rollback all the changes to before the transaction.
            ROLLBACK TRANSACTION;
        RAISERROR(@Error, 1, 0);
    END CATCH
END;
GO