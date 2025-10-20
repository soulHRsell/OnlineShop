document.addEventListener("DOMContentLoaded", function () {
    const themeToggle = document.getElementById("themeToggle");
    const icon = themeToggle.querySelector("i");

    // Apply saved theme
    const currentTheme = localStorage.getItem("theme") || "light";
    document.documentElement.setAttribute("data-bs-theme", currentTheme);
    updateIcon(currentTheme);

    // Add smooth transition after load (prevents blinking)
    setTimeout(() => document.body.classList.add("theme-transition-enabled"), 100);

    // Toggle theme
    themeToggle.addEventListener("click", () => {
        let newTheme = document.documentElement.getAttribute("data-bs-theme") === "dark" ? "light" : "dark";
        document.documentElement.setAttribute("data-bs-theme", newTheme);
        localStorage.setItem("theme", newTheme);
        updateIcon(newTheme);
    });

    function updateIcon(theme) {
        icon.className = theme === "dark" ? "bi bi-sun-fill" : "bi bi-moon-fill";
    }
});