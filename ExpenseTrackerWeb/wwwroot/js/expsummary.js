const modal = document.getElementById("expenseModal");
const generateReportButton = document.getElementById("generateReportButton");
const closeModal = document.getElementById("closeModal");
const saveToPdfButton = document.getElementById("saveToPdfButton");

generateReportButton.addEventListener("click", function () {
    fetch('/Expense/GenerateReport', {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json',
        }
    })
        .then(response => response.json())  
        .then(data => {
            const tableBody = document.querySelector("#expensestable tbody");
            tableBody.innerHTML = '';  

            if (data.expenses && data.expenses.length > 0) {
                data.expenses.forEach(expense => {
                    const row = `
                        <tr>
                            <td>${expense.expenseName}</td>
                            <td>Php ${expense.amount}</td>
                            <td>${expense.category}</td>
                            <td>${expense.date}</td>
                            <td>${expense.description}</td>
                        </tr>
                    `;
                    tableBody.innerHTML += row;  
                });
            } else {
                tableBody.innerHTML = `
                    <tr>
                        <td colspan="5">No expense data available.</td>
                    </tr>
                `;
            }
            modal.style.display = "flex";
        })
        .catch(error => {
            console.error('Error generating report:', error);
        });
});


closeModal.addEventListener("click", function () {
    modal.style.display = "none"; 
});

saveToPdfButton.addEventListener("click", function () {

    modal.style.display = "none";

    const { jsPDF } = window.jspdf;
    const doc = new jsPDF();

    const modalContent = document.getElementById('expenseModal');

    doc.text('Expense Details', 10, 10);

    const table = modalContent.querySelector('table');
    const rows = table.querySelectorAll('tr');

    let tableData = [];
    rows.forEach((row, index) => {
        const cells = row.querySelectorAll('td, th');
        const rowData = [];
        cells.forEach(cell => {
            rowData.push(cell.textContent.trim());
        });
        tableData.push(rowData);
    });

    doc.autoTable({
        startY: 20,  
        head: [tableData[0]],  
        body: tableData.slice(1), 
    });

    doc.save('expense-details.pdf');
});


window.addEventListener("click", function (event) {
    if (event.target === modal) {
        modal.style.display = "none";
    }
});

//Ini for charts and whatnots

function generateRandomColors(count) {
    return Array.from({ length: count }, () => getRandomColor());
}

function getRandomColor() {
    const letters = '0123456789ABCDEF';
    let color = '#';
    for (let i = 0; i < 6; i++) {
        color += letters[Math.floor(Math.random() * 16)];
    }
    return color;
}

function calculateTotalExpenses(data) {
    return data.reduce((sum, item) => sum + item[1], 0);
}

function findLargestExpense(data) {
    return data.reduce((max, item) => (item[1] > max.amount ? { name: item[0], amount: item[1] } : max), { name: '', amount: 0 });
}

function formatCurrency(value) {
    return new Intl.NumberFormat("en-PH", { style: "currency", currency: "PHP", maximumSignificantDigits: 3 }).format(value);
}

function updateMonthLabels() {
    const monthSelector = document.querySelector(".balance-monthYear select");
    const monthLabels = document.querySelectorAll("#month-selected");

    const updateLabels = () => {
        const selectedMonth = monthSelector.options[monthSelector.selectedIndex].text;
        monthLabels.forEach(label => label.textContent = selectedMonth);
    };

    updateLabels();
    monthSelector.addEventListener("change", updateLabels);
}

function createLineChart(allexp) {
    const labels = [...new Set(allexp.map(item => item.YearMonth))];
    const groupedData = groupDataByCategory(allexp, labels);

    const datasets = Object.keys(groupedData).map(categoryName => ({
        label: categoryName,
        data: labels.map(label => groupedData[categoryName][label] || 0),
        borderColor: getRandomColor(),
        fill: false,
        tension: 0.1
    }));

    createChart(labels, datasets, 'line-graph', 'line');
}

function groupDataByCategory(data, labels) {
    const groupedData = {};
    data.forEach(item => {
        if (!groupedData[item.CategoryName]) {
            groupedData[item.CategoryName] = {};
        }
        groupedData[item.CategoryName][item.YearMonth] = item.TotalAmount;
    });
    return groupedData;
}
