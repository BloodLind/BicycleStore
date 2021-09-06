var tokenKey = "token";
var user = {
    firstname: 'firstname',
    secondname: 'secondname',
    email: 'email'
};
initLoginButtons();

function initLoginButtons() {
    var container = document.querySelector(".container");
    var button = document.createElement('a');
    console.log(sessionStorage.getItem(tokenKey));
    var token = sessionStorage.getItem(tokenKey);
    var email = sessionStorage.getItem(user.email);
    if (token == null) {
        button.innerText = 'Login';
        button.classList.add('btn', 'btn-primary', 'bg-primary');
        button.href = 'account.html';
    } else {
        button.innerHTML = `
        <div class="d-flex flex-row justify-content-around">
            <h3>` + email + `</h3>
            <a class="btn btn-primary bg-primary" id="logout">Logout</a>
        </div>`;
    }
    container.appendChild(button);
    var logout = document.querySelector('#logout');
    if (logout != null) {
        logout.addEventListener('click', function() {
            sessionStorage.removeItem(tokenKey);
            sessionStorage.removeItem(user.firstname);
            sessionStorage.removeItem(user.secondname);
            sessionStorage.removeItem(user.email);
            document.location.href = document.location.href;
        });
    }
}