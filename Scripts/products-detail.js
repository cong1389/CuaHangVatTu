
$(function () {

    $("#phMainContent_ddlOtherPrice").change(function () {
        var proid = $('#phMainContent_ddlOtherPrice').val();

        //$('#spOtherPrice').text("111")
        $.ajax({
            //url: 'Default.aspx?categoryid=' + lstCat[limit],
            url: $('#hdURL').val() + 'ProductDetail.aspx/LoadOtherPrice',
            type: 'POST',
            contentType: "application/json",
            dataType: "json",
            //data: '{proid:' + proid + ",itemid:" + $('#phMainContent_hdProductId').val() + '}',
            data: JSON.stringify({
                "proid": proid,
                "itemid": $('#phMainContent_hdProductId').val()
            }),

            success: function (data) {
                debugger;
                // now we have the response, so hide the loader
                if (data.d == "") {
                    $('.suggestion-product').css("display", "none")
                    $('#spOtherPrice').text("Không có sản phẩm bán tại khu vực này")
                }
                else if (data.d.length > 15) {
                    $('#spOtherPrice').text("Xem sản phẩm thay thế bên dưới tại khu vực này")
                    $('.suggestion-product>ul').text("")
                    $('.suggestion-product>ul').append(data.d)
                    $('.suggestion-product').css("display", "block")

                }
                else {
                    $('#spOtherPrice').text(data.d + " VND")
                    $('.suggestion-product').css("display", "none")
                }

            }
        });
    });
});

function ClosePoupAdd() {
    location.href = location.href;
}