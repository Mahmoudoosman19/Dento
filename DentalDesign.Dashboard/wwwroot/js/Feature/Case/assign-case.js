let currentCaseIdForAssign = null;

const token = document.querySelector('input[name="__RequestVerificationToken"]')?.value;
if (!token) console.error("CSRF token missing!");


// تحميل قائمة المصممين ديناميكيًا
async function loadDesigners() {
    const select = document.getElementById("designerSelect");
    if (!select) return;

    // تحميل مرة واحدة فقط
    if (select.options.length > 1) return;

    try {
        const res = await fetch("/Case/GetDesignersList");
        if (!res.ok) throw new Error("Failed to fetch designers");

        const designers = await res.json();
        designers.forEach(d => {
            const opt = document.createElement("option");
            opt.value = d.id;
            opt.textContent = d.fullNameEn;
            select.appendChild(opt);
        });
    } catch (err) {
        console.error("Designer load error:", err);
        select.innerHTML = '<option value="">Error loading designers</option>';
    }
}

// حدث click لجميع الأزرار
document.addEventListener("click", async function (e) {
    const target = e.target;

    // فتح المودال
    const assignBtn = target.closest(".assign-designer-btn");
    if (assignBtn) {
        e.preventDefault();
        currentCaseIdForAssign = assignBtn.dataset.id;
        await loadDesigners();

        const modalEl = document.getElementById("assignDesignerModal");
        const modal = bootstrap.Modal.getInstance(modalEl) || new bootstrap.Modal(modalEl);
        modal.show();
    }

    // حفظ التعيين
    if (target.id === "assignSaveBtn") {
        e.preventDefault();
        const designerSelect = document.getElementById("designerSelect");
        const designerId = designerSelect?.value;

        if (!designerId || !currentCaseIdForAssign) {
            alert("Please select a designer.");
            return;
        }

        const token = document.querySelector('input[name="__RequestVerificationToken"]')?.value;
        if (!token) {
            console.error("CSRF token missing!");
            return;
        }

        try {
            const res = await fetch("/Case/AssignToDesigner", {
                method: "POST",
                headers: {
                    "Content-Type": "application/x-www-form-urlencoded",
                    "X-Requested-With": "XMLHttpRequest",
                    "RequestVerificationToken": token
                },
                body: `caseId=${encodeURIComponent(currentCaseIdForAssign)}&designerId=${encodeURIComponent(designerId)}`
            });

            const data = await res.json();
            if (!res.ok || !data?.isSuccess) {
                throw new Error(data?.message || "Assignment failed");
            }

            // ✅ إغلاق المودال
            const modalEl = document.getElementById("assignDesignerModal");
            const modal = bootstrap.Modal.getInstance(modalEl);
            modal?.hide();

            // ✅ عرض رسالة نجاح
            showTopAlert("Case assigned successfully", "success");

            // ✅ تحديث الواجهة فورًا (بدون Reload)
            const row = document.querySelector(`tr[data-id='${currentCaseIdForAssign}']`);
            if (row && designerSelect) {
                const designerName = designerSelect.selectedOptions[0]?.textContent || "Unknown";
                const designerCell = row.querySelector(".designer-cell");
                if (designerCell) {
                    designerCell.textContent = designerName;
                    designerCell.dataset.designerId = designerId;
                }
            }

        } catch (err) {
            console.error("Assign error:", err);
            showTopAlert("Failed to assign case", "danger");
        }
    }
});
