// Function to display the modal with error messages
function showErrorModal(message) {
    document.getElementById('modalBody').innerText = message;
    document.getElementById('errorModal').style.display = 'block';
}

// Function to hide the modal
function hideErrorModal() {
    document.getElementById('errorModal').style.display = 'none';
}

// When the user clicks on <span> (x), close the modal
document.querySelector('.close-btn').onclick = function () {
    hideErrorModal();
}

// When the user clicks anywhere outside of the modal, close it
window.onclick = function (event) {
    if (event.target === document.getElementById('errorModal')) {
        hideErrorModal();
    }
}

// Example: Show the modal if there's a validation error
document.addEventListener('DOMContentLoaded', function () {
    var errorMessage = '@Html.Raw(ViewData["ErrorMessage"])';
    if (errorMessage) {
        showErrorModal(errorMessage);
    }
});