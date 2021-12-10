var orderModule = (function () {

    var Dom = {
        continueOrder: $("#continueOrder"),
        btnExportOrder: $("#btnExportOrder"),
        linkDetailBook: $(".linkBookDetail")
    }

    function initEvents() {

        Dom.linkDetailBook.click(function (e) {
            e.preventDefault();
            var bookId = $(this).attr("uid");
            $("#order-view").empty();
            bookModule.getDetail(bookId);
        })

        Dom.continueOrder.click(function (e) {
            e.preventDefault();
            $("#detail-view").empty();
            $("#order-view").empty();
            var storeName = $(".selected-card").attr("storename");
            bookModule.getBooks(storeName, true);
        })

        Dom.btnExportOrder.click(function (e) {
            e.preventDefault();
            $("#loading").show();
            $.ajax({
                url: 'https://localhost:44310//order//ExportOrder',
                type: "GET",
                contentType: "application/json",
                dataType: "json",
                success: function (result) {
                    if (!result) {
                        return;
                    }

                    if (result.success) {
                        Utils.showPopup("Information", "Export order successful!");
                    } else {
                        if (result.returnData == Utils.OrderExportResult.EmptyOrders) {
                            Utils.showPopup("Error", "Order list cannot be empty");
                            $("#loading").hide();
                            return;
                        }
                        else if (result.returnData == Utils.OrderExportResult.NoOrders) {
                            Utils.showPopup("Error", "No orders have been exported");
                            $("#loading").hide();
                            return;
                        }
                        else if (result.returnData == Utils.OrderExportResult.ExportFail) {
                            Utils.showPopup("Error", "Export fail: " + result.error);
                            $("#loading").hide();
                            return;
                        }
                        else {
                            Utils.showPopup("Error", "Export fail!");
                            $("#loading").hide();
                            return;
                        }
                    }

                    var storeName = $(".selected-card").attr("storename");
                    bookModule.getBooks(storeName, true);

                    $("#loading").hide();
                },
                error: function (message) {
                    if (message.responseText) {
                        window.location.href = '/Error/NoPermission';
                    } else {
                        Utils.showPopup("Error", "Export fail!");
                    }
                    console.log(message.statusText);
                    $("#loading").hide();
                }
            });
        })
    }

    return {
        init: function () {
            initEvents();
        },
    };
})();

orderModule.init();