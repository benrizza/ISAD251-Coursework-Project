CREATE PROCEDURE Remove_PubOrderBasketItem(@OrderBasketID INT, @ItemID INT) --change the quantity of a specific item in an order
AS
BEGIN
    BEGIN TRANSACTION
    DECLARE @Error NVARCHAR(Max);
    DECLARE @ProcedureCheck INT;

    BEGIN TRY
        DELETE FROM dbo.PubOrderBasketItems WHERE PubOrderBasketItems.OrderBasketID = @OrderBasketID AND PubOrderBasketItems.ItemID = @ItemID

        IF (SELECT COUNT(OrderBasketID) FROM PubOrderBasketItems WHERE PubOrderBasketItems.OrderBasketID = @OrderBasketID) = 0 
        BEGIN
            EXEC @ProcedureCheck = [dbo].[Remove_PubOrderBasket] @OrderBasketID
            IF @ProcedureCheck = 0
            BEGIN
                SET @Error = 'An error was encountered - Could not update user order basket, old basket id:  ' + CAST(@OrderBasketID as VARCHAR); --set output error message
                IF @@TRANCOUNT > 0 --if anything was changed during the transaction before the error, rollback all the changes to before the transaction.
                ROLLBACK TRANSACTION;
                RAISERROR(@Error, 1, 0);
                RETURN(0);
            END
        END

        IF @@TRANCOUNT > 0 
            PRINT N'Removed order basket item from basket: ' + CAST(@OrderBasketID as VARCHAR) ;
            COMMIT;
            RETURN(1);
    END TRY
    BEGIN CATCH
        SET @Error = 'An error was encountered - Could not remove order basket item from basket: ' + CAST(@OrderBasketID as VARCHAR); --set output error message
        IF @@TRANCOUNT > 0 --if anything was changed during the transaction before the error, rollback all the changes to before the transaction.
            ROLLBACK TRANSACTION;
        RAISERROR(@Error, 1, 0);
        RETURN(0);
    END CATCH
END;
GO