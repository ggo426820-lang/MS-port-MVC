/* =========================================================
   Theme Manager — dark / light with localStorage persistence
   ========================================================= */
var ThemeManager = (function () {
    function getStored() {
        try { return localStorage.getItem('ms-theme') || 'light'; } catch (_) { return 'light'; }
    }
    function store(t) {
        try { localStorage.setItem('ms-theme', t); } catch (_) { }
    }

    function apply(theme) {
        var dark = theme === 'dark';
        document.documentElement.classList.toggle('dark', dark);
        document.documentElement.setAttribute('data-theme', theme);
        store(theme);

        var sun  = document.getElementById('icon-sun');
        var moon = document.getElementById('icon-moon');
        if (sun)  sun.classList.toggle('hidden', !dark);
        if (moon) moon.classList.toggle('hidden', dark);
    }

    function toggle() {
        apply(document.documentElement.classList.contains('dark') ? 'light' : 'dark');
    }

    function init() {
        apply(getStored());
        var btn = document.getElementById('theme-toggle');
        if (btn) btn.addEventListener('click', toggle);
    }

    return { init: init, apply: apply };
}());

/* =========================================================
   Mobile Menu
   ========================================================= */
(function () {
    var toggle  = document.getElementById('menu-toggle');
    var menu    = document.getElementById('mobile-menu');
    var iconBar = document.getElementById('menu-icon');
    var iconX   = document.getElementById('close-icon');

    function close() {
        if (!menu) return;
        menu.classList.add('hidden');
        if (iconBar) iconBar.classList.remove('hidden');
        if (iconX)   iconX.classList.add('hidden');
    }

    if (toggle && menu) {
        toggle.addEventListener('click', function () {
            var isOpen = !menu.classList.contains('hidden');
            if (isOpen) { close(); return; }
            menu.classList.remove('hidden');
            if (iconBar) iconBar.classList.add('hidden');
            if (iconX)   iconX.classList.remove('hidden');
        });

        document.addEventListener('click', function (e) {
            if (!menu.classList.contains('hidden') &&
                !toggle.contains(e.target) &&
                !menu.contains(e.target)) {
                close();
            }
        });
    }
}());

/* =========================================================
   Active Nav Link Highlighting
   ========================================================= */
(function () {
    var path = window.location.pathname.toLowerCase();
    document.querySelectorAll('.nav-link, .mobile-nav-link').forEach(function (link) {
        var href = (link.getAttribute('href') || '').toLowerCase().split('?')[0];
        if ((href === '/' && path === '/') || (href !== '/' && href.length > 1 && path.startsWith(href))) {
            link.classList.add('active');
        }
    });
}());

/* =========================================================
   Active Language Button Highlight
   ========================================================= */
(function () {
    var langParam = (new URLSearchParams(window.location.search)).get('culture') || '';
    var htmlLang  = document.documentElement.getAttribute('lang') || 'en';
    var currentLang = htmlLang.startsWith('ar') ? 'ar' : 'en';
    document.querySelectorAll('.lang-btn').forEach(function (btn) {
        var btnLang = btn.getAttribute('data-lang') || '';
        btn.classList.toggle('active', btnLang === currentLang);
    });
}());

/* =========================================================
   Sticky Header Shadow on Scroll
   ========================================================= */
(function () {
    var header = document.getElementById('site-header');
    if (!header) return;
    window.addEventListener('scroll', function () {
        header.classList.toggle('shadow-md', window.scrollY > 12);
    }, { passive: true });
}());

/* =========================================================
   Scroll Reveal (IntersectionObserver)
   ========================================================= */
(function () {
    if (!('IntersectionObserver' in window)) return;
    var io = new IntersectionObserver(function (entries) {
        entries.forEach(function (entry) {
            if (entry.isIntersecting) {
                entry.target.classList.add('fade-in');
                io.unobserve(entry.target);
            }
        });
    }, { threshold: 0.1, rootMargin: '0px 0px -40px 0px' });
    document.querySelectorAll('.observe-fade').forEach(function (el) { io.observe(el); });
}());

/* =========================================================
   Newsletter Form Loading State
   ========================================================= */
(function () {
    document.querySelectorAll('form[data-newsletter]').forEach(function (form) {
        form.addEventListener('submit', function () {
            var btn = form.querySelector('button[type="submit"]');
            if (btn) { btn.textContent = '...'; btn.disabled = true; }
        });
    });
}());

/* =========================================================
   Bootstrap — run theme init first to avoid FOUC
   ========================================================= */
ThemeManager.init();
