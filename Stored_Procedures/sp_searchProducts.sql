/*
** Stored Procedure to Get Products
** Version: 1.0
** Date: 11/13/2024
** Dev: 68358
** Ex: ExEc sp_searchProducts Apple
** Ex: ExEc sp_searchProducts Null
*/ 
USE db_shopon
GO

IF EXISTS
(
SELECT 1 FROM sys.objects WHERE type='p' AND OBJECT_ID = OBJECT_ID('[dbo].[sp_searchProducts]')
)
BEGIN
 DROP PROC [dbo].[sp_searchProducts]
END
GO

CREATE PROC [dbo].[sp_searchProducts]
(
	@v_searchKey VARCHAR(20) = NULL -- Optional Parameter
)
AS
BEGIN
	if(@v_searchKey IS NULL)
	BEGIN
		SELECT
			pro.product_id AS Id,
			pro.product_name AS Name,
			pro.imageUrl AS ImageUrl,
			pro.price AS Price,
			com.company_id AS CompanyId,
			com.company_name AS CompanyName
		FROM
		 [products] AS pro WITH(NOLOCK)
		 INNER JOIN [dbo].[companies] AS com WITH (NOLOCK)
			ON com.company_id = pro.company_id 
			AND com.isDeleted = 0
		WHERE pro.isDeleted = 0
	END
	BEGIN
		SELECT
			pro.product_id AS Id,
			pro.product_name AS Name,
			pro.imageUrl AS ImageUrl,
			pro.price AS Price,
			com.company_id AS CompanyId,
			com.company_name AS CompanyName
		FROM
		 [products] AS pro WITH(NOLOCK)
		 INNER JOIN [dbo].[companies] AS com WITH (NOLOCK)
			ON com.company_id = pro.company_id 
			AND com.isDeleted = 0
		WHERE pro.isDeleted = 0
		AND pro.product_name LIKE '%' + @v_searchKey + '%'

	END
END
GO

