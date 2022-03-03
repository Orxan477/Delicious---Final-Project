var minus = document.querySelector(".minus-click");

var plus = document.querySelector(".plus-click");

var count = minus.parentElement.nextElementSibling;

// var totalPrice = document.querySelector(".total-price");

// var pizza = document.getElementById("pizza");
// var value=pizza.options[pizza.selectedIndex].value;

// pizza.addEventListener("click", function () {
//    value = pizza.options[pizza.selectedIndex].value;
//    totalPrice.innerHTML = value * count.innerText;
// });

minus.addEventListener("click", function () {
  if (count.innerText > 0) {
    count.innerHTML -= 1;
    // totalPrice.innerHTML = value * count.innerText;
  }
});

plus.addEventListener("click", function () {
  count.innerHTML++;
  // totalPrice.innerHTML = value * count.innerText;
});
