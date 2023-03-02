var page = {
    sourceUrl: "/employee/displayall",
    tableContainer: "employee-list",
    EmpID: null,
    table: null,

};

var EmployeeData = function EmployeeData() {
    this.employeeTable = null;
    this.employeeTable = function () {
        this.columns = [
            {
                "data": null,
                "className": "text-center",
                "render": function (data, type, row, meta) {
                    var param = row.empID;
                    var content =
                        `<div class='row'>
                            <div class='col-sm-12' style='text-align:center;'>
                            <div class="form-check">
                            <input class='checkBox form-check-input' id='chkSelect' type='checkbox' value='`+ param + `'/>
                            </div>
                            </div>
                            </div>`;

                    return content;
                },
                "orderable": false,
                "autoWidth": false,
                "width": 70
            },
            {
                "data": null,
                "render": function (data, type, row, meta) {
                    return `<div class='row'>
                            <div class='col-sm-4'><a onclick='ViewEmployee(this)' href='#'>View</a></div>
                            <div class='col-sm-4'><a onclick='EditEmployee(this)' href='#'>Edit</a></div>
                            <div class='col-sm-4'><a onclick='DeleteEmployee(this)' href='#'>Delete</a></div>
                            </div>`;
                },
                "orderable": false,
                "searchable": false,
                "autoWidth": false,
                "width": 500
            },
            {
                "data": "empID", "display": "Employee ID",
                "autoWidth": false,
                "searchable": true,
                "width": 250

            },
            {
                "data": "lastName", "display": "Last Name",
                "autoWidth": false,
                "searchable": true,
                "width": 250
            },
            {
                "data": "firstName", "display": "First Name",
                "autoWidth": false,
                "searchable": true,
                "width": 250
            },
            {
                "data": "middleName", "display": "Middle Name",
                "autoWidth": false,
                "searchable": true,
                "width": 250
            },
            {
                "data": "department.description", "display": "Department",
                "autoWidth": false,
                "searchable": true,
                "width": 250
            }
            ,
            {
                "data": "departmentSection.description", "display": "Department Section",
                "autoWidth": false,
                "searchable": true,
                "width": 250
            }
            ,
            {
                "data": "civilStatus.description", "display": "Civil Status",
                "autoWidth": false,
                "searchable": true,
                "width": 250
            }

        ];

        this.generateTable = function (url, containerId, id, addClass, order) {
            var $table = createHTMLTable(containerId, id, this.columns, addClass);
            $table = $table.DataTable({
                "sAjaxSource": url,
                "bServerSide": true,
                "bProcessing": true,
                "bSearchable": true,
                "scrollX": 450,
                "scrollY": 450,
                "order": [],
                "language": {
                    "emptyTable": "No record found.",
                    "processing":
                        '<i class="fa fa-spinner fa-spin fa-3x fa-fw" style="color:#2a2b2b;"></i><span class="sr-only">Loading...</span> '
                },
                "columns": this.columns
                ,
                "search": {
                    "return": true,
                },
                "responsive": true
            });
            return $table;
        }
    }

}

var emp = new EmployeeData();

function GenerateList() {
    var table = new emp.employeeTable();

    page.table = table.generateTable(page.sourceUrl, page.tableContainer);
}

function ViewEmployee(empId) {
    $("#message").empty();
    page.EmpID = page.table.row($(empId).closest("tr")).data().empID;
    var url = '/employee/' + page.EmpID;
    window.location.href = url;
}

function EditEmployee(empId) {
    $("#message").empty();
    page.EmpID = page.table.row($(empId).closest("tr")).data().empID;
    var url = '/employee/edit/' + page.EmpID;
    window.location.href = url;
}

function DeleteEmployee(empId) {
    $("#message").empty();

    page.EmpID = page.table.row($(empId).closest("tr")).data().empID;

    var result = confirm("Are you sure you want to delete " + page.EmpID + "?");

    if (result) {
        var url = '/employee/delete/' + page.EmpID;
        window.location.href = url;
    }
}


function DeleteSelectedEmployee(empId) {
    $("#message").empty();

    page.EmpID = page.table.row($(empId).closest("tr")).data().empID;

    var result = confirm("Are you sure you want to delete " + page.EmpID + "?");

    if (result) {
        var url = '/employee/delete/' + page.EmpID;
        window.location.href = url;
    }
}


//$('#chkSelectAll').click(function () {
//    if ($(this).is(":checked")) {
//        var checkboxes = document.querySelectorAll('input[type=checkbox]:checked')

//        for (var i = 0; i < checkboxes.length; i++)
//            checkboxes[i].prop("checked", true)
//    }
//    else {
//        var checkboxes = document.querySelectorAll('input[type=checkbox]:checked')

//        for (var i = 0; i < checkboxes.length; i++)
//            checkboxes[i].prop("checked", false)
//    }
//});


$("#delete").click(function () {
    var arrayData = new Array();

    var checkboxes = document.querySelectorAll('input[type=checkbox]:checked')

    for (var i = 0; i < checkboxes.length; i++)
        arrayData.push(checkboxes[i].value)

    arrayData = arrayData.slice(0, -1);

    $.ajax({
        type: "POST",
        url: '/employee/deleteselected',
        data: { arrayOfValues: arrayData },
        success: function () {
            GenerateList();

            hasError = 'false';
            message = 'Selected Records Successfully Deleted';

            $(document).Toasts('create', {
                class: 'bg-success toastCustom',
                title: 'Message',
                subtitle: '',
                body: message,
                autohide: true,
                delay: 3000,
            });


        },
        error: function () { }
    });
});

var Toast = Swal.mixin({
    toast: true,
    position: 'top-end',
    showConfirmButton: false,
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
        $(document).Toasts('create', {
            class: 'bg-danger toastCustom',
            title: 'Error',
            subtitle: '',
            body: message,
            autohide: true,
            delay: 3000,
        });

        //Toast.fire({
        //    icon: 'error',
        //    title: '',
        //    body: message
        //});
    }
    else if (convertToBoolean(hasError) == false && message != "") {
        $(document).Toasts('create', {
            class: 'bg-info toastCustom',
            title: 'Message',
            subtitle: '',
            body: message,
            autohide: true,
            delay: 3000,
        });

        //Toast.fire({
        //    icon: 'info',
        //    title: '',
        //    body: message
        //});
    }

}

$(document).ready(function () {
    GenerateList();
});