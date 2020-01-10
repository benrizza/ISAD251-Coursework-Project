CREATE PROCEDURE Update_PubUserOrderBasket(@UserID INT, @UserOrderBasketID INT) 
AS
BEGIN
    BEGIN TRANSACTION
    DECLARE @Error NVARCHAR(Max);

    BEGIN TRY
        UPDATE dbo.PubUsers 
        SET UserOrderBasketID = @UserOrderBasketID
        WHERE UserID = @UserID;

        IF @@TRANCOUNT > 0 
            PRINT N'Edited user: ' + CAST(@UserID as varchar);
            COMMIT;
            RETURN(1)
    END TRY
    BEGIN CATCH
        SET @Error = 'An error was encountered : Could not edit the user order basket id'; --set output error message
        IF @@TRANCOUNT > 0 --if anything was changed during the transaction before the error, rollback all the changes to before the transaction.
            ROLLBACK TRANSACTION;
        RAISERROR(@Error, 1, 0);
        RETURN(0)
    END CATCH
END;