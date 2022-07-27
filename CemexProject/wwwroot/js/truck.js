"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/truckHub").build();

let map;
let marker;
let infowindow;
let markers = [];
let first = true;
const iconBlue = {
    url: "/Tblue.png",
    scaledSize: new google.maps.Size(30, 30), // scaled size
};
const iconRed = {
    url: "/Tred.png",
    scaledSize: new google.maps.Size(30, 30), // scaled size
};

$(function () {
    connection.start().then(function () {
		CreateMap();
        InvokeTrucks();

    }).catch(function (err) {
        return console.error(err.toString());
    });
});

function InvokeTrucks() {
    connection.invoke("SendTrucks").catch(function (err) {
        return console.error(err.toString());
    });
}

connection.on("ReceivedTrucks", function (trucks) {
        updateMap(trucks);
});


connection.on("updateTruckLoc", function (oldtruck, newtruck) {
    removeOldMarker(oldtruck);
    setMarker(newtruck);
});


function removeOldMarker(oldtruck) {
    for (var i = 0; i < markers.length; i++) {
        if (parseFloat(markers[i].getPosition().lat()) == parseFloat(oldtruck.lat) && parseFloat(markers[i].getPosition().lng()) == parseFloat(oldtruck.lng)) {
            markers[i].setMap(null);
            markers[i] = null;
        }
    }
}

function CreateMap() {
    map = new google.maps.Map(document.getElementById("map"),
        {
            center: {
                lat: 27.179933913267654,
                lng: 31.185619491610698
            },
            zoom: 10
        });
}
function updateMap(trucks) {
    
		//Display markers on map
		$.each(trucks, function (index, truck) {
			setMarker(truck);
        });

}

//create marker method
function setMarker(truck) {
    if (String(truck.type).trim() == String("marcedes").trim())
    {
        marker = new google.maps.Marker({
            position: new google.maps.LatLng(truck.lat, truck.lng),
            icon: iconBlue,
            map: map
        });
    }
    else
    {
        marker = new google.maps.Marker({
            position: new google.maps.LatLng(truck.lat, truck.lng),
            icon: iconRed,
            map: map
        });
    }
    //create infowindow for truck
    infowindow = new google.maps.InfoWindow({
        content: "<div class='infoDiv'><h2>" +
            truck.id + "</h2>" + "<div><h4>Name: " +
            truck.driver_name + "</h4>"+"<h4>lastUpdteDate: " +
            truck.lastUpdateDate + "</h4></div></div >" 
    });
    google.maps.event.addListener(
        marker, 'click', infoCallback(infowindow, marker));
    markers.push(marker);
}

function infoCallback(infowindow, marker) {
    return function () { infowindow.open(map, marker); };
}

