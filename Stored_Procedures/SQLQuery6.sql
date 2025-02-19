EXEC [dbo].[sp_getProducts];

EXEC [dbo].sp_getProductsById 10

SELECT * FROM orders WHERE order_id = 85;

SELECT * FROM orderitems;

SELECT company_id, company_name, isDeleted FROM companies 

-- 1015 => ONEPLUS
/*
UPDATE companies
SET company_name = 'DoCoMo'
WHERE company_id = 1015;
*/

SELECT * FROM products;


SELECT CASE
    WHEN EXISTS (
        SELECT 1
        FROM [products] AS [p]
        LEFT JOIN [companies] AS [c] ON [p].[company_id] = [c].[company_id]
        WHERE [p].[isDeleted] = CAST(0 AS bit) AND [c].[isDeleted] = CAST(0 AS bit)) THEN CAST(1 AS bit)
    ELSE CAST(0 AS bit)
END

SELECT * FROM products