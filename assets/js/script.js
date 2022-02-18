var owl = $('.owl-carousel');

$(document).ready(function(){
  owl.owlCarousel({
      items:1,
      loop:true,
      autoplay:true,
      animateOut: 'fadeOut',
      autoplayTimeout:5000,
      autoplayHoverPause:true
  });
  });

  

window.onscroll = function() {myFunction()};

// var navbar1 = document.getElementById("nav1");
var navbar2 = document.getElementById("nav2");
var sticky = navbar2.offsetTop;

function myFunction() {
  if (window.pageYOffset > sticky) {
    navbar2.classList.add("sticky");
    navbar2.classList.add("background-scroll-navbar");
  } else {
    navbar2.classList.remove("sticky");
    navbar2.classList.remove("background-scroll-navbar")
  }
}
