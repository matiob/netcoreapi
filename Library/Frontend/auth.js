function navigateTo(url) {
    isLoggedIn()
        ? window.location.href = url
        : alert('Debes estar logueado para acceder a esta página.');
}

function isLoggedIn() {
    const token = localStorage.getItem('token');
    return token !== null;
}
