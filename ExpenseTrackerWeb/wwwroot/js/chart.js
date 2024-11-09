//Line Graph
const data1 = [1200, 1900, 3000, 5000, 2000, 3700];
const data2 = [1020, 1090, 3200, 2500, 2040, 2370];
const data3 = [1200, 1500, 2300, 2500, 2090, 3300];
const data4 = [100, 100, 200, 200, 200, 300];
const months = ['June', 'July', 'August', 'September', 'October', 'November'];

const labelinput = document.getElementById('Expense-name');

const line = document.getElementById('line-graph');


new Chart(line, {
    type: 'bar',
    data: {
        labels: months,
        datasets: [{
            label: 'Food Expense',
            data: data1,
            borderWidth: 1
        }, {
            label: 'School Expense',
            data: data2,
            borderWidth: 1
        }, {
            label: 'Transportation Expense',
            data: data3,
            borderWidth: 1
        }]
    },
    options: {
        responsive: true,
        maintainAspectRatio: false,
        scales: {
            y: {
                beginAtZero: true
            }
        }

    }
});

//Pie Chart
const pie = document.getElementById('pie-chart');
const piecardview = document.getElementById('changeData');
const title = document.getElementById('title');

const labeltitle = ['Food Expense', 'School Expense', 'Transportation Expense'];

const datachange = [data1, data2, data3];
let indexData = 0;
let chartInstance;

function updateChart() {

    if (chartInstance) {
        chartInstance.destroy();
    }

    title.textContent = labeltitle[indexData];

    chartInstance = new Chart(pie, {
        type: 'pie',
        data: {
            labels: months,
            datasets: [{
                label: labeltitle[indexData],
                data: datachange[indexData],
                borderWidth: 3
            }]
        },
        options: {
            responsive: true,
            maintainAspectRatio: true,
            plugins: {
                legend: {
                    position: 'bottom',
                    labels: {
                        usePointStyle: true,
                        boxWidth: 10,
                    }
                }
            },
            layout: {
                padding: {
                    bottom: 20
                }
            }
        }
    });
}

console.log(labeltitle[indexData], " and ", datachange[indexData])

piecardview.addEventListener('click', function (event) {
    indexData = (indexData + 1) % labeltitle.length;
    updateChart();
});

updateChart();