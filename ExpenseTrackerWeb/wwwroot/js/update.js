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
        themeInputs.forEach(input => input.disabled = false); 
    } else {
        editSection.style.display = 'none';
        editButton.style.display = 'block';
        actionButtons.style.display = 'none';
        themeInputs.forEach(input => input.disabled = true); 
    }
}

function saveChanges() {
    const oldPassword = document.getElementById('old-password').value;
    const newPassword = document.getElementById('new-password').value;
    const confirmPassword = document.getElementById('confirm-password').value;

    if (!oldPassword || !newPassword || !confirmPassword) {
        alert('Please fill in all fields.');
        return;
    }

    if (newPassword !== confirmPassword) {
        alert('New password and confirm password do not match.');
        return;
    }

    fetch('/Account/UserChangePassword', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({
            Password: oldPassword,
            NewPassword: newPassword,
            NewConfirmPassword: confirmPassword,
        }),
    })
        .then(response => {
            if (!response.ok) {
                return response.json().then(data => {
                    throw new Error(data.message || 'Failed to change password.');
                });
            }
            return response.json();
        })
        .then(data => {
            alert(data.message || 'Password changed successfully!');
            toggleEdit();
        })
        .catch(error => {
            console.error('Error:', error);
            alert(error.message || 'An error occurred while updating the password.');
        });
}

function cancelEdit() {
    // Reset fields if needed
    document.getElementById('old-password').value = '';
    document.getElementById('new-password').value = '';
    document.getElementById('confirm-password').value = '';

    alert('Edit cancelled!');
    toggleEdit(); 
}
