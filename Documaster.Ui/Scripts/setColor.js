function setColor(value) {
    var javaScriptColorDictionary = JSON.stringify(value);
    var setColor = function (element, color) {
        element.css("background-color", "#" + color + " !important");
    };

    var setTextColor = function (element, color) {
        element.css("color", "#" + color + "!important");
    };

   // $(document).ready(function () {
        $('.projectStatus').each(function () {
            var color = javaScriptColorDictionary[$(this).find("option:selected").text()];
            setColor($(this), color);
            setTextColor($(this), color);
        });

        $('.projectStatus option').each(function () {
            var color = javaScriptColorDictionary[$(this).text()];
            setColor($(this), color);
            setTextColor($(this), color);
        });

        $('.projectStatus').change(function () {
            var projectStatusId = $(this).val();
            var projectId = $(this).parent().parent().parent().siblings()[0].value;
            $.post("ChangeProjectStatus", { projectId, projectStatusId });
            var color = javaScriptColorDictionary[$(this).find("option:selected").text()];
            setColor($(this), color);
            setTextColor($(this), color);
        });
  // });
}