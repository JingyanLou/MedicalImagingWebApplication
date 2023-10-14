var events = [];

$(".events").each(function () {
    var title = $(".doctor", this).text().trim() + ($(".availability", this).text().trim() === 'True' ? " - Available" : " - Not Available");

    // Extract and validate the date
    var date = moment($(".date", this).text().trim(), "DD/MM/YYYY");
    if (!date.isValid()) return;

    // Extract, validate and format start and end times
    var startTime = moment(date).set({
        hour: moment($(".start", this).text().trim(), "h:mm A").hour(),
        minute: moment($(".start", this).text().trim(), "h:mm A").minute()
    });

    if (!startTime.isValid()) return;  // Validate start time

    // Only "start" time is used here as the hardcoded event doesn't have an "end" time.
    var event = {
        title: title,
        start: startTime.format('YYYY-MM-DD'),  // Convert to the format: 'YYYY-MM-DDTHH:mm:ss'
        color: $(".availability", this).text().trim() === 'True' ? "#28a745" : "#dc3545"
    };

    events.push(event);
    console.log(events.length);  // Log the length after each event addition
    console.log(events)
});


$("#calendar").fullCalendar({
    locale: 'au',
    events: events
});
