CREATE PROCEDURE Get_PubSession(@SessionID as NVARCHAR(40))
AS
BEGIN
    SELECT PubSessions.SessionID, PubSessions.UserID, PubSessions.OrderBasketID
    FROM PubSessions
    WHERE PubSessions.SessionID = @SessionID
END;