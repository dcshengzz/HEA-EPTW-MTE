SET XACT_ABORT ON;
BEGIN TRANSACTION;

IF COL_LENGTH('dbo.tbl_TBMHazard', 'Feedback') IS NULL
BEGIN
    ALTER TABLE dbo.tbl_TBMHazard
        ADD Feedback varchar(1000) NOT NULL
            CONSTRAINT DF_tbl_TBMHazard_Feedback DEFAULT ('') WITH VALUES;
END;

ALTER TABLE dbo.tbl_TBMHazard ALTER COLUMN ActionToTaken varchar(1500) NOT NULL;

IF COL_LENGTH('dbo.tbl_CCPRecord', 'TeamLeaderName') IS NULL
BEGIN
    ALTER TABLE dbo.tbl_CCPRecord
        ADD TeamLeaderName varchar(220) NOT NULL
            CONSTRAINT DF_tbl_CCPRecord_TeamLeaderName DEFAULT ('') WITH VALUES;
END;

IF COL_LENGTH('dbo.tbl_CCPRecord', 'CoworkerName') IS NULL
BEGIN
    ALTER TABLE dbo.tbl_CCPRecord
        ADD CoworkerName varchar(220) NOT NULL
            CONSTRAINT DF_tbl_CCPRecord_CoworkerName DEFAULT ('') WITH VALUES;
END;
GO

CREATE OR ALTER PROCEDURE dbo.Procedure_TBMHazard_InsertUpdate
    @ID int,
    @Key varchar(50),
    @WorkActivity varchar(500),
    @Others varchar(500),
    @CauseOfHazard varchar(1000),
    @HappenAsResult varchar(1000),
    @ActionToTaken varchar(1500),
    @ActionRemarks varchar(1500),
    @Feedback varchar(1000),
    @Created datetime,
    @CreatedBy varchar(100),
    @Updated datetime,
    @UpdatedBy varchar(100)
AS
BEGIN
    SET NOCOUNT ON;
    IF NOT EXISTS (SELECT 1 FROM dbo.tbl_TBMHazard WHERE ID = @ID AND [Key] = @Key)
        INSERT dbo.tbl_TBMHazard
            ([Key], WorkActivity, Others, CauseOfHazard, HappenAsResult,
             ActionToTaken, ActionRemarks, Feedback, Created, CreatedBy, Updated, UpdatedBy)
        VALUES
            (@Key, @WorkActivity, '', @CauseOfHazard, @HappenAsResult,
             @ActionToTaken, @ActionRemarks, @Feedback, @Created, @CreatedBy, @Updated, @UpdatedBy);
    ELSE
        UPDATE dbo.tbl_TBMHazard SET
            WorkActivity = @WorkActivity,
            Others = '',
            CauseOfHazard = @CauseOfHazard,
            HappenAsResult = @HappenAsResult,
            ActionToTaken = @ActionToTaken,
            ActionRemarks = @ActionRemarks,
            Feedback = @Feedback,
            Updated = @Updated,
            UpdatedBy = @UpdatedBy
        WHERE ID = @ID AND [Key] = @Key;
END;
GO

CREATE OR ALTER PROCEDURE dbo.Procedure_CCPRecord_InsertUpdate
    @ID int,
    @Key varchar(50),
    @ProjectName varchar(150),
    @EquipmentName varchar(100),
    @KeyActivitiesName varchar(150),
    @ActivitiesDate datetime,
    @Description varchar(500),
    @TeamLeaderName varchar(220),
    @CoworkerName varchar(220),
    @Latitude varchar(20),
    @Longitude varchar(20),
    @Status int,
    @Created datetime,
    @CreatedBy varchar(100),
    @Updated datetime,
    @UpdatedBy varchar(100),
    @ReturnRejectReason varchar(150),
    @ApprovedBy varchar(100),
    @ApprovedDate datetime,
    @ReturnRejectDate datetime
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS (SELECT 1 FROM dbo.tbl_CCPRecord WHERE [Key] = @Key)
        UPDATE dbo.tbl_CCPRecord SET
            EquipmentName = @EquipmentName,
            [Description] = '',
            TeamLeaderName = @TeamLeaderName,
            CoworkerName = @CoworkerName,
            KeyActivitiesName = @KeyActivitiesName,
            ActivitiesDate = @ActivitiesDate,
            Latitude = @Latitude,
            Longitude = @Longitude,
            Updated = @Updated,
            UpdatedBy = @UpdatedBy,
            [Status] = @Status,
            ReturnRejectReason = @ReturnRejectReason,
            ApprovedBy = @ApprovedBy,
            ApprovedDate = @ApprovedDate,
            ReturnRejectDate = @ReturnRejectDate
        WHERE [Key] = @Key;
    ELSE
        INSERT dbo.tbl_CCPRecord
            ([Key], ProjectName, EquipmentName, KeyActivitiesName, ActivitiesDate,
             [Description], TeamLeaderName, CoworkerName, Latitude, Longitude,
             Updated, UpdatedBy, Created, CreatedBy, [Status], ReturnRejectReason,
             ApprovedBy, ApprovedDate, ReturnRejectDate)
        VALUES
            (@Key, @ProjectName, @EquipmentName, @KeyActivitiesName, @ActivitiesDate,
             '', @TeamLeaderName, @CoworkerName, @Latitude, @Longitude,
             @Updated, @UpdatedBy, @Created, @CreatedBy, @Status, @ReturnRejectReason,
             @ApprovedBy, @ApprovedDate, @ReturnRejectDate);
END;
GO

CREATE OR ALTER PROCEDURE dbo.Procedure_CCPReport
    @DateFrom datetime,
    @DateTo datetime
AS
BEGIN
    SET NOCOUNT ON;
    SELECT
        C.ProjectName,
        C.EquipmentName AS RegistrationNo,
        E.EquipmentName,
        E.[Description],
        MAX(CASE WHEN C.KeyActivitiesName = 'Entrance Barricade' THEN C.ApprovedDate END) AS CCP01,
        MAX(CASE WHEN C.KeyActivitiesName = 'Life lines & Safety Hook' THEN C.ApprovedDate END) AS CCP02,
        MAX(CASE WHEN C.KeyActivitiesName = 'Hoisting of Guiderails' THEN C.ApprovedDate END) AS CCP03,
        MAX(CASE WHEN C.KeyActivitiesName = 'Setting up of Traction Machine' THEN C.ApprovedDate END) AS CCP04,
        MAX(CASE WHEN C.KeyActivitiesName = 'Hoisting of Control Panel' THEN C.ApprovedDate END) AS CCP05,
        MAX(CASE WHEN C.KeyActivitiesName = 'Hoisting of Cage Platform' THEN C.ApprovedDate END) AS CCP06,
        MAX(CASE WHEN C.KeyActivitiesName = 'Hoisting & securing of Cwt Frame' THEN C.ApprovedDate END) AS CCP07
    FROM dbo.tbl_CCPRecord C
    LEFT JOIN dbo.tbl_Equipment E
      ON E.RegistrationNo = C.EquipmentName
     AND E.ProjectName = C.ProjectName
    WHERE C.[Status] <> 97
      AND C.ActivitiesDate >= CONVERT(date, @DateFrom)
      AND C.ActivitiesDate < DATEADD(day, 1, CONVERT(date, @DateTo))
    GROUP BY C.ProjectName, C.EquipmentName, E.EquipmentName, E.[Description]
    ORDER BY C.ProjectName, C.EquipmentName;
END;
GO

CREATE OR ALTER PROCEDURE dbo.Procedure_GetEquipmentList
    @ProjectName varchar(150)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT * FROM dbo.tbl_Equipment
    WHERE ProjectName = @ProjectName AND [Status] <> 97;
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
    WHERE T.[Status] = 1
      AND T.ProjectName = @ProjectName
      AND P.[Status] = 1
      AND
      (
          (P.ApproverUserID = @UserID AND EXISTS
              (SELECT 1 FROM dbo.tbl_UserRoles R
               WHERE R.UserID = @UserID AND R.RoleID = 'PTW APPROVER'))
          OR EXISTS
              (SELECT 1 FROM dbo.tbl_UserRoles R
               WHERE R.UserID = @UserID AND R.RoleID LIKE '%ADMIN%')
      )
    ORDER BY T.MeetingDate DESC;
END;
GO

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
        WHERE I.[Status] = 1 AND (D.ID IS NULL OR D.[Status] <> 1)
          AND (P.ID IS NULL OR P.[Status] <> 1 OR U.ID IS NULL OR U.[Status] <> 1
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
        WHERE D.[Status] = 1 AND I.[Status] IN (2, 98, 99)
          AND NOT EXISTS
          (
              SELECT 1 FROM dbo.tbl_UserRoles AdminRole
              WHERE AdminRole.UserID = I.UpdatedBy AND AdminRole.RoleID LIKE '%ADMIN%'
          )
          AND (P.ID IS NULL OR P.[Status] <> 1 OR P.ApproverUserID IS NULL
               OR P.ApproverUserID <> I.UpdatedBy
               OR I.ConductedBy = I.UpdatedBy
               OR NOT EXISTS (SELECT 1 FROM dbo.tbl_Users U
                              WHERE U.UserID = I.UpdatedBy AND U.[Status] = 1)
               OR NOT EXISTS (SELECT 1 FROM dbo.tbl_UserRoles R
                              WHERE R.UserID = I.UpdatedBy AND R.RoleID = 'PTW APPROVER'))
    )
        THROW 50003, 'Only the assigned team approver or an administrator may process this TBM.', 1;

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
    WHERE I.[Status] = 1 AND (D.ID IS NULL OR D.[Status] <> 1);
END;
GO

COMMIT TRANSACTION;
GO
