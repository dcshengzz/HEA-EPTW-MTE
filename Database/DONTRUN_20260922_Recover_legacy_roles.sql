-- Emergency recovery after the failed three-role consolidation.
-- Reconstructs legacy roles, assignments, and RoleID-based mappings from the
-- surviving Applicant / Approver / Admin records.
USE [ePTW_MTE_UAT];
GO

SET NOCOUNT ON;
SET XACT_ABORT ON;

BEGIN TRY
    BEGIN TRANSACTION;

    IF OBJECT_ID('dbo.tbl_Roles', 'U') IS NULL OR OBJECT_ID('dbo.tbl_UserRoles', 'U') IS NULL
        THROW 50100, 'Required role tables were not found.', 1;

    IF NOT EXISTS (SELECT 1 FROM dbo.tbl_Roles WHERE RoleID IN ('Applicant', 'Approver', 'Admin'))
       AND NOT EXISTS (SELECT 1 FROM dbo.tbl_UserRoles WHERE RoleID IN ('Applicant', 'Approver', 'Admin'))
        THROW 50101, 'No consolidated role master rows or user assignments were found. Run the rollback state-check script before making further changes.', 1;

    DECLARE @Now datetime = GETDATE();

    CREATE TABLE #RoleMap
    (
        CanonicalRoleID varchar(100) NOT NULL,
        LegacyRoleID varchar(100) NOT NULL,
        Module varchar(100) NOT NULL,
        [Description] varchar(250) NOT NULL
    );

    INSERT #RoleMap (CanonicalRoleID, LegacyRoleID, Module, [Description])
    VALUES
        ('Applicant', 'PTW USER', 'PTW', 'PTW user access'),
        ('Applicant', 'TBM USER', 'TBM', 'TBM user access'),
        ('Applicant', 'CKL USER', 'CKL', 'Checklist user access'),
        ('Applicant', 'CCP USER', 'CCP', 'CCP user access'),
        ('Approver', 'PTW ASSESSOR', 'PTW', 'PTW assessment access'),
        ('Approver', 'PTW SAFETY', 'PTW', 'PTW safety verification access'),
        ('Approver', 'PTW APPROVER', 'PTW', 'PTW and TBM approval access'),
        ('Approver', 'PTW CLOSURE', 'PTW', 'PTW closure verification access'),
        ('Approver', 'CCP APPROVER', 'CCP', 'CCP approval access'),
        ('Admin', 'APPLICATION ADMIN', 'ADMIN', 'Application administration access'),
        ('Admin', 'COMPANY ADMIN', 'ADMIN', 'Company administration access');

    INSERT dbo.tbl_Roles (RoleID, Module, [Description], Created, CreatedBy, Updated, UpdatedBy)
    SELECT M.LegacyRoleID, M.Module, M.[Description], @Now, 'SYSTEM', @Now, 'SYSTEM'
    FROM #RoleMap M
    WHERE NOT EXISTS (SELECT 1 FROM dbo.tbl_Roles R WHERE R.RoleID = M.LegacyRoleID);

    -- Restore each category to its former detailed assignments.
    INSERT dbo.tbl_UserRoles (UserID, RoleID, Created, CreatedBy, Updated, UpdatedBy)
    SELECT DISTINCT U.UserID, M.LegacyRoleID, @Now, 'SYSTEM', @Now, 'SYSTEM'
    FROM dbo.tbl_UserRoles U
    JOIN #RoleMap M ON M.CanonicalRoleID = U.RoleID
    WHERE NOT EXISTS
    (
        SELECT 1 FROM dbo.tbl_UserRoles X
        WHERE X.UserID = U.UserID AND X.RoleID = M.LegacyRoleID
    );

    -- Admin previously had full access, so restore every detailed application role.
    INSERT dbo.tbl_UserRoles (UserID, RoleID, Created, CreatedBy, Updated, UpdatedBy)
    SELECT DISTINCT A.UserID, M.LegacyRoleID, @Now, 'SYSTEM', @Now, 'SYSTEM'
    FROM dbo.tbl_UserRoles A
    CROSS JOIN #RoleMap M
    WHERE A.RoleID = 'Admin'
      AND NOT EXISTS
      (
          SELECT 1 FROM dbo.tbl_UserRoles X
          WHERE X.UserID = A.UserID AND X.RoleID = M.LegacyRoleID
      );

    -- Restore all other RoleID-based mappings (including menu mappings) by
    -- cloning the consolidated source row back to each corresponding legacy role.
    DECLARE @SchemaName sysname;
    DECLARE @TableName sysname;
    DECLARE @InsertColumns nvarchar(max);
    DECLARE @SelectColumns nvarchar(max);
    DECLARE @Comparison nvarchar(max);
    DECLARE @Sql nvarchar(max);

    DECLARE RoleTables CURSOR LOCAL FAST_FORWARD FOR
        SELECT S.name, T.name
        FROM sys.tables T
        JOIN sys.schemas S ON S.schema_id = T.schema_id
        JOIN sys.columns C ON C.object_id = T.object_id AND C.name = 'RoleID'
        WHERE T.object_id NOT IN (OBJECT_ID('dbo.tbl_Roles'), OBJECT_ID('dbo.tbl_UserRoles'))
        ORDER BY S.name, T.name;

    OPEN RoleTables;
    FETCH NEXT FROM RoleTables INTO @SchemaName, @TableName;
    WHILE @@FETCH_STATUS = 0
    BEGIN
        SET @InsertColumns = NULL;
        SET @SelectColumns = NULL;
        SET @Comparison = NULL;

        SELECT @InsertColumns = STUFF((
            SELECT N', ' + QUOTENAME(C.name)
            FROM sys.columns C
            JOIN sys.types TY ON TY.user_type_id = C.user_type_id
            WHERE C.object_id = OBJECT_ID(QUOTENAME(@SchemaName) + N'.' + QUOTENAME(@TableName))
              AND C.name <> 'RoleID' AND C.is_identity = 0 AND C.is_computed = 0
              AND TY.name NOT IN ('timestamp', 'rowversion')
            ORDER BY C.column_id
            FOR XML PATH(''), TYPE).value('.', 'nvarchar(max)'), 1, 2, '');

        SELECT @SelectColumns = STUFF((
            SELECT N', S.' + QUOTENAME(C.name)
            FROM sys.columns C
            JOIN sys.types TY ON TY.user_type_id = C.user_type_id
            WHERE C.object_id = OBJECT_ID(QUOTENAME(@SchemaName) + N'.' + QUOTENAME(@TableName))
              AND C.name <> 'RoleID' AND C.is_identity = 0 AND C.is_computed = 0
              AND TY.name NOT IN ('timestamp', 'rowversion')
            ORDER BY C.column_id
            FOR XML PATH(''), TYPE).value('.', 'nvarchar(max)'), 1, 2, '');

        SELECT @Comparison = STUFF((
            SELECT N' AND ((T.' + QUOTENAME(C.name) + N' = S.' + QUOTENAME(C.name) + N') OR (T.' +
                   QUOTENAME(C.name) + N' IS NULL AND S.' + QUOTENAME(C.name) + N' IS NULL))'
            FROM sys.columns C
            JOIN sys.types TY ON TY.user_type_id = C.user_type_id
            WHERE C.object_id = OBJECT_ID(QUOTENAME(@SchemaName) + N'.' + QUOTENAME(@TableName))
              AND C.name <> 'RoleID' AND C.is_identity = 0 AND C.is_computed = 0
              AND TY.name NOT IN ('timestamp', 'rowversion', 'text', 'ntext', 'image')
            ORDER BY C.column_id
            FOR XML PATH(''), TYPE).value('.', 'nvarchar(max)'), 1, 5, '');

        IF NULLIF(@InsertColumns, '') IS NOT NULL AND NULLIF(@Comparison, '') IS NOT NULL
        BEGIN
            SET @Sql = N'
                INSERT ' + QUOTENAME(@SchemaName) + N'.' + QUOTENAME(@TableName) +
                N' (RoleID, ' + @InsertColumns + N')
                SELECT M.LegacyRoleID, ' + @SelectColumns + N'
                FROM ' + QUOTENAME(@SchemaName) + N'.' + QUOTENAME(@TableName) + N' S
                JOIN #RoleMap M ON M.CanonicalRoleID = S.RoleID
                WHERE NOT EXISTS
                (
                    SELECT 1 FROM ' + QUOTENAME(@SchemaName) + N'.' + QUOTENAME(@TableName) + N' T
                    WHERE T.RoleID = M.LegacyRoleID AND ' + @Comparison + N'
                );
                DELETE FROM ' + QUOTENAME(@SchemaName) + N'.' + QUOTENAME(@TableName) +
                N' WHERE RoleID IN (''Applicant'', ''Approver'', ''Admin'');';

            EXEC sys.sp_executesql @Sql;
        END
        ELSE
        BEGIN
            SET @Sql = N'
                INSERT ' + QUOTENAME(@SchemaName) + N'.' + QUOTENAME(@TableName) + N' (RoleID)
                SELECT M.LegacyRoleID
                FROM #RoleMap M
                WHERE EXISTS
                    (SELECT 1 FROM ' + QUOTENAME(@SchemaName) + N'.' + QUOTENAME(@TableName) + N' S WHERE S.RoleID = M.CanonicalRoleID)
                  AND NOT EXISTS
                    (SELECT 1 FROM ' + QUOTENAME(@SchemaName) + N'.' + QUOTENAME(@TableName) + N' T WHERE T.RoleID = M.LegacyRoleID);
                DELETE FROM ' + QUOTENAME(@SchemaName) + N'.' + QUOTENAME(@TableName) +
                N' WHERE RoleID IN (''Applicant'', ''Approver'', ''Admin'');';
            EXEC sys.sp_executesql @Sql;
        END;

        FETCH NEXT FROM RoleTables INTO @SchemaName, @TableName;
    END;

    CLOSE RoleTables;
    DEALLOCATE RoleTables;

    -- Remove consolidated assignments only after their detailed replacements exist.
    DELETE dbo.tbl_UserRoles WHERE RoleID IN ('Applicant', 'Approver', 'Admin');
    DELETE dbo.tbl_Roles WHERE RoleID IN ('Applicant', 'Approver', 'Admin');

    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
    IF CURSOR_STATUS('local', 'RoleTables') >= 0 CLOSE RoleTables;
    IF CURSOR_STATUS('local', 'RoleTables') >= -1 DEALLOCATE RoleTables;
    THROW;
END CATCH;
GO

SELECT RoleID, COUNT(*) AS UserAssignmentCount
FROM dbo.tbl_UserRoles
GROUP BY RoleID
ORDER BY RoleID;

SELECT
    SCHEMA_NAME(O.schema_id) AS SchemaName,
    O.name AS ModuleName,
    O.type_desc AS ModuleType
FROM sys.sql_modules M
JOIN sys.objects O ON O.object_id = M.object_id
WHERE M.definition LIKE '%RoleID = ''Approver''%'
   OR M.definition LIKE '%RoleID IN (''Approver'', ''Admin'')%'
   OR M.definition LIKE '%RoleID = ''Applicant''%'
ORDER BY O.type_desc, O.name;
GO
