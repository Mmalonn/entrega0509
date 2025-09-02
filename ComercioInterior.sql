create database comercio_interior
go
use comercio_interior
go

create table Articulos(
	id int identity (1,1),
	nombre varchar(50),
	precioUnitario decimal(10,2)
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
	idCantidad int
	constraint pk_detallesFactura primary key (idDetalle, idFactura),
	constraint fk_detallesFactura_factura foreign key (idFactura)
	references Facturas(nroFactura),
	constraint fk_detallesFactura_articulo foreign key (idArticulo)
	references Articulos(id)	
)

