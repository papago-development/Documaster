var uploadFile = function (e, projectId, requirementId, customizeTabId) {
    const formData = new FormData();
    formData.append("fileUpload", e.target.files[0]);
    formData.append("projectId", projectId);
    formData.append("requirementId", requirementId);
    formData.append("customizeTabId", customizeTabId);

    $.ajax({
        type: "POST",
        url: "upload",
        contentType: false,
        processData: false,
        data: formData
    }).success(function () {
        reloadTabs.reloadCurrentTab();
    })
        .fail(function (jqXhr) {
            console.log(jqXhr);
        });
};

var applyClickTransfer = function (element) {
    $(element).click(function () {
        const hiddenFileInput = $(this).parent().find('input[type="file"]');
        hiddenFileInput.click();
    });
};

var deleteDocument = function (documentId) {
    const formData = new FormData();
    formData.append("documentId", documentId);
    $.ajax({
        type: "POST",
        url: "DeleteDocument",
        contentType: false,
        processData: false,
        data: formData
    }).success(function () {
        reloadTabs.reloadCurrentTab();
    })
        .fail(function (jqXhr) {
        });
};