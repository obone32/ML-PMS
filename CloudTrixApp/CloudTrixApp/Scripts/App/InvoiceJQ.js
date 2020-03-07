$(document).ready(function () {
    alter();

    $("#addToList").click(function () {
        alter("addToList");
        var description = $("#Description").val();
        var quantity = $("#Quantity").val();
        var price = $("#Price").val();
        var amount = $("#Amount").val();
        var igst = $("#IGST").val();
        var cgst = $("#CGST").val();
        var sgst = $("#SGST").val();

        var code = "<tr><td><input type='checkbox' name='record' /></td><td>" + description + "</td><td>" + quantity + "</td><td>" + price + "</td><td>" + amount + "</td><td>" + igst + "</td><td>" + cgst + "</td><td>" + sgst + "</td></tr>";
        $("table .tbody").append(code);
        var description = $("#Description").val('');
        var quantity = $("#Quantity").val('');
        var price = $("#Price").val('');
        var amount = $("#Amount").val('');
        var igst = $("#IGST").val('');
        var cgst = $("#CGST").val('');
        var sgst = $("#SGST").val('');
    
    $(".del").click(function () {
        $("table .tbody").find('input[name="record"]').each(function () {
            if ($(this).is(":checked")) {
                $(this).parents("tr").remove();
            }
        })
    })
    });


    //Add to List
    //alert("addToList")
    //$("#addToList").click(function (e) {
    //    alert("addToList")
    //e.preventDefault();
    //debugger;
    //if (add_validation()) {
    //    var productId = $("#Description").val(),
    //        description = $("#Description").val(),
    //        price = $("#Price").val(),
    //        quantity = $("#Quantity").val(),

    //           IGST = $("#IGST").val(),
    //        IGSTAmt = $("#IGSTAmt").val(),
    //           CGST = $("#CGST").val(),
    //        CGSTAmt = $("#CGSTAmt").val(),
    //        SGST = $("#SGST").val(),
    //        SGSTAmt = $("#SGSTAmt").val(),
    //           TotAmt = $("#TotAmt").val(),
    //        detailsTableBody = $("#detailsTable tbody");

    //    var productItem = '<tr> <td>' +
    //        ' </td><td>' + description +
    //        '</td><td>' + price +
    //        '</td><td>' + quantity +
    //        '</td><td class="">' + (parseFloat(price) * parseInt(quantity)).toFixed(2) +
    //          '</td><td>' + IGST +
    //        '</td><td>' + IGSTAmt +
    //          '</td><td>' + CGST +
    //        '</td><td>' + CGSTAmt +
    //          '</td><td>' + SGST +
    //        '</td><td>' + SGSTAmt +
    //          '</td><td class="amount">' + TotAmt +
    //        '</td><td>   <input type="button" value="Delete" onclick="deleteRow(this)"></td></tr>';
    //    detailsTableBody.append(productItem);
    //    $('#Description').val('');
    //    $('#Price').val('');
    //    $('#Quantity').val('');

    //    calculateSum();
    //    $("#Quantity").val('');
    //    $("#IGST").val(''),
    //   $("#IGSTAmt").val(''),
    //   $("#CGST").val(''),
    //   $("#CGSTAmt").val(''),
    //   $("#SGST").val(''),
    //   $("#SGSTAmt").val(''),
    //   $("#TotAmt").val('')
    //    //blankme("SubTotal");
    //    //blankme("GrandTotal")
    //}
    //});


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