//window.ProfilePage = (function () {

//    function init() {
//        const form = document.getElementById("profileForm");
//        if (!form) return;

//        form.addEventListener("submit", async (e) => {
//            e.preventDefault();

//            const formData = new FormData(form);

//            // إضافة AntiForgeryToken
//            const token = form.querySelector('input[name="__RequestVerificationToken"]');
//            if (token) formData.append("__RequestVerificationToken", token.value);

//            try {

//                fetch("/Account/Profile", {
//                    headers: { "X-Requested-With": "XMLHttpRequest" }
//                })
//                    .then(res => res.text())
//                    .then(html => {
//                        document.getElementById("mainContent").innerHTML = html;
//                        ProfilePage?.init();
//                    });

//                const res = await fetch("/Account/UpdateProfile", {
//                    method: "POST",
//                    body: formData,
//                    headers: { "X-Requested-With": "XMLHttpRequest" }
//                });

//                if (!res.ok) {
//                    // التعامل مع 400 أو أي خطأ
//                    const text = await res.text(); // ممكن يكون HTML أو رسالة خطأ
//                    console.error("Server returned error:", text);
//                    showGlobalAlert("Update failed: Bad Request", "danger");
//                    return;
//                }

//                const data = await res.json();

//                if (data.isSuccess) {
//                    updateProfileUI(formData);
//                    showGlobalAlert("Profile updated successfully!", "success");
//                } else {
//                    showGlobalAlert("Failed to update profile: " + data.message, "danger");
//                }

//            } catch (err) {
//                console.error(err);
//                showGlobalAlert("An error occurred while updating profile.", "danger");
//            }
//        });
//    }

//    function updateProfileUI(formData) {
//        ["FullNameEn", "PhoneNumber", "BirthDate"].forEach(f => {
//            const input = document.querySelector(`[name="${f}"]`);
//            if (input) input.value = formData.get(f);
//        });
//    }

//    return { init };
//})();
