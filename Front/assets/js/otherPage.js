var navbar2 = document.getElementById("nav2");
var sticky = navbar2.offsetTop;

function myFunction() {
  if (window.pageYOffset > sticky ) {
    navbar2.classList.add("stickyOtherPageNavbar");
  } else {
    navbar2.classList.remove("stickyOtherPageNavbar");
  }
}

window.onscroll = function () {myUpperFunction(),
                               myFunction(),
                               activeLink()};