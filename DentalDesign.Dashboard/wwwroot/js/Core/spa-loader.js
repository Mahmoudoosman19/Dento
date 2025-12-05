// spa-loader.js
window.SpaLoader = (function () {
    let mainContent;

    function init() {

        mainContent = document.getElementById("mainContent");
        if (!mainContent) return;

        // Back/forward
        window.addEventListener("popstate", () => {
            loadPage(location.href, false);
        });
    }

    function loadPage(url, pushState = true) {
        const searchInput = document.getElementById("searchInput");
        const searchValue = searchInput ? searchInput.value : '';
        const wasFocused = searchInput === document.activeElement;

        fetch(url, { headers: { "X-Requested-With": "XMLHttpRequest" } })
            .then(res => {
                if (!res.ok) throw new Error('Network error');
                return res.text();
            })
            .then(html => {
                mainContent.innerHTML = html;

                // Restore search input
                const newSearch = document.getElementById("searchInput");
                if (newSearch) {
                    newSearch.value = searchValue;
                    if (wasFocused) {
                        newSearch.focus();
                        newSearch.setSelectionRange(searchValue.length, searchValue.length);
                    }
                }

                // Update sidebar
                const pathname = new URL(url, origin).pathname;
                document.querySelectorAll(".sidebar-wrapper li").forEach(li => li.classList.remove("active"));
                const active = document.querySelector(`.sidebar-wrapper a[href^='${pathname}']`);
                if (active) active.parentElement.classList.add("active");

                if (pushState) history.pushState({ url }, "", url);

                // Re-bind events
                if (window.EventBinder) window.EventBinder.bindAll();
                document.dispatchEvent(new Event('mainContentUpdated'));
            })
            .catch(err => {
                console.error("Load failed:", err);
                alert("Page load failed.");
            });
    }

    return {
        init,
        loadPage
    };
})();