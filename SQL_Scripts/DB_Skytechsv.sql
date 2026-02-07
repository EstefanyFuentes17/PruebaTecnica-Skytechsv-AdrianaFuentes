/* PRUEBA TÉCNICA - SKYTECHSV
   Autor: Adriana Fuentes
   Descripción: Setup de BD, Tabla de Usuarios y Procedimientos Almacenados Requeridos
*/

USE master;
GO

-- 1. CREACIÓN DE LA BASE DE DATOS
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'DB_Skytechsv')
BEGIN
    CREATE DATABASE DB_Skytechsv;
END
GO

USE DB_Skytechsv;
GO

-- 2. CREACIÓN DE LA TABLA
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Usuarios]') AND type in (N'U'))
BEGIN
    CREATE TABLE Usuarios (
        Id INT PRIMARY KEY IDENTITY(1,1),
        Username VARCHAR(50) UNIQUE NOT NULL,
        Password VARCHAR(100) NOT NULL,
        NombreCompleto VARCHAR(100) NOT NULL,
        Estado BIT DEFAULT 1,
        FechaCreacion DATETIME DEFAULT GETDATE()
    );
END
GO

-- 3. PROCEDIMIENTOS ALMACENADOS 

-- A. VALIDAR LOGIN (C# ASP.NET Framework MVC)
CREATE OR ALTER PROCEDURE sp_ValidarUsuario
    @Username VARCHAR(50),
    @Password VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Usuarios WHERE Username = @Username AND Password = @Password AND Estado = 1)
        BEGIN
            RAISERROR('Credenciales incorrectas o cuenta desactivada.', 16, 1);
            RETURN;
        END

        SELECT Id, Username, NombreCompleto FROM Usuarios 
        WHERE Username = @Username AND Password = @Password;
    END TRY
    BEGIN CATCH
        DECLARE @ErrLogin NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(@ErrLogin, 16, 1);
    END CATCH
END
GO

-- B. LISTAR USUARIOS (VB.NET CRUD)
CREATE OR ALTER PROCEDURE sp_ListarUsuarios
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id, Username, NombreCompleto, Estado, FechaCreacion 
    FROM Usuarios ORDER BY FechaCreacion DESC;
END
GO

-- C. INSERTAR USUARIO (VB.NET CRUD)
CREATE OR ALTER PROCEDURE sp_InsertarUsuario
    @Username VARCHAR(50),
    @Password VARCHAR(100),
    @NombreCompleto VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION
            IF EXISTS (SELECT 1 FROM Usuarios WHERE Username = @Username)
            BEGIN
                RAISERROR('El nombre de usuario ya está registrado.', 16, 1);
            END

            INSERT INTO Usuarios (Username, Password, NombreCompleto)
            VALUES (@Username, @Password, @NombreCompleto);
        COMMIT TRANSACTION
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        DECLARE @ErrIns NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(@ErrIns, 16, 1);
    END CATCH
END
GO

-- D. ACTUALIZAR USUARIO (VB.NET CRUD)
CREATE OR ALTER PROCEDURE sp_ActualizarUsuario
    @Id INT,
    @Username VARCHAR(50),
    @NombreCompleto VARCHAR(100),
    @Estado BIT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Usuarios WHERE Id = @Id)
        BEGIN
            RAISERROR('El registro que intenta editar ya no existe.', 16, 1);
        END

        UPDATE Usuarios 
        SET Username = @Username, 
            NombreCompleto = @NombreCompleto, 
            Estado = @Estado
        WHERE Id = @Id;
    END TRY
    BEGIN CATCH
        DECLARE @ErrUpd NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(@ErrUpd, 16, 1);
    END CATCH
END
GO

-- E. ELIMINAR USUARIO (VB.NET CRUD)
CREATE OR ALTER PROCEDURE sp_EliminarUsuario
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Usuarios WHERE Id = @Id)
        BEGIN
            RAISERROR('No se pudo encontrar el usuario para eliminarlo.', 16, 1);
        END

        DELETE FROM Usuarios WHERE Id = @Id;
    END TRY
    BEGIN CATCH
        DECLARE @ErrDel NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(@ErrDel, 16, 1);
    END CATCH
END
GO

-- 4. DATOS INICIALES
INSERT INTO Usuarios (Username, Password, NombreCompleto)
VALUES ('admin', 'admin123', 'Administrador de Prueba');
GO