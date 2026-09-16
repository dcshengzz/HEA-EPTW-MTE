-- Apply to ePTW_MTE_UAT before deploying the matching web application.
-- Existing teams deliberately remain unassigned; configure each team's TBM approver
-- in Team Management before permitting new TBM submissions.
USE [ePTW_MTE_UAT];
GO

IF COL_LENGTH('dbo.tbl_Projects', 'ApproverUserID') IS NULL
    ALTER TABLE dbo.tbl_Projects ADD ApproverUserID varchar(100) NULL;
GO

IF EXISTS (SELECT [Name] FROM dbo.tbl_Projects GROUP BY [Name] HAVING COUNT(*) > 1)
    THROW 50004, 'Duplicate team names must be resolved before TBM approver routing is enabled.', 1;
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE object_id = OBJECT_ID('dbo.tbl_Projects') AND name = 'UX_tbl_Projects_Name')
    CREATE UNIQUE INDEX UX_tbl_Projects_Name ON dbo.tbl_Projects([Name]);
GO

CREATE OR ALTER PROCEDURE dbo.Procedure_Project_InsertUpdate
    @Photo varbinary(max),
    @Name varchar(150),
    @Address varchar(500),
    @Description varchar(2500),
    @ConstructorName varchar(150),
    @StartDate datetime,
    @EndDate datetime,
    @Status int,
    @Created datetime,
    @CreatedBy varchar(100),
    @Updated datetime,
    @UpdatedBy varchar(100),
    @ApproverUserID varchar(100) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    IF @ApproverUserID IS NOT NULL AND NOT EXISTS
    (
        SELECT 1 FROM dbo.tbl_Users U
        WHERE U.UserID = @ApproverUserID AND U.Status = 1
          AND NULLIF(LTRIM(RTRIM(U.EmailAddress)), '') IS NOT NULL
          AND EXISTS (SELECT 1 FROM dbo.tbl_UserRoles R
                      WHERE R.UserID = U.UserID AND R.RoleID = 'PTW APPROVER')
    )
        THROW 50001, 'The team TBM approver must be an active PTW approver with an email address.', 1;

    IF @Photo IS NULL
        SELECT @Photo = Photo FROM dbo.tbl_DefaultImage WHERE [Name] = 'PROJECT';

    IF EXISTS (SELECT 1 FROM dbo.tbl_Projects WHERE [Name] = @Name)
        UPDATE dbo.tbl_Projects SET
            Photo = @Photo, Address = @Address, Description = @Description,
            ConstructorName = @ConstructorName, StartDate = @StartDate,
            EndDate = @EndDate, Status = @Status,
            ApproverUserID = COALESCE(@ApproverUserID, ApproverUserID),
            Updated = @Updated, UpdatedBy = @UpdatedBy
        WHERE [Name] = @Name;
    ELSE
        INSERT dbo.tbl_Projects
            (Photo, [Name], Address, Description, ConstructorName, StartDate,
             EndDate, Status, Created, CreatedBy, Updated, UpdatedBy, ApproverUserID)
        VALUES
            (@Photo, @Name, @Address, @Description, @ConstructorName, @StartDate,
             @EndDate, @Status, @Created, @CreatedBy, @Updated, @UpdatedBy, @ApproverUserID);
END;
GO

CREATE OR ALTER PROCEDURE dbo.Procedure_TBM_GetPendingList
    @UserID varchar(100),
    @ProjectName varchar(150)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT T.*
    FROM dbo.tbl_TBMRecord T
    INNER JOIN dbo.tbl_Projects P ON P.[Name] = T.ProjectName
    WHERE T.Status = 1 AND T.ProjectName = @ProjectName
      AND P.Status = 1 AND P.ApproverUserID = @UserID
      AND EXISTS (SELECT 1 FROM dbo.tbl_UserRoles R
                  WHERE R.UserID = @UserID AND R.RoleID = 'PTW APPROVER')
    ORDER BY T.MeetingDate DESC;
END;
GO

-- The source schema already uses HEAData.dbo.EmailLogStatus for queued email.
-- This trigger queues only a transition into Submitted, never a repeat save at Status 1.
CREATE OR ALTER TRIGGER dbo.trg_TBM_TeamApproverNotification
ON dbo.tbl_TBMRecord
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS
    (
        SELECT 1 FROM inserted I
        LEFT JOIN deleted D ON D.ID = I.ID
        LEFT JOIN dbo.tbl_Projects P ON P.[Name] = I.ProjectName
        LEFT JOIN dbo.tbl_Users U ON U.UserID = P.ApproverUserID
        WHERE I.Status = 1 AND (D.ID IS NULL OR D.Status <> 1)
          AND (P.ID IS NULL OR P.Status <> 1 OR U.ID IS NULL OR U.Status <> 1
               OR NULLIF(LTRIM(RTRIM(U.EmailAddress)), '') IS NULL
               OR NOT EXISTS (SELECT 1 FROM dbo.tbl_UserRoles R
                              WHERE R.UserID = U.UserID AND R.RoleID = 'PTW APPROVER'))
    )
        THROW 50002, 'TBM submission requires an active team approver with an email address.', 1;

    IF EXISTS
    (
        SELECT 1 FROM inserted I
        INNER JOIN deleted D ON D.ID = I.ID
        LEFT JOIN dbo.tbl_Projects P ON P.[Name] = I.ProjectName
        WHERE D.Status = 1 AND I.Status IN (2, 98, 99)
          AND (P.ID IS NULL OR P.Status <> 1 OR P.ApproverUserID IS NULL
               OR P.ApproverUserID <> I.UpdatedBy
               OR I.ConductedBy = I.UpdatedBy
               OR NOT EXISTS (SELECT 1 FROM dbo.tbl_Users U
                              WHERE U.UserID = I.UpdatedBy AND U.Status = 1)
               OR NOT EXISTS (SELECT 1 FROM dbo.tbl_UserRoles R
                              WHERE R.UserID = I.UpdatedBy AND R.RoleID = 'PTW APPROVER'))
    )
        THROW 50003, 'Only the assigned team approver may process this TBM.', 1;

    INSERT INTO HEAData.dbo.EmailLogStatus
        (APP, Record_Status, [Key], [To], [From], [Subject], Body, CC, BCC, Created_at)
    SELECT 'ST', 1, I.[Key], U.EmailAddress, 'safetytools@hitachi.com',
           'TBM awaiting approval',
           'A Toolbox Meeting has been submitted for your team. Please sign in to HEA Digital Safety Tools to review it.',
           '', '', GETDATE()
    FROM inserted I
    LEFT JOIN deleted D ON D.ID = I.ID
    INNER JOIN dbo.tbl_Projects P ON P.[Name] = I.ProjectName
    INNER JOIN dbo.tbl_Users U ON U.UserID = P.ApproverUserID
    WHERE I.Status = 1 AND (D.ID IS NULL OR D.Status <> 1);
END;
GO
