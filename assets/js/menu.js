var minus=document.querySelector(".minus-click");
var plus=document.querySelector(".plus-click");
var totalPrice=document.querySelector(".total-price");
var pizza=document.querySelectorAll(".options");
[...pizza].forEach((pizza) => {
    console.log(pizza)
    pizza.addEventListener('click',function(){
        console.log("dw");
    })
})

var count=minus.parentElement.nextElementSibling;
var price=5;
minus.addEventListener("click", function(){
    if (count.innerText>0) {
        count.innerHTML-=1;
        totalPrice.innerHTML=price*count.innerText;
    }  
});

plus.addEventListener("click", function(){
    count.innerHTML++;  
    totalPrice.innerHTML=price*count.innerText;
});