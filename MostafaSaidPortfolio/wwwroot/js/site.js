// Mobile menu toggle
(function () {
    const toggle = document.getElementById('menu-toggle');
    const menu = document.getElementById('mobile-menu');
    const menuIcon = document.getElementById('menu-icon');
    const closeIcon = document.getElementById('close-icon');

    if (toggle && menu) {
        toggle.addEventListener('click', function () {
            const isOpen = !menu.classList.contains('hidden');
            menu.classList.toggle('hidden', isOpen);
            menuIcon.classList.toggle('hidden', !isOpen);
            closeIcon.classList.toggle('hidden', isOpen);
        });
    }

    // Close menu on outside click
    document.addEventListener('click', function (e) {
        if (menu && !menu.classList.contains('hidden')) {
            if (!toggle.contains(e.target) && !menu.contains(e.target)) {
                menu.classList.add('hidden');
                menuIcon.classList.remove('hidden');
                closeIcon.classList.add('hidden');
            }
        }
    });
})();

// Active nav link highlighting
(function () {
    const path = window.location.pathname.toLowerCase();
    document.querySelectorAll('.nav-link, .mobile-nav-link').forEach(function (link) {
        const href = (link.getAttribute('href') || '').toLowerCase();
        if (
            (href === '/' && path === '/') ||
            (href !== '/' && path.startsWith(href))
        ) {
            link.classList.add('active');
        }
    });
})();

// Newsletter form feedback
(function () {
    const forms = document.querySelectorAll('form[data-newsletter]');
    forms.forEach(function (form) {
        form.addEventListener('submit', function () {
            const btn = form.querySelector('button[type="submit"]');
            if (btn) {
                btn.textContent = 'Subscribing…';
                btn.disabled = true;
            }
        });
    });
})();

// Fade-in elements on scroll
(function () {
    if ('IntersectionObserver' in window) {
        const observer = new IntersectionObserver(function (entries) {
            entries.forEach(function (entry) {
                if (entry.isIntersecting) {
                    entry.target.classList.add('fade-in');
                    observer.unobserve(entry.target);
                }
            });
        }, { threshold: 0.1 });

        document.querySelectorAll('.observe-fade').forEach(function (el) {
            observer.observe(el);
        });
    }
})();
