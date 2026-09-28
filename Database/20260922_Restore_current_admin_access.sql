-- Targeted recovery for the administrator affected by the failed role migration.
USE [ePTW_MTE_UAT];
GO

DECLARE @UserID varchar(100) = 'REPLACE_WITH_YOUR_LOGIN_EMAIL';
DECLARE @Now datetime = GETDATE();

IF @UserID = 'REPLACE_WITH_YOUR_LOGIN_EMAIL' OR NULLIF(LTRIM(RTRIM(@UserID)), '') IS NULL
    THROW 50200, 'Set @UserID to the exact login email before running this script.', 1;

IF NOT EXISTS (SELECT 1 FROM dbo.tbl_Users WHERE UserID = @UserID)
    THROW 50201, 'The supplied UserID does not exist in dbo.tbl_Users.', 1;

SET XACT_ABORT ON;
BEGIN TRY
    BEGIN TRANSACTION;

    IF NOT EXISTS (SELECT 1 FROM dbo.tbl_Roles WHERE RoleID = 'APPLICATION ADMIN')
        INSERT dbo.tbl_Roles
            (RoleID, Module, [Description], Created, CreatedBy, Updated, UpdatedBy)
        VALUES
            ('APPLICATION ADMIN', 'ADMIN', 'Application administration access',
             @Now, 'SYSTEM', @Now, 'SYSTEM');

    IF NOT EXISTS
    (
        SELECT 1 FROM dbo.tbl_UserRoles
        WHERE UserID = @UserID AND RoleID = 'APPLICATION ADMIN'
    )
        INSERT dbo.tbl_UserRoles
            (UserID, RoleID, Created, CreatedBy, Updated, UpdatedBy)
        VALUES
            (@UserID, 'APPLICATION ADMIN', @Now, 'SYSTEM', @Now, 'SYSTEM');

    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
    THROW;
END CATCH;

SELECT DB_NAME() AS DatabaseName, U.UserID, U.EmailAddress, U.ConstructorName, U.[Status]
FROM dbo.tbl_Users U
WHERE U.UserID = @UserID;

SELECT UR.RoleID
FROM dbo.tbl_UserRoles UR
WHERE UR.UserID = @UserID
ORDER BY UR.RoleID;

-- This result must contain all active users. If it does, the database is repaired.
EXEC dbo.Procedure_GetUserList @UserID = @UserID;
GO
