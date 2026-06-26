-- BUG FIXES:
--   'CREATE TAABLE' -> 'CREATE TABLE'
--   'DECIML(10,2)'  -> 'DECIMAL(10,2)'
--   'REFRENCES'     -> 'REFERENCES'
--
-- NOTE: The running app uses SQLite via EF Core and seeds this same data
-- automatically on startup (see Data/AppDbContext.cs). This script is kept
-- as the reference schema and works on standard SQL databases.

CREATE TABLE Invoices (
    InvoiceID INT PRIMARY KEY,
    CustomerName VARCHAR(100)
);

CREATE TABLE InvoiceItems (
    ItemID INT PRIMARY KEY,
    InvoiceID INT,
    Name VARCHAR(100),
    Price DECIMAL(10,2),
    FOREIGN KEY (InvoiceID) REFERENCES Invoices(InvoiceID)
);

INSERT INTO Invoices (InvoiceID, CustomerName) VALUES (1, 'John Doe');
INSERT INTO InvoiceItems (ItemID, InvoiceID, Name, Price) VALUES (1, 1, 'Widget A', 19.99);
INSERT INTO InvoiceItems (ItemID, InvoiceID, Name, Price) VALUES (2, 1, 'Widget B', 29.50);
INSERT INTO InvoiceItems (ItemID, InvoiceID, Name, Price) VALUES (3, 1, 'Service Fee', 9.99);
