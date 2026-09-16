-- Apply to ePTW_MTE_UAT before deploying the matching web application.
-- Existing teams deliberately remain unassigned; configure each team's TBM approver
-- in Team Management before permitting new TBM submissions.
USE [ePTW_MTE_UAT];
GO

-- The existing password-reset workflow uses this queue. Do not change the
-- application schema if the mail service database is not available here.
IF DB_ID('HEAData') IS NULL OR OBJECT_ID('HEAData.dbo.EmailLogStatus', 'U') IS NULL
    THROW 50005, 'HEAData.dbo.EmailLogStatus is required for TBM email notifications.', 1;
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

-- The original procedure declares @ApprovedBy varchar(20), although user IDs
-- and tbl_TBMRecord.ApprovedBy allow 100 characters. Preserve the existing
-- upsert behavior while preventing approver ID truncation.
CREATE OR ALTER PROCEDURE dbo.Procedure_TBMRecord_InsertUpdate
    @ID int,
    @Key varchar(50),
    @ProjectName varchar(150),
    @MeetingDate datetime,
    @Supervisor varchar(100),
    @ConductedBy varchar(100),
    @ActionsPreviousCompleted varchar(1),
    @Description varchar(2000),
    @DescriptionPM varchar(2000),
    @Remarks varchar(2000),
    @TodayTeamActionGoal varchar(1500),
    @TodayTouchAndCall varchar(1500),
    @Feedback varchar(1500),
    @ReturnRejectReason varchar(150),
    @ReturnRejectDate datetime,
    @ApprovedBy varchar(100),
    @ApprovedDate datetime,
    @Latitude varchar(20),
    @Longitude varchar(20),
    @Status int,
    @Created datetime,
    @CreatedBy varchar(100),
    @Updated datetime,
    @UpdatedBy varchar(100),
    @SupervisorName varchar(220),
    @ConductedByName varchar(220),
    @Safety varchar(100),
    @SafetyName varchar(220),
    @ConductedPosition varchar(50),
    @ConductedCompany varchar(150),
    @ApproveName varchar(220),
    @ApprovePosition varchar(50),
    @ApproveCompany varchar(150),
    @ApproveRemarks varchar(2000),
    @SafetyDeclaration1 varchar(1),
    @SafetyDeclaration2 varchar(1),
    @SafetyDeclaration3 varchar(1),
    @SafetyDeclaration4 varchar(1)
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS (SELECT 1 FROM dbo.tbl_TBMRecord WHERE [Key] = @Key)
        UPDATE dbo.tbl_TBMRecord SET
            ProjectName = @ProjectName, MeetingDate = @MeetingDate,
            Supervisor = @Supervisor, ConductedBy = @ConductedBy,
            SupervisorName = @SupervisorName, ConductedByName = @ConductedByName,
            DescriptionPM = @DescriptionPM, Remarks = @Remarks,
            TodayTeamActionGoal = @TodayTeamActionGoal,
            TodayTouchAndCall = @TodayTouchAndCall, Feedback = @Feedback,
            ActionsPreviousCompleted = @ActionsPreviousCompleted,
            Description = @Description, ReturnRejectReason = @ReturnRejectReason,
            ReturnRejectDate = @ReturnRejectDate, ApprovedBy = @ApprovedBy,
            ApprovedDate = @ApprovedDate, Latitude = @Latitude,
            Longitude = @Longitude, Status = @Status,
            Created = @Created, CreatedBy = @CreatedBy,
            Updated = @Updated, UpdatedBy = @UpdatedBy,
            Safety = @Safety, SafetyName = @SafetyName,
            ConductedPosition = @ConductedPosition,
            ConductedCompany = @ConductedCompany,
            ApproveName = @ApproveName, ApprovePosition = @ApprovePosition,
            ApproveCompany = @ApproveCompany, ApproveRemarks = @ApproveRemarks,
            SafetyDeclaration1 = @SafetyDeclaration1,
            SafetyDeclaration2 = @SafetyDeclaration2,
            SafetyDeclaration3 = @SafetyDeclaration3,
            SafetyDeclaration4 = @SafetyDeclaration4
        WHERE [Key] = @Key;
    ELSE
        INSERT dbo.tbl_TBMRecord
            ([Key], ProjectName, MeetingDate, Supervisor, ConductedBy,
             SupervisorName, ConductedByName, ActionsPreviousCompleted,
             Description, DescriptionPM, TodayTeamActionGoal,
             TodayTouchAndCall, Feedback, ReturnRejectReason,
             ReturnRejectDate, ApprovedBy, ApprovedDate, Latitude, Longitude,
             Status, Created, CreatedBy, Updated, UpdatedBy, Safety,
             SafetyName, Remarks, ConductedPosition, ConductedCompany,
             ApproveName, ApprovePosition, ApproveCompany, ApproveRemarks,
             SafetyDeclaration1, SafetyDeclaration2, SafetyDeclaration3,
             SafetyDeclaration4)
        VALUES
            (@Key, @ProjectName, @MeetingDate, @Supervisor, @ConductedBy,
             @SupervisorName, @ConductedByName, @ActionsPreviousCompleted,
             @Description, @DescriptionPM, @TodayTeamActionGoal,
             @TodayTouchAndCall, @Feedback, @ReturnRejectReason,
             @ReturnRejectDate, @ApprovedBy, @ApprovedDate, @Latitude,
             @Longitude, @Status, @Created, @CreatedBy, @Updated, @UpdatedBy,
             @Safety, @SafetyName, @Remarks, @ConductedPosition,
             @ConductedCompany, @ApproveName, @ApprovePosition,
             @ApproveCompany, @ApproveRemarks, @SafetyDeclaration1,
             @SafetyDeclaration2, @SafetyDeclaration3, @SafetyDeclaration4);
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
