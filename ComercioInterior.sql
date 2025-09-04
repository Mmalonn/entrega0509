use master
go
drop database comercio_interior
go
create database comercio_interior
go
use comercio_interior
go

create table Articulos(
	id int identity (1,1),
	nombre varchar(50),
	precioUnitario decimal(10,2),
	estaActivo bit
	constraint pk_articulos primary key(id)
)

create table FormasDePago(
	id int identity (1,1),
	nombre varchar(50),
	estaActivo bit
	constraint pk_formasdepago primary key(id)
)

create table Facturas(
	nroFactura int identity (1,1),
	fecha date,
	idForma int,
	cliente varchar(50),
	estaActivo bit
	constraint pk_factura primary key (nroFactura),
	constraint fk_factura_forma foreign key(idForma)
	references FormasDePago(id)
)

create table DetallesFactura(
	idDetalle int,
	idFactura int,
	idArticulo int,
	cantidad int,
	estaActivo bit
	constraint pk_detallesFactura primary key (idDetalle, idFactura),
	constraint fk_detallesFactura_factura foreign key (idFactura)
	references Facturas(nroFactura),
	constraint fk_detallesFactura_articulo foreign key (idArticulo)
	references Articulos(id)	
)
go

CREATE PROCEDURE SP_GUARDAR_ARTICULO
@id int,
@nombre varchar(50),
@precioUnitario decimal(10,2),
@estaActivo bit
AS
BEGIN 
	IF @id = 0
		BEGIN
			insert into ARTICULOS (nombre, precioUnitario, estaActivo) 
			values (@nombre,@precioUnitario,@estaActivo)	
		END
	ELSE
		BEGIN
			update ARTICULOS 
			set nombre= @nombre, precioUnitario= @precioUnitario, estaActivo= @estaActivo 
			where id=@id
		END
END
go

CREATE PROCEDURE SP_GUARDAR_FORMAPAGO
	@id int,
	@nombre varchar(50),
	@estaActivo bit
AS
BEGIN
	IF @id = 0
		BEGIN
			INSERT INTO FormasDePago(nombre, estaActivo) VALUES (@nombre, @estaActivo);	
		END
	ELSE
		BEGIN
			UPDATE FormasDePago SET nombre = @nombre, estaActivo = @estaActivo WHERE id = @id;	
		END
END
GO

CREATE PROCEDURE SP_INSERTAR_DETALLE
	@nroFactura int,
	@idDetalle int,
	@idArticulo int,
	@cantidad int,
	@estaActivo bit
AS
BEGIN
	INSERT INTO DetallesFactura(idDetalle, idFactura, idArticulo, cantidad, estaActivo) VALUES (@idDetalle, @nroFactura, @idArticulo, @cantidad, @estaActivo);	
END
go

CREATE PROCEDURE SP_INSERTAR_FACTURA
	@cliente varchar(50),
	@idForma int,
	@estaActivo bit,
	@nroFactura int output
AS
BEGIN
	INSERT INTO Facturas(cliente, fecha, idForma , estaActivo) VALUES (@cliente, GETDATE(), @idForma, @estaActivo);
	SET @nroFactura = SCOPE_IDENTITY();
END
GO

CREATE PROCEDURE SP_RECUPERAR_FACTURA_POR_ID
	@id int
AS
BEGIN
	SELECT f.*, df.cantidad, a.*
	  FROM Facturas f
	  INNER JOIN DetallesFactura df ON f.nroFactura = df.idFactura
	  INNER JOIN Articulos a ON df.idArticulo = a.id
	  WHERE f.nroFactura = @id;
END
GO

CREATE PROCEDURE SP_RECUPERAR_FACTURAS
AS
BEGIN
	SELECT f.*, df.cantidad, a.*
	  FROM Facturas f
	  INNER JOIN DetallesFactura df ON df.idFactura =f.nroFactura
	  INNER JOIN Articulos a ON a.id = df.idArticulo
	  ORDER BY f.nroFactura;
END
GO

CREATE PROCEDURE SP_RECUPERAR_ARTICULOS_POR_CODIGO
	@id int
AS
BEGIN
	SELECT * FROM Articulos WHERE id = @id;
END
GO

CREATE PROCEDURE SP_RECUPERAR_FORMAPAGO_POR_CODIGO
	@id int
AS
BEGIN
	SELECT * FROM FormasDePago WHERE id = @id;
END
GO

CREATE PROCEDURE SP_RECUPERAR_ARTICULOS
AS
BEGIN
	SELECT * FROM Articulos
END
GO

CREATE PROCEDURE SP_RECUPERAR_FORMASPAGO
AS
BEGIN
	SELECT * FROM FormasDePago
END
GO

CREATE PROCEDURE SP_REGISTRAR_BAJA_ARTICULO
	@id int 
AS
BEGIN
	UPDATE Articulos SET estaActivo = 0 WHERE id = @id;
END
GO

CREATE PROCEDURE SP_REGISTRAR_BAJA_FORMAPAGO
	@id int 
AS
BEGIN
	UPDATE FormasDePago SET estaActivo = 0 WHERE id = @id;
END
GO

INSERT INTO FormasDePago (nombre, estaActivo) VALUES ('Efectivo',1);
INSERT INTO FormasDePago (nombre, estaActivo) VALUES ('Tarjeta de Credito',1);
INSERT INTO FormasDePago (nombre, estaActivo) VALUES ('Transferencia Bancaria',1);
INSERT INTO FormasDePago (nombre, estaActivo) VALUES ('Cheque',1);
INSERT INTO FormasDePago (nombre, estaActivo) VALUES ('Debito',1);
INSERT INTO FormasDePago (nombre, estaActivo) VALUES ('PayPal',1);
GO

INSERT INTO Articulos (nombre, precioUnitario, estaActivo) VALUES ('Cuaderno A4 rayado', 2.50, 1);
INSERT INTO Articulos (nombre, precioUnitario, estaActivo) VALUES ('Boligrafo azul', 0.75, 1);
INSERT INTO Articulos (nombre, precioUnitario, estaActivo) VALUES ('Carpeta de anillas', 3.20, 1);
INSERT INTO Articulos (nombre, precioUnitario, estaActivo) VALUES ('Lapiz grafito HB', 0.50, 1);
INSERT INTO Articulos (nombre, precioUnitario, estaActivo) VALUES ('Caja de lapices de colores', 6.00, 1);
INSERT INTO Articulos (nombre, precioUnitario, estaActivo) VALUES ('Sacapuntas metalico', 1.00, 1);
INSERT INTO Articulos (nombre, precioUnitario, estaActivo) VALUES ('Goma de borrar', 0.85, 1);
INSERT INTO Articulos (nombre, precioUnitario, estaActivo) VALUES ('Marcador negro', 1.50, 1); 
INSERT INTO Articulos (nombre, precioUnitario, estaActivo) VALUES ('Set de reglas (20cm)', 2.10, 1);
INSERT INTO Articulos (nombre, precioUnitario, estaActivo) VALUES ('Plumones de colores', 8.50, 1);
INSERT INTO Articulos (nombre, precioUnitario, estaActivo) VALUES ('Tijera escolar', 2.75, 1);
INSERT INTO Articulos (nombre, precioUnitario, estaActivo) VALUES ('Corrector liquido', 1.25, 1);
GO

INSERT INTO Facturas (fecha, idForma, cliente, estaActivo) VALUES (GETDATE(), 1, 'Marcos Torres', 1);
INSERT INTO Facturas (fecha, idForma, cliente, estaActivo) VALUES (GETDATE(), 2, 'Ana Gomez', 1);
INSERT INTO Facturas (fecha, idForma, cliente, estaActivo) VALUES (GETDATE(), 3, 'Carlos Perez', 1);
INSERT INTO Facturas (fecha, idForma, cliente, estaActivo) VALUES (GETDATE(), 1, 'Laura Fernandez', 1);
INSERT INTO Facturas (fecha, idForma, cliente, estaActivo) VALUES (GETDATE(), 5, 'Javier Rodriguez', 1);
INSERT INTO Facturas (fecha, idForma, cliente, estaActivo) VALUES (GETDATE(), 2, 'Maria Lopez', 1);
INSERT INTO Facturas (fecha, idForma, cliente, estaActivo) VALUES (GETDATE(), 3, 'Roberto Sanchez', 1);
INSERT INTO Facturas (fecha, idForma, cliente, estaActivo) VALUES (GETDATE(), 1, 'Camila Diaz', 1);
INSERT INTO Facturas (fecha, idForma, cliente, estaActivo) VALUES (GETDATE(), 4, 'Andres Garcia', 1);
INSERT INTO Facturas (fecha, idForma, cliente, estaActivo) VALUES (GETDATE(), 5, 'Sofia Ramirez', 1);
INSERT INTO Facturas (fecha, idForma, cliente, estaActivo) VALUES (GETDATE(), 2, 'Pedro Fernandez', 1);
GO

INSERT INTO DetallesFactura (idDetalle, idFactura, idArticulo, cantidad, estaActivo) VALUES (1, 1, 1, 1, 1);
INSERT INTO DetallesFactura (idDetalle, idFactura, idArticulo, cantidad, estaActivo) VALUES (2, 1, 3, 2, 1);
INSERT INTO DetallesFactura (idDetalle, idFactura, idArticulo, cantidad, estaActivo) VALUES (1, 2, 2, 1, 1);
INSERT INTO DetallesFactura (idDetalle, idFactura, idArticulo, cantidad, estaActivo) VALUES (1, 3, 4, 1, 1);
INSERT INTO DetallesFactura (idDetalle, idFactura, idArticulo, cantidad, estaActivo) VALUES (2, 3, 5, 1, 1);
INSERT INTO DetallesFactura (idDetalle, idFactura, idArticulo, cantidad, estaActivo) VALUES (1, 4, 6, 1, 1);
INSERT INTO DetallesFactura (idDetalle, idFactura, idArticulo, cantidad, estaActivo) VALUES (1, 5, 7, 3, 1);
INSERT INTO DetallesFactura (idDetalle, idFactura, idArticulo, cantidad, estaActivo) VALUES (1, 6, 9, 1, 1);
INSERT INTO DetallesFactura (idDetalle, idFactura, idArticulo, cantidad, estaActivo) VALUES (1, 7, 10, 1, 1);
INSERT INTO DetallesFactura (idDetalle, idFactura, idArticulo, cantidad, estaActivo) VALUES (2, 7, 1, 1, 1);
INSERT INTO DetallesFactura (idDetalle, idFactura, idArticulo, cantidad, estaActivo) VALUES (1, 8, 11, 2, 1);
INSERT INTO DetallesFactura (idDetalle, idFactura, idArticulo, cantidad, estaActivo) VALUES (1, 9, 12, 1, 1);
INSERT INTO DetallesFactura (idDetalle, idFactura, idArticulo, cantidad, estaActivo) VALUES (2, 9, 2, 1, 1);
INSERT INTO DetallesFactura (idDetalle, idFactura, idArticulo, cantidad, estaActivo) VALUES (1, 10, 5, 1, 1);
INSERT INTO DetallesFactura (idDetalle, idFactura, idArticulo, cantidad, estaActivo) VALUES (1, 11, 10, 1, 1);
INSERT INTO DetallesFactura (idDetalle, idFactura, idArticulo, cantidad, estaActivo) VALUES (3, 1, 5, 2, 1);
INSERT INTO DetallesFactura (idDetalle, idFactura, idArticulo, cantidad, estaActivo) VALUES (4, 1, 6, 1, 1);
INSERT INTO DetallesFactura (idDetalle, idFactura, idArticulo, cantidad, estaActivo) VALUES (2, 2, 7, 3, 1);
INSERT INTO DetallesFactura (idDetalle, idFactura, idArticulo, cantidad, estaActivo) VALUES (3, 2, 8, 1, 1);
INSERT INTO DetallesFactura (idDetalle, idFactura, idArticulo, cantidad, estaActivo) VALUES (3, 3, 9, 2, 1);
INSERT INTO DetallesFactura (idDetalle, idFactura, idArticulo, cantidad, estaActivo) VALUES (2, 4, 10, 1, 1);
INSERT INTO DetallesFactura (idDetalle, idFactura, idArticulo, cantidad, estaActivo) VALUES (3, 4, 11, 1, 1);
INSERT INTO DetallesFactura (idDetalle, idFactura, idArticulo, cantidad, estaActivo) VALUES (2, 5, 12, 1, 1);
INSERT INTO DetallesFactura (idDetalle, idFactura, idArticulo, cantidad, estaActivo) VALUES (3, 5, 1, 1, 1);
INSERT INTO DetallesFactura (idDetalle, idFactura, idArticulo, cantidad, estaActivo) VALUES (4, 5, 2, 1, 1);
INSERT INTO DetallesFactura (idDetalle, idFactura, idArticulo, cantidad, estaActivo) VALUES (2, 6, 3, 2, 1);
INSERT INTO DetallesFactura (idDetalle, idFactura, idArticulo, cantidad, estaActivo) VALUES (3, 7, 4, 1, 1);
INSERT INTO DetallesFactura (idDetalle, idFactura, idArticulo, cantidad, estaActivo) VALUES (4, 7, 5, 1, 1);
INSERT INTO DetallesFactura (idDetalle, idFactura, idArticulo, cantidad, estaActivo) VALUES (2, 8, 6, 1, 1);
INSERT INTO DetallesFactura (idDetalle, idFactura, idArticulo, cantidad, estaActivo) VALUES (3, 8, 7, 1, 1);
INSERT INTO DetallesFactura (idDetalle, idFactura, idArticulo, cantidad, estaActivo) VALUES (3, 9, 8, 1, 1);
INSERT INTO DetallesFactura (idDetalle, idFactura, idArticulo, cantidad, estaActivo) VALUES (4, 9, 9, 1, 1);
INSERT INTO DetallesFactura (idDetalle, idFactura, idArticulo, cantidad, estaActivo) VALUES (2, 10, 10, 1, 1);
INSERT INTO DetallesFactura (idDetalle, idFactura, idArticulo, cantidad, estaActivo) VALUES (2, 11, 11, 1, 1);
INSERT INTO DetallesFactura (idDetalle, idFactura, idArticulo, cantidad, estaActivo) VALUES (3, 11, 12, 1, 1);
GO

select * from articulos
select * from FormasDePago

select * from facturas
