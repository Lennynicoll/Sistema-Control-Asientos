IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'SistemaDeControlAsientosDB')
BEGIN
    CREATE DATABASE SistemaDeControlAsientosDB;
END
GO

USE SistemaDeControlAsientosDB;
GO

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Estudiantes')
BEGIN
    CREATE TABLE Estudiantes (
        EstudianteId INT IDENTITY(1,1) PRIMARY KEY,
        Matricula NVARCHAR(20) NOT NULL UNIQUE,
        Nombre NVARCHAR(100) NOT NULL,
        Carrera NVARCHAR(100) NOT NULL,
        FechaRegistro DATETIME DEFAULT GETDATE()
    );
END
GO

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Asientos')
BEGIN
    CREATE TABLE Asientos (
        AsientoId INT IDENTITY(1,1) PRIMARY KEY,
        Fila NVARCHAR(5) NOT NULL,
        Numero INT NOT NULL,
        Estado NVARCHAR(20) DEFAULT 'Disponible',
        CONSTRAINT UQ_Asiento_Fila_Numero UNIQUE (Fila, Numero)
    );
END
GO

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Asignaciones')
BEGIN
    CREATE TABLE Asignaciones (
        AsignacionId INT IDENTITY(1,1) PRIMARY KEY,
        EstudianteId INT NOT NULL,
        AsientoId INT NOT NULL UNIQUE,
        FechaAsignacion DATETIME DEFAULT GETDATE(),
        CONSTRAINT FK_Asignaciones_Estudiantes FOREIGN KEY (EstudianteId) REFERENCES Estudiantes(EstudianteId),
        CONSTRAINT FK_Asignaciones_Asientos FOREIGN KEY (AsientoId) REFERENCES Asientos(AsientoId)
    );
END
GO

IF NOT EXISTS (SELECT 1 FROM Asientos)
BEGIN
    INSERT INTO Asientos (Fila, Numero) VALUES 
    ('A', 1), ('A', 2), ('A', 3),
    ('B', 1), ('B', 2), ('B', 3),
    ('C', 1), ('C', 2), ('C', 3);
END
GO
