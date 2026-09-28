-- Restores role-specific procedure definitions changed by the failed
-- Applicant / Approver / Admin consolidation. Does not modify table data.
USE [ePTW_MTE_UAT];
GO

CREATE OR ALTER PROCEDURE dbo.Procedure_CCP_GetPendingList
    @UserID varchar(100),
    @ProjectName varchar(150)
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS (SELECT 1 FROM dbo.tbl_UserRoles WHERE UserID = @UserID AND RoleID = 'CCP APPROVER')
        SELECT * FROM dbo.tbl_CCPRecord WHERE [Status] = 1 AND ProjectName = @ProjectName;
    ELSE
        SELECT * FROM dbo.tbl_CCPRecord WHERE 1 = 0;
END;
GO

CREATE OR ALTER PROCEDURE dbo.Procedure_CHK_GetPendingList
    @UserID varchar(100),
    @ProjectName varchar(150)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @TEMP table ([Key] varchar(50));
    IF EXISTS (SELECT 1 FROM dbo.tbl_UserRoles WHERE UserID = @UserID AND RoleID = 'PTW APPROVER')
        INSERT @TEMP SELECT [Key] FROM dbo.tbl_ChecklistRecord
        WHERE [Status] = 1 AND ProjectName = @ProjectName;
    SELECT * FROM dbo.tbl_ChecklistRecord
    WHERE [Key] IN (SELECT [Key] FROM @TEMP)
    ORDER BY MeetingDate DESC;
END;
GO

CREATE OR ALTER PROCEDURE dbo.Procedure_GetConstructorList
    @LoginID varchar(100)
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS (SELECT 1 FROM dbo.tbl_UserRoles WHERE UserID = @LoginID AND RoleID = 'APPLICATION ADMIN')
        SELECT * FROM dbo.tbl_Constructors;
    ELSE
        SELECT * FROM dbo.tbl_Constructors
        WHERE [Name] IN (SELECT ConstructorName FROM dbo.tbl_Users WHERE UserID = @LoginID);
END;
GO

CREATE OR ALTER PROCEDURE dbo.Procedure_GetMyCCPList
    @UserID varchar(100),
    @ProjectName varchar(150)
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS (SELECT 1 FROM dbo.tbl_UserRoles WHERE UserID = @UserID AND RoleID = 'APPLICATION ADMIN')
        SELECT * FROM dbo.tbl_CCPRecord WHERE ProjectName = @ProjectName;
    ELSE IF EXISTS (SELECT 1 FROM dbo.tbl_UserRoles WHERE UserID = @UserID AND RoleID = 'CCP APPROVER')
        SELECT * FROM dbo.tbl_CCPRecord WHERE ProjectName = @ProjectName;
    ELSE
        SELECT * FROM dbo.tbl_CCPRecord
        WHERE (CreatedBy = @UserID OR ApprovedBy = @UserID) AND ProjectName = @ProjectName;
END;
GO

CREATE OR ALTER PROCEDURE dbo.Procedure_GetMyChecklistList
    @UserID varchar(100),
    @ProjectName varchar(150)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT A.* FROM dbo.tbl_ChecklistRecord A
    WHERE ProjectName = @ProjectName
    ORDER BY MeetingDate DESC;
END;
GO

CREATE OR ALTER PROCEDURE dbo.Procedure_GetMyTBMList
    @UserID varchar(100),
    @ProjectName varchar(150)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT A.* FROM dbo.tbl_TBMRecord A
    WHERE ProjectName = @ProjectName
    ORDER BY MeetingDate DESC;
END;
GO

CREATE OR ALTER PROCEDURE dbo.Procedure_GetRoleList
    @LoginID varchar(100)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @ApplicationAdmin int;
    DECLARE @CompanyAdmin int;

    SELECT @ApplicationAdmin = COUNT(*) FROM dbo.tbl_UserRoles
    WHERE UserID = @LoginID AND RoleID = 'APPLICATION ADMIN';
    SELECT @CompanyAdmin = COUNT(*) FROM dbo.tbl_UserRoles
    WHERE UserID = @LoginID AND RoleID = 'COMPANY ADMIN';

    IF @ApplicationAdmin > 0
        SELECT * FROM dbo.tbl_Roles ORDER BY Module;
    ELSE IF @CompanyAdmin > 0
        SELECT * FROM dbo.tbl_Roles
        WHERE RoleID NOT IN
        (
            'APPLICATION ADMIN', 'COMPANY ADMIN', 'PTW ASSESSOR', 'PTW SAFETY',
            'PTW APPROVER', 'PTW USER', 'TBM APPROVER', 'TBM USER', 'PTW CLOSURE'
        )
        ORDER BY Module;
END;
GO

CREATE OR ALTER PROCEDURE dbo.Procedure_GetUserList
    @UserID varchar(100)
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS (SELECT 1 FROM dbo.tbl_UserRoles WHERE UserID = @UserID AND RoleID = 'APPLICATION ADMIN')
    BEGIN
        SELECT U.*,
            LTRIM(STUFF((SELECT DISTINCT ', ' + CAST(R.RoleID AS varchar(20))
                FROM dbo.tbl_UserRoles R WHERE R.UserID = U.UserID
                FOR XML PATH('')), 1, 1, '')) AS Roles
        FROM dbo.tbl_Users U
        WHERE U.[Status] = 1;
    END
    ELSE IF EXISTS (SELECT 1 FROM dbo.tbl_UserRoles WHERE UserID = @UserID AND RoleID = 'COMPANY ADMIN')
    BEGIN
        SELECT U.*,
            LTRIM(STUFF((SELECT DISTINCT ', ' + CAST(R.RoleID AS varchar(20))
                FROM dbo.tbl_UserRoles R WHERE R.UserID = U.UserID
                FOR XML PATH('')), 1, 1, '')) AS Roles
        FROM dbo.tbl_Users U
        WHERE U.ConstructorName IN
            (SELECT ConstructorName FROM dbo.tbl_Users WHERE UserID = @UserID)
          AND U.[Status] = 1;
    END
    ELSE
    BEGIN
        SELECT U.*,
            LTRIM(STUFF((SELECT DISTINCT ', ' + CAST(R.RoleID AS varchar(20))
                FROM dbo.tbl_UserRoles R WHERE R.UserID = U.UserID
                FOR XML PATH('')), 1, 1, '')) AS Roles
        FROM dbo.tbl_Users U
        WHERE U.UserID = @UserID AND U.[Status] = 1;
    END;
END;
GO

CREATE OR ALTER PROCEDURE dbo.Procedure_PTW_GetPendingList
    @UserID varchar(100),
    @ProjectName varchar(150)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @TEMP table ([Key] varchar(50));
    DECLARE @MainCont int;
    DECLARE @MainConstructorName varchar(150);
    DECLARE @UserConstructorName varchar(150);

    SELECT @MainConstructorName = ConstructorName FROM dbo.tbl_Projects WHERE [Name] = @ProjectName;
    SELECT @MainCont = COUNT(*) FROM dbo.tbl_Users
    WHERE UserID = @UserID AND ConstructorName = @MainConstructorName;
    SELECT @UserConstructorName = ConstructorName FROM dbo.tbl_Users WHERE UserID = @UserID;

    IF EXISTS (SELECT 1 FROM dbo.tbl_UserRoles WHERE UserID = @UserID AND RoleID = 'PTW ASSESSOR')
    BEGIN
        IF @MainCont = 1
            INSERT @TEMP SELECT [Key] FROM dbo.tbl_PTWRecord
            WHERE [Status] = 1 AND ProjectName = @ProjectName;
        ELSE
            INSERT @TEMP SELECT [Key] FROM dbo.tbl_PTWRecord
            WHERE [Status] = 1 AND ProjectName = @ProjectName AND RequestCompany = @UserConstructorName;
    END;

    IF EXISTS (SELECT 1 FROM dbo.tbl_UserRoles WHERE UserID = @UserID AND RoleID = 'PTW SAFETY')
        INSERT @TEMP SELECT [Key] FROM dbo.tbl_PTWRecord
        WHERE [Status] = 2 AND ProjectName = @ProjectName;

    IF @MainCont = 1 AND EXISTS
        (SELECT 1 FROM dbo.tbl_UserRoles WHERE UserID = @UserID AND RoleID = 'PTW APPROVER')
        INSERT @TEMP SELECT [Key] FROM dbo.tbl_PTWRecord
        WHERE [Status] = 3 AND ProjectName = @ProjectName;

    IF @MainCont = 1 AND EXISTS
        (SELECT 1 FROM dbo.tbl_UserRoles WHERE UserID = @UserID AND RoleID = 'PTW ASSESSOR')
        INSERT @TEMP SELECT [Key] FROM dbo.tbl_PTWRecord
        WHERE [Status] = 4 AND ProjectName = @ProjectName;

    IF @MainCont = 1 AND EXISTS
        (SELECT 1 FROM dbo.tbl_UserRoles WHERE UserID = @UserID AND RoleID = 'PTW SAFETY')
        INSERT @TEMP SELECT [Key] FROM dbo.tbl_PTWRecord
        WHERE [Status] = 4 AND ProjectName = @ProjectName;

    IF @MainCont = 1 AND EXISTS
        (SELECT 1 FROM dbo.tbl_UserRoles WHERE UserID = @UserID AND RoleID = 'PTW APPROVER')
        INSERT @TEMP SELECT [Key] FROM dbo.tbl_PTWRecord
        WHERE [Status] = 4 AND ProjectName = @ProjectName;

    INSERT @TEMP SELECT [Key] FROM dbo.tbl_PTWRecord
    WHERE [Status] = 4 AND ProjectName = @ProjectName AND RequestBy = @UserID;

    IF EXISTS (SELECT 1 FROM dbo.tbl_UserRoles WHERE UserID = @UserID AND RoleID = 'PTW CLOSURE')
        INSERT @TEMP SELECT [Key] FROM dbo.tbl_PTWRecord
        WHERE [Status] = 5 AND ProjectName = @ProjectName;

    SELECT * FROM dbo.tbl_PTWRecord
    WHERE [Key] IN (SELECT [Key] FROM @TEMP)
    ORDER BY [Status], RequestDate DESC;
END;
GO

-- Verification: this should return no rows for the eight repaired procedures.
SELECT O.name AS ObjectName
FROM sys.sql_modules M
JOIN sys.objects O ON O.object_id = M.object_id
WHERE O.name IN
(
    'Procedure_CCP_GetPendingList', 'Procedure_CHK_GetPendingList',
    'Procedure_GetConstructorList', 'Procedure_GetMyCCPList',
    'Procedure_GetMyChecklistList', 'Procedure_GetMyTBMList',
    'Procedure_GetRoleList', 'Procedure_GetUserList', 'Procedure_PTW_GetPendingList'
)
AND
(
    M.definition LIKE '%RoleID = ''Admin''%'
    OR M.definition LIKE '%RoleID = ''Applicant''%'
    OR M.definition LIKE '%RoleID = ''Approver''%'
    OR M.definition LIKE '%RoleID IN (''Approver'', ''Admin'')%'
);
GO
