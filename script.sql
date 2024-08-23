CREATE DATABASE DBSISTEMA_VENTA
GO

USE DBSISTEMA_VENTA
GO

CREATE TABLE ROL(
	IdRol int primary key identity,
	Descripcion varchar(50),
	FechaRegistro datetime default getdate()
)
GO

CREATE TABLE PERMISO(
	IdPermiso int primary key identity,
	IdRol int references ROL(IdRol),
	NombreMenu varchar(50),
	FechaRegistro datetime default getdate()
)
GO

CREATE TABLE PROVEEDOR(
	IdProveedor int primary key identity,
	Documento varchar(50),
	RazonSocial varchar(50),
	Correo varchar(50),
	Telefono varchar(50),
	Estado bit,
	FechaRegistro datetime default getdate()
)
GO

CREATE TABLE CLIENTE(
	IdCliente int primary key identity,
	Documento varchar(50),
	NombreCompleto varchar(50),
	Correo varchar(50),
	Telefono varchar(50),
	Estado bit,
	FechaRegistro datetime default getdate()
)
GO

CREATE TABLE USUARIO(
	IdUsuario int primary key identity,
	Documento varchar(50),
	NombreCompleto varchar(50),
	Correo varchar(50),
	Clave varchar(50),
	IdRol int references ROL(IdRol),
	Estado bit,
	FechaRegistro datetime default getdate()
)
GO

CREATE TABLE CATEGORIA(
	IdCategoria int primary key identity,
	Descripcion varchar(50),
	Estado bit,
	FechaRegistro datetime default getdate()
)
GO

CREATE TABLE PRODUCTO(
	IdProducto int primary key identity,
	Codigo varchar(50),
	Nombre varchar(50),
	Descripcion varchar(50),
	IdCategoria int references CATEGORIA(IdCategoria),
	Stock int not null default 0,
	PrecioCompra decimal(10,2) default 0,
	PrecioVenta decimal(10,2) default 0,
	Estado bit,
	FechaRegistro datetime default getdate()
)
GO

CREATE TABLE COMPRA(
	IdCompra int primary key identity,
	IdUsuario int references USUARIO(IdUsuario),
	IdProveedor int references PROVEEDOR(IdProveedor),
	TipoDocumento varchar(50),
	NumeroDocumento varchar(50),
	MontoTotal decimal(10,2),
	FechaRegistro datetime default getdate()
)
GO

CREATE TABLE DETALLE_COMPRA(
	IdDetalleCompra int primary key identity,
	IdCompra int references COMPRA(IdCompra),
	IdProducto int references PRODUCTO(IdProducto),
	PrecioCompra decimal(10,2) default 0,
	PrecioVenta decimal(10,2) default 0,
	Cantidad int,
	MontoTotal decimal(10,2),
	FechaRegistro datetime default getdate()
)
GO

CREATE TABLE VENTA(
	IdVenta int primary key identity,
	IdUsuario int references USUARIO(IdUsuario),
	TipoDocumento varchar(50),
	NumeroDocumento varchar(50),
	DocumentoCliente varchar(50),
	NombreCliente varchar(50),
	MontoPago decimal(10,2),
	MontoCambio decimal(10,2),
	MontoTotal decimal(10,2),
	FechaRegistro datetime default getdate()
)
GO

CREATE TABLE DETALLE_VENTA(
	IdDetalleVenta int primary key identity,
	IdVenta int references VENTA(IdVenta),
	IdProducto int references PRODUCTO(IdProducto),
	PrevioVenta decimal(10,2),
	Cantidad int,
	Subtotal decimal(10,2),
	FechaRegistro datetime default getdate()
)
GO