-- Apply after 20260916_TBM_team_approver.sql and before publishing the matching site.
-- Admin retains the five workflow roles used by the existing pending-list procedures.
USE [ePTW_MTE_UAT];
GO

SET XACT_ABORT ON;
BEGIN TRANSACTION;

IF NOT EXISTS (SELECT 1 FROM dbo.tbl_UserRoles WHERE UPPER(RoleID) LIKE '%ADMIN%')
    THROW 50010, 'No Admin role assignment was found. Review Admin role IDs before applying this migration.', 1;

;WITH AdminUsers AS
(
    SELECT DISTINCT UserID FROM dbo.tbl_UserRoles WHERE UPPER(RoleID) LIKE '%ADMIN%'
), RequiredRoles AS
(
    SELECT RoleID FROM (VALUES
        ('PTW ASSESSOR'), ('PTW SAFETY'), ('PTW APPROVER'),
        ('PTW CLOSURE'), ('CCP APPROVER')
    ) R(RoleID)
)
INSERT dbo.tbl_UserRoles (UserID, RoleID, Created, CreatedBy, Updated, UpdatedBy)
SELECT A.UserID, R.RoleID, GETDATE(), 'SYSTEM', GETDATE(), 'SYSTEM'
FROM AdminUsers A
CROSS JOIN RequiredRoles R
WHERE NOT EXISTS
(
    SELECT 1 FROM dbo.tbl_UserRoles Existing
    WHERE Existing.UserID = A.UserID AND Existing.RoleID = R.RoleID
);

COMMIT TRANSACTION;
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
    WHERE T.Status = 1 AND T.ProjectName = @ProjectName AND P.Status = 1
      AND
      (
          (P.ApproverUserID = @UserID AND EXISTS
              (SELECT 1 FROM dbo.tbl_UserRoles R
               WHERE R.UserID = @UserID AND R.RoleID = 'PTW APPROVER'))
          OR EXISTS
              (SELECT 1 FROM dbo.tbl_UserRoles R
               WHERE R.UserID = @UserID AND UPPER(R.RoleID) LIKE '%ADMIN%')
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
          AND (P.ID IS NULL OR P.Status <> 1 OR I.ConductedBy = I.UpdatedBy
               OR NOT EXISTS (SELECT 1 FROM dbo.tbl_Users U
                              WHERE U.UserID = I.UpdatedBy AND U.Status = 1)
               OR
               (
                   NOT EXISTS (SELECT 1 FROM dbo.tbl_UserRoles R
                               WHERE R.UserID = I.UpdatedBy AND UPPER(R.RoleID) LIKE '%ADMIN%')
                   AND (P.ApproverUserID IS NULL OR P.ApproverUserID <> I.UpdatedBy
                        OR NOT EXISTS (SELECT 1 FROM dbo.tbl_UserRoles R
                                       WHERE R.UserID = I.UpdatedBy AND R.RoleID = 'PTW APPROVER'))
               ))
    )
        THROW 50003, 'Only the assigned team approver or an Admin may process this TBM.', 1;

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
