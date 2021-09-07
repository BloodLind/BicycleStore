const apikey = "https://localhost:44373/api/Bicycles/";
var query = window.location.search.substring(1);
var querryParametrs = parseQueryString(query);
var token = sessionStorage.getItem(tokenKey);


const toBase64 = file => new Promise((resolve, reject) => {
    const reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = () => resolve(reader.result);
    reader.onerror = error => reject(error);
});

loadData();

async function loadData() {

    if (querryParametrs.id != 'undefined' && querryParametrs.hasOwnProperty('id') != false) {
        getApiElement();
    } else {
        var image = document.createElement('img');
        image.style.height = '16em';
        document['bicycle'].querySelector('.image-preview').appendChild(image);
        var file = document['bicycle'].elements['image'].files[0];
        image.src = file == null ? 'drawable/noImage.png' : await toBase64(file);


        document.querySelector("#sbm").addEventListener('click', async function() {
            var form = document['bicycle'];
            var bicycle = {
                'tittle': form.elements['tittle'].value,
                'model': form.elements['model'].value,
                'info': form.elements['info'].value,
                'color': form.elements['color'].value,
                'price': form.elements['price'].value,
                'image': '',
            };
            bicycle.image = await toBase64(form.elements['image'].files[0]);
            apiCreateElement(bicycle.tittle, bicycle.model, bicycle.info, bicycle.color, bicycle.id, bicycle.price, bicycle.image);
        });
    }


    document['bicycle'].elements['image'].addEventListener('change', async function(e) {
        var image = await toBase64(document['bicycle'].elements['image'].files[0]);
        document['bicycle'].querySelector('.image-preview>img').src = image;
    });
}





async function getApiElement() {
    var request = await fetch(apikey + querryParametrs.id, {
        method: 'GET',
        headers: { 'Accept': 'application/json', 'Content-Type': 'application/json', 'Authorization': 'bearer ' + token }
    });
    if (request.ok == true) {
        let bicycle = (await request.json());
        var form = document['bicycle'];
        form.elements['tittle'].value = bicycle.tittle;
        form.elements['model'].value = bicycle.model;
        form.elements['price'].value = bicycle.price;
        form.elements['info'].value = bicycle.info;
        form.elements['color'].value = bicycle.color;
        var id = document.createElement('input');
        id.type = 'hidden';
        id.value = bicycle.id;
        id.name = 'bicycle-id';
        form.appendChild(id);
        var image = document.createElement('img');
        image.style.height = '16em';
        form.querySelector('.image-preview').appendChild(image);
        var file = document['bicycle'].elements['image'].files[0];
        image.src = file == null ? (bicycle.photo == null ? 'drawable/noImage.png' : bicycle.photo.base64Photo) : await toBase64(file);


        document.querySelector("#sbm").addEventListener('click', async function() {
            var bicycle = {
                'tittle': form.elements['tittle'].value,
                'model': form.elements['model'].value,
                'info': form.elements['info'].value,
                'color': form.elements['color'].value,
                'id': form.elements['bicycle-id'].value,
                'price': form.elements['price'].value,
                'image': ''
            };
            bicycle.image = document['bicycle'].querySelector('.image-preview>img').src;
            apiCreateElement(bicycle.tittle, bicycle.model, bicycle.info, bicycle.color, bicycle.id, bicycle.price, bicycle.image);
        });
    }
}
async function apiCreateElement(tittle, model, info, color, id, price, image) {


    var token = sessionStorage.getItem("token");
    var request = await fetch(apikey, {
        method: 'POST',
        headers: { 'Accept': 'application/json', 'Content-Type': 'application/json', 'Authorization': 'bearer ' + token },
        body: JSON.stringify({
            tittle,
            model,
            info,
            color,
            id,
            image,
            'price': parseInt(price)
        })
    });
    if (request.ok == true) {

        window.location.href = "index.html";
    } else {


        let errorData = await request.json();

        var form = document['bicycle'];

        let errors = document.getElementsByClassName("error");
        if (errors.length > 0)
            errors.forEach(element => {
                element.parentElement.removeChild(element);
            });
        if (errorData.errors) {
            for (let key in errorData.errors) {

                if (errorData.errors[key]) {
                    const p = document.createElement("p");
                    p.style.display = "block";
                    p.classList.add("error");
                    p.append(errorData.errors[key]);
                    form.getElementsByName(key.toLowerCase())[0].parentElement.parentElement.appendChild(p);

                }
            }

        } else {


            for (let key in errorData) {

                if (errorData[key]) {
                    const p = document.createElement("p");
                    p.classList.add("error");
                    p.append(errorData[key]);
                    document.getElementsByName(key.toLowerCase())[0].parentElement.parentElement.appendChild(p);
                }
            }
        }


    }
}

function parseQueryString(query) {
    var vars = query.split("&");
    var query_string = {};
    for (var i = 0; i < vars.length; i++) {
        var pair = vars[i].split("=");
        var key = decodeURIComponent(pair[0]);
        var value = decodeURIComponent(pair[1]);
        // If first entry with this name
        if (typeof query_string[key] === "undefined") {
            query_string[key] = decodeURIComponent(value);
            // If second entry with this name
        } else if (typeof query_string[key] === "string") {
            var arr = [query_string[key], decodeURIComponent(value)];
            query_string[key] = arr;
            // If third or later entry with this name
        } else {
            query_string[key].push(decodeURIComponent(value));
        }
    }
    return query_string;
}