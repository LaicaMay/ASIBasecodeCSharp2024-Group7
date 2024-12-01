function createChart(labels, datasets, chartID, chartType) {
    const ctx = document.getElementById(chartID).getContext('2d');

    const chartOptions = getChartOptions(chartType);

    new Chart(ctx, {
        type: chartType,
        data: {
            labels: labels,
            datasets: datasets
        },
        options: chartOptions
    });

}

function getChartOptions(chartType) {
    const options = {
        responsive: true,
        maintainAspectRatio: false,
        plugins: {
            legend: {
                display: true,
                position: 'top'
            }
        }
    };

    if (chartType === 'line') {
        // Line chart specific options
        options.scales = {
            x: {
                title: {
                    display: true,
                    text: 'Monthly Expenses'
                }
            },
            y: {
                beginAtZero: true,
                title: {
                    display: true,
                    text: 'Expense in ₱'
                },
                ticks: {
                    callback: (value) => {
                        return new Intl.NumberFormat("en-PH", { style: "currency", currency: "PHP", maximumSignificantDigits: 3 }).format(value);
                    }
                }
            }
        };
    } else if (chartType === 'pie') {
        // Pie chart specific options
        options.plugins.tooltip = {
            callbacks: {
                label: function (context) {
                    return `${ new Intl.NumberFormat("en-PH", { style: "currency", currency: "PHP", maximumSignificantDigits: 3 }).format(context.raw)}`;
                }
            }
        };
    }

    return options;
}
