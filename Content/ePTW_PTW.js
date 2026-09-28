function OnFileUploadComplete(s, e) {
    if (e.isValid === false) {
        alert(e.errorText && /4194304|maximum allowed size/i.test(e.errorText)
            ? "File size exceeds the maximum allowed size, which is 4MB."
            : (e.errorText || "The selected file is not allowed."));
        return;
    }
    if (e.callbackData !== "") {
        lblFileName.SetText(e.callbackData);
        btnDeleteFile.SetVisible(true);
    }
    cbAfterUploadFile.PerformCallback();
    cbAfterUploadDoc.PerformCallback();
}

function OnClick(s, e) {
    callback.PerformCallback(lblFileName.GetText());
}

function OnCallbackComplete(s, e) {
    if (e.result === "ok") {
        
    }
}
