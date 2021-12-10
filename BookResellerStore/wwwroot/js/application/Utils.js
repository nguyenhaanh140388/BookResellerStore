var Utils = (function () {

    const OrderResult = {
        QuantityInValid: -3,
        ExceedQuantityStock: -2,
        OrderFail: -1,
        OrderSuccessful: 1
    }

    const OrderExportResult = {
        EmptyOrders: -2,
        NoOrders: -3,
        ExportFail: -1,
        ExportSuccessful: 1,
    }

    function showPopup(title, content) {
        $("#dialog-message").attr("title", title);
        $("#message").html(content);
        $("#dialog-message").dialog({
            modal: true,
            width: 400,
            open: function (event, ui) {
                setTimeout(function () {
                    $("#dialog-message").dialog("close");
                }, 2000);
            }
        });
    }

    function getTotalOrder(selector) {
        $.ajax({
            url: 'https://localhost:44310//order//TotalOrder',
            type: "GET",
            contentType: "application/json",
            dataType: "json",
            success: function (result) {
                if (!result) {
                    $(selector).html(0);
                }

                $(selector).html(result);
            },
            error: function (message) {
                $(selector).html(0);
            }
        });
    }

    function refeshOrderNotification() {
        Utils.getTotalOrder(".badge");
    }

    return {
        showPopup: showPopup,
        getTotalOrder: getTotalOrder,
        refeshOrderNotification: refeshOrderNotification,
        OrderResult: OrderResult,
        OrderExportResult: OrderExportResult
    }
})();

