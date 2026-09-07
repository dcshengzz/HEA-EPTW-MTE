function OnFileUploadComplete(s, e) {
    if (e.callbackData !== "") {
        lblFileName.SetText(e.callbackData);
        btnDeleteFile.SetVisible(true);
    }
}

function OnClick(s, e) {
    callback.PerformCallback(lblFileName.GetText());
}

function OnCallbackComplete(s, e) {
    if (e.result === "ok") {
        lblFileName.SetText(null);
        btnDeleteFile.SetVisible(false);
    }
}