interface Attendee {
    FirstName: string,
    LastName: string,
    Company: string,
    Email: string
}

function getOs(): Promise<string> {
    return new Promise(resolve => {
        var xhr = new XMLHttpRequest();
        xhr.open('GET', '/api/platform', true)
        xhr.send(null);

        xhr.onreadystatechange = function (e) {
            if (xhr.readyState === 4) {
                resolve(xhr.responseText);
            }
        };
    });
}

function getAttendees(): Promise<Attendee[]> {
    return new Promise(resolve => {
        var xhr = new XMLHttpRequest();
        xhr.open('GET', '/api/attendees', true)
        xhr.send(null);

        xhr.onreadystatechange = function (e) {
            if (xhr.readyState === 4) {
                var obj = JSON.parse(xhr.responseText) as Attendee[];
                resolve(obj);
            }
        };
    });
}

async function init() {
    var attendees = await getAttendees();
    var os = await getOs();
    var osEl = document.getElementById('os');
    var attendeesEl = document.getElementById('attendees');

    osEl.innerText = os;

    for (var i = 0; i < attendees.length; ++i) {
        var li = document.createElement('li');
        li.innerText = attendees[i].FirstName + ' ' + attendees[i].LastName + ' (' + attendees[i].Company + ')';
        attendeesEl.appendChild(li);
    }
}

init();