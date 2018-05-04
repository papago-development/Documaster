var uploadFile = function (e) {
    const formData = new FormData();
    formData.append("file", e.target.files[0]);

    $.ajax({
        type: "POST",
        url: "requirement/upload",
        contentType: false,
        processData: false,
        data: formData
    }).fail(function (jqXhr) {
    });
};

var applyClickTransfer = function (e) {

    $(e).parent().parent().find('input[type="file"]').trigger("click");
    $(e).change(function (x) {
        uploadFile(x);
    });
};