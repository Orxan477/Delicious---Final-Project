var owl = $('.owl-carousel');

$(document).ready(function(){
  owl.owlCarousel({
      items:1,
      loop:true,
      autoplay:true,
      animateOut: 'fadeOut',
      autoplayTimeout:10000,
      autoplayHoverPause:true
  });
  });