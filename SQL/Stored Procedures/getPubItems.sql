CREATE PROCEDURE Get_PubItems(@ItemName as NVARCHAR(100) = NULL, @PageNumber INT = 0, @ItemsPerPage INT, @ItemType as VARCHAR(5) = NULL)
AS
BEGIN
    IF @ItemType IS NULL
        BEGIN
			DECLARE @RowOffset INT;
			DECLARE @NumberOfRows INT = 0;

			SET @RowOffset = @PageNumber * @ItemsPerPage;

			IF @ItemName IS NULL
			BEGIN
				SELECT PubItems.ItemID, PubItems.ItemName, PubItems.ItemType, PubItems.ItemPrice, PubItems.ItemImagePath, PubItems.ItemDescription, PubItems.ItemStock, PubItems.ItemOnSale
				FROM PubItems
				ORDER BY PubItems.ItemID
                OFFSET @RowOffset ROWS
				FETCH NEXT @ItemsPerPage ROWS ONLY;
                SET @NumberOfRows = (SELECT COUNT(ItemID) FROM PubItems)
				RETURN(@NumberOfRows)
			END
			ELSE
			BEGIN
				SELECT PubItems.ItemID, PubItems.ItemName, PubItems.ItemType, PubItems.ItemPrice, PubItems.ItemImagePath, PubItems.ItemDescription, PubItems.ItemStock, PubItems.ItemOnSale
				FROM PubItems, FREETEXTTABLE(dbo.PubItems, ItemName, @ItemName) AS RankTable
				WHERE PubItems.ItemID = RankTable.[KEY]
				ORDER BY RankTable.RANK desc
				OFFSET @RowOffset ROWS
				FETCH NEXT @ItemsPerPage ROWS ONLY;
                SET @NumberOfRows = (SELECT COUNT(ItemID) FROM PubItems, FREETEXTTABLE(dbo.PubItems, ItemName, @ItemName) AS RankTable WHERE PubItems.ItemID = RankTable.[KEY])
				RETURN(@NumberOfRows)
			END
            
        END
    ELSE
        BEGIN
			IF @ItemName IS NULL
			BEGIN
				SELECT PubItems.ItemID, PubItems.ItemName, PubItems.ItemType, PubItems.ItemPrice, PubItems.ItemImagePath, PubItems.ItemDescription, PubItems.ItemStock, PubItems.ItemOnSale
				FROM PubItems
				WHERE PubItems.ItemType = @ItemType
				ORDER BY PubItems.ItemID
                OFFSET @RowOffset ROWS
				FETCH NEXT @ItemsPerPage ROWS ONLY;
                SET @NumberOfRows = (SELECT COUNT(ItemID) FROM PubItems WHERE PubItems.ItemType = @ItemType)
				RETURN(@NumberOfRows)
			END
			ELSE
			BEGIN
				SELECT PubItems.ItemID, PubItems.ItemName, PubItems.ItemType, PubItems.ItemPrice, PubItems.ItemImagePath, PubItems.ItemDescription, PubItems.ItemStock, PubItems.ItemOnSale
				FROM PubItems, FREETEXTTABLE(dbo.PubItems, ItemName, @ItemName) AS RankTable
				WHERE PubItems.ItemID = RankTable.[KEY] AND PubItems.ItemType = @ItemType
				ORDER BY RankTable.RANK desc 
                OFFSET @RowOffset ROWS
				FETCH NEXT @ItemsPerPage ROWS ONLY;
                SET @NumberOfRows = (SELECT COUNT(ItemID) FROM PubItems, FREETEXTTABLE(dbo.PubItems, ItemName, @ItemName) AS RankTable WHERE PubItems.ItemID = RankTable.[KEY] AND PubItems.ItemType = @ItemType)
				RETURN(@NumberOfRows)
				RETURN
			END
        END
END;





--CREATE PROCEDURE Get_PubItems(@ItemName as NVARCHAR(100), @ItemType as VARCHAR(5) = NULL)
--AS
--BEGIN
 --   IF @ItemType IS NULL
--        BEGIN
--            SELECT PubItems.ItemID, PubItems.ItemName, PubItems.ItemType, PubItems.ItemPrice, PubItems.ItemImagePath, PubItems.ItemDescription, PubItems.ItemStock, PubItems.ItemOnSale
---            FROM PubItems
--            WHERE PubItems.ItemName LIKE (@ItemName + '%') 
 --           RETURN
--        END
 --   ELSE
 --       BEGIN
 --           SELECT PubItems.ItemID, PubItems.ItemName, PubItems.ItemType, PubItems.ItemPrice, PubItems.ItemImagePath, PubItems.ItemDescription, PubItems.ItemStock, PubItems.ItemOnSale
 --           FROM PubItems
--            WHERE PubItems.ItemName LIKE (@ItemName + '%') AND PubItems.ItemType = @ItemType
--            RETURN
--        END
--END;






--CREATE PROCEDURE Get_PubItems(@ItemName as NVARCHAR(100) = NULL, @ItemType as VARCHAR(5) = NULL)
--AS
--BEGIN
--    IF @ItemType IS NULL
--        BEGIN
--			IF @ItemName IS NULL
--			BEGIN
--				SELECT PubItems.ItemID, PubItems.ItemName, PubItems.ItemType, PubItems.ItemPrice, PubItems.ItemImagePath, PubItems.ItemDescription, PubItems.ItemStock, PubItems.ItemOnSale
--				FROM PubItems
--				RETURN
--			END
--			ELSE
--			BEGIN
--				SELECT PubItems.ItemID, PubItems.ItemName, PubItems.ItemType, PubItems.ItemPrice, PubItems.ItemImagePath, PubItems.ItemDescription, PubItems.ItemStock, PubItems.ItemOnSale
--				FROM PubItems, FREETEXTTABLE(dbo.PubItems, ItemName, @ItemName) AS RankTable
--				WHERE PubItems.ItemID = RankTable.[KEY]
--				ORDER BY RankTable.RANK desc
--				RETURN
--			END
            
--        END
--    ELSE
--        BEGIN
--			IF @ItemName IS NULL
--			BEGIN
--				SELECT PubItems.ItemID, PubItems.ItemName, PubItems.ItemType, PubItems.ItemPrice, PubItems.ItemImagePath, PubItems.ItemDescription, PubItems.ItemStock, PubItems.ItemOnSale
--				FROM PubItems
--				WHERE PubItems.ItemType = @ItemType
--				RETURN
--			END
--			ELSE
--			BEGIN
--				SELECT PubItems.ItemID, PubItems.ItemName, PubItems.ItemType, PubItems.ItemPrice, PubItems.ItemImagePath, PubItems.ItemDescription, PubItems.ItemStock, PubItems.ItemOnSale
--				FROM PubItems, FREETEXTTABLE(dbo.PubItems, ItemName, @ItemName) AS RankTable
--				WHERE PubItems.ItemID = RankTable.[KEY] AND PubItems.ItemType = @ItemType
--				ORDER BY RankTable.RANK desc 
--				RETURN
--			END
--        END
--EN