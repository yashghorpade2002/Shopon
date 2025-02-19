/*** Stored Procedure to insert order** Version: 1.0** Date: 11/13/2024** Dev: 68358** Ex: EXEC sp_insertOrder**  */ USE db_shopon;
GO

IF EXISTS
(
    SELECT 1 
    FROM sys.objects 
    WHERE type = 'P' 
    AND OBJECT_ID = OBJECT_ID('[dbo].[sp_insertOrder]')
)
BEGIN
    DROP PROCEDURE [dbo].[sp_insertOrder];
END;
GO

CREATE PROCEDURE [dbo].[sp_insertOrder]
(
    @v_customerId    INT = NULL,          -- Optional Parameter
    @v_aspnetUserId  NVARCHAR(450) = NULL, -- Optional Parameter
    @v_orderTotal    FLOAT,
    @v_orderId       INT OUT
)
AS
BEGIN
    -- Ensure that only one of the parameters is provided
    IF (@v_customerId IS NULL AND @v_aspnetUserId IS NULL)
    BEGIN
        THROW 50001, 'Both CustomerId and AspnetUserId cannot be null', 18;
    END
    IF (@v_customerId IS NOT NULL AND @v_aspnetUserId IS NOT NULL)
    BEGIN
        THROW 50002, 'Both CustomerId and AspnetUserId cannot be provided at the same time', 18;
    END

    -- Check for valid customer
    IF (@v_customerId IS NOT NULL AND NOT EXISTS (SELECT 1 FROM customers WHERE customer_id = @v_customerId AND isDeleted = 0))
    BEGIN
        THROW 50003, 'No such customer found or customer is deleted', 18;
    END

    -- Check for valid ASP.NET User
    IF (@v_aspnetUserId IS NOT NULL AND NOT EXISTS (SELECT 1 FROM AspNetUsers WHERE Id = @v_aspnetUserId))
    BEGIN
        THROW 50004, 'No such user found in AspNetUsers', 18;
    END

    -- Check for valid order total
    IF (@v_orderTotal <= 0)
    BEGIN
        THROW 50005, 'Invalid order total', 18;
    END

    -- Insert the order into the orders table
    INSERT INTO orders
    (
        orderstatus,
        order_date,
        customer_id,
        orderTotal,
        AspnetUserId
    )
    VALUES
    (
        'New',                         -- Default order status
        GETDATE(),                     -- Current date/time
        @v_customerId,                 -- Customer ID (can be NULL)
        @v_orderTotal,                 -- Total order amount
        @v_aspnetUserId                -- ASP.NET User ID (can be NULL)
    );

    -- Set the output parameter to the newly inserted order ID
    SET @v_orderId = @@IDENTITY;

END;
GO
	

	