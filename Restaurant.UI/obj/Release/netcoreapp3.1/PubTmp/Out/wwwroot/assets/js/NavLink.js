const sections = document.querySelectorAll("section");
const navLi = document.querySelectorAll(".nav2Right  ul li .nav-link");

function activeLink() {
    var current = "";

    sections.forEach((section) => {
        const sectionTop = section.offsetTop;

        if (pageYOffset >= sectionTop - 60) {
            current = section.getAttribute("id");
        }
    });
    let currentLinkActive = document.querySelector("." + current);

    navLi.forEach((link) => {

        link.classList.remove("active-color");
        currentLinkActive.classList.add("active-color");

    });
};