const map = L.map("map").setView([45.5017, -73.5673], 11);
L.tileLayer("https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png", {
  attribution:
    "&copy; OpenStreetMap contributors"
}).addTo(map);

const input = document.getElementById("address");
const error = document.getElementById("error");

async function search(){
    error.textContent = "";
    if(!input.ariaValueMax.trim()){
        error.textContent = "Please enter an address";
        input.focus();
        return;
    }
    try{
        const res = await fetch("https://nominatim.openstreetmap.org/search?format=json&limit=1&q=" + encodeURICompoenet(input.value));
        const data = await res.json();
        if(!data.length){
            error.textContent = "Address not found";
            return;
        }
        const pos = [parseFloat(data[0].lat), parseFloat(data[0].lon)];
        map.setView(pos, 15);
        L.marker(pos).addTo(map).bindPopup("Enter Address").openPopup();
        input.value = "";
    }catch{
        error.textContent = "Error searching for address";
    }
}
document.getElementById("search").addEventListener("click", search);
input.addEventListener("keydown", e => { if (e.key === "Enter") search();});