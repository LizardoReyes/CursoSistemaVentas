-- SP REGISTRAR CATEGORIA
CREATE PROC SP_REGISTRARCATEGORIA(
	@Descripcion VARCHAR(50),
	@Estado BIT,
	@Resultado INT OUTPUT,
	@Mensaje VARCHAR(500) OUTPUT
)
AS
BEGIN
	SET @Resultado = 0

	IF NOT EXISTS(SELECT * FROM CATEGORIA WHERE Descripcion = @Descripcion)
	BEGIN
		INSERT INTO CATEGORIA(Descripcion, Estado)
		VALUES(@Descripcion, @Estado)
		SET @Resultado = SCOPE_IDENTITY()
		
	END
	ELSE
		SET @Mensaje = 'No se puede repetir la descripcion de una categoria'
END
GO

-- SP EDITAR CATEGORIA
CREATE PROC SP_EDITARCATEGORIA(
	@IdCategoria INT,
	@Descripcion VARCHAR(50),
	@Estado BIT,
	@Resultado BIT OUTPUT,
	@Mensaje VARCHAR(500) OUTPUT
)
AS
BEGIN
	SET @Resultado = 1

	IF NOT EXISTS(SELECT * FROM CATEGORIA WHERE Descripcion = @Descripcion AND IdCategoria != @IdCategoria)
	BEGIN
		UPDATE CATEGORIA
		SET Descripcion = @Descripcion, Estado = @Estado
		WHERE IdCategoria = @IdCategoria
	END
	ELSE
		SET @Resultado = 0
		SET @Mensaje = 'No se puede repetir la descripcion de una categoria'
END
GO

-- SP ELIMINAR CATEGORIA
CREATE PROC SP_ELIMINARCATEGORIA(
	@IdCategoria INT,
	@Resultado BIT OUTPUT,
	@Mensaje VARCHAR(500) OUTPUT
)
AS
BEGIN
	SET @Resultado = 1

	IF NOT EXISTS(SELECT * FROM CATEGORIA C INNER JOIN PRODUCTO P ON C.IdCategoria = P.IdCategoria WHERE C.IdCategoria = @IdCategoria)
	BEGIN
		DELETE TOP(1) FROM CATEGORIA
		WHERE IdCategoria = @IdCategoria
	END
	ELSE
		SET @Resultado = 0
		SET @Mensaje = 'La categoria se encuentra asociada a un producto'
END
GO

-- DATOS DE PRUEBA
INSERT INTO CATEGORIA(Descripcion, Estado) VALUES('Lacteos', 1)
INSERT INTO CATEGORIA(Descripcion, Estado) VALUES('Carnes', 1)
INSERT INTO CATEGORIA(Descripcion, Estado) VALUES('Frutas', 1)
INSERT INTO CATEGORIA(Descripcion, Estado) VALUES('Verduras', 1)
INSERT INTO CATEGORIA(Descripcion, Estado) VALUES('Abarrotes', 1)
INSERT INTO CATEGORIA(Descripcion, Estado) VALUES('Bebidas', 1)
INSERT INTO CATEGORIA(Descripcion, Estado) VALUES('Limpieza', 1)
INSERT INTO CATEGORIA(Descripcion, Estado) VALUES('Higiene', 1)
INSERT INTO CATEGORIA(Descripcion, Estado) VALUES('Otros', 1)