$("#addnewsButton").click(function() {
    // alert("Knappen funkar");    

    let Header = $("#Header").val();
    let Intro = $("#Intro").val();
    let Paragraf = $("#Paragraf").val();
    let AuthorId = $("#AuthorId").val();
    let Categories = [];

    if( $('#CategoryId :selected').length > 0){
        //build an array of selected values
        $('#CategoryId :selected').each(function(i, selected) {
            Categories[i] = $(selected).val();
        });
    }

    $.ajax({
        type: "GET",
        data: { Header:Header,Intro:Intro,Paragraf:Paragraf,CategoryId:Categories,AuthorId:AuthorId },
        url: "/news/AddNews",
        contentType: true,
        processData: true,
        success: function (response) {
            if (response.success) {
                alert(response.message);
                console.log("Success");
                console.log(response);

            } else {
                // DoSomethingElse()
                alert(response.responseText);
                console.log("Faulty ok");
            }
        },

        error: function (response) {
            alert("Error!");
            console.log("Error");
        }

    });
});