var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblOrder').DataTable({
        "ajax": {
            "url": "/Shopping/GetAll",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "orderId", "width": "5%" },
            { "data": "applicationUser.userName", "width": "20%" },
            { "data": "shoppingCart.vehicleName", "width": "15%" },
            { "data": "shoppingCart.vehicleColor", "width": "10%" },
            { "data": "shoppingCart.companyName", "width": "15%" },
            { "data": "shoppingCart.type", "width": "10%" },
            {
                "data": "id",
                "render": function (data, type, row) {
                    return '<a href="/Orders/OrdersAdminDetails/' + data + '" class="btn btn-info">View Order</a> ' +
                        '<a href="/Orders/CreateInvoice/' + data + '" class="btn btn-info">Create Invoice</a>';
                },
                "width": "25%"
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
