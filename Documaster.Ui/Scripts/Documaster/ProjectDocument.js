var reloadCurrentTab = function () {
    const currentIndex = $("#tabs").tabs("option", "active");
    $("#tabs").tabs('load', currentIndex);
};

var uploadFile = function (e, projectId, requirementId) {
    const formData = new FormData();
    formData.append("fileUpload", e.target.files[0]);
    formData.append("projectId", projectId);
    formData.append("requirementId", requirementId);
    $.ajax ({
            type: "POST",
            url: "upload",
            contentType: false,
            processData: false,
            data: formData
        }).success (function () {
            reloadCurrentTab();
        })
        .fail (function (jqXhr) {
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
            reloadCurrentTab();
        })
        .fail(function (jqXhr) {
        });
};
