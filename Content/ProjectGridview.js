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
        //pageToolbar.GetItemByName("Edit").SetEnabled(enabled);
        //pageToolbar.GetItemByName("Lock").SetEnabled(enabled);
        //pageToolbar.GetItemByName("Unlock").SetEnabled(enabled);
        //pageToolbar.GetItemByName("Delete").SetEnabled(enabled);
        //pageToolbar.GetItemByName("Export").SetEnabled(enabled);
        //pageToolbar.GetItemByName("Edit").SetEnabled(gridView.GetFocusedRowIndex() !== -1);
    }




    function deleteEquipmentRecord() {
        if (confirm('Confirm Delete?')) {
            gvEquipment.PerformCallback('delete');
        }
    }
    function deleteStaffRecord() {
        if (confirm('Confirm Delete?')) {
            gvStaff.PerformCallback('delete');
        }
    }
    function deleteConstructorRecord() {
        if (confirm('Confirm Delete?')) {
            gvConstructor.PerformCallback('delete');
        }
    }

    function onPageToolbarItemClick(s, e) {
        switch (e.item.name) {
            case "AddEquipment":
                gvEquipment.AddNewRow();
                break;
            case "DeleteEquipment":
                deleteEquipmentRecord();
                break;

            case "AddManpower":
                gvStaff.AddNewRow();
                break;
            case "DeleteManpower":
                deleteStaffRecord();
                break;

            case "AddConstructor":
                gvConstructor.AddNewRow();
                break;
            case "DeleteConstructor":
                deleteConstructorRecord();
                break;
            //case "UnlockEquipment":
            //    unlockequipmentRecords();
            //    break;



            //case "ToggleFilterPanel":
            //    toggleFilterPanel();
            //    break;
            //case "New":
            //    gridView.AddNewRow();
            //    break;
            //case "Edit":
            //    gridView.StartEditRow(gridView.GetFocusedRowIndex());
            //    break;
            //case "Lock":
            //    lockSelectedRecords();
            //    break;
            //case "Unlock":
            //    unlockSelectedRecords();
            //    break;
        }
    }






    //function lockSelectedRecords() {
    //    gridView.PerformCallback('lock');
    //}
    //function unlockSelectedRecords() {
    //    gridView.PerformCallback('unlock');
    //}
    //function deleteSelectedRecords() {
    //    if (confirm('Confirm Delete?')) {
    //        gridView.PerformCallback('delete');
    //    }
    //}
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