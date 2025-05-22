$(document).ready(function () {
    $(".add-to-cart").click(function () {
        var productid = document.getElementById("ProductId").value;
        var soluong = $('#txtsoluong').val();
        $.ajax({
            url: 'api/cart/add',
            type: "POST",
            dataType: "JSON",
            data: {
                productID = productid,
                amount: soluong
            },
            success: function (response) {
                loadHeaderCart();
                location.reload();
            },
            error: function (error) {
                alert("there was n error posting the data to th eserver!" + error.responseText);
            }
        });
    });
});
function loadHeaderCart() {
    $("miniCart").load("/AjaxContent/HeaderCart");
    $("numberCart").load("/AjaxContent/NumberCart");
}