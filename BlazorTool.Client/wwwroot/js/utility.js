window.BlazorTool = {
    copyToClipboard: async function (text) {
        try {
            await navigator.clipboard.writeText(text);
            console.log('Text copied to clipboard successfully!');
            return true;
        } catch (err) {
            console.error('Could not copy text: ', err);
            return false;
        }
    }
};

window.BlazorToolWeather = {
    isSupported: function(){
        return !!navigator.geolocation;
    },
    getLocation: function () {
        return new Promise(function (resolve, reject) {
            if (!navigator.geolocation) {
                reject('Geolocation unsupported');
                return;
            }
            navigator.geolocation.getCurrentPosition(function (pos) {
                resolve({ lat: pos.coords.latitude, lng: pos.coords.longitude });
            }, function (err) {
                reject(err.message || 'Geolocation error');
            }, { enableHighAccuracy: false, timeout: 8000, maximumAge: 600000 });
        });
    }
};