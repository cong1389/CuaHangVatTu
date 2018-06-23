//$('#header-categories').mouseover(function () { $('.categories-main-list').show(); }).mouseout(function () { $('.categories-main-list').hide(); });

//$('.cl-item').hover(function () {
//    $(this).find('.sub-cate').stop(true).delay(500).slideDown(500);
//}, function () {
//    $(this).find('.sub-cate').stop(true).delay(1000).slideUp(500);
//});


$(function () {
    
    $("#slider-range").slider({
        range: true,
        min: 0,
        max: 100000,
        values: [0, 100000],
        slide: function (event, ui) {
            //$("#amount").val("$" + ui.values[0] + " - $" + ui.values[1]);
            $('.range__values__from').val(ui.values[0]);
            $('.range__values__to').val(ui.values[1]);
        }
    });
    //$("#amount").val("$" + $("#slider-range").slider("values", 0) +
    //" - $" + $("#slider-range").slider("values", 1));
    $('.range__values__from').val($("#slider-range").slider("values", 0));
    $('.range__values__to').val($("#slider-range").slider("values", 1));
});
 