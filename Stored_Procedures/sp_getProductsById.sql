/*
** Stored Procedure to Get Products
** Version: 1.0
** Date: 11/13/2024
** Dev: 68358
** Ex: ExEc sp_getProductsById 1
**
** All the user defined variables will have one @ symbol
*/ 
USE db_shopon
GO

IF EXISTS
(
SELECT 1 FROM sys.objects WHERE type='p' AND OBJECT_ID = OBJECT_ID('[dbo].[sp_getProductsById]')
)
BEGIN
 DROP PROC [dbo].[sp_getProductsById]
END
GO

CREATE PROC [dbo].[sp_getProductsById]
(
	@v_product_id INT
)
AS
BEGIN
	SELECT
		pro.product_id		AS Id,
		pro.product_name	AS Name,
		pro.imageUrl		AS ImageUrl,
		pro.price			AS Price,
		com.company_id		AS CompanyId,
		com.company_name	AS CompanyName,
		prorat.rating		AS Ratings,
		prorat.product_id	AS ProductIdFK,
		cat.category_name	AS CategoryName
	FROM [dbo].[products] AS pro WITH(NOLOCK)
	INNER JOIN [dbo].[companies] AS com WITH (NOLOCK)
        ON com.company_id = pro.company_id 
	INNER JOIN [dbo].[productratings] AS prorat WITH (NOLOCK)
		ON pro.product_id = prorat.product_id
	INNER JOIN [dbo].[categories] AS cat WITH (NOLOCK)
		ON pro.category_id = cat.category_id
		AND cat.isDeleted = 0
		AND prorat.isDeleted = 0
        AND com.isDeleted = 0
	WHERE pro.isDeleted = 0
	AND pro.product_id = @v_product_id

END
GO

