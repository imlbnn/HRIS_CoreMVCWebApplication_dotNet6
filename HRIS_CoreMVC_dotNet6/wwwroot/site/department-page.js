var page = {
    sourceUrl: "/department/displayall",
    tableContainer: "department-list"
};

var DepartmentData = function DepartmentData() {
    this.departmentTable = null;
    this.departmentTable = function () {
        this.columns = [
            {
                "data": "code",
                "display": "Department Code",
                "autoWidth": true,
                "searchable": true
            },
            {
                "data": "description",
                "display": "Department Name",
                "autoWidth": true,
                "searchable": true
            }];


        this.generateTable = function (url, containerId, id, addClass, order) {
            var $table = createHTMLTable(containerId, id, this.columns, addClass);
            $table = $table.DataTable({
                "sAjaxSource": url,
                "bServerSide": true,
                "bProcessing": true,
                "bSearchable": true,
                "scrollX": true,
                "scrollY": true,
                "order": [[0, 'asc']],
                "language": {
                    "emptyTable": "No record found.",
                    "processing":
                        '<i class="fa fa-spinner fa-spin fa-3x fa-fw" style="color:#2a2b2b;"></i><span class="sr-only">Loading...</span> '
                },
                "columns": this.columns
            });
            return $table;


        }
    }
}

var depData = new DepartmentData();

function GenerateList() {
    var table = new depData.departmentTable();

    datatable = table.generateTable(page.sourceUrl, page.tableContainer);
}

$(document).ready(function () {
    GenerateList();
});