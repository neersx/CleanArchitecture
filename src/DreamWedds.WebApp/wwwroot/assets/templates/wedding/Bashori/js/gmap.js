jQuery(function($) {
    // Asynchronously Load the map API 
    var script = document.createElement('script');
    script.src = "http://maps.googleapis.com/maps/api/js?sensor=false&callback=initialize";
    document.body.appendChild(script);
});

function initialize() {
    var map;
    // Set the latitude & longitude for our location (London Eye)
    var myLatlng = new google.maps.LatLng(32.725508, -117.222057);
    var mapOptions = {
    	scrollwheel: false, // stop scroll to zoom
        center: myLatlng, // Set our point as the centre location
        zoom: 14, // Set the zoom level
        mapTypeId: 'roadmap' // set the default map type
    };
            
    // Display a map on the page
    map = new google.maps.Map(document.getElementById("map_canvas"), mapOptions);
    // Allow our satellite view have a tilted display (This only works for certain locations) 
    map.setTilt(45);

    // Create our info window content   
    var infoWindowContent = '<div class="info_content">' +
        '<h3>John Doe</h3>' +
        '<p>4918 N Harbor Dr #204a, San Diego, CA 92106, United States</p>' +
        '<p><a href="http://themepack.net" target="_blank">themepack.net</a></p>' +
        '<p>+1 619-940-5330</p>' +
    	'</div>';

    // Initialise the inforWindow
    var infoWindow = new google.maps.InfoWindow({
        content: infoWindowContent
    });
                
    // Add a marker to the map based on our coordinates
    var marker = new google.maps.Marker({
        position: myLatlng,
        map: map,
        title: 'ThemePackNet, BD',
        "icon": 'images/gmaplogo.png'
    });

    // Display our info window when the marker is clicked
    google.maps.event.addListener(marker, 'click', function() {
        infoWindow.open(map, marker);
    });



 }