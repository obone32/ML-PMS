var currentItemPrice;

$(document).ready(function () {

    //datetimepicker
    //$('.mydatepicker').datepicker(
    //    { format: 'dd/mm/yyyy', autoclose: true });


    ////Get Customer
    //$.ajax({
    //    type: "GET", url: "/Admin/GetCustomer",
    //    datatype: "Json",
    //    success: function (data) {
    //        $.each(data, function (index, value) {
    //            $('#Customer').append('<option value="' + value.Id + '">' + value.Name + '</option>')
    //        })
    //    }
    //});

    //// Get Project by Customer

    //$('#Customer').change(function () {
    //    $('#Project').empty();
    //    $.ajax({
    //        type: "GET", url: "/Admin/GetProjectByCustomer",
    //        datatype: "Json",
    //        data: { customerId: $('#Customer').val() },
    //        success: function (data) {
    //            $.each(data, function (index, value) {
    //                $('#Project').append('<option value="' + value.Id + '">' + value.ProjectName + '</option>')
    //            });
    //            //Check if required
    //            //LoadProjectDetails($("#Project option:selected").val())
    //        }
    //    })
    //});

    //Add to List
    $("#addToList").click(function (e) {
        alert("addToList");
        e.preventDefault();
        debugger;
        if (add_validation()) {
            var productId = $("#Description").val(),
                description = $("#Description").val(),
                price = $("#Price").val(),
                quantity = $("#Quantity").val(),

                   IGST = $("#IGST").val(),
                IGSTAmt = $("#IGSTAmt").val(),
                   CGST = $("#CGST").val(),
                CGSTAmt = $("#CGSTAmt").val(),
                SGST = $("#SGST").val(),
                SGSTAmt = $("#SGSTAmt").val(),
                   TotAmt = $("#TotAmt").val(),
                detailsTableBody = $("#detailsTable tbody");

            var productItem = '<tr> <td>' +
                ' </td><td>' + description +
                '</td><td>' + price +
                '</td><td>' + quantity +
                '</td><td class="">' + (parseFloat(price) * parseInt(quantity)).toFixed(2) +
                  '</td><td>' + IGST +
                '</td><td>' + IGSTAmt +
                  '</td><td>' + CGST +
                '</td><td>' + CGSTAmt +
                  '</td><td>' + SGST +
                '</td><td>' + SGSTAmt +
                  '</td><td class="amount">' + TotAmt +
                '</td><td>   <input type="button" value="Delete" onclick="deleteRow(this)"></td></tr>';
            detailsTableBody.append(productItem);
            $('#Description').val('');
            $('#Price').val('');
            $('#Quantity').val('');

            calculateSum();
            $("#Quantity").val('');
            $("#IGST").val(''),
           $("#IGSTAmt").val(''),
           $("#CGST").val(''),
           $("#CGSTAmt").val(''),
           $("#SGST").val(''),
           $("#SGSTAmt").val(''),
           $("#TotAmt").val('')
            //blankme("SubTotal");
            //blankme("GrandTotal")
        }
    });
    
    // Save the Form with details
    $("#BtnSave").click(function (e) {
        alert("click1");
        e.preventDefault();
        if (submitValidation()) {
           
            var orderArr = [];
           
            orderArr.length = 0;
            $.each($("#detailsTable tbody tr"),
                function () {
                    orderArr.push({
                        //DescriptionId: parseInt($(this).find('td:eq(0)').html()),
                        InvoiceItemID: $(this).find('input').val(),
                        Description: $(this).find('td:eq(1)').html(),
                        Rate: parseFloat($(this).find('td:eq(2)').html()),
                        Quantity: parseInt($(this).find('td:eq(3)').html()),
                        //DiscountAmount: parseFloat($(this).find('td:eq(4)').html()),

                        IGST: parseFloat($(this).find('td:eq(5)').html()),
                        IGSTRate: parseFloat($(this).find('td:eq(6)').html()),
                        CGST: parseFloat($(this).find('td:eq(7)').html()),
                        CGSTRate: parseFloat($(this).find('td:eq(8)').html()),
                        SGST: parseFloat($(this).find('td:eq(9)').html()),
                        SGSTRate: parseFloat($(this).find('td:eq(10)').html()),
                        DiscountAmount: parseFloat($(this).find('td:eq(11)').html()),

                    })
                });
            
            var data = JSON.stringify({
                InvoiceID: $("#InvoiceID").val(),
                InvoiceNo: $("#InvoiceNo").val(),
                InvoiceDate: $("#InvoiceDate").val(),
                ClientID: $("#ClientID").val(),
                ProjectID: $("#ProjectID").val(),
                ClientContactNo: $("#ClientContactNo").val(),
                ClientEMail: $("#ClientEMail").val(),
                ClientAddress1: $("#ClientAddress1").val(),
                ClientGSTIN: $("#ClientGSTIN").val(),
                //PaymentMethod: $("#Payment").val(),
                TotalAmt: parseFloat($("#SubTotal").text()),
                //Notes: $("#Notes").val(),
                //Status: $("#InvoiceStatus").val(),
                //Discount: parseFloat($('#Discount').val()).toFixed(2),
                //GrandTotal: parseFloat($('#GrandTotal').text()).toFixed(2),
                Items: orderArr
            });
           
            //console.log(data);
            if (this.id == "BtnEdit") {
                UpdateOrder(data);
            }
            else {
                saveOrder(data);
            }
            window.location.href="/Invoice/Index";
            //$.when(saveOrder(data)).then(function (response) {
            //    console.log(response);
            //    location.href = "/Admin/InvoiceList"
            //}).fail(function (err) { error(response.message) })
        }
    });

    //Function for Save Order and Update Order
    function saveOrder(data) {
        //debugger;
        return $.ajax({
            type: 'POST',
            url: '/Invoice/Create',
            data: data,
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
        })
    }
    function UpdateOrder(data) {
        return $.ajax({
            type: 'POST',
            url: '/Invoice/Edit',
            data: data,
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
        })
    }

    // Submit Validation
    function submitValidation() {
        //var iStaus = document.getElementById("InvoiceStatus").value;
        //var payment = document.getElementById("Payment").value;
        //var code = document.getElementById("Code").value;
        //var idate = document.getElementById("Date").value;
        //var customer = document.getElementById("Customer").value;
        //var project = document.getElementById("Project").value;
        //var total = parseFloat($("#SubTotal").text());
        //var gtotal = parseFloat($("#GrandTotal").text());

        //if (iStaus == "" || payment == "" || code == "" || idate == "" || customer == "" || project == "" || (total == "" || total == 0.00 || isNaN(total)) || (gtotal == "" || gtotal == 0.00 || isNaN(gtotal))) {
        //    if (iStaus == "") { document.getElementById("error_InvoiceStatus").style.display = "block" }
        //    else { document.getElementById("error_InvoiceStatus").style.display = "none" }
        //    if (payment == "") { document.getElementById("error_Payment").style.display = "block" }
        //    else { document.getElementById("error_Payment").style.display = "none" }
        //    if (code == "") { document.getElementById("error_Code").style.display = "block" }
        //    else { document.getElementById("error_Code").style.display = "none" }
        //    if (idate == "") { document.getElementById("error_IDate").style.display = "block" }
        //    else { document.getElementById("error_IDate").style.display = "none" }
        //    if (customer == "") { document.getElementById("error_Customer").style.display = "block" }
        //    else { document.getElementById("error_Customer").style.display = "none" }
        //    if (project == "") { document.getElementById("error_Project").style.display = "block" }
        //    else { document.getElementById("error_Project").style.display = "none" }
        //    if (total == "" || total === 0.00 || isNaN(total)) { document.getElementById("error_SubTotal").style.display = "block" }
        //    else { document.getElementById("error_SubTotal").style.display = "none" }
        //    if (gtotal == "" || gtotal === 0.00 || isNaN(gtotal)) { document.getElementById("error_GrandTotal").style.display = "block" }
        //    else { document.getElementById("error_GrandTotal").style.display = "none" }
        //    return !1
        //}
        //else { return !0 }
        return !0
    }

    //Add_Validation
    function add_validation() {
        //var description = document.getElementById("Description").value;
        //var quantity = document.getElementById("Quantity").value;
        //var price = document.getElementById("Price").value;

        //if (quantity == "" || description == "" || price == "") {
        //    if (quantity == "") { document.getElementById("error_Quantity").style.display = "block" }
        //    else { document.getElementById("error_Quantity").style.display = "none" }
        //    if (description == "") { document.getElementById("error_description").style.display = "block" }
        //    else { document.getElementById("error_description").style.display = "none" }
        //    if (price == "") { document.getElementById("error_price").style.display = "block" }
        //    else { document.getElementById("error_price").style.display = "none" }
        //    return !1
        //}
        //else { return !0 }
        return !0
    }

    // Blankme Id
    //function blankme(id) {
    //    var val = document.getElementById(id).value;
    //    var error_id = "error_" + id; if (val == "" || val === 0.00) { document.getElementById(error_id).style.display = "block" }
    //    else { document.getElementById(error_id).style.display = "none" }
    //}

    // Calculate Sum
    function calculateSum() {
        debugger;
        var sum = 0;
        $(".amount").each(function () {
            var value = $(this).text();
            if (!isNaN(value) && value.length !== 0) { sum += parseFloat(value) }
        });
        if (sum == 0.0) {
            $('#Discount').text("0");
            $('#GrandTotal').text("0")
        }
        $('#SubTotal').text(sum.toFixed(2));
        $('#GrandTotal').text(sum.toFixed(2));
        var b = parseFloat($('#Discount').val()).toFixed(2);
        if (isNaN(b)) return;
        var a = parseFloat($('#SubTotal').text()).toFixed(2); $('#GrandTotal').text(a - b)
    };
    $('.amount').each(function () { calculateSum() });

    // Function for Discount
    function DiscountAmount() {
        blankme("#Discount");
        blankme("#GrandTotal");
        var b = parseFloat($("#Discount").val());
        if (isNaN(b)) return;
        var a = parseFloat($('#SubTotal').text()).toFixed(2); $('#GrandTotal').text(a - b)
    }
    $('#Discount').change(function () {
        calculateSum();
    });

    $(':input[type="number"]').bind('keypress', function (e) {
        if (e.keyCode == '9' || e.keyCode == '16') {
            return;
        }
        var code;
        if (e.keyCode) code = e.keyCode;
        else if (e.which) code = e.which;
        if (e.which == 46)
            return false;
        if (code == 8 || code == 46)
            return true;
        if (code < 48 || code > 57)
            return false;
    });




});