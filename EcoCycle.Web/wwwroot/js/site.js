(function () {
    const html = document.documentElement;
    const toggleBtn = document.getElementById('themeToggle');
    const themeIcon = document.getElementById('themeIcon');

    function setTheme(theme) {
        html.setAttribute('data-bs-theme', theme);
        localStorage.setItem('ecocycle-theme', theme);
        if (themeIcon) {
            themeIcon.className = theme === 'dark' ? 'bi bi-moon-fill' : 'bi bi-sun-fill';
        }
    }

    const savedTheme = localStorage.getItem('ecocycle-theme') || 'dark';
    setTheme(savedTheme);

    if (toggleBtn) {
        toggleBtn.addEventListener('click', function () {
            const current = html.getAttribute('data-bs-theme');
            setTheme(current === 'dark' ? 'light' : 'dark');
        });
    }

    document.querySelectorAll('.alert-dismissible .btn-close').forEach(function (btn) {
        btn.addEventListener('click', function () {
            this.closest('.alert').classList.remove('show');
        });
    });
})();
