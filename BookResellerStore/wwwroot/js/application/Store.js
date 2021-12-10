var storeModule = (function () {

    function getStores() {
        $.ajax({
            url: 'https://localhost:44310//store/GetStores',
            type: "GET",
            contentType: "application/json",
            dataType: "json",
            success: function (result) {
                if (!result) {
                    return;
                }

                $.each(result, function (index, value) {
                    $('.left')
                        .append($('<div>')
                            .attr({ 'storename': value })
                            .addClass('card')
                            .css({ "padding": 10})
                            .click(function () {
                                $(this).addClass('selected-card') // add class to clicked element
                                    .siblings() // get siblings
                                    .removeClass('selected-card'); // remove class from sibling elements
                                var storeName = $(this).attr("storename");
                                bookModule.getBooks(storeName, true);
                                $("#detail-view").empty();
                                $("#order-view").empty();
                            })
                            .append($('<div>')
                                .addClass('container')
                                .append($('<img>')
                                    .attr({ 'src': '/images/shop-icon.png' })
                                    .css({ "width": 80, "height": 80})
                                )
                                .append($('<h4>')
                                    .append($('<b>')
                                        .html('Store ' + value)
                                    )
                                )
                            )
                        )
                });
            },
            error: function (message) {
                console.log(message.statusText);
            }
        });
    }

    function initStore() {
        // delete content old view.
        getStores();
    }

    function initEvents() {

    }

    return {
        init: function () {
            initStore();
            initEvents();
        }
    };
})();

storeModule.init();