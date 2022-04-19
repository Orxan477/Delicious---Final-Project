var navbar2 = document.getElementById("nav2");
var sticky = navbar2.offsetTop;
var header = document.querySelector(".myNavbar");
header.classList.add("background-scroll-navbar");

function myFunction() {
    if (window.pageYOffset > sticky) {
    navbar2.classList.add("stickyOtherPageNavbar");
  } else {
    navbar2.classList.remove("stickyOtherPageNavbar");
  }
}

window.onscroll = function () {myFunction(),
                               activeLink()};