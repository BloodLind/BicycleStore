var tokenKey = "token";
var user = {
    firstname: 'firstname',
    secondname: 'secondname',
    email: 'email'
};
var apikey = "https://localhost:44373/api/Account/";

initLoginForm();

function initLoginForm() {
    document.getElementById('submitLogin').addEventListener('click', function() {
        getTokenAsync()
    })
}

async function getTokenAsync() {
    const credentials = {
        login: document.querySelector('#login').value,
        password: document.querySelector('#password').value
    }
    console.log(credentials);
    const response = await fetch(apikey, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(credentials)
    })

    const data = await response.json()
    if (response.ok === true) {
        sessionStorage.setItem(tokenKey, data.token);
        sessionStorage.setItem(user.firstname, data.firstname);
        sessionStorage.setItem(user.secondname, data.secondname);
        sessionStorage.setItem(user.email, data.email);
        document.location.href = 'index.html';
    } else {
        var massage = document.createElement('pre');
        massage.classList.add('text-danger', 'h3', 'text-center');
        massage.innerText = "Wrong password or login!";
        document.querySelector('body').appendChild(massage);
    }
}