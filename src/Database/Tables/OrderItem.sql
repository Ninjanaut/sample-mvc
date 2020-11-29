CREATE TABLE [dbo].[OrderItem]
(
    OrderId int not null,
    ProductId int not null,
    Quantity int not null,
    constraint PK_OrderItem_Id primary key (OrderId, ProductId),
    constraint FK_OrderItem_OrderId foreign key (OrderId) references [Order] (Id),
    constraint FK_OrderItem_ProductId foreign key (ProductId) references Product (Id)
)
