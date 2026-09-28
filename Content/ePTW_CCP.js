function OnFileUploadComplete(s, e) {
    if (e.isValid === false) {
        alert(e.errorText && /4194304|maximum allowed size/i.test(e.errorText)
            ? "File size exceeds the maximum allowed size, which is 4MB."
            : (e.errorText || "The selected file is not allowed."));
        return;
    }
    if (e.callbackData !== "") {
        if (typeof lblFileName !== "undefined") lblFileName.SetText(e.callbackData);
        if (typeof btnDeleteFile !== "undefined") btnDeleteFile.SetVisible(true);
    }
    if (typeof cbAfterUploadDoc !== "undefined") cbAfterUploadDoc.PerformCallback();
    if (typeof cbDisplayImage !== "undefined") cbDisplayImage.PerformCallback();
}

function OnClick(s, e) {
    callback.PerformCallback(lblFileName.GetText());
}

function OnCallbackComplete(s, e) {
    if (e.result === "ok") {
        
    }
}

