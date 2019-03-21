var colorTool = function (colorDictionary) {
    var setStatusDropdownColor = function (element, statusName) {
        var color = colorDictionary[statusName];
        element.css("background-color", "#" + color + " !important");
        element.css("color", "#" + color + "!important");
    };

    return { setStatusDropdownColor: setStatusDropdownColor}
}