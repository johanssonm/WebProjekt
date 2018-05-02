$("#calcButton").click(function() {

    let n = $("#Number").val();

    $.ajax({
        type: "GET",
        data: {n},
        url: "../jquery/calc",
        dataType: 'json',
        //contentType: false,
        processData: true,
        success: function(response) {
            if (response.success) {
                alert(response.result);
                console.log("Success");
                console.log(response);

            } else {
                // DoSomethingElse()
                alert(response.responseText);
                console.log("Faulty ok");
            }
        },

        error: function(response) {
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