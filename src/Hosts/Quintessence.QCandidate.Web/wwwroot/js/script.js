const containerHeight = 1200;
const containerWidth = 600;
const minutesinDay = 60 * 12;
let collisions = [];
let width = [];
let leftOffSet = [];

// append one event to calendar
var createEvent = (height, top, left, units, time, title, assessors, location, documentname, documentlink) => {

  let node = document.createElement("DIV");
  node.className = "event";
  var html = 
  "<span class='title'>" + title + "</span> \
  <br><span class='location'> " + time + " - " + location + "</span>";
  
  if(assessors != ''){
    html += "<span class='assessor'>(" + assessors + ")</span>";
  }
  
  if(documentname != null && documentlink != null){
    html += "<br><span class='documentlink'><a href='" + documentlink + "' target='_blank'>" + documentname + "</a></span>";
  }

  // Customized CSS to position each event
  node.innerHTML = html;
  node.style.width = (containerWidth/units) + "px";
  node.style.height = height + "px";
  node.style.top = top + "px";
  node.style.left = 54 + left + "px";

  document.getElementById("events").appendChild(node);
}

/* 
collisions is an array that tells you which events are in each 30 min slot
- each first level of array corresponds to a 30 minute slot on the calendar 
  - [[0 - 30mins], [ 30 - 60mins], ...]
- next level of array tells you which event is present and the horizontal order
  - [0,0,1,2] 
  ==> event 1 is not present, event 2 is not present, event 3 is at order 1, event 4 is at order 2
*/

function getCollisions (events) {

  //resets storage
  collisions = [];

  for (var i = 0; i < 24; i ++) {
    var time = [];
    for (var j = 0; j < events.length; j++) {
      time.push(0);
    }
    collisions.push(time);
  }

  events.forEach((event, id) => {
    let end = event.endPixelOffset;
    let start = event.startPixelOffset;
    let time = event.time;
    let title = event.title;
    let assessors = event.assessors;
    let location = event.location;
    let documentname = event.documentName;
    let documentlink = event.documentLink;
    let order = 1;

    while (start < end) {
      timeIndex = Math.floor(start/30);

      while (order < events.length) {
        if (collisions[timeIndex].indexOf(order) === -1) {
          break;
        }
        order ++;
      }

      collisions[timeIndex][id] = order;
      start = start + 30;
    }

    collisions[Math.floor((end-1)/30)][id] = order;
  });
};

/*
find width and horizontal position

width - number of units to divide container width by
horizontal position - pixel offset from left
*/
function getAttributes (events) {

  //resets storage
  width = [];
  leftOffSet = [];

  for (var i = 0; i < events.length; i++) {
    width.push(0);
    leftOffSet.push(0);
  }

  collisions.forEach((period) => {

    // number of events in that period
    let count = period.reduce((a,b) => {
      return b ? a + 0 : a;
    })

    if (count > 1) {
      period.forEach((event, id) => {
        // max number of events it is sharing a time period with determines width
        if (period[id]) {
          if (count > width[id]) {
            width[id] = count;
          }
        }

        if (period[id] && !leftOffSet[id]) {
          leftOffSet[id] = period[id];
        }
      })
    }
  });
};

var layOutDay = (events) => {

// clear any existing nodes
var myNode = document.getElementById("events");
myNode.innerHTML = '';

  getCollisions(events);
  getAttributes(events);

  events.forEach((event, id) => {
    let height = (event.endPixelOffset - event.startPixelOffset) / minutesinDay * containerHeight;
    let top = event.startPixelOffset / minutesinDay * containerHeight; 
    let end = event.endPixelOffset;
    let start = event.startPixelOffset;
    let time = event.time;
    let title = event.title;
    let assessors = event.assessors;
    let location = event.location;
    let documentname = event.documentName;
    let documentlink = event.documentLink;
    let units = width[id];
    if (!units) {units = 1};
    let left = (containerWidth / width[id]) * (leftOffSet[id] - 1) + 10;
    if (!left || left < 0) {left = 10};
    createEvent(height, top, left, units, time, title, assessors, location, documentname, documentlink);
  });
}