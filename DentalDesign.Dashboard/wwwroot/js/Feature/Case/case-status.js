    let currentCaseId = null;

    document.addEventListener("click", function (e) {
        // فتح المودال
        if (e.target.closest(".change-status-btn")) {
        e.preventDefault();
    const btn = e.target.closest(".change-status-btn");
    currentCaseId = btn.dataset.id;
    const currentStatus = btn.dataset.current;
    const statusSelect = document.getElementById("statusSelect");
    if (statusSelect) statusSelect.value = currentStatus;

    const modalEl = document.getElementById("statusModal");
    if (modalEl) {
                const modal = bootstrap.Modal.getInstance(modalEl) || new bootstrap.Modal(modalEl);
    modal.show();
            }
        }

    // حفظ الحالة
    if (e.target.id === "saveStatus") {
        e.preventDefault();
    const statusSelect = document.getElementById("statusSelect");
    const newStatus = statusSelect ? parseInt(statusSelect.value, 10) : null;

    if (newStatus === null || !currentCaseId) return;

    fetch("/Case/UpdateStatus", {
        method: "POST",
    headers: {
        "Content-Type": "application/x-www-form-urlencoded",
    "X-Requested-With": "XMLHttpRequest",
    "RequestVerificationToken": document.querySelector('input[name="__RequestVerificationToken"]')?.value
                },
    body: `caseId=${encodeURIComponent(currentCaseId)}&statusId=${encodeURIComponent(newStatus)}`
            })
            .then(async (res) => {
                const data = await res.json();

    // ✅ تحقق من النجاح
    if (res.ok && data?.isSuccess) {
                    // تحديث الحالة في الجدول
                    const statusCell = document.querySelector(`tr[data-id='${currentCaseId}'] .status-cell`);
    if (statusCell) statusCell.textContent = data.statusName;

    // ✅ إغلاق المودال
    const modalEl = document.getElementById("statusModal");
    const modal = bootstrap.Modal.getInstance(modalEl);
    if (modal) modal.hide();

    // ✅ عرض رسالة نجاح علوية (بدون SweetAlert)
    showTopAlert("Status updated successfully", "success");
                } else {
                    throw new Error(data?.message || "Update failed");
                }
            })
            .catch((err) => {
        console.error("Update failed:", err);
    showTopAlert("Failed to update status", "danger");
            });
        }
    });

    // ✅ دالة لإظهار رسالة علوية
    function showTopAlert(message, type = "success") {
        // إزالة الرسالة القديمة إن وجدت
        const oldAlert = document.getElementById("top-alert");
    if (oldAlert) oldAlert.remove();

    // إنشاء رسالة جديدة
    const alertDiv = document.createElement("div");
    alertDiv.id = "top-alert";
    alertDiv.className = `alert alert-${type} alert-dismissible fade show`;
    alertDiv.style.cssText = `
    position: fixed;
    top: 60px;
    right: 20px;
    z-index: 9999;
    max-width: 350px;
    `;
    alertDiv.innerHTML = `
    ${message}
    <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
    `;

    // إضافته إلى الـ body
    document.body.appendChild(alertDiv);

        // إزالته تلقائيًا بعد 3 ثواني
        setTimeout(() => {
            const alert = document.getElementById("top-alert");
    if (alert) alert.remove();
        }, 3000);
    }
