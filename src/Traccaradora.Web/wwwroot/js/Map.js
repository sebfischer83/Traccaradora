export function initMap() {
    var mymap = L.map('mapid').setView([51.505, -0.09], 13);
    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        'attribution': 'Kartendaten &copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> Mitwirkende',
        'useCache': true
    }).addTo(mymap);
    let map = new MapObj(mymap);
    window.myMap = map;
}

export function clear() {
    let mapObj = window.myMap;
    mapObj.removeAllMarker();
}

export function setView() {
    let mapObj = window.myMap;
    let arr = [];
    // create an array of latlng coordinates from all markers
    mapObj.markers.forEach(m => {
        arr.push(m.getLatLng());
    });
    // get the bounds for the map to include all markers
    var bounds = new L.LatLngBounds(arr);
    mapObj.map.fitBounds(bounds);
}

export function addMarker(lat, lon, description) {
    var marker = L.marker([lat, lon]).addTo(window.myMap.map);
    marker.bindPopup(description);
    window.myMap.addMarker(marker);
}

class MapObj {
    constructor(map) {
        this.map = map;
        this.markers = new Set();
    }

    addMarker(marker) {
        this.markers.add(marker);
    } 

    removeAllMarker() {
        this.markers.forEach(m => {
            this.map.removeLayer(m);
        });
        this.markers.clear();
    }
}