$(document).ready(function () {
    $(".click").click(function () {
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
});