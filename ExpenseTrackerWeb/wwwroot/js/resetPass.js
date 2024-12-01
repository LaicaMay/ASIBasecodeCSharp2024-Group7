document.getElementById('reset-submit').addEventListener('click', async function () {
    const userId = document.getElementById('reset-userId').value;
    const resetToken = document.getElementById('reset-token').value.trim();
    const newPass = document.getElementById('reset-newPass').value.trim();
    const confirmPass = document.getElementById('reset-confirmPass').value.trim();

    const sendReset = document.getElementById('reset-submit');

    const resetPassData = {
        UserId: userId,
        Token: resetToken,
        NewPassword: newPass,
        NewConfirmPassword: confirmPass
    };

    sendReset.disable = true;
    sendReset.textContent = 'Sending...'

    try {
        const response = await fetch('/Account/ChangePassword', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(resetPassData)
        });

        const data = await response.json();

        if (response.ok && data.success) {
            document.getElementById('success-reset').classList.add('show');
            document.getElementById('success-reset').classList.remove('hide');
            document.getElementById('blur-resetSuccess').classList.add('show');
            document.getElementById('blur-resetSuccess').classList.remove('hide');
        } else {
            const errorList = document.getElementById('error-messages-list');
            errorList.innerHTML = ''; 

            if (data.message) {
                const errorItem = document.createElement('li');
                errorItem.textContent = data.message;
                errorList.appendChild(errorItem);
            }

            if (data.errors) {
                for (const key in data.errors) {
                    data.errors[key].forEach(error => {
                        const errorItem = document.createElement('li');
                        errorItem.textContent = error;
                        errorList.appendChild(errorItem);
                    });
                }
            }

            document.getElementById('span-error-msg').classList.add('show');
            document.getElementById('span-error-msg').classList.remove('hide');
            document.getElementById('blur-resetPass').classList.add('show');
            document.getElementById('blur-resetPass').classList.remove('hide');
        }
    } catch (error) {
        console.error('Error', error);
        alert('Something went wrong.');
    } finally {
        sendReset.disabled = false;
        sendReset.textContent = 'Reset Password';
    }
});

document.getElementById('blur-resetPass').addEventListener('click', function (event) {
    event.stopPropagation();

    document.getElementById('span-error-msg').classList.add('hide');
    document.getElementById('span-error-msg').classList.remove('show');
    document.getElementById('blur-resetPass').classList.add('hide');
    document.getElementById('blur-resetPass').classList.remove('show');

});
