$('#ddlDepartment').change(function () {

    var code = $('#ddlDepartment').val();
    $.getJSON("/departmentalsection/getsections/" + code, function (data) {
        var options = $("#ddlSection");
        $('#ddlSection option').remove();
        $.each(data, function (key, val) {
            options.append($("<option></option>").val(val.code).html(val.description));
        });
    });
});

//Date picker
$('#dateOfBirth').datetimepicker({
    format: 'L'
});


var Toast = Swal.mixin({
    toast: true,
    position: 'top-end',
    showConfirmButton: true,
    timer: 3000
});

function convertToBoolean(val) {
    switch (val) {
        case true:
        case "true":
        case 1:
        case "1":
        case "on":
        case "yes":
            return true;
        default:
            return false;
    }
}


window.onload = function () {

    if (convertToBoolean(hasError) == true && message != "") {
        //$(document).Toasts('create', {
        //    class: 'bg-danger toastCustom',
        //    title: 'Message',
        //    subtitle: '',
        //    body: message
        //});

        Toast.fire({
            icon: 'error',
            title: message
        });
    }
    else if (convertToBoolean(hasError) == false && message != "") {
        //$(document).Toasts('create', {
        //    class: 'bg-info toastCustom',
        //    title: 'Message',
        //    subtitle: '',
        //    body: message
        //});

        Toast.fire({
            icon: 'info',
            title: message
        });
    }

}