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
        type: "GET",
        data: {latitude: pos.latitude, longitude: pos.longitude},
        url: "../api/services/location",
        success: function(response){
            document.getElementById("weatherWidget").innerHTML = response;
        }
    })
}

$(function(){
    getLocation();
})