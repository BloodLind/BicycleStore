const apikey = "https://localhost:44373/api/Bicycles/";
var query = window.location.search.substring(1);
var querryParametrs = parseQueryString(query);

console.log(querryParametrs.hasOwnProperty('id'));

if (querryParametrs.id != 'undefined' && querryParametrs.hasOwnProperty('id') != false) {
    getApiElement();
} else {
    document.querySelector("#sbm").addEventListener('click', function() {
        var form = document['bicycle'];
        var bicycle = {
            'tittle': form.elements['tittle'].value,
            'model': form.elements['model'].value,
            'info': form.elements['info'].value,
            'color': form.elements['color'].value,
            'price': form.elements['price'].value
        };
        apiCreateElement(bicycle.tittle, bicycle.model, bicycle.info, bicycle.color, bicycle.id, bicycle.price);
    });
}

async function getApiElement() {
    var request = await fetch(apikey + querryParametrs.id);
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
        document.querySelector("#sbm").addEventListener('click', function() {
            var bicycle = {
                'tittle': form.elements['tittle'].value,
                'model': form.elements['model'].value,
                'info': form.elements['info'].value,
                'color': form.elements['color'].value,
                'id': form.elements['bicycle-id'].value,
                'price': form.elements['price'].value
            };
            apiCreateElement(bicycle.tittle, bicycle.model, bicycle.info, bicycle.color, bicycle.id, bicycle.price);
        });
    }
}

async function apiCreateElement(tittle, model, info, color, id, price) {
   
    var request = await fetch(apikey, {
        method: 'POST',
        headers: { 'Accept': 'application/json', 'Content-Type': 'application/json' },
        body: JSON.stringify({
            tittle,
            model,
            info,
            color,
            id,
            'price': parseInt(price)
        })
    });
    if (request.ok == true)
    {
        
        window.location.href = "index.html";
    }
        else
        {
            

            let errorData  = await request.json();
            console.log(errorData);
            var form = document['bicycle'];

           let errors = document.getElementsByClassName("error");
           if(errors.length > 0 )
           errors.forEach(element => {
                element.parentElement.removeChild(element);
            });
            if(errorData.errors)
            {
              for(let key in errorData.errors )
              {

                if(errorData.errors[key])
                {
                    const p = document.createElement("p");
                    p.style.display = "block";
                    p.classList.add("error");
                    p.append(errorData.errors[key]);
                    console.log(errorData.errors[key]);
                    form.getElementsByName(key.toLowerCase())[0].parentElement.parentElement.appendChild(p);
                  
                }
              }
              
            }
            else
            {

              
                for(let key in errorData )
              {

                if(errorData[key])
                {
                    const p = document.createElement("p");
                    p.classList.add("error");
                    p.append(errorData[key]);
                    console.log(errorData[key]);
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