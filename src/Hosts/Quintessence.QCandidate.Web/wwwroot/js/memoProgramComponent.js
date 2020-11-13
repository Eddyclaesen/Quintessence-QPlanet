
var el = document.getElementsByClassName("tabs")[0];
var instance = M.Tabs.init(el);

var memos = document.getElementById('memo-list');

// Example 1 - Simple list
new Sortable(memos, {
    animation: 150,
    ghostClass: 'ghost-background-class'
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
}