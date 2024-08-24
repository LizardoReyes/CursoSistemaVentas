USE DBSISTEMA_VENTA
GO

-- Creamos usuarios
INSERT INTO USUARIO(Documento, NombreCompleto, Correo, Clave,IdRol,	Estado)
VALUES ('101010', 'ADMIN', 'lizardo@gmail.com', '123', 1, 1)
INSERT INTO USUARIO(Documento, NombreCompleto, Correo, Clave,IdRol,	Estado)
VALUES ('20', 'EMPLEADO', 'empleado@gmail.com', '456', 2, 1)

-- Asignamos rol
INSERT INTO ROL(Descripcion) VALUES('ADMINISTRADOR')
INSERT INTO ROL(Descripcion) VALUES('EMPLEADO')

-- Asignamos permisos
INSERT INTO PERMISO(IdRol, NombreMenu) VALUES
(1, 'menuusuarios'),
(1, 'menumantenedor'),
(1, 'menuventas'),
(1, 'menucompras'),
(1, 'menuclientes'),
(1, 'menuproveedores'),
(1, 'menureportes'),
(1, 'menuacercade')

INSERT INTO PERMISO(IdRol, NombreMenu) VALUES
(2, 'menuventas'),
(2, 'menucompras'),
(2, 'menuclientes'),
(2, 'menuproveedores'),
(2, 'menuacercade')

-- Ver tablas
SELECT * FROM PERMISO
SELECT * FROM USUARIO
SELECT * FROM ROL

-- Ver permisos por id
SELECT P.IdRol, P.NombreMenu FROM PERMISO P
INNER JOIN ROL R
ON R.IdRol = P.IdRol
INNER JOIN USUARIO U
ON U.IdRol = R.IdRol
WHERE U.IdUsuario = 2