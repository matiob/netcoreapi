function fetchData(url, form, id = "", method = 'POST') {
  const formData = new FormData(form);
  const data = {};
  formData.forEach((value, key) => {
    data[key] = value;
  });
  if (id) {
    data.id = id;
    url = `${url}/${id}`;
    method = "PUT";
  }
  return fetch(`https://localhost:7209/api/${url}`, {
    method: method,
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(data),
  })
    .then((response) => response.json())
    .then((data) => {
      if (window.location.pathname === "/") {
        if (data.status === 200) {
          setFromLocalStorage("token", data.data);
          navigateTo("index.html");
        } else {
          alert("Usuario o contraseña incorrectos");
        }
      } else {
        return data;
      }
    })
    .catch((error) => {
      console.error("Error:", error);
    });
}

function fillSelects(url, selectId, contentField, valueField, textField) {
  fetch(`https://localhost:7209/api/${url}`)
    .then((response) => response.json())
    .then((data) => {
      if (data.status === 200) {
        const select = $(`#${selectId}`);
        select.empty();
        select.append(
          $("<option>", {
            value: "",
            text: `Seleccione un ${contentField}`,
          })
        );
        data.data.forEach((element) => {
          select.append(
            $("<option>", {
              value: element[valueField],
              text: element[textField],
            })
          );
        });
      } else {
        const select = $(`#${selectId}`);
        select.empty();
        select.append(
          $("<option>", {
            value: "",
            text: `Seleccione un ${contentField}`,
          })
        );
      }
    })
    .catch((error) => console.error("Error al llenar el select:", error));
}

function deleteData(url, id) {
  fetch(`https://localhost:7209/api/${url}/${id}`, {
    method: "DELETE",
    headers: {
      "Content-Type": "application/json",
    },
  })
    .then((response) => response.json())
    .then((data) => {
      if (data.status === 200) {
        alert("Registro eliminado");
        window.location.reload();
      } else {
        alert("Error al eliminar");
      }
    })
    .catch((error) => {
      console.error("Error:", error);
    });
}

function getDataById(url, id) {
  return fetch(`https://localhost:7209/api/${url}/${id}`)
    .then((response) => response.json())
    .then((res) => {
      if (res.status === 200) {
        setFromLocalStorage("item", res.data);
      } else {
        alert("Error al cargar el contenido");
      }
    })
    .catch((error) => {
      console.error("Error:", error);
    });
}


// AJAX
function ajaxData(url, form) {
  $.ajax({
    url: `https://localhost:7209/api/${url}`,
    type: "POST",
    contentType: "application/json",
    dataType: "json",
    data: JSON.stringify(
      $(form)
        .serializeArray()
        .reduce(function (obj, item) {
          obj[item.name] = item.value;
          return obj;
        }, {})
    ),
    /* headers: {
            'Authorization': 'Bearer ' + tuTokenJWT  // JWT
        }, */
    success: function (response) {
      console.log(response);
    },
    error: function (xhr, status, error) {
      console.error("Error en la solicitud AJAX:", error);
    },
  });
}