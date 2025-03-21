$(document).ready(function () {
    $("#treeData").hummingbird();

    $("#expandAll").click(function () {
        $("#treeData").hummingbird("expandAll");
    });
    $("#collapseAll").click(function () {
      $("#treeData").hummingbird("collapseAll");
    });
});
