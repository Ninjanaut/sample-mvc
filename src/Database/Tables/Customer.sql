CREATE TABLE [dbo].[Customer]
(
    Id int identity not null,
    FirstName nvarchar(255) not null,
    LastName nvarchar(255) not null,
    CreatedOn DateTime not null,
    constraint PK_Customer_Id primary key (Id)
)
