
function createChart(months, datasets, chartID, chartType) {
    const ctx = document.getElementById(chartID).getContext('2d');
    new Chart(ctx, {
        type: chartType, 
        data: {
            labels: months, 
            datasets: datasets, 
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            scales: {
                y: {
                    beginAtZero: true 
                }
            },
            plugins: {
                legend: {
                    display: true, 
                    position: 'top' 
                }
            }
        }
    });
}
