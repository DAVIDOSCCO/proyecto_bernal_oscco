(function (cibertec) {
    window.cibertec.getLocation = function () {
        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(showPosition);
        }
    }

    function showPosition(position) {
        var location = { lat: -12.122453/*position.coords.latitude*/, lng: -77.0275475 /*position.coords.longitude*/ };
        var map = new google.maps.Map(document.getElementById('googleMap'), { zoom: 15, center: location });

        var marker = new google.maps.Marker({ position: location, map: map, title: 'Mi Casa!' });
    }

})(window.cibertec = window.cibertec || {});