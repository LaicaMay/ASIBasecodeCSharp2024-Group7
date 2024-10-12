
document.getElementById('generateReportButton').addEventListener('click', function() {
    document.getElementById('reportOverlay').style.display = 'flex';
    });

document.getElementById('exitReportButton').addEventListener('click', function() {
    document.getElementById('reportOverlay').style.display = 'none';
    });

// Adjust layout on orientation change
window.addEventListener('resize', function() {
        if (window.innerHeight < window.innerWidth) {
    // Landscape orientation
    document.querySelector('.expensecontents').style.flexDirection = 'column';
document.querySelector('.charts').style.flexDirection = 'column';
        } else {
    // Portrait orientation
    document.querySelector('.expensecontents').style.flexDirection = 'row';
document.querySelector('.charts').style.flexDirection = 'row';
        }
    });