function setTheme(theme) {
    if (theme === 'dark') {
        document.body.classList.add('dark-mode');
    } else {
        document.body.classList.remove('dark-mode');
    }
}

function toggleEdit() {
    const editSection = document.querySelector('.edit-section');
    const editButton = document.querySelector('.edit-button');
    const actionButtons = document.querySelector('.action-buttons');
    const themeInputs = document.querySelectorAll('.preference-section input');

    if (editSection.style.display === 'none' || editSection.style.display === '') {
        editSection.style.display = 'block';
        editButton.style.display = 'none';
        actionButtons.style.display = 'block';
        themeInputs.forEach(input => input.disabled = false); // Enable theme inputs
    } else {
        editSection.style.display = 'none';
        editButton.style.display = 'block';
        actionButtons.style.display = 'none';
        themeInputs.forEach(input => input.disabled = true); // Disable theme inputs
    }
}

function saveChanges() {
    // Implement save changes logic
    alert('Changes saved!');
    toggleEdit(); // Hide the edit section and show the edit button again
}

function cancelEdit() {
    // Implement cancel logic
    alert('Edit cancelled!');
    toggleEdit(); // Hide the edit section and show the edit button again
}