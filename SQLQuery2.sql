use CRUD;

CREATE TABLE [dbo].[Products] (
    [ProductId]          UNIQUEIDENTIFIER NOT NULL,
    [ProductName]        NVARCHAR (100)   NULL,
    [Price]              DECIMAL (18)     NULL,
    [ProdcutDescription] NVARCHAR (MAX)   NULL,
    [CreatedOn]          DATETIME         NULL,
    [UpdateOn]           DATETIME         NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED ([ProductId] ASC)
);

select * from Products;

CREATE PROCEDURE Product_CRUD
    @ProductId UNIQUEIDENTIFIER ,
    @ProductName NVARCHAR(100) = NULL,
    @Price DECIMAL(18, 2) = NULL,
    @ProdcutDescription NVARCHAR(500) = NULL,
    @CreatedOn DATETIME = NULL,
    @UpdateOn DATETIME = NULL,
    @Action NVARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;

    IF @Action = 'Insert'
    BEGIN
        INSERT INTO Products (ProductId, ProductName, Price, ProdcutDescription, CreatedOn, UpdateOn)
        VALUES (@ProductId, @ProductName, @Price, @ProdcutDescription, GETDATE(), NULL);
    END
    ELSE IF @Action = 'Update'
    BEGIN
        UPDATE Products
        SET ProductName = @ProductName,
            Price = @Price,
            ProdcutDescription = @ProdcutDescription,
            UpdateOn = GETDATE()
        WHERE ProductId = @ProductId;
    END
    ELSE IF @Action = 'Delete'
    BEGIN
        DELETE FROM Products WHERE ProductId = @ProductId;
    END
    ELSE IF @Action = 'Select'
    BEGIN
        SELECT * FROM Products WHERE ProductId = @ProductId;
    END
    ELSE IF @Action = 'SelectAll'
    BEGIN
        SELECT * FROM Products;
    END
END