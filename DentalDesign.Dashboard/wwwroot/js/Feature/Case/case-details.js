    // ✅ تحميل تفاصيل الحالة
    function loadCaseDetails(event, caseId) {
        event.preventDefault();

    fetch(`/Case/Details?id=${encodeURIComponent(caseId)}`, {
        headers: {"X-Requested-With": "XMLHttpRequest" }
        })
        .then(res => {
            if (!res.ok) throw new Error("Failed to load case details");
    return res.text();
        })
        .then(html => {
            // استبدال محتوى #mainContent
            const mainContent = document.getElementById("mainContent");
    if (mainContent) mainContent.innerHTML = html;

            // تحديث active state في sidebar (اختياري)
            document.querySelectorAll(".sidebar-wrapper li").forEach(li => li.classList.remove("active"));
            // لا حاجة لتحديث active لأنك في نفس الصفحة
        })
        .catch(err => {
        console.error("Load case error:", err);
    alert("Failed to load case details.");
        });
    }
