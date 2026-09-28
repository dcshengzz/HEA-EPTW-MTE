-- READ-ONLY UAT recovery check. This script does not change any data or modules.
-- Run it after the failed 20260922 role-consolidation attempt.
USE [ePTW_MTE_UAT];
GO

SET NOCOUNT ON;

SELECT RoleID, Description
FROM dbo.tbl_Roles
ORDER BY RoleID;

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

DECLARE @LegacyRoleCount int =
(
    SELECT COUNT(*)
    FROM dbo.tbl_Roles
    WHERE RoleID NOT IN ('Applicant', 'Approver', 'Admin')
);

IF @LegacyRoleCount > 0
BEGIN
    PRINT 'Legacy roles are still present. The transactional consolidation did not commit, so its role-data changes were rolled back.';
    PRINT 'Deploy the rolled-back application build. Do not run the consolidation script again.';
END
ELSE
BEGIN
    PRINT 'Only the three consolidated roles remain. Exact recovery requires restoring the UAT database from the backup taken before the consolidation script.';
    PRINT 'Do not attempt to infer the old assignments: several distinct roles were merged and that information is no longer present.';
END;
GO
