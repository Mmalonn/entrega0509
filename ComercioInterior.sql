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
	nombre varchar(50)
	constraint pk_formasdepago primary key(id)
)

create table Facturas(
	nroFactura int identity (1,1),
	fecha date,
	idForma int,
	cliente varchar(50),
	constraint pk_factura primary key (nroFactura),
	constraint fk_factura_forma foreign key(idForma)
	references FormasDePago(id)
)

create table DetallesFactura(
	idDetalle int,
	idFactura int,
	idArticulo int,
	cantidad int
	constraint pk_detallesFactura primary key (idDetalle, idFactura),
	constraint fk_detallesFactura_factura foreign key (idFactura)
	references Facturas(nroFactura),
	constraint fk_detallesFactura_articulo foreign key (idArticulo)
	references Articulos(id)	
)
go

CREATE PROCEDURE SP_GUARDAR_ARTICULO
@id int,
@nombre varchar(20),
@precioUnitario int
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

CREATE PROCEDURE SP_INSERTAR_DETALLE
	@nroFactura int,
	@idDetalle int,
	@idArticulo int,
	@cantidad int
AS
BEGIN
	INSERT INTO DetallesFactura(idDetalle, idFactura, idArticulo, cantidad) VALUES (@idDetalle, @nroFactura, @idArticulo, @cantidad);	
END
go

CREATE PROCEDURE SP_INSERTAR_FACTURA
	@cliente varchar(50),
	@fecha date,
	@idForma int,
	@id int output
AS
BEGIN
	INSERT INTO Facturas(cliente, fecha, idForma ) VALUES (@cliente, GETDATE(), @idForma);
	SET @id = SCOPE_IDENTITY();
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
	  INNER JOIN DetallesFactura df ON df.idDetalle =f.nroFactura
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

CREATE PROCEDURE SP_RECUPERAR_ARTICULOS
AS
BEGIN
	SELECT * FROM Articulos
END
GO

CREATE PROCEDURE SP_REGISTRAR_BAJA_ARTICULO
	@id int 
AS
BEGIN
	UPDATE Articulos SET estaActivo = 0 WHERE id = @id;
END
GO
