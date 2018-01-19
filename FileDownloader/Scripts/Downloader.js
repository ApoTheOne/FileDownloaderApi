function Download() {
    var xhr = new XMLHttpRequest();

    var checkedValues = [];
    var inputElements = document.getElementsByClassName('imageOptions');

    //var checkedValues = [];
    //$.each($("input[name='chk_options']:checked"), function () {
    //    checkedValues.push($(this).val());
    //});
    for (var i = 0; inputElements[i]; ++i) {
        var id;
        var checkBox = inputElements[i].getElementsByClassName('chkBox')[0];
        var hiddenId = inputElements[i].getElementsByClassName('hdnId')[0];
        if (checkBox.checked === true) {
            checkedValues.push({ id: hiddenId.value })
        }
    }

    var url = "http://localhost:64069/api/Values/DownloadMultipleFile"
    xhr.open("POST", url, true);
    xhr.setRequestHeader("Content-type", "application/json");
    xhr.send(JSON.stringify(checkedValues));
}