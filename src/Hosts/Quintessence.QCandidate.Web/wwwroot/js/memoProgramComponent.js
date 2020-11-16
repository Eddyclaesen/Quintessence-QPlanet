var el = document.getElementsByClassName("tabs")[0];
var instance = M.Tabs.init(el);


var memos = document.getElementById('memo-list');

// Example 1 - Simple list
new Sortable(memos,
    {
        animation: 150,
        ghostClass: 'ghost-background-class',
        onUpdate: function (/**Event*/ evt) {
            saveMemos();
        },
    });

function openMemo(evt, memoName) {
    var i, tabcontent, tablinks;
    tabcontent = document.getElementsByClassName("memo");
    for (i = 0; i < tabcontent.length; i++) {
        tabcontent[i].style.display = "none";
    }
    tablinks = document.getElementsByClassName("list-group-item");
    for (i = 0; i < tablinks.length; i++) {
        tablinks[i].className = tablinks[i].className.replace(" active", "");
    }

    tablinks = document.getElementsByClassName("list-group-item-new");
    for (i = 0; i < tablinks.length; i++) {
        tablinks[i].className = tablinks[i].className.replace(" active", "");
    }

    document.getElementById(memoName).style.display = "block";
    evt.currentTarget.className += " active";
};

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
        },
        failure: function (response) {
            console.log(response);
        }
    }); 

}

