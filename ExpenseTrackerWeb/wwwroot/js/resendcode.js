document.getElementById('resendView').addEventListener('click', function (event) {
    event.stopPropagation();

    document.getElementById('resend-new-cont').classList.remove('hide');
    document.getElementById('resend-new-cont').classList.add('show');
    document.getElementById('login-blur').classList.remove('hide');
    document.getElementById('login-blur').classList.add('show');
});

document.getElementById('login-blur').addEventListener('click', function (event) {
    event.stopPropagation();

    document.getElementById('resend-new-cont').classList.remove('show');
    document.getElementById('resend-new-cont').classList.add('hide');
    document.getElementById('login-blur').classList.remove('show');
    document.getElementById('login-blur').classList.add('hide');
});

document.getElementById('send-new-code').addEventListener('click', async function () {
    const email = document.getElementById('resend-email').value.trim();
    const errorMessageElement = document.getElementById('resend-error-message');
    const sendButton = document.getElementById('send-new-code');

    // Clear previous error messages
    errorMessageElement.textContent = '';
    errorMessageElement.style.color = 'white';

    if (!email) {
        errorMessageElement.style.color = 'red';
        errorMessageElement.textContent = 'Email is required.';
        return;
    }

    const resendCode = { Email: email };

    // Disable the button and show "Processing..."
    sendButton.disabled = true;
    sendButton.textContent = 'Processing...';

    try {
        const response = await fetch('/Mail/ResendVerification', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(resendCode)
        });

        const data = await response.json();

        if (response.ok && data.success) {
            errorMessageElement.style.color = 'green';
            errorMessageElement.textContent = data.message;
        } else {
            errorMessageElement.style.color = 'red';
            errorMessageElement.textContent = data.message || 'Failed to send the code. Please try again.';
            if (data.errors) {
                console.error('Validation Errors:', data.errors);
            }
        }
    } catch (error) {
        console.error('Error:', error);
        errorMessageElement.textContent = 'Something went wrong. Please try again later.';
    } finally {
        // Re-enable the button and reset its text
        sendButton.disabled = false;
        sendButton.textContent = 'Send';
    }
});


//document.getElementById('send-new-code').addEventListener('click', async function () {
//    const email = document.getElementById('resend-email').value.trim();
//    const errorMessageElement = document.getElementById('resend-error-message');
//    errorMessageElement.textContent = '';
//    errorMessageElement.style.color = 'white';

//    if (!email) {
//        errorMessageElement.style.color = 'red';
//        errorMessageElement.textContent = 'Email is required.';
//        return;
//    }

//    const resendCode = {
//        Email: email
//    };

//    try {
//        const response = await fetch('/Mail/ResendVerification', {
//            method: 'POST',
//            headers: {
//                'Content-Type': 'application/json'
//            },
//            body: JSON.stringify(resendCode)
//        });

//        const data = await response.json();

//        if (response.ok && data.success) {
//            errorMessageElement.style.color = 'green';
//            errorMessageElement.textContent = 'Temporay code was sent.';
//        } else {       
//            errorMessageElement.style.color = 'red';
//            errorMessageElement.textContent = data.message || 'Failed to send the code. Please try again.';
//            if (data.errors) {
//                console.error('Validation Errors:', data.errors);
//            }
//        }
//    } catch (error) {
//        console.error('Error:', error);
//        errorMessageElement.textContent = 'Something went wrong. Please try again later.';
//    }
//});


//document.getElementById('send-new-code').addEventListener('click', async function () {
//    const email = document.getElementById('resend-email').value.trim();

//    const resendCode = {
//        Email: email
//    };

//    try {
//        const response = await fetch('/Mail/ResendVerification', {
//            method: 'POST',
//            headers: {
//                'Content-Type': 'application/json'
//            },
//            body: JSON.stringify(resendCode)
//        });

//        const data = await response.json();

//        if (response.ok && data.success) {
//            alert('SendSuccess');
//        } else {
//            alert('fail');
//        }

//    } catch (error) {
//        console.error('Error', error);
//        alert('Something went wrong.');
//    }
//});