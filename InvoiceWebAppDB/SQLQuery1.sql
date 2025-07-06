USE InvoiceWebAppDB;

CREATE TABLE Users(
	Id INT PRIMARY KEY IDENTITY(1,1),
	FullName NVARCHAR (100),
	UserName NVARCHAR (50),
	Password NVARCHAR (255),
	Email NVARCHAR(100),
	Phone NVARCHAR(20),
	IsVerified Bit
);

CREATE TABLE Invoices (
	Id INT PRIMARY KEY IDENTITY(1,1),
	TotalAmount DECIMAL(10,2),
	InvoiceDate DATETIME DEFAULT GETDATE()

);

CREATE TABLE InvoiceItems(
	Id INT PRIMARY KEY IDENTITY(1,1),
	InvoiceId INT FOREIGN KEY REFERENCES Invoices(Id) ON DELETE CASCADE , 
	ProductName NVARCHAR(100),
	Quantity INT ,
	UnitPrice DECIMAL(10,2)
);

CREATE TYPE InvoiceItemType AS TABLE
(
    ProductName NVARCHAR(100),
    Quantity INT,
    UnitPrice DECIMAL(10, 2)
);

CREATE PROCEDURE sp_AddInvoiceWithItems
    @TotalAmount DECIMAL(10,2),
    @InvoiceDate DATETIME,
    @Items InvoiceItemType READONLY
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Invoices (TotalAmount, InvoiceDate)
    VALUES (@TotalAmount, @InvoiceDate);

    DECLARE @InvoiceId INT = SCOPE_IDENTITY();

    INSERT INTO InvoiceItems (InvoiceId, ProductName, Quantity, UnitPrice)
    SELECT @InvoiceId, ProductName, Quantity, UnitPrice
    FROM @Items;
END


CREATE VIEW vw_InvoiceSummary AS
SELECT 
    I.Id ,
    I.InvoiceDate,
    SUM(II.Quantity * II.UnitPrice) AS TotalAmount
FROM Invoices I
JOIN InvoiceItems II ON I.Id = II.InvoiceId
GROUP BY I.Id, I.InvoiceDate

