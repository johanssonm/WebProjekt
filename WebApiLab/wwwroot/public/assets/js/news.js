
function getLocation() {
    $.ajax({
        success: function(response){
            navigator.geolocation.getCurrentPosition(sendLocation)
        }
    });
};

function sendLocation(position) {
    let pos = position.coords;
    $.ajax({
        type: "POST",
        data: {la: pos.latitude, lo: pos.longitude},
        url: "",
        success: function(response){
            
        }
    })
}

$(function(){
    getLocation();
})


$("#editnewsButton").click(function () {
    // alert("Knappen funkar");    

    let Id = $("#EditId").val();
    let Header = $("#EditHeader").val();
    let Intro = $("#EditIntro").val();
    let Paragraf = $("#EditParagraf").val();

    $.ajax({
        type: "GET",
        data: { Id, Header, Intro, Paragraf },
        url: "../news/EditNews",
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

$("#addnewsButton").click(function() {
    // alert("Knappen funkar");    

    let Header = $("#Header").val();
    let Intro = $("#Intro").val();
    let Paragraf = $("#Paragraf").val();
    let CategoryName = $("#CategoryName").val();

    $.ajax({
        type: "GET",
        data: { Header,Intro,Paragraf,CategoryName },
        url: "../news/AddNews",
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

$("#removenewsButton").click(function () {
    // alert("Knappen funkar");    

    let newsid = $("#newsid").val();

    $.ajax({
        type: "GET",
        data: {newsid},
        url: "../news/RemoveNews",
        //contentType: false,
        // processData: false,
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

$("#countButton").click(function () {
    // alert("Knappen funkar");
    $.ajax({
        type: "GET",
        // data: { n },
        url: "../news/CountNews",
        //contentType: false,
        // processData: false,
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

$("#seedButton").click(function () {
    // alert("Knappen funkar");
    $.ajax({
        type: "GET",
        // data: { n },
        url: "../news/SeedNews",
        //contentType: false,
        // processData: false,
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

$("#usernameButton").click(function () {

    let username = $("#Username").val();

    $.ajax({
        type: "GET",
        data: { username },
        url: "../jquery/validateusername",
        dataType: 'json',
        //contentType: false,
        processData: true,
        success: function (response) {
            if (response.success) {
                alert(response.username);
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



