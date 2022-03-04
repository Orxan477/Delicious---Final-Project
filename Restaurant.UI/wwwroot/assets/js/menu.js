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
})



var minus = document.querySelector(".minus-click");
var plus = document.querySelector(".plus-click");
//var count = minus.parentElement.nextElementSibling;

 var totalPrice = document.querySelector(".total-price");

 var pizza = document.getElementById("pizza");
 var value=pizza.options[pizza.selectedIndex].value;

 pizza.addEventListener("click", function () {
    value = pizza.options[pizza.selectedIndex].value;
console.log(value);
    //totalPrice.innerHTML = value * count.innerText;
 });

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
