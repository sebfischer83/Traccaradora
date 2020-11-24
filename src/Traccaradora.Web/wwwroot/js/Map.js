export function initMap() {
    var mymap = L.map('mapid').setView([51.505, -0.09], 13);
    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        'attribution': 'Kartendaten &copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> Mitwirkende',
        'useCache': true
    }).addTo(mymap);
    window.myMap = mymap;
}

export function addMarker(lat, lon, description) {
    var marker = L.marker([lat, lon]).addTo(window.myMap);
    marker.bindPopup(description);
    window.myMap.setView([lat, lon], 13);
}