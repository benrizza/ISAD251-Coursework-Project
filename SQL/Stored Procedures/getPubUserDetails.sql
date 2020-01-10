CREATE PROCEDURE Get_PubUserDetails(@UserID as INT)
AS
BEGIN
    SELECT PubUsers.UserId, PubUsers.UserFirstName, PubUsers.UserLastName, PubUsers.UserAccessRank, PubUsers.UserOrderBasketID
    FROM PubUsers
    WHERE PubUsers.UserID = @UserID
END;
GO