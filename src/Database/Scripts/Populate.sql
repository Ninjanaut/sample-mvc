-- Insert sample customer
--------------------------------------------------------
insert into dbo.Customer (FirstName, LastName, CreatedOn)
values ('Han', 'Solo', getdate());

-- insert sample products
--------------------------------------------------------
insert into dbo.Product ([Name], Number, Price)
values 
('Blaster pistol', 123456789, 123),
('Blaster rifle',  234567890, 234);

-- Insert sample order with order items
--------------------------------------------------------
insert into dbo.[Order] (CustomerId, VoucherPercentageDiscount, CreatedOn)
values (1, 10, getdate());

insert into dbo.OrderItem(OrderId, ProductId, Quantity)
values (1, 1, 2), (1, 2, 1);