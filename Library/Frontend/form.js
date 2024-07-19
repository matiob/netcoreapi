/* $(document).ready(function() {
    $("#form-register").validate({
        rules: {
            inputName: {
                required: true,
                maxlength: 10,
                minlength: 3
            },
            inputEmail: { // MAIL
                required: true,
                email: true
            },
            inputPassword: {
                required: true,
                minlength: 8
            },
            inputPasswordCheck: { // CHECK PASSWORD
                required: true,
                minlength: 8,
                equalTo: "#inputPassword"
            },
            inputPhone: { // DIGITS
                digits: true
            },
            inputURL: { // URL FORMAT
                url: true
            }
        },
        messages: {
            inputName: {
                required: "El nombre es requerido",
                minlength: "Minimo 3 caracteres",
                maxlength: jQuery.validator.format("Máximo {0} caracteres")
            }
        },
        submitHandler: function(formulario) {
            console.log("Recibe el formulario como parametro", formulario);
        },
        errorClass: "is-invalid",
        validClass: "is-valid",
        debug: true
    })
    $("#save").click(function(params) {
        if ($("#form-register").valid()) {
            alert("OK")
        } else {
            alert("Error")
        }
    })

    // Validación custom
    $.validator.addMethod("nombre", function(valor, elemento, valorRecibido) {
        // logica del metodo
        // pongo el nombre de mi metodo como prop en las validaciones
        return true // retorna booleano
    }, "Mensaje de error")
}) */

$(document).ready(function () {
  const urlParams = new URLSearchParams(window.location.search);
  const id = urlParams.get("id");
  const detailMode = urlParams.has("detail");
  fillSelects("Generos", "generoId", "género", "id", "nombre");
  fillSelects("Autores", "autorId", "autor", "id", "nombre");
  if (id) {
    getDataById("Libros", id).then((_) => {
      const item = getFromLocalStorage("item");
      cargarContenido(item);
    });
    detalle(detailMode);
  }
  $("#form-crud").validate({
    rules: {
      titulo: {
        required: true,
        minlength: 3,
        maxlength: 50,
      },
      isbn: {
        required: true,
        minlength: 10,
        maxlength: 13,
      },
      fechaPublicacion: {
        required: true,
      },
      genero: {
        required: true,
      },
      autor: {
        required: true,
      },
    },
    messages: {
      titulo: {
        required: "El título es requerido.",
        minlength: "El título debe tener al menos {0} caracteres.",
        maxlength: "El título no debe exceder los {0} caracteres.",
      },
      isbn: {
        required: "El ISBN es requerido.",
        minlength: "El ISBN debe tener al menos {0} caracteres.",
        maxlength: "El ISBN no debe exceder los {0} caracteres.",
      },
      fechaPublicacion: {
        required: "La fecha de publicación es requerida.",
      },
      genero: {
        required: "Seleccione un género.",
      },
      autor: {
        required: "Seleccione un autor.",
      },
    },
    errorClass: "is-invalid",
    validClass: "is-valid",
    debug: true,
  });
  $("#save").click(function (event) {
    event.preventDefault();
    if ($("#form-crud").valid()) {
      const form = $("#form-crud")[0];
      const url = "Libros";
      const res = id
        ? fetchData(url, form, id, "PUT")
        : fetchData(url, form, undefined, "POST");
      res.then((data) => {
        if (data.status === 200) {
          alert("Registro guardado");
          removeFromLocalStorage("item");
          navigateTo("list.html");
        } else {
          alert("Error en el servidor, intente nuevamente mas tarde");
        }
      });
    } else {
      alert("Hay errores en el formulario");
    }
  });
});

function cargarContenido(item) {
  $("#id").val(item.id);
  $("#titulo").val(item.titulo);
  $("#isbn").val(item.isbn);
  $("#fechaPublicacion").val(item.fechaPublicacion);
  $("#generoId").val(item.generoId);
  $("#autorId").val(item.autorId);
}

function detalle(isDetail) {
  if (isDetail) {
    $("#titulo, #isbn, #fechaPublicacion, #genero, #autor").prop(
      "disabled",
      true
    );
    $("#save").hide();
  }
}
