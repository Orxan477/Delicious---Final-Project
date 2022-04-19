$(document).ready(function () {
    //$(document).on("click", "#load", function () {
    //    let productCount = $(".products").children().length;
    //    let dbProCount = $("#productCount").val();
    //    $.ajax({
    //        url: "/Menu/LoadProduct",
    //        data: {
    //            skip: productCount
    //        },
    //        method: "GET",
    //        success: function (result) {
    //            $(".products").append(result)
    //            productCount = $(".products").children().length;
                    
    //            if (productCount >= dbProCount) {
                    $("#load").remove();
    //            }
    //        }
    //    })
    //})
    
    $(document).on("click", "#pizza-order-button", function (ev) {
        ev.preventDefault();
        var as=$("#pizzas :selected").val();
        console.log("value=" + as);
        var pizzaId = ev.target.nextElementSibling.value;
        $.ajax({
            url: "/Menu/AddBasket",
            data: {
                id: pizzaId,
                priceId: as
            },
            type: "GET",
            success: function (result) {
                alertify.set('notifier', 'position', 'top-right');
                alertify.success('Add to Card');
            }
        })
    })


    $(document).on("click", "#modal-pizza", function (ev) {
        var pizzaId = ev.target.nextElementSibling.value;
        $.ajax({
            url: "/Menu/ModalPartial",
            data: {
                id: pizzaId
            },
            type: "GET",
            success: function (result) {
                $(".append-modal").append(result)
            }
        })
    })

    $(document).on("click", "#modalClosePartial", function (ev) {
        $(".append-modal").children().remove();
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

    $(document).on('click', '.category li a', function (ev) {
        ev.preventDefault();
        let category = $(this).attr('data-id');
        let products = $('.product-item');

        products.each(function () {
            if (category == $(this).attr('data-id')) {
                $(this).parent().fadeIn();
            }
            else {
                $(this).parent().hide();
            }
        })
        //if (category == 'all') {
        //    products.parent().fadeIn();
        //}
    })
});
