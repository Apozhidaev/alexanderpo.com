window.onerror = function () {
    var message = JSON.stringify(arguments) + 
        "\n\n\nDo you want clear local storage?";
    if (window.confirm(message)) {
        window.localStorage.clear();
        window.location.reload();
    }
};