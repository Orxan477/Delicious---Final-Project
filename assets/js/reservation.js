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

// const sections = document.querySelectorAll("section");
// const navLi = document.querySelectorAll(".nav2Right  ul li .nav-link");

// function activeLink() {
//   var current = "";

//   sections.forEach((section) => {
//     const sectionTop = section.offsetTop;
    
//     if (pageYOffset >= sectionTop-60) {
//       current = section.getAttribute("id");
//      }
//   });
//   let currentLinkActive=document.querySelector("."+current);
//   console.log(currentLinkActive);

//   navLi.forEach((link) => {

//     link.classList.remove("active-color");
//     currentLinkActive.classList.add("active-color");
    
//   });
// };