var dataTable;
var minDate, maxDate;

$.fn.dataTable.ext.search.push(
    function (settings, data, dataIndex) {
        var min = minDate.val();
        var max = maxDate.val();
        var date = new Date(data[2]);

        if (
            (min === null && max === null) ||
            (min === null && date <= max) ||
            (min <= date && max === null) ||
            (min <= date && date <= max)
        ) {
            return true;
        }
        return false;
    }
);

$(document).ready(function () {
    minDate = new DateTime($('#min'), {
        format: 'MMMM Do YYYY'
    });
    maxDate = new DateTime($('#max'), {
        format: 'MMMM Do YYYY'
    });

    var table = loadDataTable();

    $('#min, #max').on('change', function () {
        table.draw();
    });
});

function loadDataTable() {
    const url = window.location.pathname;
    const id = url.substring(url.lastIndexOf('/') + 1);

    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": `/Student/GetAllGrades?id=${id}`
        },
        "columns": [
            { "data": "subject.name", "width": "15%" },
            { "data": "mark", "width": "15%" },
            { "data": "dateTime", "width": "15%" },
        ]
    });
    return dataTable;
}
