var owl = $(".owl-carousel");

$(document).ready(function () {
  owl.owlCarousel({
    items: 1,
    loop: true,
    autoplay: true,
    animateOut: "fadeOut",
    autoplayTimeout: 7000,
    autoplayHoverPause: false,
  });
});

window.onscroll = function () {
  myFunction(), myFunctionNavbar(), myFunctionHomeNavbar();
};

var navbar2 = document.getElementById("nav2");
var sticky = navbar2.offsetTop;

function myFunction() {
  if (window.pageYOffset > sticky) {
    navbar2.classList.add("sticky");
    navbar2.classList.add("background-scroll-navbar");
  } else {
    navbar2.classList.remove("sticky");
    navbar2.classList.remove("background-scroll-navbar");
    navbar2.classList.add("background-scroll-0-navbar");
  }
}

var home = document.getElementById("homeIntro");
var homeNav = document.getElementById("home-nav");
var homeSticky = home.offsetHeight;

var about = document.getElementById("about");
var aboutNav = document.getElementById("about-nav");
var aboutSticky = about.scrollHeight;

var sum=homeSticky+aboutSticky;

var choose=document.getElementById("choose")
var chooseSticky=choose.scrollHeight;
console.log("choose="+chooseSticky);

var sim2=sum+chooseSticky;

console.log("about="+homeSticky);


function myFunctionNavbar() {
  if (window.pageYOffset <= aboutSticky) {
    aboutNav.classList.remove("active-color");
  } else {
    aboutNav.classList.add("active-color");
  }
}


function myFunctionHomeNavbar() {
  if (window.pageYOffset <= homeSticky && window.pageYOffset > aboutSticky) {
    aboutNav.classList.remove("active-color");
    homeNav.classList.add("active-color");
  }
}

function myFunctionHomeNavbar() {
  if (window.pageYOffset > sum && window.pageYOffset < sim2) {
    aboutNav.classList.add("active-color");
    homeNav.classList.remove("active-color");
  }
}




var specialNames = document.querySelectorAll(".special-name");

[...specialNames].forEach((name) => {
  name.addEventListener("click", async function (ev) {

    ev.preventDefault();
    ChangeActiveSpecial(name);
    CurrentActiveProperty();
    GetClickData(name);

  });
});

function ChangeActiveSpecial(name) {
  let currentActiveSpecial = document.querySelector(".active-special");
  currentActiveSpecial.classList.remove("active-special");
  name.classList.add("active-special");
}

function CurrentActiveProperty() {
  let currentActiveSpecialProp = document.querySelector(".active-special-property");
  currentActiveSpecialProp.classList.remove("active-special-property");
  currentActiveSpecialProp.classList.add("d-none");
}

function GetClickData(name) {
  let clickNameDataId = name.getAttribute("data-id");
  let clickNameProp = document.getElementById(clickNameDataId);
  ChangeActiveProperty(clickNameProp);
}


function ChangeActiveProperty(clickNameProp){
  clickNameProp.classList.remove("d-none");
  clickNameProp.classList.add("active-special-property");
}