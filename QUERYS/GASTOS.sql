--CREATE DATABASE GASTOS
-- usrmvcgas
-- 7o*H3ug9
-- .\MSSQLSERVER2017
CREATE DATABASE GASTOS_DEMO
GO 
PRINT 'SE CREO EXITOSAMENTE'
USE GASTOS_DEMO
GO 
PRINT 'SE ENCUENTRA UTILIZADA EXITOSAMENTE'

--EXEC SP_MenuXUserID 2

CREATE TABLE CatUsuario(
    IDUsr INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(255), 
    Paterno VARCHAR(255),
    Materno VARCHAR(255),
    Usuario NVARCHAR(255),
    Password NVARCHAR(255)
);
GO

CREATE PROCEDURE [SP_UsersID] @ID INT
AS
SELECT * FROM CatUsuario WHERE IDUsr=@ID
GO

INSERT INTO CatUsuario(Nombre,Paterno, Materno, Usuario, Password) 
VALUES('Admon','Admon', 'Admon', '0A4B0DED0E2B0BBF0DCE0CB70D7105EF', '083C064C0D1405EF0D14064C');
GO
INSERT INTO CatUsuario(Nombre,Paterno, Materno, Usuario, Password) VALUES ('USUARIO', 'PRUEBAS', 'TEST', '0DCE0C980C1C', '05EF060E062D064C066B');
GO


--0DCE0C980C1C| 05EF060E062D064C066B

CREATE PROCEDURE spGetUsuario @Usuario nvarchar(255), @Contra nvarchar(255)
AS
SELECT * FROM CatUsuario WHERE Usuario=@Usuario AND Password=@Contra
GO

CREATE TABLE Menu(
	IDMenu INT IDENTITY(1,1) PRIMARY KEY,
	Menu VARCHAR(30) NOT NULL,
	Ruta VARCHAR(255) NOT NULL,
	Accion VARCHAR(30) NOT NULL,
	Img VARCHAR(255) NOT NULL,
	IDOrden int NULL
);
GO
-- Los Menus

INSERT INTO Menu(Menu, Ruta, Accion, Img, IDOrden) VALUES('Inicio', 'Main', '', 'img/Home-I.png', 1) 
INSERT INTO Menu(Menu, Ruta, Accion, Img, IDOrden) VALUES('Transacciones', 'Transaccion', '', 'img/Build.png', 2) 
INSERT INTO Menu(Menu, Ruta, Accion, Img, IDOrden) VALUES('Catalogo', 'CatGasto', '', 'img/Docs.png', 3) 
INSERT INTO Menu(Menu, Ruta, Accion, Img, IDOrden) VALUES('Administración', 'Admon', '', 'img/Foco.png', 4) 
INSERT INTO Menu(Menu, Ruta, Accion, Img, IDOrden) VALUES('Asignación', 'Asign', '', 'img/Docs.png', 5) 
INSERT INTO Menu(Menu, Ruta, Accion, Img, IDOrden) VALUES('Salir', 'OUT', '', 'img/Direc-I.png', 99999) 
GO

CREATE PROCEDURE SP_MenuXUserID @IDUsr INT
AS
SELECT IDMenu,Ruta, Img FROM Menu WHERE IDMenu IN(SELECT IDMenu FROM Accesos WHERE IDUsr=@IDUsr) ORDER BY IDOrden ASC
GO

CREATE PROCEDURE SP_Menu @IDMenu INT AS 
SELECT IDMenu, Menu FROM Menu
GO

CREATE TABLE Accesos(
	IDAcceso INT IDENTITY(1,1) PRIMARY KEY,
    IDUsr INT,
	IDMenu INT
);
GO

INSERT INTO Accesos(IDUsr,IDMenu) VALUES(1,1)
INSERT INTO Accesos(IDUsr,IDMenu) VALUES(1,2)
INSERT INTO Accesos(IDUsr,IDMenu) VALUES(1,3)
INSERT INTO Accesos(IDUsr,IDMenu) VALUES(1,4)
INSERT INTO Accesos(IDUsr,IDMenu) VALUES(1,6)
GO
INSERT INTO Accesos(IDUsr,IDMenu) VALUES(2,1)
INSERT INTO Accesos(IDUsr,IDMenu) VALUES(2,2)
INSERT INTO Accesos(IDUsr,IDMenu) VALUES(2,3)
INSERT INTO Accesos(IDUsr,IDMenu) VALUES(2,4)
INSERT INTO Accesos(IDUsr,IDMenu) VALUES(2,6)
INSERT INTO Accesos(IDUsr,IDMenu) VALUES(2,5)
GO

CREATE TABLE TTransaccion(
    IDGas INT IDENTITY(1,1) PRIMARY KEY,
	IDGastos INT NOT NULL,
	Fecha DATETIME,
    Gasto Decimal(12,2),
    GastoEsp INT NOT NULL,
	IDUsr INT
);
GO

CREATE PROCEDURE SP_TransGastos(@IDUsr INT, @FecIni VARCHAR(10), @FecFin VARCHAR(10), @nTipoGasto INT) AS
IF @nTipoGasto != -1
BEGIN
    SELECT A.IDGas, FORMAT(CONVERT(date, A.Fecha, 103), 'dd/MM/yyyy') Fecha , B.Gastos, A.Gasto FROM 
    TTransaccion A INNER JOIN CatGastos B ON A.IDGastos=B.IDGastos 
    WHERE A.IDUsr=@IDUsr AND A.Fecha BETWEEN CONCAT(@FecIni, ' ', '00:00:00') AND CONCAT(@FecFin, ' ', '23:59:59')
	AND GastoEsp =  @nTipoGasto ORDER BY A.Fecha DESC
END
ELSE
    SELECT A.IDGas, FORMAT(CONVERT(date, A.Fecha, 103), 'dd/MM/yyyy') Fecha , B.Gastos, A.Gasto FROM 
    TTransaccion A INNER JOIN CatGastos B ON A.IDGastos=B.IDGastos 
    WHERE A.IDUsr=@IDUsr AND A.Fecha BETWEEN CONCAT(@FecIni, ' ', '00:00:00') AND CONCAT(@FecFin, ' ', '23:59:59') ORDER BY A.Fecha DESC
GO

CREATE TABLE CatGastos(
    IDGastos INT IDENTITY(1,1) PRIMARY KEY,
    Gastos VARCHAR(255),
	IDUsr INT NULL
);
GO

CREATE PROCEDURE SP_CatGastos(@IDUsr INT) AS 
SELECT IDGastos, Gastos FROM CatGastos WHERE IDUsr = @IDUsr
GO


CREATE PROCEDURE spInsertGasto(@Fecha datetime, @nIDGasto int, @WGas float, @EsGasEsp int, @IDUsr int)
AS
INSERT INTO TTransaccion (Fecha, IDGastos, Gasto, GastoEsp, IDUsr) VALUES(@Fecha, @nIDGasto, @WGas, @EsGasEsp, @IDUsr);
GO

CREATE PROCEDURE spUpdateGasto (@Fecha datetime, @nIDGasto int, @WGas float, @EsGasEsp int, @IDUsr int ,@ID int)
AS
UPDATE TTransaccion SET Fecha=@Fecha, IDGastos=@nIDGasto , Gasto=@WGas , GastoEsp=@EsGasEsp, IDUsr=@IDUsr WHERE IDGas=@ID
GO

CREATE PROCEDURE spDeleteGasto (@ID int)
AS
DELETE FROM TTransaccion WHERE IDGas=@ID
GO

CREATE PROCEDURE SP_TransacID @ID INT
AS
SELECT * FROM TTransaccion WHERE IDGas=@ID
GO

CREATE PROCEDURE spInsertCatGasto(@WGas nvarchar(30), @IDUsr int)
AS
INSERT INTO CatGastos (Gastos, IDUsr) VALUES(@WGas, @IDUsr);
GO

CREATE PROCEDURE spUpdateCatGasto (@WGas nvarchar(30), @IDUsr int, @ID int)
AS
UPDATE CatGastos SET Gastos=@WGas, IDUsr=@IDUsr WHERE IDGastos=@ID
GO

CREATE PROCEDURE SP_CatGastoDELID @ID INT, @IDUsr INT
AS
DELETE FROM CatGastos WHERE IDGastos=@ID AND IDUsr=@IDUsr
GO

CREATE PROCEDURE SP_CatGastosID (@IDGastos INT, @IDUsr INT)
AS
SELECT * FROM CatGastos WHERE IDGastos=@IDGastos AND IDUsr= @IDUsr
GO

CREATE PROCEDURE SP_User(@IDUsr INT) AS
SELECT IDUsr, (Nombre + ' ' + Paterno + ' ' + Materno) AS NomCom FROM CatUsuario 
GO

CREATE PROCEDURE SP_UsersUPD @Nombre nvarchar(30), @Paterno nvarchar(30), @Materno nvarchar(30), @Usuario nvarchar(255), @Contra nvarchar(255), @ID INT
AS
UPDATE CatUsuario SET Nombre=@Nombre, Paterno=@Paterno , Materno=@Materno , Usuario=@Usuario, Password=@Contra WHERE IDUsr=@ID
GO

CREATE PROCEDURE SP_UsersINS @Nombre nvarchar(30), @Paterno nvarchar(30), @Materno nvarchar(30), @Usuario nvarchar(255), @Contra nvarchar(255)
AS
INSERT INTO CatUsuario(Nombre,Paterno, Materno, Usuario, Password) VALUES(@Nombre,@Paterno, @Materno, @Usuario, @Contra)
GO

CREATE PROCEDURE SP_UsersDELID @ID INT
AS
DELETE FROM CatUsuario WHERE IDUsr=@ID
GO

CREATE PROCEDURE SPLLenaAsign @IDUsr INT AS
SELECT A.IDMenu, A.Menu, B.IDUsr FROM Menu A INNER JOIN Accesos B ON A.IDMenu=B.IDMenu WHERE IDUsr=@IDUsr ORDER BY A.IDMenu ASC
GO

CREATE PROCEDURE SP_Menu_Falta @IDUsr INT AS
SELECT IDMenu, Menu FROM Menu WHERE NOT IDMenu IN(
SELECT A.IDMenu FROM Menu A INNER JOIN Accesos B ON A.IDMenu=B.IDMenu WHERE IDUsr=@IDUsr)
GO

CREATE PROCEDURE SPAccesoINS @IDUsr INT, @IDMenu INT AS
INSERT INTO Accesos(IDUsr,IDMenu) VALUES(@IDUsr, @IDMenu)
GO

CREATE PROCEDURE SPAccesoDEL @IDUsr INT, @IDMenu INT AS
DELETE FROM Accesos WHERE IDUsr = @IDUsr AND IDMenu =@IDMenu
GO

CREATE PROCEDURE SP_REP_GASTOS(@IDUsr INT, @FecIni VARCHAR(10), @FecFin VARCHAR(10), @nTipoGasto INT) AS
IF @nTipoGasto != -1
BEGIN
    SELECT FORMAT(CONVERT(date, A.Fecha, 103), 'dd/MM/yyyy') Col2 , B.Gastos Col1, A.Gasto Col3 FROM 
    TTransaccion A INNER JOIN CatGastos B ON A.IDGastos=B.IDGastos 
    WHERE A.IDUsr=@IDUsr AND A.Fecha BETWEEN CONCAT(@FecIni, ' ', '00:00:00') AND CONCAT(@FecFin, ' ', '23:59:59')
	AND GastoEsp =  @nTipoGasto ORDER BY A.Fecha DESC
END
ELSE
    SELECT FORMAT(CONVERT(date, A.Fecha, 103), 'dd/MM/yyyy') Col2 , B.Gastos Col1, A.Gasto Col3 FROM 
    TTransaccion A INNER JOIN CatGastos B ON A.IDGastos=B.IDGastos 
    WHERE A.IDUsr=@IDUsr AND A.Fecha BETWEEN CONCAT(@FecIni, ' ', '00:00:00') AND CONCAT(@FecFin, ' ', '23:59:59') ORDER BY A.Fecha DESC
GO


CREATE PROCEDURE SP_TransacTOTAL @IDUsr INT, @FecIni NVARCHAR(10), @FecFin NVARCHAR(10), @GastoEsp INT
AS
IF @GastoEsp!=-1
    SELECT SUM(A.Gasto) [Gasto] FROM TTransaccion A INNER JOIN CatGastos B ON A.IDGastos=B.IDGastos WHERE A.IDUsr=@IDUsr AND GastoEsp=@GastoEsp AND (A.Fecha BETWEEN  @FecIni+' 00:00:00' AND @FecFin + ' 23:59:59')
ELSE
    SELECT SUM(A.Gasto) [Gasto] FROM TTransaccion A INNER JOIN CatGastos B ON A.IDGastos=B.IDGastos WHERE A.IDUsr=@IDUsr AND (A.Fecha BETWEEN  @FecIni+' 00:00:00' AND @FecFin + ' 23:59:59')
GO

