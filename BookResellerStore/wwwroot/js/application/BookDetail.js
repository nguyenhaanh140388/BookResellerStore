var bookDetailModule = (function () {

    var Dom = {
        previous: $("#previous"),
        btnCreateOrder: $("#btnCreateOrder"),
        linkViewOrder: $("#viewOrder"),
    }


    function initEvents() {

        Dom.previous.click(function (e) {
            e.preventDefault();
            $("#detail-view").empty();
            var storeName = $(".selected-card").attr("storename");
            bookModule.getBooks(storeName, true);
        })

        $("#previousDetail").click(function (e) {
            e.preventDefault();
        })

        Dom.btnCreateOrder.click(function (e) {
            e.preventDefault();

            var order = {
                BookId: $(".single-item").attr("uid"),
                Quantity: parseInt($("#quantity").val()),
            }

            bookModule.createOrder(order, false);
        })

        Dom.linkViewOrder.click(function (e) {
            e.preventDefault();

            bookModule.getOrder();
        })
    }

    function setQuantity(upordown, numberInStock) {
        var quantity = document.getElementById('quantity');

        if (parseInt(quantity.value) > 1) {
            if (upordown == 'up') {
                if (parseInt(quantity.value) == numberInStock) {
                    return;
                }

                ++document.getElementById('quantity').value;
            }
            else if (upordown == 'down') { --document.getElementById('quantity').value; }
        }
        else if (parseInt(quantity.value) == 1) {
            if (upordown == 'up') { ++document.getElementById('quantity').value; }
        }
        else { document.getElementById('quantity').value = 1; }
    }

    return {
        init: function () {
            initEvents();
        },
        setQuantity: setQuantity
    };
})();

bookDetailModule.init();