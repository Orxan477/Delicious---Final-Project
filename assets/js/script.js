var owl = $('.owl-carousel');

$(document).ready(function(){
  owl.owlCarousel({
      items:1,
      loop:true,
      autoplay:true,
      autoplayTimeout:4000,
      autoplayHoverPause:true
  });
  });