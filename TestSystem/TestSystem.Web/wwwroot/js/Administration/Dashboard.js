$(document).ready(function () {
    $('#example').DataTable(
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