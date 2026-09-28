-- Run on the target ePTW_MTE_UAT database before publishing the matching web app.
-- This migration changes only the two requested CCP Key Activities.
USE [ePTW_MTE_UAT];
GO

SET XACT_ABORT ON;
BEGIN TRY
    BEGIN TRANSACTION;

    DECLARE @Required TABLE (Name varchar(150) PRIMARY KEY);
    INSERT INTO @Required (Name)
    VALUES ('Maintenance Work'), ('Hoisting of lift cage');

    UPDATE Existing
    SET Status = 1,
        Updated = GETDATE(),
        UpdatedBy = 'SYSTEM'
    FROM dbo.tbl_KeyActivities Existing
    JOIN @Required Required
      ON UPPER(LTRIM(RTRIM(Existing.Name))) = UPPER(Required.Name)
    WHERE Existing.Module = 'CCP' AND Existing.Status <> 1;

    INSERT INTO dbo.tbl_KeyActivities
        (Module, Name, Description, Status, Created, CreatedBy, Updated, UpdatedBy)
    SELECT 'CCP', Required.Name, Required.Name, 1,
           GETDATE(), 'SYSTEM', GETDATE(), 'SYSTEM'
    FROM @Required Required
    WHERE NOT EXISTS
    (
        SELECT 1
        FROM dbo.tbl_KeyActivities Existing WITH (UPDLOCK, HOLDLOCK)
        WHERE Existing.Module = 'CCP'
          AND UPPER(LTRIM(RTRIM(Existing.Name))) = UPPER(Required.Name)
    );

    IF EXISTS
    (
        SELECT 1
        FROM @Required Required
        WHERE NOT EXISTS
        (
            SELECT 1
            FROM dbo.tbl_KeyActivities Existing
            WHERE Existing.Module = 'CCP' AND Existing.Status = 1
              AND UPPER(LTRIM(RTRIM(Existing.Name))) = UPPER(Required.Name)
        )
    )
        THROW 50040, 'A required CCP activity is still missing or inactive.', 1;

    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
    THROW;
END CATCH;
GO

SELECT Name, Status
FROM dbo.tbl_KeyActivities
WHERE Module = 'CCP'
  AND UPPER(LTRIM(RTRIM(Name))) IN ('MAINTENANCE WORK', 'HOISTING OF LIFT CAGE')
ORDER BY Name;
GO
