$(document).ready(function () {
  loadData('Libros');
});

function loadData(url) {
  fetch(`https://localhost:7209/api/${url}`)
    .then((response) => response.json())
    .then((data) => {
      if (data.status === 200) {
        fillData(data.data);
      } else if (data.status === 404) {
        emptyData();
      } else {
        alert("Error en el servidor");
      }
    })
    .catch((error) => {
      console.error("Error al cargar:", error);
    });
}

function fillData(data) {
  $("#tabla tbody").empty();
  data.forEach((libro) => {
    let row = `
            <tr data-id="${libro.id}">
              <td>${libro.id}</td>
              <td>${libro.titulo}</td>
              <td>${libro.genero}</td>
              <td>${libro.autor}</td>
              <td>${libro.fechaPublicacion}</td>
              <td>${libro.isbn}</td>
              <td>
                <button class="btn btn-sm btn-info me-1 btn-ver" data-id="${libro.id}">Ver</button>
                <button class="btn btn-sm btn-warning me-1 btn-editar" data-id="${libro.id}">Editar</button>
                <button class="btn btn-sm btn-danger btn-eliminar" data-id="${libro.id}">Eliminar</button>
              </td>
            </tr>
          `;
    $("#tabla tbody").append(row);
  });
  addEvents();
  $(".btn-crear").click(function () {
    navigateTo(`form.html`);
  });
}

function emptyData() {
  let row = `
            <tr id="mensajeError">
                <td colspan="7" class="text-center text-danger">No hay datos para mostrar.</td>
            </tr>`;
  $("#tabla tbody").append(row);
}

function addEvents() {
    $("#tabla tbody").off("click", ".btn-ver, .btn-editar, .btn-eliminar");
  $(".btn-ver").click(function () {
    const dataId = $(this).data("id");
    navigateTo(`form.html?detail=true&id=${dataId}`);
  });
  $(".btn-editar").click(function () {
    const dataId = $(this).data("id");
    navigateTo(`form.html?edit=true&id=${dataId}`);
  });
  $(".btn-eliminar").click(function () {
    const dataId = $(this).data("id");
    confirm("¿Estás seguro de eliminar?") && deleteData('Libros', dataId);
  });
}
