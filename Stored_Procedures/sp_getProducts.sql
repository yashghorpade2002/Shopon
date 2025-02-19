/*
** Stored Procedure to Get Products
** Version: 1.0
** Date: 11/13/2024
** Dev: 68358
** Ex: ExEc sp_getProducts
*/ 
USE db_shopon
GO

IF EXISTS
(
SELECT 1 FROM sys.objects WHERE type='p' AND OBJECT_ID = OBJECT_ID('[dbo].[sp_getProducts]')
)
BEGIN
 DROP PROC [dbo].[sp_getProducts]
END
GO

CREATE PROC [dbo].[sp_getProducts]
AS
BEGIN
	SELECT
		pro.product_id AS Id,
		pro.product_name AS Name,
		pro.imageUrl AS ImageUrl,
		pro.price AS Price,
		com.company_name AS CompanyName
	FROM
	 [products] AS pro WITH(NOLOCK)
	 INNER JOIN [dbo].[companies] AS com WITH (NOLOCK)
        ON com.company_id = pro.company_id 
        AND com.isDeleted = 0
	WHERE pro.isDeleted = 0;

END
GO

