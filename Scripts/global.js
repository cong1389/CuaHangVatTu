$(document).ready(function () {
    $('.ui-banner-slider-slider').bxSlider({
        pagerCustom: '#bx-pager',
        auto: true
    });

    var dialog = $("#dialog-message").dialog({
        autoOpen: false,
        height: 300,
        width: 450,
        modal: true,
        buttons: {
            "Kiểm tra đơn hàng": function () {
                // $(this).dialog("close");
                if ($("#gblorderId").val().toString() == "" && $('#gblMobileId').val() == "") {
                    $('#requireinput').css("display", "block");
                    $('#invalidation').css("display", "none");
                    return false;
                }
                $.ajax({
                    //url: 'Default.aspx?categoryid=' + lstCat[limit],
                    url: $('#hdURL').val() + 'reviewOrder.aspx/CheckOrder',
                    type: 'POST',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",

                    data: JSON.stringify({ 'orderno': $("#gblorderId").val().toString(), 'phonenumber': $('#gblMobileId').val() }),
                    success: function (data) {
                        $('#requireinput').css("display", "none");
                        //alert(data.d)  
                        if (data.d == "1") {
                            location.href = $('#hdURL').val() + "review-order/" + $("#gblorderId").val() + "/"
                        }
                        else {
                            $('#invalidation').css("display", "block");
                        }

                    }
                });

            },
            "Bỏ qua": function () {
                $('#invalidation').css("display", "none");
                $('#requireinput').css("display", "none");
                $(this).dialog("close");
            }
        }
    });
    $("#check-order").button().on("click", function () {
        dialog.dialog("open");
    });
    $('.cl-item').hover(function () {
        $(this).find('.sub-cate').stop(true).delay(200).slideDown(200);
    }, function () {
        $(this).find('.sub-cate').stop(true).delay(200).slideUp(200);
    });

    $('.ui-tab-nav .ui-switchable-trigger').on('click', function () {
        $('.ui-tab-nav .ui-switchable-trigger').removeClass('ui-tab-active');
        $('.ui-tab-pane').hide();
        if ($(this).hasClass('product-detail'))
            $('#pdt').show();
        else {
            $('#comment').show();
        }
        $(this).addClass('ui-tab-active');
    });
    $('.image-nav .image-nav-item').on('mouseover', function () {        
        $('.image-nav .image-nav-item').removeClass('active current');
        var source = $(this).find('img').attr('src');
        $('.ui-image-viewer-thumb-frame img').attr('src', source);
        $(this).addClass('active current');
    });
    $('.detail-page .ui-image-viewer').bxSlider({
        pagerCustom: '.detail-page .image-nav'
    });
});