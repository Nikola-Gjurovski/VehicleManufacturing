var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblDataGallery').DataTable({
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
