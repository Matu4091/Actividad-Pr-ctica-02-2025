CREATE DATABASE PRACTICA_01_2025;
GO

USE PRACTICA_01_2025;
GO

CREATE TABLE Libros(
isbn varchar(50),
titulo varchar(100) not null,
autor varchar(100) not null,
nro_paginas int not null,
stock int not null,
precio decimal(12,2) not null,
fecha_baja date null,
CONSTRAINT PK_Libros PRIMARY KEY (isbn));
GO

CREATE TABLE Formas_Pagos(
id_forma_pago int IDENTITY(1,1),
forma_pago varchar(100) not null,
CONSTRAINT PK_Formas_Pagos PRIMARY KEY (id_forma_pago));
GO

CREATE TABLE Facturas(
nro_factura int IDENTITY(1,1),
fecha date not null,
id_forma_pago int not null,
cliente varchar(100),
CONSTRAINT PK_Facturas PRIMARY KEY (nro_factura),
CONSTRAINT FK_Facturas_Formas_Pagos FOREIGN KEY (id_forma_pago)
REFERENCES Formas_Pagos (id_forma_pago));
GO

CREATE TABLE Detalles_Facturas(
id_detalle_factura int IDENTITY(1,1),
isbn varchar(50) not null,
cantidad int not null,
nro_factura int not null,
CONSTRAINT PK_Detalles_Facturas PRIMARY KEY (id_detalle_factura),
CONSTRAINT FK_Detalles_Facturas_Libros FOREIGN KEY (isbn)
REFERENCES Libros (isbn),
CONSTRAINT FK_Detalles_Facturas_Facturas FOREIGN KEY (nro_factura)
REFERENCES Facturas (nro_factura));
GO

INSERT INTO Formas_Pagos (forma_pago)
VALUES ('Debito'),
       ('Credito'),
       ('Efectivo'),
       ('Transferencia');
GO

CREATE PROCEDURE OBTENER_LIBROS
AS
BEGIN
    SELECT * FROM Libros
END
GO

CREATE PROCEDURE OBTENER_LIBRO_X_ISBN
@isbn varchar(50)
AS
BEGIN
    SELECT * FROM Libros
	WHERE isbn = @isbn
END
GO

CREATE PROCEDURE MODIFICAR_LIBROS
@isbn varchar(50) = '',
@titulo varchar(100),
@autor varchar(100),
@nro_paginas int,
@stock int,
@precio decimal(12,2)
AS 
BEGIN 
	IF NOT EXISTS (SELECT 1 FROM Libros WHERE @isbn = isbn)
	BEGIN
	     INSERT INTO Libros (isbn, titulo, autor, nro_paginas, stock, precio)
	     VALUES (@isbn, @titulo, @autor, @nro_paginas, @stock, @precio)
	END
	ELSE
	BEGIN	    
        UPDATE Libros
	    SET titulo = @titulo, autor = @autor, nro_paginas = @nro_paginas, stock = @stock, precio = @precio
	    WHERE isbn = @isbn
    END
END
GO

CREATE PROCEDURE DAR_BAJA_LIBRO
@isbn varchar(50)
AS
BEGIN
    UPDATE Libros 
	SET fecha_baja = GETDATE()
	WHERE isbn = @isbn
END	
GO

CREATE PROCEDURE OBTENER_FACTURAS
AS
BEGIN 
    SELECT * FROM Facturas f
    JOIN Formas_Pagos fp ON fp.id_forma_pago = f.id_forma_pago
END
GO

CREATE PROCEDURE OBTENER_FACTURA_X_ID
@nro_factura int
AS
BEGIN
    SELECT * FROM Facturas WHERE nro_factura = @nro_factura
END
GO

CREATE PROCEDURE MODIFICAR_FACTURAS
@nro_factura int = 0,
@fecha date, 
@id_forma_pago int,
@cliente varchar(100),
@new_id int OUTPUT
AS
BEGIN
    IF NOT EXISTS (SELECT 1 FROM Facturas WHERE nro_factura = @nro_factura)
	BEGIN
	    INSERT INTO Facturas (fecha, id_forma_pago, cliente)
	    VALUES (@fecha, @id_forma_pago, @cliente)

		SET @new_id = SCOPE_IDENTITY()
	END
	ELSE
	BEGIN
	    UPDATE Facturas
		SET fecha = @fecha, id_forma_pago = @id_forma_pago, cliente = @cliente
		WHERE nro_factura = @nro_factura

		SET @new_id = @nro_factura
	END
END	
GO

CREATE PROCEDURE AGREGAR_DETALLE
@isbn varchar(50),
@cantidad int,
@nro_factura int
AS
BEGIN 
    INSERT INTO Detalles_Facturas (isbn, cantidad, nro_factura)
	VALUES (@isbn, @cantidad, @nro_factura)
END
GO

CREATE PROCEDURE ELIMINAR_FACTURA
@nro_factura int
AS
BEGIN 
    BEGIN TRY
	    BEGIN TRANSACTION;

		DELETE Detalles_Facturas
	    WHERE nro_factura = @nro_factura
	    DELETE Facturas 
	    WHERE nro_factura = @nro_factura

		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
	    ROLLBACK TRANSACTION;
		THROW;
    END CATCH
END
GO

CREATE PROCEDURE OBTENER_METODOS_PAGOS
AS
BEGIN
    SELECT * FROM Formas_Pagos
END
GO

CREATE PROCEDURE OBTENER_DETALLES_FACTURAS
@nro_factura int
AS
BEGIN
    SELECT * FROM Detalles_Facturas df
    JOIN Libros l ON l.isbn = df.isbn
    WHERE df.nro_factura = @nro_factura
END
GO

CREATE PROCEDURE OBTENER_ULTIMA_FACTURA
AS
BEGIN
    SELECT TOP 1 nro_factura FROM Facturas ORDER BY nro_factura DESC
END
GO
