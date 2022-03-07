$(document).ready(function () {
    $(document).on("click", "#load", function () {
        let productCount = $(".products").children().length;
        let dbProCount = $("#productCount").val();
        console.log("product="+productCount)
        console.log("db="+dbProCount)
        $.ajax({
            url: "/Menu/LoadProduct",
            data: {
                skip: productCount
            },
            method: "GET",
            success: function (result) {
                $(".products").append(result)
                productCount = $(".products").children().length;
                    
                if (productCount >= dbProCount) {
                    $("#load").remove();
                }
            }
        })
    })

    $(document).on("click", "#pizza-order", function (ev) {
        ev.preventDefault();
        var pizza = document.getElementById("pizza");
        var value = pizza.options[pizza.selectedIndex].value;
        console.log("value=" + value)
        var pizzaId = ev.target.nextElementSibling.value;
        $.ajax({
            url: "/Menu/AddBasket",
            data: {
                id: pizzaId,
                priceId: value
            },
            type: "GET",
            success: function (result) {
                alertify.set('notifier', 'position', 'top-right');
                alertify.success('Add to Card');
            }
        })
    })

    

    $(document).on("click", "#otherCatBut", function (ev) {
        ev.preventDefault();
        var otherMenId = ev.target.nextElementSibling.value;
        $.ajax({
            url: "/Menu/AddBasket",
            data: {
                id: otherMenId
            },
            type: "GET",
            success: function (result) {
                alertify.set('notifier', 'position', 'top-right');
                alertify.success('Add to Card');
            }
        })
    });

    //$(document).on("click", "#deleteProduct", function (ev) {
    //    ev.preventDefault();
    //    var proId = ev.target.nextElementSibling;
    //    console.log(proId);
    //    //$.ajax({
    //    //    url: "/Menu/RemoveFromCart",
    //    //    data: {
    //    //        id: proId
    //    //    },
    //    //    type: "GET",
    //    //    success: function (result) {
    //    //        alertify.set('notifier', 'position', 'top-right');
    //    //        alertify.success('Remove');
    //    //    }
    //    //})
    //});
});
