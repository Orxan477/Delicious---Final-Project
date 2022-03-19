
//back-to-home-link
var upper = document.getElementById("upper");
function myUpperFunction() {
  
  if (window.pageYOffset >= 80) {
    upper.classList.remove("d-none");
    upper.classList.add("d-flex");
  } else {
    upper.classList.add("d-none");
    upper.classList.remove("d-flex");
  }
}


window.onscroll = function () {
    myUpperFunction()
};