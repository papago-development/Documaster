var reloadCurrentDiv = function () {
    const currentIndex = $(".documentLine");
    $(".documentLine").load();
console.log(currentIndex);
};

var uploadFile = function(e, documentType) {
    const formData = new FormData();
    formData.append("fileUpload", e.target.files[0]);
    formData.append("documentType", documentType);
    $.ajax({
         type: "POST",
        url: "upload",
        contentType: false,
        processData: false,
        data: formData
    }).success(function () {
        reloadCurrentDiv();
    })
    .fail(function(jqXhr) {
    });
};

var applyClickTransfer = function (element) {
    $(element).click(function () {
        const hiddenFileInput = $(this).parent().find('input[type="file"]');
        hiddenFileInput.click();
    });
};

var deletePhoto = function (photoId) {
    const formData = new FormData();
    formData.append("photoId", photoId);
    $.ajax({
        type: "POST",
        url: "DeletePhoto",
        contentType: false,
        processData: false,
        data: formData
    }).success(function () {
       reloadCurrentDiv();
    })
        .fail(function (jqXhr) {
        });
};
