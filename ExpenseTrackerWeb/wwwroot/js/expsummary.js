const modal = document.getElementById("expenseModal");
const generateReportButton = document.getElementById("generateReportButton");
const closeModal = document.getElementById("closeModal");
const saveToPdfButton = document.getElementById("saveToPdfButton");

generateReportButton.addEventListener("click", function () {
    modal.style.display = "flex";
    document.getElementById("generateReportButton").addEventListener("click", function () {
        fetch('/Expense/GenerateReport', {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
            }
        })
            .then(response => response.json())  // Parse JSON response
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

                alert(data.message);  
            })
            .catch(error => {
                console.error('Error generating report:', error);
            });
    });

});

closeModal.addEventListener("click", function () {
    modal.style.display = "none"; 
});


saveToPdfButton.addEventListener("click", function () {
    alert("Not Saved Yet");
    modal.style.display = "none"; 
});

window.addEventListener("click", function (event) {
    if (event.target === modal) {
        modal.style.display = "none";
    }
});