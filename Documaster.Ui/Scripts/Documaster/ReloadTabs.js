var reloadTabs = {
    reloadCurrentTab: function () {
        $("#tabs").tabs();
        const currentIndex = $("#tabs").tabs("option", "active");
        $("#tabs").tabs('load', currentIndex);
    }
};
