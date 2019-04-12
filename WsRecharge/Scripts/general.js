$('.numberonly').keydown(function (e) {
    if (e.ctrlKey || e.altKey) {
        e.preventDefault();
    } else {
        var key = e.keyCode;
        if (!((key == 9) || (key == 8) || (key == 46) || (key >= 35 && key <= 40) || (key >= 48 && key <= 57) || (key >= 96 && key <= 105))) {
            e.preventDefault();
        }
    }
});

function formatDate(ms) {
    var date = new Date(parseInt(ms));
    var hour = date.getHours();
    var mins = date.getMinutes() + '';
    var time = "AM";
    // find time
    if (hour >= 12) {
        time = "PM";
    }
    // fix hours format
    if (hour > 12) {
        hour -= 12;
    }
    else if (hour == 0) {
        hour = 12;
    }
    // fix minutes format
    if (mins.length == 1) {
        mins = "0" + mins;
    }
    // return formatted date time string
    return (date.getDate().toString().length == 1 ? "0" : "") + (date.getDate()) + "/" + ((date.getMonth() + 1).toString().length == 1 ? "0" : "") + (date.getMonth() + 1) + "/" + date.getFullYear() + " " + hour + ":" + mins + " " + time;
}