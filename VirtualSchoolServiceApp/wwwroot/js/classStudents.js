var dataTable;
var dataTable2;

$(document).ready(function () {
    loadDataTable();
    loadDataTableSubjects();
});

function loadDataTable() {

    const url = window.location.pathname;
    const id = url.substring(url.lastIndexOf('/') + 1);
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": `/Classes/GetAllStudents?id=${id}`
        },
        "columns": [
            { "data": "firstName", "width": "25%" },
            { "data": "lastName", "width": "25%" },
            { "data": "email", "width": "35%" },
            {
                "data": "id", "render": function (data) {
                    return `
                        <div class="w-75 btn-group" role="group">
                            <a onClick=Delete('/Classes/DeleteStudent?classId=${id}&userId=${data}') class="btn btn-danger mx-2"><i class="bi bi-trash"></i> Remove</a>
                        </div>
                    `
                },
                "width": "15%"
            },
        ]
    });
}

function loadDataTableSubjects() {

    const url = window.location.pathname;
    const id = url.substring(url.lastIndexOf('/') + 1);
    dataTable2 = $('#tblDataSubjects').DataTable({
        "ajax": {
            "url": `/Classes/GetAllSubjects?id=${id}`
        },
        "columns": [
            { "data": "subject.name", "width": "85%" },
            {
                "data": "subject.id", "render": function (data) {
                    return `
                        <div class="w-75 btn-group" role="group">
                            <a onClick=Delete('/Classes/RemoveSubject?classId=${id}&subjectId=${data}') class="btn btn-danger mx-2"><i class="bi bi-trash"></i> Remove</a>
                        </div>
                    `
                },
                "width": "15%"
            },
        ]
    });
}

function Delete(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, remove it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: "DELETE",
                success: function (data) {
                    if (data.success) {
                        dataTable.ajax.reload();
                        dataTable2.ajax.reload();
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
