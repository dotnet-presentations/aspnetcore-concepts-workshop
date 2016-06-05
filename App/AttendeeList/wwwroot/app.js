var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator.throw(value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : new P(function (resolve) { resolve(result.value); }).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments)).next());
    });
};
function getOs() {
    return new Promise(resolve => {
        var xhr = new XMLHttpRequest();
        xhr.open('GET', '/api/platform', true);
        xhr.send(null);
        xhr.onreadystatechange = function (e) {
            if (xhr.readyState === 4) {
                resolve(xhr.responseText);
            }
        };
    });
}
function getAttendees() {
    return new Promise(resolve => {
        var xhr = new XMLHttpRequest();
        xhr.open('GET', '/api/attendees', true);
        xhr.send(null);
        xhr.onreadystatechange = function (e) {
            if (xhr.readyState === 4) {
                var obj = JSON.parse(xhr.responseText);
                resolve(obj);
            }
        };
    });
}
function init() {
    return __awaiter(this, void 0, void 0, function* () {
        var attendees = yield getAttendees();
        var os = yield getOs();
        var osEl = document.getElementById('os');
        var attendeesEl = document.getElementById('attendees');
        osEl.innerText = os;
        for (var i = 0; i < attendees.length; ++i) {
            var li = document.createElement('li');
            li.innerText = attendees[i].FirstName + ' ' + attendees[i].LastName + ' (' + attendees[i].Company + ')';
            attendeesEl.appendChild(li);
        }
    });
}
init();
//# sourceMappingURL=app.js.map