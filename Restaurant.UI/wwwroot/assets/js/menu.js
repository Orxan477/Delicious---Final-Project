$(document).ready(function () {
    $(document).on("click", "#load", function () {
        let productCount = $(".products").children().length;
        let dbProCount = $("#productCount").val();
        $.ajax({
            url: "/Menu/LoadProduct",
            data: {
                skip: productCount
            },
            method: "GET",
            success: function (result) {
                $(".products").append(result)
                productCount = $(".products").children().length;
                if (productCount == dbProCount) {
                    console.log(result)
                    $("#load").remove();
                }
            }
        })
    })

    //$(document).on("click", "#pizza-order", function (ev) {
    //    ev.preventDefault();
    //    var pizza = document.getElementById("pizza");
    //    var value = pizza.options[pizza.selectedIndex].value;
    //    var pizzaId = ev.target.nextElementSibling.value;
    //    console.log(pizzaId);
    //    $.ajax({
    //        url: "/Menu/AddBasket",
    //        data: {
    //            id: pizzaId,
    //            priceId: value
    //        },
    //        type: "GET"
    //    })
    //})
})



var minus = document.querySelector(".minus-click");
var plus = document.querySelector(".plus-click");
//var count = minus.parentElement.nextElementSibling;

 var totalPrice = document.querySelector(".total-price");



//////var modalPizza = document.getElementById("modal-pizza")
//////console.log(modalPizza)



//minus.addEventListener("click", function () {
//    if (count.innerText > 0) {
//        count.innerHTML -= 1;
//        // totalPrice.innerHTML = value * count.innerText;
//    }
//});

//plus.addEventListener("click", function () {
//    count.innerHTML++;
//    // totalPrice.innerHTML = value * count.innerText;
//});
