var homeTable = function () {
    $(document).ready(function () {
        $.get('/api/crypto', function (data) {
            console.log(data)
            var html = '';
            for (var i = 0; i < data.length; i++) {
                html += '<tr class="cryptotable">' +
                    '<td>' +
                    '<picture>' +
                    '<img src="' + data[i].logo + '" alt="logo" width="36" height="36">' +
                    '</picture>' +
                    '&emsp;' + data[i].name + '</td>' +
                    '<td class="text-muted">' + data[i].ticker + '</td>' +
                    '<td>' + '$' + data[i].price.toFixed(2) + '</td>' +
                    '<td>' + '$' + data[i].marketCap.toFixed(0).toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",") + '</td>';
                if (data[i].percentChange >= 0) {
                    html +=
                        '<td>' +
                        '<span class="text-success">' +
                        '<i class="fas fa-caret-up me-1">' + data[i].percentChange.toFixed(2) + '%' + '</i>' +
                        '</span>' +
                        '</td>';
                } else {
                    html +=
                        '<td>' +
                        '<span class="text-danger">' +
                        '<i class="fas fa-caret-down me-1">' + data[i].percentChange.toFixed(2) + '%' + '</i>' +
                        '</span>' +
                        '</td>';
                }
                html +=
                    '<td>' + data[i].circulatingSupply.toFixed(0).toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",") + '</td>';
                html +=
                    '<td class=" text-center">' +
                    '<a href="/Assets/Details?assetName=' + data[i].name +
                    '"class="button btn btn-primary rounded-pill">Details</a>' +
                    '</td>' +
                    '<td>' +
                    '<a href="/Assets/Swap" class="button btn btn-success rounded-pill">Buy</a>' +
                    '</td>' +
                    '</tr>';
            }
            $('#tbody').first().append(html);
        })
    })
}


function Fav(asssetTicker, item) {

    var t = $("input[name='__RequestVerificationToken']").val();
    $.ajax({
        url: '/Trading/AddToFavorites',
        type: 'POST',
        data: {
            ticker: asssetTicker
        },
        headers: {
            "RequestVerificationToken": t
        },
        success: function (data) {
            $(item).toggleClass("fill");
        },
        error: function (jqXHR) { // Http Status is not 200
        },
        complete: function (jqXHR, status) { // Whether success or error it enters here
        }
    });
};