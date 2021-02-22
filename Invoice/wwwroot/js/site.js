// Write your JavaScript code.

$(document).ready(function () {
    $(function () {
        var idGen = new Generator();
        $('#Code').val(idGen.getId());

        $('#Date').datepicker({
            format: 'mm/dd/yyyy'
        });

        //get suppliers
        $.ajax({
            type: "GET",
            url: "/Sales/GetCustomers",
            datatype: "Json",
            success: function (data) {
                $.each(data, function (index, value) {
                    console.log(value);
                    $('#Customer').append('<option value="' + value.id + '">' + value.name+ '</option>');
                });
            }
        });

        $("#Search").autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: '/admin/products/',
                    dataType: "json",
                    data: { query: $("#Search").val() },
                    success: function (data) {
                        console.log(data);
                        response($.map(data, function (item) {
                            return { label: item.name, value: item.price };
                        }));
                    },
                    error: function (xhr, status, error) {
                        alert("Error");
                    },
                });
            },
            minLength: 1,
            select: function (event, ui) {
                detailsTableBody = $("#detailsTable tbody");
                var productItem = '<tr>  + <td>' + ui.item.label + '</td><td class="price">' + ui.item.value + '</td><td>' + '<input  class="qunatity" type= "number" min= "1" step= "1" value= "1"  max="99" />' + '</td><td class="amount">' + ui.item.value  + '</td><td><a data-itemId="' + 0 + '" href="#" class="deleteItem"><i class="fa fa-trash"></i></a></td></tr>';
                detailsTableBody.append(productItem);

                //alert('you have selected ' + ui.item.label + ' ID: ' + ui.item.value);
                $('#Search').val("");
                calculateSum();
                return false;
            }
        });
    });
    $(document).on('click', 'a.deleteItem', function (e) {
        e.preventDefault();
        var $self = $(this);
        $(this).parents('tr').css("background-color", "#1f306f").fadeOut(800, function () {
            $(this).remove();
            calculateSum();
        });
    });

    $(document).on('change', '.qunatity', function (e) {
        var quantity = parseInt($(this).val());
        var price = parseFloat($(this).closest('tr').children('td:eq(1)').text());
        var sum = quantity * price;
        $(this).closest('tr').children('td:eq(3)').text(sum.toFixed(2)); 
        calculateSum();

    })


        //save button click
    $("#BtnSave").click(function (e) {
        e.preventDefault();

        if (submitValidation()) {

            var discount; 
            if (parseFloat($('#Discount').val()).toFixed(2) == "NaN")
                discount = 0;
                

            var orderArr = [];
            orderArr.length = 0;

            $.each($("#detailsTable tbody tr"), function () {
                orderArr.push({

                    Name: $(this).find('td:eq(0)').html(),
                    Price: parseFloat($(this).find('td:eq(1)').html()),
                    Quantity: parseInt($(this).find('.qunatity').val()),
                    Amount: parseFloat($(this).find('td:eq(3)').html())
                });
            });

            var data = JSON.stringify({
                CustomerId: $("#Customer").val(),
                SaleCode: $("#Code").val(),
                SalesDate: $("#Date").val(),
                PaymentMethod: $("#Payment").val(),
                Total: parseFloat($("#SubTotal").text()),
                Notes: $("#Notes").val(),
                Status: $("#Status").val(),
                Discount: discount,
                GrandTotal: parseFloat($('#GrandTotal').val()).toFixed(2),
                Items: orderArr
            });

            console.log(data);
            $.when(saveOrder(data)).then(function (response) {
                console.log(response);
                location.href = "/Sales/index";
            }).fail(function (err) {
             
            });
        }
    });


    $('#BtnUpdate').click(function (e) {
        e.preventDefault();

        if (submitValidation()) {

            var orderArr = [];
            orderArr.length = 0;

            $.each($("#detailsTable tbody tr"), function () {
                orderArr.push({
                    Name: $(this).find('td:eq(0)').html(),
                    Price: parseFloat($(this).find('td:eq(1)').html()),
                    Quantity: parseInt($(this).find('.qunatity').val()),
                    Amount: parseFloat($(this).find('td:eq(3)').html())
                });
            });

            var data = {
                Id : parseInt($("#BtnUpdate").attr("data-sale-Id")),
                CustomerId: parseInt($("#Customer").val()),
                SaleCode: $("#Code").val(),
                SalesDate: $("#Date").val(),
                PaymentMethod: $("#Payment").val(),
                Total: parseFloat($("#SubTotal").text()),
                Notes: $("#Notes").val(),
                Status: $("#Status").val(),
                Discount: parseFloat($('#Discount').val()).toFixed(2),
                GrandTotal: parseFloat($('#GrandTotal').val()).toFixed(2),
                Items: orderArr
            };

            console.log(data);
            $.when(updateOrder(data)).then(function (response) {
                console.log(response);
                location.href = "/Sales/index";
            }).fail(function (err) {
                console.log(err);
            });
        }

     });
  

      ////total calculation
    function calculateSum() {
    var sum = 0;
    // iterate through each td based on class and add the values
    $(".amount").each(function () {

        var value = $(this).text();
        // add only if the value is number
        if (!isNaN(value) && value.length !== 0) {
            sum += parseFloat(value);
        }
    });

    if (sum == 0.0) {
        $('#Discount').text("0");
        $('#GrandTotal').text("0");
    }
    //console.log(sum);
    $('#SubTotal').text(sum.toFixed(2));
    $('#GrandTotal').val(sum.toFixed(2));

    var b = parseFloat($('#Discount').val()).toFixed(2);
    if (isNaN(b)) return;
    var a = parseFloat($('#SubTotal').text()).toFixed(2);
    var c = parseFloat(a - b).toFixed(2);
    $('#GrandTotal').val(c);
};
    $('.amount').each(function () {
    calculateSum();
    });

});

function submitValidation() {
        var customer = document.getElementById("Customer").value;
        var code = document.getElementById("Code").value;
        var date = document.getElementById("Date").value;
        var paymentmethod = document.getElementById("Payment").value;
        var pStaus = document.getElementById("Status").value;
        var total = parseFloat($("#SubTotal").text());
        var gtotal = parseFloat($("#GrandTotal").val());

        if (customer == "" || pStaus == "" || code == "" || date == "" || paymentmethod == "" || (total == "" || total == 0.00 || isNaN(total)) || (gtotal == "" || gtotal == 0.00 || isNaN(gtotal))) {

        if (pStaus == "") {
            document.getElementById("error_Status").style.display = "block";
        }
        else {
            document.getElementById("error_Status").style.display = "none";
        }

        if (customer == "") {
            document.getElementById("error_Customer").style.display = "block";
        }
        else {
            document.getElementById("error_Customer").style.display = "none";
        }
        if (code == "") {
            document.getElementById("error_Code").style.display = "block";
        }
        else {
            document.getElementById("error_Code").style.display = "none";
        }
        if (date == "") {
            document.getElementById("error_Date").style.display = "block";
        }
        else {
            document.getElementById("error_Date").style.display = "none";
        }
        if (paymentmethod == "") {
            document.getElementById("error_Payment").style.display = "block";
        }
        else {
            document.getElementById("error_Payment").style.display = "none";
        }
        if (total == "" || total === 0.00 || isNaN(total)) {
            document.getElementById("error_SubTotal").style.display = "block";
        }
        else {
            document.getElementById("error_SubTotal").style.display = "none";
        }
        if (gtotal == "" || gtotal === 0.00 || isNaN(gtotal)) {
            document.getElementById("error_GrandTotal").style.display = "block";
        }
        else {
            document.getElementById("error_GrandTotal").style.display = "none";
        }

        return false;
    }
    else {
        return true;
    }


}
function blankme(id) {

    var val = document.getElementById(id).value;
    var error_id = "error_" + id;

    if (val == "" || val === 0.00) {

        document.getElementById(error_id).style.display = "block";
    }
    else {
        document.getElementById(error_id).style.display = "none";
    }
}
function DiscountAmount() {
    //blankme("Discount");
    //blankme("GrandTotal");
    var b = parseFloat($('#Discount').val()).toFixed(2);
    if (isNaN(b)) return;
    var a = parseFloat($('#SubTotal').text()).toFixed(2);
    var c = parseFloat(a - b).toFixed(2);
    $('#GrandTotal').val(c);
}
function saveOrder(data) {
        return $.ajax({
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            type: 'POST',
            url: "/Sales/AddSale",
            data: data
        });
}
function updateOrder(data) {
    return $.ajax({
        dataType: 'json',
        type: 'POST',
        url: "/Sales/EditSale",
        data: data
    });
}
function Generator() { };

Generator.prototype.rand = Math.floor(Math.random() * 26) + Date.now();
Generator.prototype.getId = function () {
    return this.rand++;
};
