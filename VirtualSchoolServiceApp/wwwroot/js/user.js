var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Users/GetAll"
        },
        "columns": [
            { "data": "firstName", "width": "15%" },
            { "data": "lastName", "width": "15%" },
            { "data": "email", "width": "15%" },
            {
                "data": "id", "render": function (data, type, row) {
                    return `
                    <span>${row.isStudent ? "Student" : ""}${row.isTeacher ? "Teacher" : ""}${row.isParent ? "Parent" : ""}</span>                
                    `
                },
                "width": "15%"
            },
            {
                "data": "id", "render": function (data, type, row) {
                    return `
                        <div class="w-75 btn-group" role="group">
                            <a onClick=Delete('/Users/Delete?id=${data}') class="btn btn-danger mx-2"><i class="bi bi-trash"></i> Delete</a>
                        </div>
                    `
                },
                "width": "15%"
            },
        ]
    });
}
/*"data": "id", "render": function (data, type, row)*/
function Delete(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: "DELETE",
                success: function (data) {
                    if (data.success) {
                        dataTable.ajax.reload();
                        toastr.success(data.message);
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            })
        }
    })
}