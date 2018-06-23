var is_loading = true; // initialize is_loading by false to accept new loading
var lstCat = $('#hdCategoryidlist').val().split(',');
$('#hdValueScrol').val(0);
var totalCat = lstCat.length; // limit items per page
var limit = 1;
var successLoad = true;
$(function () {
    $(window).scroll(function () {
        var currentValue = $('#hdValueScrol').val();
        console.log(currentValue + "-" + $(window).scrollTop());
        var scrollTop = $(window).scrollTop();
        if ($('.content-page').length != 0) {
            if (scrollTop > currentValue && scrollTop - currentValue > 20) {
                if (is_loading == true && successLoad == true) { // stop loading many times for the same page
                    // set is_loading to true to refuse new loading
                    if (limit == totalCat - 1)
                        is_loading = false;
                    // display the waiting loader
                    $('#loader').show();
                    $('#hdValueScrol').val(scrollTop);
                    successLoad = false;
                    //execute an ajax query to load more statments
                    $.ajax({
                        //url: 'Default.aspx?categoryid=' + lstCat[limit],
                        url: 'Default.aspx/LoadProductByCategoryId',
                        type: 'POST',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        data: '{catId:' + lstCat[limit] + '}',
                        success: function (data) {
                            // now we have the response, so hide the loader
                            $('#loader').hide();
                            // append: add the new statments to the existing data
                            $('.content-page').append(data.d);
                            // set is_loading to false to accept new loading                            
                            limit = limit + 1;
                            $("div[id^='fixed-placeholder']").each(function (i, el) {
                                $(this).remove();
                            });
                            successLoad = true;
//                            seajs.use('http://style.aliunicorn.com/js/6v/biz/site/en/wholesale/home/??home.js?t=b594680a_5c9de0bd1a', function (HomeIndex) {
//                                HomeIndex.init();
//                            });
                        }
                    });
                }
            }
        }
    });


});