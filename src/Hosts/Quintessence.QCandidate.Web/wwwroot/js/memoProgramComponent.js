function saveCalendarDay(evt, calendarDayId) {

    var text = document.getElementById(calendarDayId).value;
    var memoProgramComponentId = document.getElementById("memoProgramComponentId").value;

    var url = window.location.protocol + "//" + window.location.hostname + ":44329/MemoProgramComponents/" + memoProgramComponentId + "/calendarDays/" + calendarDayId;

    $.post(url, { note: text }, function (result) {
        $("span").html(result);
    });

    //alert(url);

};