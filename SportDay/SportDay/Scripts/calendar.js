var Bookings = [];
$(".Bookings").each(function () {
    var memorandum = $(".Info", this).text().trim();
    var Email = $(".Email", this).text().trim();
    var Name = $(".Name", this).text().trim();
    var Date = $(".Date", this).text().trim();
    var Booking = {
        "Name": Name,
        "Email": Email,
        "memorandum": memorandum,
        "Date":Date
    };
    Bookings.push(Booking);
    
});
$("#calendar").fullCalendar({
    locale: 'au',
    events: Bookings,
    dayClick: function (date, allDay, jsEvent, view) {
        var d = new Date(date);
        var m = moment(d).format("DD/MM/YYYY");
        m = encodeURIComponent(m);
        var uri = "/Bookings/Create?date=" + m;
        $(location).attr('href', uri);
    }
});
