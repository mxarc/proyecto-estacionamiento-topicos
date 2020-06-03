CREATE DATABASE Estacionamiento
GO;
USE Estacionamiento;
CREATE TABLE Cajones
(
-- Clave del cajón, constraints clave única
-- y valor no mayor a 3 dígitos y que sea positivo
    Clave       INT NOT NULL
          CONSTRAINT PK_CAJONES
            PRIMARY KEY
        CONSTRAINT CHK_CLAVE_LEN
            CHECK (Clave >= 0 AND Clave <= 999),
-- Disponibilidad del cajón
    Ocupado  BIT NOT NULL DEFAULT 0,
-- Descripción del cajón
    Descripcion TEXT
)
GO
CREATE TABLE RegistroEntradas
(
    ID            INT          NOT NULL IDENTITY (1, 1) -- identidad, incremento 1 en 1
-- ID Primario para ENTRADAS
        CONSTRAINT PK_ENTRADAS
            PRIMARY KEY,
-- Datos de registro para entrada y salida
    CodigoEntrada NVARCHAR(32) NOT NULL,
    MatriculaAuto NVARCHAR(8)  NOT NULL,
    HoraEntrada   DATETIME     DEFAULT GETDATE(),
-- Llave foranea del cajón
    CajonID       INT          NOT NULL
        CONSTRAINT FK_ENTRADAS_CAJONES
            REFERENCES Cajones
)
GO

CREATE TABLE Propietarios
(
    ID       INT          NOT NULL IDENTITY (1, 1)
-- ID Primario para Propietarios
        CONSTRAINT PK_PROPIETARIO
            PRIMARY KEY,
    Nombre   nvarchar(32) NOT NULL,
    Apellido nvarchar(32) NOT NULL,
)
GO
CREATE TABLE Pensiones
(
    ID               INT          NOT NULL IDENTITY (1, 1)
-- ID Primario para PENSIONES
        CONSTRAINT PK_PENSIONES
            PRIMARY KEY,
-- Datos de pensiones
    MatriculaAuto    nvarchar(8)  NOT NULL,
    ModeloAuto       nvarchar(32) NOT NULL,
    FechaAlta        DATETIME     NOT NULL,
    FechaVencimiento DATETIME     NOT NULL,
    CuotaPago        int DEFAULT 0,
-- ID propietario
    PropietarioID    INT          NOT NULL
        CONSTRAINT FK_PROPIETARIO_PENSIONES
            REFERENCES Propietarios,
-- Referencia al cajón de estacionamiento
    CajonID          INT          NOT NULL
        CONSTRAINT FK_CAJON_PENSIONES
            REFERENCES Cajones
)
GO

SELECT CajonID FROM RegistroEntradas WHERE CodigoEntrada='FQZPGRBQ'
SELECT DiffMinutos=DATEDIFF(minute, HoraEntrada, GETDATE()) FROM RegistroEntradas WHERE CodigoEntrada='FQZPGRBQ'
DELETE FROM RegistroEntradas WHERE MatriculaAuto


SELECT MatriculaAuto, ModeloAuto, FechaAlta, FechaVencimiento, CuotaPago, Nombre, Apellido, CajonID
FROM Pensiones INNER JOIN Propietarios P on Pensiones.PropietarioID = P.ID