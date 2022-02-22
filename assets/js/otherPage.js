var navbarOtherPage2 = document.getElementById("nav2OtherPage");
var stickyOther =navbarOtherPage2.offsetTop;

function myFunctionOTherPage() {
  if (window.pageYOffset > stickyOther ) {
    navbarOtherPage2.classList.add("stickyOtherPageNavbar");
  } else {
    navbarOtherPage2.classList.remove("stickyOtherPageNavbar");
  }
}

window.onscroll = function () {myUpperFunction(),
                               myFunctionOTherPage(),
                               activeLink()};