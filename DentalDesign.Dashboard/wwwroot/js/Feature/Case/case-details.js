// ================================
// fetch case
// ================================
function loadCaseDetails(event, caseId) {
    event.preventDefault();

    fetch(`/Case/Details?id=${encodeURIComponent(caseId)}`, {
        headers: { "X-Requested-With": "XMLHttpRequest" }
    })
        .then(res => {
            if (!res.ok) throw new Error("Failed to load case details");
            return res.text();
        })
        .then(html => {
            const mainContent = document.getElementById("mainContent");
            if (!mainContent) return;

            mainContent.innerHTML = html;

            mainContent.querySelectorAll("[data-model-url]").forEach(div => {
                console.log("Initializing 3D viewer for:", div.getAttribute("data-model-url"));
                init3DViewer(div);
            });

            document.querySelectorAll(".sidebar-wrapper li").forEach(li => li.classList.remove("active"));
        })
        .catch(err => {
            console.error("Load case error:", err);
            alert("Failed to load case details.");
        });
}

function init3DViewer(viewerDiv) {
    const modelUrl = viewerDiv.getAttribute("data-model-url");
    if (!modelUrl) return;

    const scene = new THREE.Scene();
    const camera = new THREE.PerspectiveCamera(75, 600 / 400, 0.1, 2000);
    camera.position.set(0, 0, 200);

    const renderer = new THREE.WebGLRenderer({ antialias: true });
    renderer.setSize(600, 400);
    viewerDiv.appendChild(renderer.domElement);

    // OrbitControls
    const controls = new THREE.OrbitControls(camera, renderer.domElement);
    controls.enableDamping = true;
    controls.dampingFactor = 0.05;

    // Light
    const light = new THREE.DirectionalLight(0xffffff, 1);
    light.position.set(1, 1, 1).normalize();
    scene.add(light);

    // Load STL via fetch (supports arrayBuffer for private URLs)
    fetch(modelUrl)
        .then(res => res.arrayBuffer())
        .then(data => {
            const loader = new THREE.STLLoader();
            const geometry = loader.parse(data);

            const material = new THREE.MeshNormalMaterial(); // أو بدلها MeshStandardMaterial مع الخامات إذا STL يحتوي ألوان
            const mesh = new THREE.Mesh(geometry, material);

            // Center model
            geometry.computeBoundingBox();
            const center = geometry.boundingBox.getCenter(new THREE.Vector3());
            mesh.position.sub(center);

            scene.add(mesh);
        })
        .catch(err => console.error("Failed to load STL:", err));

    function animate() {
        requestAnimationFrame(animate);
        controls.update();
        renderer.render(scene, camera);
    }
    animate();
}
