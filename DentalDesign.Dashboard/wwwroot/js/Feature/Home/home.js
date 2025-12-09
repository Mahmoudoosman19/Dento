function initHomePage() {

    // لو الصفحة مش صفحة الـ Home، متعملش حاجة
    if (!document.getElementById("totalCasesCount")) return;

    loadTotalCases();
    loadStatusCases(1, "newCasesCount");
    loadStatusCases(2, "activeCasesCount");
    loadStatusCases(4, "approvedCasesCount");

    document.querySelectorAll(".status-card").forEach(card => {
        card.addEventListener("click", () => {
            const status = card.getAttribute("data-status");
            // استخدم spa-loader بدل fetch العادي
            SpaLoader.loadPage(`/Home/StatusList?statusId=${status}`);
        });
    });
}

async function loadTotalCases() {
    const response = await fetch('/Home/GetAllCases');
    const data = await response.json();
    document.getElementById("totalCasesCount").innerText = data.totalCount;
}

async function loadStatusCases(statusId, elementId) {
    const response = await fetch(`/Home/GetCasesByStatus?StatusId=${statusId}`);
    const data = await response.json();
    document.getElementById(elementId).innerText = data.totalCount;
}

// ------ IMPORTANT ------

// أول تشغيل عند تحميل الصفحة
document.addEventListener("DOMContentLoaded", initHomePage);

// تشغيل عند تحميل أي صفحة عبر الـ SPA loader
document.addEventListener("mainContentUpdated", initHomePage);
