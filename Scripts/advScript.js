$(function () {
    $(".DivisionBox .SubMenu").hover(function () {
        $(this).animate({ "top": "-245px" }, 400, "swing");
    }, function () {
        $(this).stop(true, false).animate({ "top": "0px" }, 400, "swing");
    });
})

function ItemMouseOver(objItem) {
    var objO = 'pn_' + objItem.id;
    $("#" + objO).css('display', 'block');
}

function ItemMouseOut(objItem) {
    var objO = 'pn_' + objItem.id;
    $("#" + objO).css('display', 'none');
    
}

function WishList_Click(sItemNo) {
    $get("MainContent_ItemNo").value = sItemNo
    $get("MainContent_cmdSaveItemtoWishList").click();
}

function CloseLogin_AddWishList() {
    ShowTopLink();
    $get("MainContent_cmdSaveItemtoWishList").click();
}

function ShowTopLink() {
    $.ajax({
        type: "POST",
        url: "http://anhdv/lienhoa/AjaxFunction.aspx",
        data: {action: "GETTOPLINK" },
        success: function (theResponse) {
            $("#toplink").html(theResponse);
        }
    });
}

function buyall(sWishListNo) {
    $get("MainContent_WishListNo").value = sWishListNo;
    $get("MainContent_AddWishListToBasket").click();
}

function DeleteLine(nLineNo, sWishListNo) {
    $get("MainContent_LineNo").value = nLineNo;
    $get("MainContent_WishListNo").value = sWishListNo;
    $get("MainContent_DeleteLine").click();
}


function FormatNumber(obj, decimals) {
    var pnumber;
    if (eval(obj))
        pnumber = eval(obj).value;
    else
        pnumber = obj;

    pnumber = pnumber.replace(",","");

    if (isNaN(pnumber)) { 
        obj.value='';
        return 0;
    }
    if (pnumber == ''){ 
        obj.value='';
        return 0;
    }
    if (pnumber == '0') {
        obj.value = '';
        return 0;
    }
    var snum = new String(pnumber);
    var nIJ = snum.length - 1;
    var kIJ = 0;
    var result = '';
    while (nIJ >= 0) {
        if (kIJ == 3) {
            result = ',' + result;
            result = snum[nIJ] + result;
            kIJ = 1;
        } else {
            kIJ += 1;
            result = snum[nIJ] + result;
        }
        nIJ -= 1;
    }
    obj.value = result;
}

function AddItemCompare(sItemNo) {
    $.ajax({
        type: "POST",
        url: "http://anhdv/sieuthigiare/AjaxFunction.aspx",
        data: { itemno: sItemNo, action: "ADDTOCOMPARE" },
        success: function (theResponse) {
            $("#prdcompare").html(theResponse);
        }
    });
}

function RemoveItemCompare(sItemNo) {
    $.ajax({
        type: "POST",
        url: "http://anhdv/sieuthigiare/AjaxFunction.aspx",
        data: { itemno: sItemNo, action: "REMOVECOMPARE" },
        success: function (theResponse) {
            $("#prdcompare").html(theResponse);
        }
    });
}

function ShowCart() {
    $.ajax({
        type: "POST",
        url: "http://anhdv/sieuthigiare/AjaxFunction.aspx",
        data: { itemno: "", action: "GETCARTTOTAL" },
        success: function (theResponse) {
            $("#carthead").html(theResponse);
        }
    });
    $.ajax({
        type: "POST",
        url: "http://anhdv/sieuthigiare/AjaxFunction.aspx",
        data: { itemno: "", action: "GETCARTDETAIL" },
        success: function (theResponse) {
            $("#cartbody").html(theResponse);
        }
    });
    window.scrollTo(0, 0);
}

function DelCartLine(nLineNo) {
    $.ajax({
        type: "POST",
        url: "http://anhdv/sieuthigiare/AjaxFunction.aspx",
        data: { lineno: nLineNo, action: "DELETEROW" },
        success: function (theResponse) {
            ShowCart();
        }
    });
}
