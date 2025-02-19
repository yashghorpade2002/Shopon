SELECT * FROM orderitems WHERE order_id =1

SELECT * FROM products WHERE product_id = 2

EXEC sp_insertOrderItems 1,2,-1

SELECT * FROM orders

SELECT * FROM orders ORDER BY order_id DESC;