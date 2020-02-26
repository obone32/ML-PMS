

// Save the Form with details
$(document).ready(function () {
    $("#BtnSave").click(function (e) {
        e.preventDefault();
        if (submitValidation()) {
            var orderArr = [];
            orderArr.length = 0;

            $.each($("#detailsTable tbody tr"),
                function () {
                    orderArr.push({
                        RoleManagementDetailsID: $(this).find('input').html(),
                        FormID: $(this).find('td:eq(1)').html(),
                        AddPermission: ($(this).find('td:eq(3)')).find('input[name=AddPermission]').is(':checked'),
                        UpdatePermission: ($(this).find('td:eq(4)')).find('input[name=UpdatePermission]').is(':checked'),
                        DeletePermission: ($(this).find('td:eq(5)')).find('input[name=DeletePermission]').is(':checked'),
                        ViewPermission: ($(this).find('td:eq(6)')).find('input[name=ViewPermission]').is(':checked'),
                    })
                });

            var data = JSON.stringify({
                RoleID: $("#RoleID").val(),
                UserTypeID: $("#UserTypeID").val(),
                Description: $("#Description").val(),
                Items: orderArr
            });
            console.log(data);
            if (this.id == "BtnEdit") {
                UpdateOrder(data);
            }
            else {
                saveOrder(data);
            }
            window.location.href = "/RoleManagement/Index";
        }
    });

    //$(document).on('click', '#BtnEdit', function () {
    $("#BtnEdit").click(function (e) {
        e.preventDefault();
        if (submitValidation()) {
            var orderArr = [];
            orderArr.length = 0;

            $.each($("#detailsTable tbody tr"),
                function () {
                    orderArr.push({
                        RoleManagementDetailsID: $(this).find('td:eq(0)').html(),
                        FormID: $(this).find('td:eq(1)').html(),
                        AddPermission: ($(this).find('td:eq(3)')).find('input[name=AddPermission]').is(':checked'),
                        UpdatePermission: ($(this).find('td:eq(4)')).find('input[name=UpdatePermission]').is(':checked'),
                        DeletePermission: ($(this).find('td:eq(5)')).find('input[name=DeletePermission]').is(':checked'),
                        ViewPermission: ($(this).find('td:eq(6)')).find('input[name=ViewPermission]').is(':checked'),
                    })
                });
            //alert($(this).find('td:eq(0)').val());
            var data = JSON.stringify({
                RoleID: $("#RoleID").val(),
                UserTypeID: $("#UserTypeID").val(),
                Description: $("#Description").val(),
                Items: orderArr
            });

            console.log(data);
            if (this.id == "BtnEdit") {
                UpdateOrder(data);
            }
            window.location.href = "/RoleManagement/Index";
        }
    });

    //Function for Save Order and Update Order
    function saveOrder(data) {
        //debugger;
        return $.ajax({
            type: 'POST',
            url: '/RoleManagement/Create',
            data: data,
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
        })
    };

    function UpdateOrder(data) {
        return $.ajax({
            type: 'POST',
            // url: '@Url.Action("Edit")',
            url: '/RoleManagement/Edit',
            data: data,
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
        })
    };

    // Submit Validation
    function submitValidation() {

        return !0
    };

    //Add_Validation
    function add_validation() {

        return !0
    };

});









