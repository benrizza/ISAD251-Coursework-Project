CREATE PROCEDURE Remove_PubOrderBasket(@OrderBasketID INT) --change the quantity of a specific item in an order
AS
BEGIN
    BEGIN TRANSACTION
    DECLARE @Error NVARCHAR(Max);

    BEGIN TRY
        UPDATE dbo.PubUsers
        SET PubUsers.UserOrderBasketID = 0
        WHERE PubUsers.UserOrderBasketID = @OrderBasketID

        UPDATE dbo.PubSessions
        SET PubSessions.OrderBasketID = 0
        WHERE PubSessions.OrderBasketID = @OrderBasketID

        IF @@TRANCOUNT > 0 
            PRINT N'Removed order basket: ' + CAST(@OrderBasketID as VARCHAR) ;
            COMMIT;
            RETURN(1);
    END TRY
    BEGIN CATCH
        SET @Error = 'An error was encountered : Could not remove order basket: ' + CAST(@OrderBasketID as VARCHAR); --set output error message
        IF @@TRANCOUNT > 0 --if anything was changed during the transaction before the error, rollback all the changes to before the transaction.
            ROLLBACK TRANSACTION;
        RAISERROR(@Error, 1, 0);
        RETURN(0);
    END CATCH
END;
GO