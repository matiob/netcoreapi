function getFromLocalStorage(key) {
  if (!key) {
    alert("Error: Se requiere una clave.");
  } else {
    const item = localStorage.getItem(key);
    return item ? JSON.parse(item) : null;
  }
}

function setFromLocalStorage(key, value) {
  if (!key || !value) {
    alert("Error: Se requiere una clave y un valor.");
  } else {
    const item = JSON.stringify(value);
    localStorage.setItem(key, item);
  }
}

function removeFromLocalStorage(key) {
  !key
    ? alert("Error: Se requiere una clave.")
    : localStorage.removeItem(key);
}
