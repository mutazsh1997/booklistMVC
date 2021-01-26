let dataTable;

document.addEventListener('DOMContentLoaded', (event) => {

    dataTable = $('#DT_LOAD').DataTable({
        "ajax": {
            "url": "Books/getAll/",
            "type": "GET",
            "datatype": "json",
        },
        "columns": [
            { "data": "bookName", "width": "30%" },
            { "data": "authName", "width": "30%" },
            { "data": "bookYear", "width": "30%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                               <a href="/Books/Upsert?id=${data}" class="btn btn-success text-white"
                                style="cursor:pointer; width:100px;">
                                    Edits
                                </a>
                                <a class="btn btn-danger text-white"
                                style="cursor:pointer; width:100px;"
                                onclick="onDelete('/Books/delete?id=${data}')">
                                    Delete
                                </a>
                            </div>`
                },
                "width": "30%"
            },
        ],
        "language": {
            "emptyTable": "no data found"
        },
        "width": "100%"
    });


});


function onDelete(id) {

    console.log("delete");

    swal({
        title: "are you sure",
        text: "delete for ever",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    }).then((willdelete) => {
        if (willdelete) {
            $.ajax({
                type: "DELETE",
                url: id,
                success: function (data) {
                    if (data.success) {

                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    } else {
                        toastr.error(data.message);

                    }
                }
            })
        }
    })
}