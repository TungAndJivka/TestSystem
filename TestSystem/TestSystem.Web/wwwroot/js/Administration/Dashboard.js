$(document).ready(function () {
    $('#existingTestsTable').DataTable(
        {
            "pagingType": "full_numbers",
            "fnDrawCallback": function () {
                var $paginate = this.siblings('.dataTables_paginate');

                if (this.api().data().length <= this.fnSettings()._iDisplayLength) {
                    $paginate.hide();
                }
                else {
                    $paginate.show();
                }
            }
        })
});


$(document).ready(function () {
    $('#resultstable').DataTable(
        {
            "pagingType": "full_numbers",
            "fnDrawCallback": function () {
                var $paginate = this.siblings('.dataTables_paginate');

                if (this.api().data().length <= this.fnSettings()._iDisplayLength) {
                    $paginate.hide();
                }
                else {
                    $paginate.show();
                }
            }
        })
});

