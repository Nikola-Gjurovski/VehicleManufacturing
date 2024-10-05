var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData2').DataTable({
        "ajax": {
            "url": "/VehicleFormulas/GetAll",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "name", "width": "20%" },
            { "data": "engines", "width": "15%" },
            { "data": "chassis", "width": "20%" },
            { "data": "doors", "width": "15%" },
            { "data": "wheels", "width": "15%" },
            {
                "data": "image",
                "render": function (data) {
                    return '<img src="' + data + '" alt="Image" class="img-thumbnail" style="max-width:100px; max-height:100px;"/>';
                },
                "width": "10%"
            },
            {
                "data": "id",
                "render": function (data, type, row) {
                    return `
                        <div class="btn-group" role="group" aria-label="Actions">
                            <a href="/VehicleFormulas/Edit/${data}" class="btn btn-sm btn-primary">Edit</a>
                            <a href="/VehicleFormulas/Details/${data}" class="btn btn-sm btn-info">Details</a>
                            <a href="/VehicleFormulas/Delete/${data}" class="btn btn-sm btn-danger">Delete</a>
                        </div>
                    `;
                },
                "width": "15%"
            }
        ],
        "responsive": true,
        "autoWidth": false,
        "pagingType": "full_numbers",
        "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]],
        "language": {
            "emptyTable": "No data available"
        }
    });
}
