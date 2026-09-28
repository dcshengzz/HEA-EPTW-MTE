-- Deploy to ePTW_MTE_UAT before publishing the matching web application.
-- Run a representative rename in UAT during a quiet period: this procedure
-- locks team-linked tables while it changes their ProjectName references.
-- Execute as the caller: EXECUTE AS OWNER cannot reach HEAData on servers
-- where cross-database impersonation is disabled.
USE [ePTW_MTE_UAT];
GO

CREATE OR ALTER PROCEDURE dbo.Procedure_Project_Rename
    @OldName varchar(150),
    @NewName varchar(150),
    @ApproverUserID varchar(100),
    @UpdatedBy varchar(100)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    SET @NewName = LTRIM(RTRIM(@NewName));
    IF NULLIF(@OldName, '') IS NULL OR NULLIF(@NewName, '') IS NULL
        THROW 50020, 'Both the existing and new team names are required.', 1;
    IF NULLIF(@UpdatedBy, '') IS NULL
        THROW 50021, 'The user making the change is required.', 1;
    IF @ApproverUserID IS NOT NULL AND NOT EXISTS
    (
        SELECT 1 FROM dbo.tbl_Users U
        WHERE U.UserID = @ApproverUserID AND U.Status = 1
          AND NULLIF(LTRIM(RTRIM(U.EmailAddress)), '') IS NOT NULL
          AND EXISTS (SELECT 1 FROM dbo.tbl_UserRoles R
                      WHERE R.UserID = U.UserID AND R.RoleID = 'PTW APPROVER')
    )
        THROW 50022, 'Select an active TBM approver with an email address.', 1;

    -- Every persisted team reference in this application is named ProjectName.
    -- Abort if a new schema stores one outside dbo or with an unsupported type.
    IF EXISTS
    (
        SELECT 1
        FROM sys.tables T
        JOIN sys.schemas S ON S.schema_id = T.schema_id
        JOIN sys.columns C ON C.object_id = T.object_id
        JOIN sys.types TY ON TY.user_type_id = C.user_type_id
        WHERE C.name = 'ProjectName'
          AND (S.name <> 'dbo' OR C.is_computed = 1
               OR TY.name NOT IN ('varchar', 'nvarchar', 'char', 'nchar'))
    )
        THROW 50023, 'Review unsupported ProjectName columns before renaming a team.', 1;

    -- A non-cascading FK to the team name cannot be updated safely in place.
    IF EXISTS
    (
        SELECT 1
        FROM sys.foreign_keys FK
        JOIN sys.foreign_key_columns FKC ON FKC.constraint_object_id = FK.object_id
        JOIN sys.columns RC ON RC.object_id = FKC.referenced_object_id
                           AND RC.column_id = FKC.referenced_column_id
        WHERE FK.referenced_object_id = OBJECT_ID('dbo.tbl_Projects')
          AND RC.name = 'Name' AND FK.update_referential_action <> 1
    )
        THROW 50024, 'A non-cascading team-name foreign key requires DBA review.', 1;

    DECLARE @TableName sysname;
    DECLARE @Sql nvarchar(max);

    BEGIN TRY
        BEGIN TRANSACTION;

        IF NOT EXISTS
        (
            SELECT 1 FROM dbo.tbl_Projects WITH (UPDLOCK, HOLDLOCK)
            WHERE [Name] = @OldName
        )
            THROW 50025, 'The team no longer exists.', 1;

        IF EXISTS
        (
            SELECT 1 FROM dbo.tbl_Projects WITH (UPDLOCK, HOLDLOCK)
            WHERE [Name] = @NewName AND [Name] <> @OldName
        )
            THROW 50026, 'Another team already uses that name.', 1;

        UPDATE dbo.tbl_Projects
        SET [Name] = @NewName,
            ApproverUserID = COALESCE(@ApproverUserID, ApproverUserID),
            Updated = GETDATE(),
            UpdatedBy = @UpdatedBy
        WHERE [Name] = @OldName;

        IF @@ROWCOUNT <> 1
            THROW 50027, 'The team rename did not update exactly one team.', 1;

        -- Includes PTW, TBM, CCP, checklists, equipment, documents, and
        -- team membership tables without assuming their physical table names.
        -- TABLOCKX/HOLDLOCK prevents a concurrent insert with the old name
        -- into a table that has already been updated before commit.
        DECLARE TeamReferenceTables CURSOR LOCAL FAST_FORWARD FOR
            SELECT T.name
            FROM sys.tables T
            JOIN sys.schemas S ON S.schema_id = T.schema_id
            JOIN sys.columns C ON C.object_id = T.object_id
            WHERE S.name = 'dbo' AND C.name = 'ProjectName'
            ORDER BY T.name;

        OPEN TeamReferenceTables;
        FETCH NEXT FROM TeamReferenceTables INTO @TableName;
        WHILE @@FETCH_STATUS = 0
        BEGIN
            SET @Sql = N'UPDATE dbo.' + QUOTENAME(@TableName) +
                       N' WITH (TABLOCKX, HOLDLOCK) SET ProjectName = @NewName' +
                       N' WHERE ProjectName = @OldName;';
            EXEC sys.sp_executesql @Sql,
                N'@OldName varchar(150), @NewName varchar(150)',
                @OldName = @OldName, @NewName = @NewName;
            FETCH NEXT FROM TeamReferenceTables INTO @TableName;
        END;
        CLOSE TeamReferenceTables;
        DEALLOCATE TeamReferenceTables;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        IF CURSOR_STATUS('local', 'TeamReferenceTables') >= 0
            CLOSE TeamReferenceTables;
        IF CURSOR_STATUS('local', 'TeamReferenceTables') >= -1
            DEALLOCATE TeamReferenceTables;
        THROW;
    END CATCH
END;
GO
