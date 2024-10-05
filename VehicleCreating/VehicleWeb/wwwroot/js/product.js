var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/VehicleParts/GetAll",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "name", "width": "20%" },
            { "data": "price", "width": "15%" },
            { "data": "description", "width": "20%" },
            { "data": "manufacturer", "width": "15%" },
            { "data": "vehicleFormula.name", "width": "15%" },
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
                            <a href="/VehicleParts/Edit/${data}" class="btn btn-sm btn-primary">Edit</a>
                            <a href="/VehicleParts/Details/${data}" class="btn btn-sm btn-info">Details</a>
                            <a href="/VehicleParts/Delete/${data}" class="btn btn-sm btn-danger">Delete</a>
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
