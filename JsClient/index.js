var apikey = "https://localhost:44373/api/Bicycles/";
var tokenKey = "token";
getApiData();


async function getApiData() {
    var token = sessionStorage.getItem(tokenKey);
    var response = await fetch(apikey, {
        method: 'GET',
        headers: {
            'Access-Control-Allow-Origin': '*',
            'Authorization': 'bearer ' + token
        }
    });
    if (response.ok == true) {
        let Bicycles = (await response.json());
        console.log(Bicycles);
        let table = document.querySelector('tbody');
        Bicycles.forEach(element => table.append(createRow(element)));
    }
}

function createRow(bicycle) {
    let row = document.createElement('tr');


    let tittle = document.createElement('td');
    tittle.append(bicycle.tittle);
    let model = document.createElement('td');
    model.append(bicycle.model)
    let price = document.createElement('td');
    price.append(bicycle.price)
    let color = document.createElement('td');
    color.append(bicycle.color)

    let edit = document.createElement('a');
    edit.href = 'edit.html' + '?id=' + bicycle.id;
    edit.innerText = 'Edit';
    edit.classList.add('btn', 'btn-primary', 'bg-primary');

    let del = document.createElement('a');
    del.classList.add('btn', 'btn-danger', 'bg-danger', 'remove-btn');
    del.setAttribute('value', bicycle.id);
    del.innerText = 'Delete';

    del.addEventListener('click', function(e) {
        let value = e.target.getAttribute('value');
        var row = document.getElementById( value);
        document.querySelector('tbody').removeChild(row);
        fetch(apikey + value, { method: 'DELETE' });
    });
    row.append(tittle);
    row.append(model);
    row.append(price);
    row.append(color);
    let edittd = document.createElement('td');
    edittd.appendChild(edit);
    row.append(edittd);
    let deltd = document.createElement('td');
    deltd.appendChild(del);
    row.append(deltd);
    row.id = bicycle.id;
    return row;
}