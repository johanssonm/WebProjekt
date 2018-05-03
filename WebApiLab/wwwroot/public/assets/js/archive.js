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

