CREATE TABLE [dbo].[Order]
(
	Id int identity not null,
    CustomerId int not null,
    VoucherPercentageDiscount int,
    CreatedOn DateTime not null,
    constraint PK_Order_Id primary key (Id),
    constraint FK_Order_CustomerId foreign key (CustomerId) references Customer (Id)
)
