CREATE PROCEDURE Get_PubUser(@UserID as INT)
AS
BEGIN
    SELECT *
    FROM PubUsers
    WHERE PubUsers.UserID = @UserID
END;
GO