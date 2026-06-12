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
   Scroll Reveal — IntersectionObserver with stagger support
   ========================================================= */
(function () {
    if (!('IntersectionObserver' in window)) {
        // Fallback: make all elements visible immediately
        document.querySelectorAll('.observe-fade, .observe-left, .observe-right, .observe-scale, .section-head').forEach(function (el) {
            el.style.opacity = '1';
            el.style.transform = 'none';
        });
        return;
    }

    var ioOptions = { threshold: 0.08, rootMargin: '0px 0px -50px 0px' };

    // Auto-assign stagger classes to siblings within a grid
    document.querySelectorAll('[data-stagger]').forEach(function (parent) {
        var children = parent.querySelectorAll('.observe-fade');
        children.forEach(function (child, i) {
            child.classList.add('stagger-' + Math.min(i + 1, 6));
        });
    });

    var io = new IntersectionObserver(function (entries) {
        entries.forEach(function (entry) {
            if (entry.isIntersecting) {
                entry.target.classList.add('visible');
                io.unobserve(entry.target);
            }
        });
    }, ioOptions);

    document.querySelectorAll('.observe-fade, .observe-left, .observe-right, .observe-scale, .section-head').forEach(function (el) {
        io.observe(el);
    });
}());

/* =========================================================
   Counter Animation — stat numbers count up on enter
   ========================================================= */
(function () {
    if (!('IntersectionObserver' in window)) return;

    function animateCount(el) {
        var target = parseFloat(el.getAttribute('data-count') || el.textContent) || 0;
        var suffix = el.getAttribute('data-suffix') || '';
        var duration = 1200;
        var start = performance.now();
        var isInt = Number.isInteger(target);

        function step(now) {
            var progress = Math.min((now - start) / duration, 1);
            var eased = 1 - Math.pow(1 - progress, 3);
            var value = target * eased;
            el.textContent = (isInt ? Math.round(value) : value.toFixed(1)) + suffix;
            if (progress < 1) requestAnimationFrame(step);
        }
        requestAnimationFrame(step);
    }

    var countIO = new IntersectionObserver(function (entries) {
        entries.forEach(function (entry) {
            if (entry.isIntersecting) {
                animateCount(entry.target);
                countIO.unobserve(entry.target);
            }
        });
    }, { threshold: 0.5 });

    document.querySelectorAll('[data-count]').forEach(function (el) {
        countIO.observe(el);
    });
}());

/* =========================================================
   Skill Pills — staggered entrance animation
   ========================================================= */
(function () {
    if (!('IntersectionObserver' in window)) return;
    var pillsSection = document.querySelector('.skills-pills-container');
    if (!pillsSection) return;

    var pillIO = new IntersectionObserver(function (entries) {
        entries.forEach(function (entry) {
            if (entry.isIntersecting) {
                var pills = entry.target.querySelectorAll('.skill-pill');
                pills.forEach(function (pill, i) {
                    pill.style.opacity = '0';
                    pill.style.transform = 'translateY(12px)';
                    setTimeout(function () {
                        pill.style.transition = 'opacity 0.4s ease, transform 0.4s ease';
                        pill.style.opacity = '1';
                        pill.style.transform = 'translateY(0)';
                    }, i * 45);
                });
                pillIO.unobserve(entry.target);
            }
        });
    }, { threshold: 0.2 });

    pillIO.observe(pillsSection);
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
   Language Switch — smooth fade transition before navigation
   ========================================================= */
(function () {
    document.querySelectorAll('.lang-btn').forEach(function (btn) {
        btn.addEventListener('click', function (e) {
            var href = btn.getAttribute('href');
            if (!href) return;
            // Don't animate if already on that language
            if (btn.classList.contains('active')) return;
            e.preventDefault();
            document.documentElement.classList.add('lang-switching');
            setTimeout(function () {
                window.location.href = href;
            }, 230);
        });
    });
}());

/* =========================================================
   Bootstrap — run theme init first to avoid FOUC
   ========================================================= */
ThemeManager.init();
