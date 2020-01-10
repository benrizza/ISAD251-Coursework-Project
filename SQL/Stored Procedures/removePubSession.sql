CREATE PROCEDURE Remove_PubSession(@SessionID NVARCHAR(40)) --change the quantity of a specific item in an order
AS
BEGIN
    BEGIN TRANSACTION
    DECLARE @Error NVARCHAR(Max);

    BEGIN TRY
        DELETE FROM dbo.PubSessions WHERE PubSessions.SessionID = @SessionID

        IF @@TRANCOUNT > 0 
            PRINT N'Removed order: ' + CAST(@SessionID as VARCHAR) ;
            COMMIT;
            RETURN(1);
    END TRY
    BEGIN CATCH
        SET @Error = 'An error was encountered : Could not remove order: ' + CAST(@SessionID as VARCHAR); --set output error message
        IF @@TRANCOUNT > 0 --if anything was changed during the transaction before the error, rollback all the changes to before the transaction.
            ROLLBACK TRANSACTION;
        RAISERROR(@Error, 1, 0);
        RETURN(0);
    END CATCH
END;
GO