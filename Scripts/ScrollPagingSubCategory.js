var is_loading = true; // initialize is_loading by false to accept new loading
var lstCat = $('#phMainContent_hdSubCategoryidlist').val().split(',');
$('#phMainContent_hdSubCateValueScrol').val(0);
var totalCat = lstCat.length; // limit items per page
var limit = 0;
var successLoad = true;


$(function () {
    $(window).scroll(function () {

        var currentValue = $('#phMainContent_hdSubCateValueScrol').val();
        console.log(currentValue + "-" + $(window).scrollTop());
        var scrollTop = $(window).scrollTop();

        if (scrollTop > currentValue && scrollTop - currentValue > 20) {
            if (is_loading == true && successLoad == true) { // stop loading many times for the same page
                // set is_loading to true to refuse new loading
                if (limit == totalCat - 1)
                    is_loading = false;
                // display the waiting loader
                $('#loader').show();
                $('#phMainContent_hdSubCateValueScrol').val(scrollTop);
                console.log("Limit:" + limit);
                console.log("value:" + lstCat[limit]);
                successLoad = false;
                //execute an ajax query to load more statments
                $.ajax({
                    //url: 'Default.aspx?categoryid=' + lstCat[limit],
                    url: $('#hdURL').val() + 'SubCategory.aspx/LoadProductBySubCategory',
                    type: 'POST',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: '{catId:' + lstCat[limit] + '}',
                    success: function (data) {
                        successLoad = true;
                        // now we have the response, so hide the loader
                        $('#loader').hide();
                        // append: add the new statments to the existing data
                        $('.bc-floors-wrap').append(data.d);

                        // set is_loading to false to accept new loading                            
                        limit = limit + 1;
                        //                            $("div[id^='div-callback-category']").each(function (i, el) {
                        //                                $(this).remove();
                        //                            });
                    }
                });
            }

        }
    });


});

$('.bc-cate-has-child').hover(function () {
    $(this).stop(true).delay(500).addClass('bc-cate-child-active');
}, function () {
    $(this).stop(true).delay(1000).removeClass('bc-cate-child-active');
});