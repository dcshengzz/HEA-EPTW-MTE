(function () {
    function onGridViewInit(s, e) {
        AddAdjustmentDelegate(adjustGridView);
        updateToolbarButtonsState();
    }
    function onGridViewSelectionChanged(s, e) {
        updateToolbarButtonsState();
    }
    function adjustGridView() {
        gridView.AdjustControl();
    }
    function updateToolbarButtonsState() {
        var enabled = gridView.GetSelectedRowCount() > 0;
        var edititem = pageToolbar.GetItemByName("Edit");
        if (pageToolbar.GetItemByName("Edit") != null)
            pageToolbar.GetItemByName("Edit").SetEnabled(enabled);
        if (pageToolbar.GetItemByName("Lock") != null)
            pageToolbar.GetItemByName("Lock").SetEnabled(enabled);
        if (pageToolbar.GetItemByName("Unlock") != null)
            pageToolbar.GetItemByName("Unlock").SetEnabled(enabled);
        //pageToolbar.GetItemByName("ResetPassword").SetEnabled(enabled);
        //pageToolbar.GetItemByName("Export").SetEnabled(enabled);
        //pageToolbar.GetItemByName("Edit").SetEnabled(gridView.GetFocusedRowIndex() !== -1);
    }
    function onPageToolbarItemClick(s, e) {
        switch (e.item.name) {
            //case "ToggleFilterPanel":
            //    toggleFilterPanel();
            //    break;
            case "New":
                gridView.AddNewRow();
                break;
            case "NewDetail":
                detailgridView.AddNewRow();
                break;
            case "DeleteDetail":
                deleteSelectedRecords();
                break;
            case "Edit":
                gridView.StartEditRow(gridView.GetFocusedRowIndex());
                break;
            case "Lock":
                lockSelectedRecords();
                break;
            case "Unlock":
                unlockSelectedRecords();
                break;
            case "ResetPassword":
                resetRecords();
                break;
        }
    }
    function deleteSelectedRecords() {
        if (confirm('Confirm Delete the User Role ?')) {
            detailgridView.PerformCallback('delete');
        }
    }
    function lockSelectedRecords() {
        gridView.PerformCallback('lock');
    }
    function unlockSelectedRecords() {
        gridView.PerformCallback('unlock');
    }
    function resetRecords() {
        if (confirm('Confirm Reset the Account, the Password will send thru email ?')) {
            gridView.PerformCallback('ResetPassword');
        }
    }
    function onFiltersNavBarItemClick(s, e) {
        var filters = {
            All: "",
            Active: "[Status] = 1",
            Bugs: "[Kind] = 1",
            Suggestions: "[Kind] = 2",
            HighPriority: "[Priority] = 1"
        };
        gridView.ApplyFilter(filters[e.item.name]);
        HideLeftPanelIfRequired();
    }
    function toggleFilterPanel() {
        filterPanel.Toggle();
    }
    function onFilterPanelExpanded(s, e) {
        adjustPageControls();
        searchButtonEdit.SetFocus();
    }

    window.onGridViewInit = onGridViewInit;
    window.onGridViewSelectionChanged = onGridViewSelectionChanged;
    window.onPageToolbarItemClick = onPageToolbarItemClick;
    window.onFilterPanelExpanded = onFilterPanelExpanded;
    window.onFiltersNavBarItemClick = onFiltersNavBarItemClick;

})();