-- SP REGISTRAR USUARIO
CREATE PROC SP_REGISTRARUSUARIO(
	@Documento VARCHAR(50),
	@NombreCompleto VARCHAR(50),
	@Correo VARCHAR(50),
	@Clave VARCHAR(50),
	@IdRol INT,
	@Estado BIT,
	@IdUsuarioResultado INT OUTPUT,
	@Mensaje VARCHAR(500) OUTPUT
)
AS
BEGIN
	SET @IdUsuarioResultado = 0
	SET @Mensaje = ''

	IF NOT EXISTS(SELECT * FROM USUARIO WHERE Documento = @Documento)
	BEGIN
		INSERT INTO USUARIO(Documento, NombreCompleto, Correo, Clave, IdRol, Estado)
		VALUES(@Documento, @NombreCompleto, @Correo, @Clave, @IdRol, @Estado)
		SET @IdUsuarioResultado = SCOPE_IDENTITY()
		
	END
	ELSE
		SET @Mensaje = 'No se puede repetir el documento para más de un usuario'
END
GO

-- Probando el store procedure con parametros de salida
DECLARE @IdUsuarioResultado INT
DECLARE @Mensaje VARCHAR(500)

EXEC SP_REGISTRARUSUARIO '1234', 'pruebas', 'test@gmail.com', '456', 2, 1, @IdUsuarioResultado OUTPUT, @Mensaje OUTPUT

SELECT @IdUsuarioResultado 'id'
SELECT @Mensaje 'mensaje'


-- SP ACTUALIZAR USUARIO
CREATE PROC SP_EDITARUSUARIO(
	@IdUsuario INT,
	@Documento VARCHAR(50),
	@NombreCompleto VARCHAR(50),
	@Correo VARCHAR(50),
	@Clave VARCHAR(50),
	@IdRol INT,
	@Estado INT,
	@Respuesta BIT OUTPUT,
	@Mensaje VARCHAR(500) OUTPUT
)
AS
BEGIN
	SET @Respuesta = 0
	SET @Mensaje = ''

	IF NOT EXISTS(SELECT * FROM USUARIO WHERE Documento = @Documento AND IdUsuario != @IdUsuario)
	BEGIN
		UPDATE USUARIO
		SET Documento = @Documento,
			NombreCompleto = @NombreCompleto,
			Correo = @Correo,
			Clave = @Clave,
			IdRol = @IdRol,
			Estado = @Estado
		WHERE IdUsuario = @IdUsuario
		SET @Respuesta = 1
	END
	ELSE
		SET @Mensaje = 'No se puede repetir el documento para más de un usuario'
END
GO

-- Probando el store procedure con parametros de salida
DECLARE @Respuesta BIT
DECLARE @Mensaje VARCHAR(500)
EXEC SP_EDITARUSUARIO 4, '12345', 'pruebas2', 'test43@gmail.com', '4567', 2, 1, @Respuesta OUTPUT, @Mensaje OUTPUT
SELECT @Respuesta 'respuesta'
SELECT @Mensaje 'mensaje'

SELECT * FROM USUARIO


-- SP ELIMINAR USUARIO
CREATE PROC SP_ELIMINARUSUARIO(
	@IdUsuario INT,
	@Respuesta BIT OUTPUT,
	@Mensaje VARCHAR(500) OUTPUT
)
AS
BEGIN
	SET @Respuesta = 0
	SET @Mensaje = ''
	DECLARE @PasoReglas BIT = 1

	IF EXISTS(SELECT * FROM COMPRA C INNER JOIN USUARIO U ON C.IdUsuario = U.IdUsuario WHERE U.IdUsuario = @IdUsuario)
	BEGIN
		SET @PasoReglas = 0
		SET @Respuesta = 0
		SET @Mensaje = 'No se puede eliminar porque tiene compras asociadas\n'
	END

	IF EXISTS(SELECT * FROM VENTA V INNER JOIN USUARIO U ON V.IdUsuario = U.IdUsuario WHERE U.IdUsuario = @IdUsuario)
	BEGIN
		SET @PasoReglas = 0
		SET @Respuesta = 0
		SET @Mensaje = @Mensaje + 'No se puede eliminar porque tiene ventas asociadas\n'
	END

	IF @PasoReglas = 1
	BEGIN
		IF EXISTS(SELECT * FROM USUARIO WHERE IdUsuario = @IdUsuario)
			BEGIN
				DELETE FROM USUARIO WHERE IdUsuario = @IdUsuario
				SET @Respuesta = 1
			END
			ELSE
				SET @Mensaje = 'El usuario no existe'
	END
END
GO

-- Probando el store procedure con parametros de salida
DECLARE @Respuesta BIT
DECLARE @Mensaje VARCHAR(500)
EXEC SP_ELIMINARUSUARIO 4, @Respuesta OUTPUT, @Mensaje OUTPUT
SELECT @Respuesta 'respuesta'
SELECT @Mensaje 'mensaje'

SELECT * FROM USUARIO