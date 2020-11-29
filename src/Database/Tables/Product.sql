CREATE TABLE [dbo].[Product]
(
    Id int identity not null,
    Number int not null,
    [Name] nvarchar(255) not null,
    Price decimal(10,2) not null,
    constraint PK_Product_Id primary key (Id),
)
