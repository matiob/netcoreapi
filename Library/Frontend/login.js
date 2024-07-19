$(document).ready(function () {
  isSession();

  $("#form-register").validate({
    rules: {
      username: {
        required: true,
        // maxlength: 10,
        minlength: 3,
      },
      password: {
        required: true,
        minlength: 8,
        pattern: /^(?=.*[A-Z])(?=.*[!@#$%^&*]).*$/
      },
    },
    messages: {
      username: {
        required: "El nombre es requerido",
        minlength: "Mínimo 3 caracteres",
        maxlength: jQuery.validator.format("Máximo {0} caracteres"),
      },
      password: {
        required: "La contraseña es requerida",
        minlength: "Mínimo 8 caracteres",
        pattern:
          "Debe contener al menos una letra mayúscula y un carácter especial",
      },
    },
    errorClass: "is-invalid",
    validClass: "is-valid",
    debug: true,
  });

  $("#save").click(function (event) {
    event.preventDefault();
    if ($("#form-register").valid()) {
      fetchData("Login", $("#form-register")[0]);
    } else {
      alert("Error al iniciar sesion");
    }
  });

  $("#logout").click(function (event) {
    event.preventDefault();
    removeFromLocalStorage("token");
    alert("Sesión cerrada");
    isSession();
    navigateTo("index.html");
  });
});

function isSession() {
  if (isLoggedIn()) {
    $("#login-container").hide();
    $("#cards-container").show();
  } else {
    $("#login-container").show();
    $("#cards-container").hide();
  }
}