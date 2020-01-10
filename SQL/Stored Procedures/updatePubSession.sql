CREATE PROCEDURE Update_PubSession(@SessionID NVARCHAR(40), @OrderBasketID INT = 0, @UserID INT) 
AS
BEGIN
    BEGIN TRANSACTION
    DECLARE @Error NVARCHAR(Max);

    BEGIN TRY
        UPDATE dbo.PubSessions
        SET OrderBasketID = @OrderBasketID, UserID = @UserID
        WHERE SessionID = @SessionID;

        IF @@TRANCOUNT > 0 
            PRINT N'updated session: ID = ' + CAST(@SessionID as varchar);
            COMMIT;
			RETURN(1)
    END TRY
    BEGIN CATCH
        SET @Error = 'An error was encountered : Could not update session'; --set output error message
        IF @@TRANCOUNT > 0 --if anything was changed during the transaction before the error, rollback all the changes to before the transaction.
            ROLLBACK TRANSACTION;
        RAISERROR(@Error, 1, 0);
		RETURN(0)
    END CATCH;
END;