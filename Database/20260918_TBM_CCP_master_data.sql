-- Run against ePTW_MTE_UAT before publishing the matching web application.
-- Safe to rerun: updates the ten active TBM briefing rows and ensures the
-- two requested CCP activities are active.
USE [ePTW_MTE_UAT];
GO

SET XACT_ABORT ON;
BEGIN TRY
    BEGIN TRANSACTION;

    DECLARE @Briefings TABLE (Ordinal int PRIMARY KEY, Question varchar(1000) NOT NULL);
    INSERT INTO @Briefings (Ordinal, Question)
    VALUES
        (1, '1. Health check (Eg: Are you OK to work ? Any Fatigue ?)'),
        (2, '2. PPE Check (Eg: Attire, Safety Shoes, Safety Harness, Safety Belt, Helmet c/w chin strap, Hand Gloves etc).'),
        (3, '3. Set-up barricades and signages before start work.'),
        (4, '4. Use of proper tools & instruments for suitable equipment.'),
        (5, '5. No drinks, food and smoking within premises.'),
        (6, '6. Maintain proper housekeeping.'),
        (7, '7. Safety driving / riding habit complying to traffic regulations'),
        (8, '8. Safety ownership, authority to stop work and report near miss / hazard to supervisor.'),
        (9, '9. Operation key must pass to team leader before start of ES work.'),
        (10, '10. Others : Access to Private Lobby is Strictly Prohibited without Approval from Building Owner.');

    DECLARE @ExistingCount int;
    DECLARE @Selection varchar(500);
    DECLARE @DocumentStatus int;

    SELECT @ExistingCount = COUNT(*)
    FROM dbo.tbl_TemplateDetails
    WHERE TemplateName = 'TBM' AND Status = 1
      AND NULLIF(LTRIM(RTRIM(Selection)), '') IS NOT NULL;

    IF @ExistingCount > 11
        THROW 50030, 'Review extra active TBM briefing rows before applying this migration.', 1;

    SELECT TOP (1)
        @Selection = Selection,
        @DocumentStatus = DocumentStatus
    FROM dbo.tbl_TemplateDetails
    WHERE TemplateName = 'TBM' AND Status = 1
      AND NULLIF(LTRIM(RTRIM(Selection)), '') IS NOT NULL
    ORDER BY Sort, ID;

    SET @Selection = COALESCE(@Selection, 'Yes|No|NA');
    SET @DocumentStatus = COALESCE(@DocumentStatus, 0);

    ;WITH Existing AS
    (
        SELECT ID, ROW_NUMBER() OVER (ORDER BY Sort, ID) AS Ordinal
        FROM dbo.tbl_TemplateDetails
        WHERE TemplateName = 'TBM' AND Status = 1
          AND NULLIF(LTRIM(RTRIM(Selection)), '') IS NOT NULL
    )
    UPDATE T
    SET Question = B.Question,
        Sort = B.Ordinal * 10,
        Updated = GETDATE(),
        UpdatedBy = 'SYSTEM'
    FROM dbo.tbl_TemplateDetails T
    JOIN Existing E ON E.ID = T.ID
    JOIN @Briefings B ON B.Ordinal = E.Ordinal;

    ;WITH Surplus AS
    (
        SELECT ID, ROW_NUMBER() OVER (ORDER BY Sort, ID) AS Ordinal
        FROM dbo.tbl_TemplateDetails
        WHERE TemplateName = 'TBM' AND Status = 1
          AND NULLIF(LTRIM(RTRIM(Selection)), '') IS NOT NULL
    )
    UPDATE T
    SET Status = 97,
        Updated = GETDATE(),
        UpdatedBy = 'SYSTEM'
    FROM dbo.tbl_TemplateDetails T
    JOIN Surplus S ON S.ID = T.ID
    WHERE S.Ordinal > 10;

    INSERT INTO dbo.tbl_TemplateDetails
        (TemplateName, Question, Selection, Status, Sort,
         Created, CreatedBy, Updated, UpdatedBy, DocumentStatus)
    SELECT 'TBM', B.Question, @Selection, 1, B.Ordinal * 10,
           GETDATE(), 'SYSTEM', GETDATE(), 'SYSTEM', @DocumentStatus
    FROM @Briefings B
    WHERE B.Ordinal > @ExistingCount;

    UPDATE dbo.tbl_KeyActivities
    SET Status = 1, Updated = GETDATE(), UpdatedBy = 'SYSTEM'
    WHERE Module = 'CCP'
      AND Name IN ('Maintenance Work', 'Hoisting of lift cage')
      AND Status <> 1;

    INSERT INTO dbo.tbl_KeyActivities
        (Module, Name, Description, Status, Created, CreatedBy, Updated, UpdatedBy)
    SELECT 'CCP', A.Name, A.Name, 1, GETDATE(), 'SYSTEM', GETDATE(), 'SYSTEM'
    FROM (VALUES ('Maintenance Work'), ('Hoisting of lift cage')) A(Name)
    WHERE NOT EXISTS
    (
        SELECT 1 FROM dbo.tbl_KeyActivities Existing
        WHERE Existing.Module = 'CCP' AND Existing.Name = A.Name
    );

    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
    THROW;
END CATCH;
GO
