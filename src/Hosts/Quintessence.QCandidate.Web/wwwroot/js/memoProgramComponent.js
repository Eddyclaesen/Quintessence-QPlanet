function saveCalendarDay(evt, calendarDayId) {

    var text = document.getElementById(calendarDayId).value;
    var memoProgramComponentId = document.getElementById("memoProgramComponentId").value;
    var urlShort = "/MemoProgramComponents/" + memoProgramComponentId + "/calendarDays/" + calendarDayId;

    $.post(urlShort, { note: text }, function (result) {
        if (result.status === 'error') {
            console.log(result.message);
        }
    });
    
};

class Memo {
    constructor(id, position, title, content) {
        this.Id = id;
        this.Position = position;
        this.Title = title;
        this.Content = content;
    }
}

function saveMemos() {

    var memoProgramComponentId = document.getElementById("memoProgramComponentId").value;
    var urlShort = "/MemoProgramComponents/" + memoProgramComponentId + "/memos";

    var memosToUpdate = [];
    var everyChild = document.querySelectorAll("#memo-list div");
    for (var i = 0; i < everyChild.length; i++) {
        memosToUpdate.push(new Memo(everyChild[i].id, i+1, null, null));
    }

    $.ajax({
        contentType: 'application/json; charset=utf-8',
        type: 'POST',
        url: urlShort,
        data: JSON.stringify(memosToUpdate),
        success: function () {
            console.log("No issue saving the calendar entry.");
        },
        failure: function (response) {
            console.log(response);
        }
    }); 

}

