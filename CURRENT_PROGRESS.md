# ePTW progress handoff (2026-09-18)

This note records the local working-tree state for continuation after `/clear`.
It is not a UAT deployment record. Preserve the existing dirty worktree; do not
reset or discard unrelated changes.

## Environment

- Project: `HEA.ePTW` under `HEA-EPTW-MTE/EPTW`.
- Local site was reachable at `http://localhost:50981` through IIS Express.
- The local `Web.config` currently points at SQL Server `.`, database
  `ePTW_MTE_UAT`, and enables the Debug/localhost CAPTCHA bypass. Do **not**
  publish this local configuration or its credentials to UAT.
- No UAT web application publish or UAT SQL migration was performed in this work.

## Latest request: left menu and CCP activities

- `Root.master.cs`: stopped injecting the “Admin Access” group. The database-
  driven left menu now filters “Admin Access”, “Admin Settings”, and “Document
  Management” (including direct children). This hides navigation only; it does
  not revoke page permissions. The right-side configuration menu was not changed.
- `CCP/NewCCP.aspx.cs`: keeps “Maintenance Work” and “Hoisting of lift cage”
  present and deduplicated on first load and postback, including an older
  session list.
- `Database/20260918_CCP_key_activities.sql`: new, idempotent CCP-only
  migration to activate or insert the two master-data rows in the target
  `ePTW_MTE_UAT` database. It was run successfully against the **local**
  database; each activity has exactly one active local row. Run it on the
  actual UAT database before publishing the matching application.
- The web project compiled successfully with Visual Studio 2022 Enterprise
  MSBuild. Existing Newtonsoft.Json version-conflict and unused-variable
  warnings remain. `git diff --check` passed for the latest code changes.
- A post-change browser UI retest of the left menu has **not** been done.
  The two CCP options were visible in an earlier local dropdown screenshot;
  the latest binding code compiled and the local master rows were verified.

## Earlier ePTW work in the same dirty tree

- TBM briefing text and CCP activity master-data changes:
  `Database/20260918_TBM_CCP_master_data.sql` (also idempotently touches the
  same two CCP rows). Local TBM page showed all 11 requested briefing items.
- CCP Pending Approval menu/page exists; local page loaded but had no records,
  so an approval action was not exercised.
- Checklist N/A placement, disabled child controls, mandatory asterisks, and
  equipment display changes are in `Checklist/NewChecklist.aspx[.cs]`.
  All four active local teams had zero equipment, so detailed checklist
  sections could not be rendered for an end-to-end UI check.
- Team Management hides deleted teams (status 97) after navigation; local UI
  showed four active teams and excluded the two deleted teams. Its team-name
  field was confirmed editable without saving a rename. The “Not assigned”
  approver display is in code, but no active local team had a missing approver
  to verify it visually.
- PTW “Team Name” label rendered locally. PTW and CCP upload handlers produced
  the requested “4 MB” wording in a focused handler test; no actual oversized
  upload was performed.
- `Database/20260918_Team_rename.sql` is present. Its rename procedure was
  previously deployed to the local database and exercised in a rollback
  smoke test; no real team was renamed for UI testing.
- `Database/20260917_Admin_full_access.sql` is present. Recheck its target
  deployment status before relying on it.

## Local QA account

- User ID: `qa.local@eptw.test.invalid`; active, assigned to Team M1.
- Roles: `TBM USER`, `PTW USER`, `CCP USER`, `CKL USER`.
  It has **no** admin or approval role.
- The account was created only in the local `ePTW_MTE_UAT` database with
  `CreatedBy = CODEX_LOCAL_QA_20260918`. Sign-in and Team M1 visibility were
  verified on the local site.
- The password was given to the user in chat. It is intentionally **not**
  stored in this file or the repository. Ask the user for it or reset the
  local account if needed after `/clear`.
- A separate temporary account used for earlier screenshots was removed;
  its 11 pre-existing role rows were left unchanged.

## Evidence and next steps

- Earlier local screenshots: `C:/Users/73068002/OneDrive - Hitachi Group/HEA-EPTW-MTE/screenshorts`
  (14 PNG files; the folder name is intentionally spelled `screenshorts`).
- Publish the rebuilt application to UAT **without** the local `Web.config`
  settings, and run the required database scripts on the correct UAT database.
- Recheck the left menu with an admin login and a normal user login in UAT.
  Recheck both CCP dropdown choices there; selecting either may have no
  question template unless corresponding template-detail data is supplied.
- Exercise a real CCP pending-approval record, checklist with equipment, a
  missing-approver team, and an actual >4 MB upload when suitable UAT data is
  available. Do not submit/delete/rename real records solely for a smoke test.
