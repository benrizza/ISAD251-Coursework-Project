CREATE PROCEDURE Update_PubOrderBasketItem(@OrderBasketID INT, @ItemID INT, @ItemQuantity INT = 0) --change the quantity of a specific item in an order
AS
BEGIN
    BEGIN TRANSACTION
    DECLARE @Error NVARCHAR(Max);

    BEGIN TRY
        UPDATE dbo.PubOrderBasketItems
        SET ItemQuantity = @ItemQuantity
        WHERE PubOrderBasketItems.OrderBasketID = @OrderBasketID AND PubOrderBasketItems.ItemID = @ItemID;

        IF @@TRANCOUNT > 0 
            PRINT N'Edited pub basket item: ' + CAST(@ItemID as VARCHAR) + ' from order: ' + CAST(@OrderBasketID as VARCHAR);
            COMMIT;
            RETURN(1)
    END TRY
    BEGIN CATCH
        SET @Error = 'An error was encountered : Could not edit the pub order basket item'; --set output error message
        IF @@TRANCOUNT > 0 --if anything was changed during the transaction before the error, rollback all the changes to before the transaction.
            ROLLBACK TRANSACTION;
        RAISERROR(@Error, 1, 0);
        RETURN(0)
    END CATCH
END;
GO