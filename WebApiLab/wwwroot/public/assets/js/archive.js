function getNews(callback) {
    $.ajax({
        type: "GET",
        url: "../news/JsonFeed/",
        success: function(response){
            callback(response);
        }
    })
    
}

$(function(){
    var $records = getNews(drawTable);

    function drawTable(result){
        $("#myTable").dynatable({
            dataset: {
                records: result
              }
        });
    
    }


})

$("#myTable").prop("class", "table table-striped");

$("#dynatable-query-search-myTable").prop("class", "form-control");

