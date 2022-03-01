var minus = document.querySelectorAll(".minus-click");
var plus = document.querySelectorAll(".plus-click");
var countSelect = document.querySelector(".count-select")
var count = countSelect.parentElement.nextElementSibling;
console.log(count)


var totalPrice = document.querySelector(".total-price");

var pizza = document.getElementById("pizza");
var value=pizza.options[pizza.selectedIndex].value;


pizza.addEventListener("click", function () {
   value = pizza.options[pizza.selectedIndex].value;
   totalPrice.innerHTML = value * count.innerText;
});


[...minus].forEach((min) => {
    var count = min.parentElement.nextElementSibling;
    min.addEventListener("click", function () {
        if (count.innerText > 0) {
            count.innerHTML -= 1;
            totalPrice.innerHTML = value

        }
    });

    [...plus].forEach((pl) => {
        pl.addEventListener("click", function () {
            count.innerHTML++;
            totalPrice.innerHTML = value * count.innerText;
        });
    });
})