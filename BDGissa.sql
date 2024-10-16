USE [master]
GO
/****** Object:  Database [GissaBD]    Script Date: 15/10/2024 13:24:37 ******/
CREATE DATABASE [GissaBD]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'GissaBD', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\GissaBD.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'GissaBD_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\GissaBD_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [GissaBD] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [GissaBD].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [GissaBD] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [GissaBD] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [GissaBD] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [GissaBD] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [GissaBD] SET ARITHABORT OFF 
GO
ALTER DATABASE [GissaBD] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [GissaBD] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [GissaBD] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [GissaBD] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [GissaBD] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [GissaBD] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [GissaBD] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [GissaBD] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [GissaBD] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [GissaBD] SET  ENABLE_BROKER 
GO
ALTER DATABASE [GissaBD] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [GissaBD] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [GissaBD] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [GissaBD] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [GissaBD] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [GissaBD] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [GissaBD] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [GissaBD] SET RECOVERY FULL 
GO
ALTER DATABASE [GissaBD] SET  MULTI_USER 
GO
ALTER DATABASE [GissaBD] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [GissaBD] SET DB_CHAINING OFF 
GO
ALTER DATABASE [GissaBD] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [GissaBD] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [GissaBD] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [GissaBD] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'GissaBD', N'ON'
GO
ALTER DATABASE [GissaBD] SET QUERY_STORE = ON
GO
ALTER DATABASE [GissaBD] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [GissaBD]
GO
/****** Object:  Table [dbo].[test_Nationality]    Script Date: 15/10/2024 13:24:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[test_Nationality](
	[IdNationality] [bigint] IDENTITY(1,1) NOT NULL,
	[NationalityType] [varchar](50) NOT NULL,
 CONSTRAINT [PK_TNationality] PRIMARY KEY CLUSTERED 
(
	[IdNationality] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[test_Rol]    Script Date: 15/10/2024 13:24:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[test_Rol](
	[IdRol] [bigint] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](50) NOT NULL,
 CONSTRAINT [PK_TRol] PRIMARY KEY CLUSTERED 
(
	[IdRol] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[test_Skills]    Script Date: 15/10/2024 13:24:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[test_Skills](
	[IdSkills] [bigint] IDENTITY(1,1) NOT NULL,
	[SkillsType] [varchar](50) NOT NULL,
 CONSTRAINT [PK_test_Skills] PRIMARY KEY CLUSTERED 
(
	[IdSkills] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[test_User]    Script Date: 15/10/2024 13:24:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[test_User](
	[IdUser] [bigint] IDENTITY(1,1) NOT NULL,
	[IdRol] [bigint] NOT NULL,
	[IdNationality] [bigint] NULL,
	[Identification] [varchar](20) NOT NULL,
	[NameUser] [varchar](40) NOT NULL,
	[LastName] [varchar](30) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[PasswordUser] [nvarchar](100) NOT NULL,
	[ConfPassUser] [nvarchar](100) NOT NULL,
	[Phone] [varchar](15) NOT NULL,
	[State] [bit] NOT NULL,
	[TempKey] [bit] NOT NULL,
	[TempKeyExp] [datetime] NOT NULL,
	[Distric] [varchar](20) NULL,
	[Street] [varchar](10) NULL,
	[TempCode] [nvarchar](100) NULL,
	[SecondPhone] [varchar](15) NULL,
	[SoftSkills] [varchar](255) NULL,
 CONSTRAINT [PK_TUser] PRIMARY KEY CLUSTERED 
(
	[IdUser] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[test_Nationality] ON 

INSERT [dbo].[test_Nationality] ([IdNationality], [NationalityType]) VALUES (1, N'Identificación Nacional')
INSERT [dbo].[test_Nationality] ([IdNationality], [NationalityType]) VALUES (3, N'Cédula de Extranjería')
SET IDENTITY_INSERT [dbo].[test_Nationality] OFF
GO
SET IDENTITY_INSERT [dbo].[test_Rol] ON 

INSERT [dbo].[test_Rol] ([IdRol], [Description]) VALUES (1, N'Administrador')
INSERT [dbo].[test_Rol] ([IdRol], [Description]) VALUES (2, N'Consultor')
SET IDENTITY_INSERT [dbo].[test_Rol] OFF
GO
SET IDENTITY_INSERT [dbo].[test_Skills] ON 

INSERT [dbo].[test_Skills] ([IdSkills], [SkillsType]) VALUES (1, N'Buena comunicación')
INSERT [dbo].[test_Skills] ([IdSkills], [SkillsType]) VALUES (2, N'Buena organización')
INSERT [dbo].[test_Skills] ([IdSkills], [SkillsType]) VALUES (3, N'Trabajo en equipo')
INSERT [dbo].[test_Skills] ([IdSkills], [SkillsType]) VALUES (4, N'Puntualidad')
INSERT [dbo].[test_Skills] ([IdSkills], [SkillsType]) VALUES (5, N'Ser creativo')
INSERT [dbo].[test_Skills] ([IdSkills], [SkillsType]) VALUES (6, N'Facilidad de adaptación')
SET IDENTITY_INSERT [dbo].[test_Skills] OFF
GO
SET IDENTITY_INSERT [dbo].[test_User] ON 

INSERT [dbo].[test_User] ([IdUser], [IdRol], [IdNationality], [Identification], [NameUser], [LastName], [Email], [PasswordUser], [ConfPassUser], [Phone], [State], [TempKey], [TempKeyExp], [Distric], [Street], [TempCode], [SecondPhone], [SoftSkills]) VALUES (1, 1, 1, N'117620845', N'CarlosAdmin', N'Zumbado Cárdenas', N'carloszumbadocardenas@gmail.com', N'582427cabb723aaeed54b3d3c', N'582427cabb723aaeed54b3d3c', N'87883882', 1, 1, CAST(N'2024-10-15T12:27:01.310' AS DateTime), NULL, NULL, NULL, NULL, N'Buena comunicación, Trabajo en equipo')
INSERT [dbo].[test_User] ([IdUser], [IdRol], [IdNationality], [Identification], [NameUser], [LastName], [Email], [PasswordUser], [ConfPassUser], [Phone], [State], [TempKey], [TempKeyExp], [Distric], [Street], [TempCode], [SecondPhone], [SoftSkills]) VALUES (2, 2, 1, N'116539868', N'CarlosCliente', N'Zumbado Cárdenas', N'sectormaster320@gmail.com', N'582427cabb723aaeed54b3d3c', N'582427cabb723aaeed54b3d3c', N'76548329', 1, 0, CAST(N'2024-10-12T22:00:52.567' AS DateTime), NULL, NULL, NULL, NULL, N'Buena comunicación, Trabajo en equipo')
INSERT [dbo].[test_User] ([IdUser], [IdRol], [IdNationality], [Identification], [NameUser], [LastName], [Email], [PasswordUser], [ConfPassUser], [Phone], [State], [TempKey], [TempKeyExp], [Distric], [Street], [TempCode], [SecondPhone], [SoftSkills]) VALUES (35, 2, 1, N'117654367', N'Prueba', N'Prueba', N'prueba1@gmail.com', N'582427cabb723aaeed54b3d3c', N'582427cabb723aaeed54b3d3c', N'87883883', 1, 0, CAST(N'2024-10-15T12:17:19.660' AS DateTime), NULL, NULL, NULL, N'12345678', N'')
SET IDENTITY_INSERT [dbo].[test_User] OFF
GO
/****** Object:  StoredProcedure [dbo].[ActualizarPerfil]    Script Date: 15/10/2024 13:24:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[ActualizarPerfil]
    @NameUser VARCHAR(50),
    @LastName VARCHAR(50),
    @Email VARCHAR(50),
    @Phone VARCHAR(30),
    @Identification VARCHAR(50)
AS
BEGIN
    -- Verificar si el correo electrónico ya está en uso por otro usuario
    IF EXISTS (SELECT 1 FROM [dbo].[test_User] WHERE Email = @Email AND Identification != @Identification)
    BEGIN
        -- El correo electrónico ya está en uso por otro usuario, devolver un error
        RETURN -1; -- Código de error para correo electrónico duplicado
    END
    ELSE
    BEGIN
        -- El correo electrónico no está en uso por otro usuario, proceder con la actualización del perfil
        BEGIN
            -- Actualizar el perfil del usuario
            UPDATE [dbo].[test_User]
            SET NameUser = @NameUser,
                LastName = @LastName,
                Phone = @Phone,
                Email = @Email
            WHERE Identification = @Identification;

            -- Verificar si se actualizó algún registro
            IF @@ROWCOUNT > 0
            BEGIN
                -- Éxito: la actualización del perfil se realizó correctamente
                RETURN 0;
            END
        END
    END
END
GO
/****** Object:  StoredProcedure [dbo].[ChangePassword]    Script Date: 15/10/2024 13:24:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ChangePassword]
    @Codigo NVARCHAR(100),
    @Email VARCHAR(50),
    @PasswordUser VARCHAR(25)
AS
BEGIN
    -- Verificar si el correo electrónico y el código coinciden en la base de datos
    IF EXISTS (
        SELECT 1 
        FROM test_User
        WHERE Email = @Email 
          AND TempCode = @Codigo
    )
    BEGIN
        -- Actualizar la contraseña del usuario con la nueva contraseña encriptada
        UPDATE test_User
        SET PasswordUser = @PasswordUser,
            TempCode = NULL  -- Limpiar el código temporal
        WHERE Email = @Email
          AND TempCode = @Codigo

        -- Retorna 1 para indicar que se ha restablecido la contraseña exitosamente
        RETURN 1;
    END
    ELSE
    BEGIN
        -- Retorna 0 para indicar que el correo electrónico y/o el código no son válidos
        RETURN 0;
    END
END
GO
/****** Object:  StoredProcedure [dbo].[ChangeRol]    Script Date: 15/10/2024 13:24:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ChangeRol]
    @IdUser bigint
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Success INT = 0;

    -- Obtener el rol actual del usuario
    DECLARE @RolActual varchar(50);
    SELECT @RolActual = Description
    FROM TRol
    WHERE IdRol = (SELECT IdRol FROM test_User WHERE IdUser = @IdUser);

    -- Cambiar el rol del usuario
    BEGIN TRY
        UPDATE test_User
        SET IdRol = (CASE 
                        WHEN @RolActual = 'Administrador' THEN (SELECT IdRol FROM TRol WHERE Description = 'Cliente')
                        WHEN @RolActual = 'Cliente' THEN (SELECT IdRol FROM TRol WHERE Description = 'Administrador')
                     END)
        WHERE IdUser = @IdUser;

        SET @Success = 1; -- Indica que la operación fue exitosa
    END TRY
    BEGIN CATCH
        SET @Success = 0; -- Indica que la operación falló
    END CATCH

    SELECT @Success AS Success; -- Devuelve el valor de éxito o fracaso
END 
GO
/****** Object:  StoredProcedure [dbo].[ConsultNationality]    Script Date: 15/10/2024 13:24:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ConsultNationality]
	
AS
BEGIN
	
	select IdNationality 'value', NationalityType 'text' from test_Nationality

END
GO
/****** Object:  StoredProcedure [dbo].[ConsultRol]    Script Date: 15/10/2024 13:24:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ConsultRol]
	
AS
BEGIN
	
	select IdRol 'value', Description 'text' from test_Rol

END
GO
/****** Object:  StoredProcedure [dbo].[ConsultSkills]    Script Date: 15/10/2024 13:24:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ConsultSkills]
	
AS
BEGIN
	
	select IdSkills 'value', SkillsType 'text' from test_Skills

END
GO
/****** Object:  StoredProcedure [dbo].[ConsultUsers]    Script Date: 15/10/2024 13:24:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ConsultUsers]
    @IdUser BIGINT
AS
BEGIN
    SELECT IdUser,
           Identification,
           NameUser,
           LastName,
           Phone,
           Email,
           SecondPhone,  
           State,
           IdRol
    FROM dbo.test_User
    WHERE IdUser != @IdUser
END

GO
/****** Object:  StoredProcedure [dbo].[Login]    Script Date: 15/10/2024 13:24:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Login]
	@Email varchar(50),
	@PasswordUser varchar(25)
AS
BEGIN
	SELECT IdUser, IdRol, Identification, NameUser, LastName, Email, Phone FROM [dbo].[test_User] WHERE Email = @Email and PasswordUser=@PasswordUser and State = 1
END
GO
/****** Object:  StoredProcedure [dbo].[RecoverAccount]    Script Date: 15/10/2024 13:24:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RecoverAccount]
	@Email	VARCHAR(50)
AS
BEGIN
	
	SELECT	IdUser, NameUser, Email
	FROM	test_User
	WHERE	(Email = @Email)
		AND State		= 1

END
GO
/****** Object:  StoredProcedure [dbo].[RegistrerAccount]    Script Date: 15/10/2024 13:24:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RegistrerAccount]
    @IdNationality bigint,
    @Identification varchar(20),
    @NameUser varchar(40),
    @LastName varchar(30),
    @Email varchar(50),
    @PasswordUser varchar(25), 
    @ConfPassUser varchar(25),
    @Phone varchar(15),
    @SecondPhone varchar(15) NULL,  
    @SoftSkills varchar(255)  
AS
BEGIN
    IF NOT EXISTS (SELECT 1 FROM [dbo].[test_User] WHERE Email = @Email OR Identification = @Identification)
    BEGIN
        -- Inserta en TUser incluyendo SoftSkills
        INSERT INTO [dbo].[test_User] (IdNationality, Identification, NameUser, LastName, Email, PasswordUser, ConfPassUser, Phone, SecondPhone, SoftSkills, State, TempKey, TempKeyExp, IdRol)
        VALUES (@IdNationality, @Identification, @NameUser, @LastName, @Email, @PasswordUser, @ConfPassUser, @Phone, @SecondPhone, @SoftSkills, 1, 0, GETDATE(), 2)
    END
    ELSE
    BEGIN
        -- El nuevo correo electrónico ya está en uso, devolver un código de error
        RETURN 1;
    END
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateProfile]    Script Date: 15/10/2024 13:24:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateProfile]
    @NameUser VARCHAR(50),
    @LastName VARCHAR(50),
    @Email VARCHAR(50),
    @Phone VARCHAR(30),
    @Identification VARCHAR(50)
AS
BEGIN
    -- Verificar si el correo electrónico ya está en uso por otro usuario
    IF EXISTS (SELECT 1 FROM [dbo].[test_User] WHERE Email = @Email AND Identification != @Identification)
    BEGIN
        -- El correo electrónico ya está en uso por otro usuario, devolver un error
        RETURN -1; -- Código de error para correo electrónico duplicado
    END
    ELSE
    BEGIN
        -- El correo electrónico no está en uso por otro usuario, proceder con la actualización del perfil
        BEGIN
            -- Actualizar el perfil del usuario
            UPDATE [dbo].[test_User]
            SET NameUser = @NameUser,
                LastName = @LastName,
                Phone = @Phone,
                Email = @Email
            WHERE Identification = @Identification;

            -- Verificar si se actualizó algún registro
            IF @@ROWCOUNT > 0
            BEGIN
                -- Éxito: la actualización del perfil se realizó correctamente
                RETURN 0;
            END
        END
    END
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateState]    Script Date: 15/10/2024 13:24:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateState]
	@IdUser BIGINT
AS
BEGIN

	UPDATE	test_User
	SET		State = (CASE WHEN State = 1 THEN 0 ELSE 1 END)
	WHERE	IdUser = @IdUser

END
GO
/****** Object:  StoredProcedure [dbo].[UpdateTempKey]    Script Date: 15/10/2024 13:24:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateTempKey]
	@IdUser				bigint,
	@TempKey	varchar(4)
AS
BEGIN

	UPDATE dbo.test_User
	SET TempCode = @TempKey,
		TempKey = 1,
        TempKeyExp = DATEADD(mi,30,GETDATE())
	WHERE IdUser = @IdUser

END
GO
/****** Object:  StoredProcedure [dbo].[UpdateUser]    Script Date: 15/10/2024 13:24:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateUser]
	@Identification varchar(20),
	@NameUser varchar(30),
	@LastName varchar(30),
	@IdUser bigint,
	@IdRol bigint,
	@Email varchar(50)
AS
BEGIN
	
	UPDATE	dbo.test_User
	   SET	Identification = @Identification,
			NameUser = @NameUser,
			LastName = @LastName,
			Email = @Email,
			IdRol = @IdRol
	 WHERE	IdUser = @IdUser;

END
GO
USE [master]
GO
ALTER DATABASE [GissaBD] SET  READ_WRITE 
GO
