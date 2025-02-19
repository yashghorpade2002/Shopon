/*
** Stored Procedure to Get Products
** Version: 1.0
** Date: 11/13/2024
** Dev: 68358
** Ex: ExEc sp_insertOrderItems 1,1,100  -- Order_Id, Price, Qty
**
** All the user defined variables will have one @ symbol
*/ 
USE db_shopon
GO

IF EXISTS
(
SELECT 1 FROM sys.objects WHERE type='p' AND OBJECT_ID = OBJECT_ID('[dbo].[sp_insertOrderItems]')
)
BEGIN
 DROP PROC [dbo].[sp_insertOrderItems]
END
GO

CREATE PROC [dbo].[sp_insertOrderItems]
(
	@v_orderId		INT,
	@v_productId	INT,
	@v_qty			INT
)
AS
BEGIN
	DECLARE
		@v_amount FLOAT,
		@v_price FLOAT

	-- Check For Order
	if NOT EXISTS 
	(
		SELECT 1 FROM orders WHERE order_id = @v_orderId AND isDeleted = 0
	)
	BEGIN
		THROW 60002, 'No such order found', 18
	END

	-- Check For Qty
	if(@v_qty <= 0)
	BEGIN
		THROW 60003, 'Invalid QTY', 18
	END

	-- Check For product
	if NOT EXISTS
	(
		SELECT 1 FROM products WHERE product_id = @v_productId AND isDeleted = 0
	)
	BEGIN
		THROW 60004, 'No such product Found', 18
	END

	SELECT @v_price = price  FROM products WHERE product_id = @v_productId AND isDeleted = 0

	SET @v_amount = @v_price * @v_qty

	INSERT INTO orderitems
	(
		order_id,
		product_id,
		qty,
		amount
	) VALUES 
	(
		@v_orderId,
		@v_productId,
		@v_qty,
		@v_amount
	)
END
GO

