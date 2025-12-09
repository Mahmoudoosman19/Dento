window.showGlobalAlert = function (message, type = "info", timeout = 3000) {
    const container = document.getElementById("mainContentAlertContainer");
    if (!container) return;

    const alertDiv = document.createElement("div");
    alertDiv.className = `alert alert-${type} alert-dismissible fade show`;
    alertDiv.role = "alert";
    alertDiv.innerHTML = `
        ${message}
        <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
    `;

    container.innerHTML = "";
    container.style.display = "block";
    container.appendChild(alertDiv);

    if (timeout > 0) {
        setTimeout(() => {
            alertDiv.classList.remove("show");
            setTimeout(() => {
                container.innerHTML = "";
                container.style.display = "none";
            }, 150); 
        }, timeout);
    }
};
