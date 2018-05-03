$(function () {
    $(document).ready(function () {

        $('#existingTestsTable').DataTable(
            {
                retrieve: true,
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
                retrieve: true,
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

    $("#tests-tab").on("click", () => {
        $('#results').hide();
        $('#tests').show();
    });

    $("#results-tab").on("click", () => {
        $('#tests').hide();
        $('#results').show();
    });

});
