// designer-create.js
function initDesignerCreate() {
    const form = document.getElementById('createDesignerForm');
    if (!form || form.dataset.bound) return;

    form.addEventListener('submit', async (e) => {
        e.preventDefault();
        e.stopPropagation();

        const formData = new FormData(form);
        const tokenInput = document.querySelector('input[name="__RequestVerificationToken"]');
        if (!tokenInput) {
            alert("Security token missing.");
            return;
        }
        const token = tokenInput.value;

        try {
            const res = await fetch(form.action, {
                method: 'POST',
                headers: {
                    'X-Requested-With': 'XMLHttpRequest',
                    'RequestVerificationToken': token
                },
                body: formData
            });

            const contentType = res.headers.get('content-type');
            if (contentType && contentType.includes('application/json')) {
                const data = await res.json();
                if (data?.isSuccess) {
                    if (data?.isSuccess) {
                        // ✅ الحل المثالي:
                        window.history.back(); // يعود إلى Index كما كانت
                    } else {
                        alert(data.message || 'Error');
                    }
                }
            } else {

                const html = await res.text();
                document.getElementById('mainContent').innerHTML = html;
                initDesignerCreate();
            }
        } catch (err) {
            console.error('Create failed:', err);
            alert('An error occurred.');
        }
    });

    form.dataset.bound = 'true';
}

// التهيئة
if (document.readyState === 'loading') {
    document.addEventListener('DOMContentLoaded', initDesignerCreate);
} else {
    initDesignerCreate();
}
document.addEventListener('mainContentUpdated', initDesignerCreate);