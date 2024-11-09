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
    const themeInputs = document.querySelector('.dropdown');
    const newPassInput = document.getElementById('#new-password');
    const confirmPass = document.getElementById('#confirm-password');

    if (editSection.style.display === 'none' || editSection.style.display === '') {
        editSection.style.display = 'block';
        editButton.style.display = 'none';
        actionButtons.style.display = 'block';
        newPassInput.style.display = 'block';
        confirmPass.style.display = 'block';
        themeInputs.forEach(input => input.disabled = false); 
    } else {
        editSection.style.display = 'none';
        editButton.style.display = 'block';
        actionButtons.style.display = 'none';
        themeInputs.forEach(input => input.disabled = true); 
    }
}

function saveChanges() {
    // save changes
    alert('Changes saved!');
    toggleEdit(); 
}

function cancelEdit() {
    // cancel 
    alert('Edit cancelled!');
    toggleEdit(); 
}