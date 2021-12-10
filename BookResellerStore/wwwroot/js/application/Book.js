var bookModule = (function () {

    var Dom = {
        gridView: $("#grid-view"),
        detailView: $("#detail-view"),
        orderView: $("#order-view"),
        btnSearchBook: $("#btnSearchBook"),
        txtSearchBook: $("#txtSearchBook"),
    }

    function getOrder() {

        $.ajax({
            url: 'https://localhost:44310//order//Index',
            type: "GET",
            contentType: "application/json",
            dataType: "html",
            success: function (result) {
                if (!result) {
                    return;
                }

                Dom.gridView.empty();
                Dom.detailView.empty();
                Dom.orderView.html(result);

                $("#loading").hide();
            },
            error: function (message) {
                console.log(message.statusText);
                $("#loading").hide();
            }
        });
    }

    function getDetail(bookId) {

        $.ajax({
            url: 'https://localhost:44310//book//BookDetail',
            type: "GET",
            contentType: "application/json",
            dataType: "html",
            data: {
                bookId: bookId
            },
            success: function (result) {
                if (!result) {
                    return;
                }

                Dom.gridView.empty();
                Dom.detailView.html(result);

                $("#loading").hide();
            },
            error: function (message) {
                console.log(message.statusText);
                $("#loading").hide();
            }
        });
    }

    function getBooks(paraValue, isGetByStoreName) {

        var url = 'https://localhost:44310//book/GetListAvailableBooks'
        var data =
        {
            storeName: paraValue
        }

        if (!isGetByStoreName) {
            url = 'https://localhost:44310//book/FindByKeyword'
            data =
            {
                keyWord: paraValue
            }
        }

        $("#loading").show();
        $.ajax({
            url: url,
            type: "GET",
            contentType: "application/json",
            dataType: "json",
            data: data,
            success: function (result) {
                if (!result) {
                    return;
                }

                Dom.gridView.empty();
                Dom.orderView.empty();
                Dom.detailView.empty();
                $.each(result, function (index, value) {
                    Dom.gridView
                        .append($('<div>')
                            .attr({ 'uid': value.bookId })
                            .addClass('card-book')
                            .click(function () {
                                $(this).addClass('selected-card') // add class to clicked element
                                    .siblings() // get siblings
                                    .removeClass('selected-card'); // remove class from sibling elements
                            })
                            .append($('<div>')
                                .addClass('container-book')
                                .append($('<h4>')
                                    .append($('<b>')

                                        .append($("<a>")
                                            .css("cursor", "pointer")
                                            .html(value.bookName)
                                            .click(function () {
                                                getDetail(value.bookId);
                                            })
                                        )
                                    )
                                )
                                .append($('<p>')
                                    .html("Author: " + "<b>" + value.author + "</b>")
                                )
                                .append($('<p>')
                                    .html("ISBN Code: " + "<b>" + value.isbncode + "</b>")
                                )
                                .append($('<p>')
                                    .addClass('price')
                                    .html("Price: " + "<b>" + value.price + '$' + "</b>")
                                )
                                .append($('<p>')
                                    .html("In Stock: " + "<b>" + value.numberInStock + "</b>")
                                )
                                .append($('<p>')
                                    .append($('<button>')
                                        .html('Order')
                                        .click(function () {
                                            var order = {
                                                BookId: $(this).parents("div.card-book").attr("uid"),
                                                Quantity: 1,
                                            }

                                            // Clear detail view
                                            $("#detail-view").empty();
                                            createOrder(order, true);
                                        })
                                    )
                                )
                            )
                        )
                });

                $("#loading").hide();
            },
            error: function (message) {
                console.log(message.statusText);
                $("#loading").hide();
            }
        });
    }

    function createOrder(order, isBack) {
        $("#loading").show();
        $.ajax({
            url: 'https://localhost:44310//Order//CreateOrder',
            type: "POST",
            contentType: "application/json",
            dataType: "json",
            data: JSON.stringify(order),
            success: function (result) {
                if (!result) {
                    return;
                }

                if (result.success) {
                    Utils.showPopup("Information", "Create order successful!");
                } else {
                    if (result.returnData == Utils.OrderResult.ExceedQuantityStock) {
                        Utils.showPopup("Warning", "The quantity ordered does not exceed the quantity in stock");
                        $("#loading").hide();
                        return;
                    }
                    else if (result.returnData == Utils.OrderResult.QuantityInValid) {
                        Utils.showPopup("Error", "Quantity cannot be 0");
                        $("#loading").hide();
                        return;
                    }
                    else if (result.returnData == Utils.OrderResult.OrderFail) {
                        Utils.showPopup("Error", "Create order fail!");
                        $("#loading").hide();
                        return;
                    }
                    else {
                        Utils.showPopup("Error", "Create order fail!");
                        $("#loading").hide();
                        return;
                    }
                }

                // refresh order notification
                Utils.refeshOrderNotification();

                if (isBack) {
                    // refresh all books.
                    var storeName = $(".selected-card").attr("storename");
                    getBooks(storeName, true);
                }
                else {
                    var bookId = $(".single-item").attr("uid");
                    bookModule.getDetail(bookId);
                }

                $("#loading").hide();
            },
            error: function (message) {
                if (message.responseText) {
                    window.location.href = '/Error/NoPermission';
                } else {
                    Utils.showPopup("Error", "Create order fail!");
                }

                console.log(message.statusText);
                $("#loading").hide();
            }
        });
    }

    function initEvents() {
        Dom.btnSearchBook.click(function (e) {
            e.preventDefault();
            getBooks(Dom.txtSearchBook.val(), false);
        })
    }

    return {
        init: function () {
            initEvents();
        },
        getBooks: getBooks,
        createOrder: createOrder,
        getDetail: getDetail,
        getOrder: getOrder
    };
})();

bookModule.init();