document.getElementById('toggleCheckbox').addEventListener('change', function () {
    const dayTitle = document.querySelector('.dayTitle');
    const dateNow = document.querySelector('.date-now');
    const checkboxContainer = document.querySelector('.checkbox-container');
    const dateStartEnd = document.querySelector('.date-start-end');

    if (this.checked) {
        dayTitle.style.opacity = 0;
        dayTitle.style.pointerEvents = 'none';
        dayTitle.style.position = 'absolute';
        dayTitle.style.left = '100px';

        dateNow.style.opacity = 0;
        dateNow.style.pointerEvents = 'none';

        checkboxContainer.style.opacity = 1;
        checkboxContainer.style.pointerEvents = 'auto';
        checkboxContainer.style.position = 'absolute';
        checkboxContainer.style.right = '40px';

        dateStartEnd.style.opacity = 1;
        dateStartEnd.style.pointerEvents = 'auto';
        dateStartEnd.style.position = 'absolute';
        dateStartEnd.style.right = '41px';
        dateStartEnd.style.top = '246px';
        dateStartEnd.style.display = 'flex';
        dateStartEnd.style.gap = '10px';

    } else {
        dayTitle.style.opacity = 1;
        dayTitle.style.pointerEvents = 'auto';
        dateNow.style.opacity = 1;
        dateNow.style.pointerEvents = 'auto';

        checkboxContainer.style.opacity = 0;
        checkboxContainer.style.pointerEvents = 'none';

        dateStartEnd.style.opacity = 0;
        dateStartEnd.style.pointerEvents = 'none';
    }
});

document.getElementById('ok-btn').addEventListener('click', function (event) {
    event.stopPropagation();

    const expenseName = document.getElementById('expense-name').value.trim();
    const amount = document.getElementById('amount').value.trim();
    const categoryId = document.getElementById('category-id').value;
    const description = document.getElementById('description').value.trim(); 
    let date = document.getElementById('date-only').value.trim();
    let startDate = document.getElementById('start-date').value.trim();
    let endDate = document.getElementById('end-date').value.trim(); 
    const checkbox = document.getElementById('toggleCheckbox');

    let selectedDays = [];
    document.querySelectorAll('.day-checkbox:checked').forEach(checkbox => {
        selectedDays.push(checkbox.value);
    });
    const daysOfWeek = selectedDays.join(',');
    const setDay = selectedDays.length > 0;

    const today = new Date().toISOString().split('T')[0];

    if (!expenseName || !amount || !categoryId || !description) {
        alert('Please fill in all required fields.');
        return;
    }

    if (checkbox.checked) {
        startDate = today;
        endDate = today;
        date = null; 
    } else {      
        date = today;
        startDate = null;
        endDate = null; 
    }

    const expenseData = {
        ExpenseName: expenseName,
        Amount: amount,
        CategoryId: categoryId,
        Date: date,
        Description: description,
        SetDay: setDay,
        DaysOfWeek: daysOfWeek,
        StartDate: startDate,
        EndDate: endDate
    };

        fetch('/Expense/AddExpense', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(expenseData)
        })
        .then(response => {
            if (response.ok) {
                document.getElementById('success-added-modal').classList.remove('hide');
                document.getElementById('success-added-modal').classList.add('show');

                document.getElementById('expense-name').value = '';
                document.getElementById('amount').value = '';
                document.getElementById('category-id').selectedIndex = 0;
                document.getElementById('date-only').value = '';
                document.getElementById('description').value = '';
                document.querySelectorAll('.day-checkbox').forEach(checkbox => checkbox.checked = false);
                document.getElementById('start-date').value = '';
                document.getElementById('end-date').value = '';
            } else {
                alert('Failed to save expense. Please try again.');
            }
        })
        .catch(error => console.error('Error:', error));
});


document.getElementById('add-id').addEventListener('click', function (event) {
    event.stopPropagation();

    let expenseCont = document.getElementById('add-expense-container');
    let addExpnModal = document.getElementById('expense-add');

    if (expenseCont.classList.contains('hide')) {
        expenseCont.classList.remove('hide');
        expenseCont.classList.add('show');
        addExpnModal.classList.remove('hide');
        addExpnModal.classList.add('show');

    }
});

document.getElementById('add-blur').addEventListener('click', function (event) {
    event.stopPropagation();
    document.getElementById('add-expense-container').classList.remove('show');
    document.getElementById('add-expense-container').classList.add('hide');
    document.getElementById('expense-add').classList.remove('show');
    document.getElementById('expense-add').classList.add('hide');
});

document.getElementById('m-add-id').addEventListener('click', function (event) {
    event.stopPropagation();

    let expenseCont = document.getElementById('add-expense-container');
    let addExpnModal = document.getElementById('expense-add');

    if (expenseCont.classList.contains('hide')) {
        expenseCont.classList.remove('hide');
        expenseCont.classList.add('show');
        addExpnModal.classList.remove('hide');
        addExpnModal.classList.add('show');

    }
});

document.getElementById('save-btn').addEventListener('click', function (event) {
    event.stopPropagation();

    let confirmModal = document.getElementById('confirmation-modal');
    let addExpnModal = document.getElementById('expense-add');

    if (confirmModal.classList.contains('hide')) {
        confirmModal.classList.remove('hide');
        confirmModal.classList.add('show');
        addExpnModal.classList.remove('show');
        addExpnModal.classList.add('hide');

    }
});

document.getElementById('cancel-btn').addEventListener('click', function (event) {
    event.stopPropagation();

    let confirmModal = document.getElementById('confirmation-modal');
    let addExpnModal = document.getElementById('expense-add');

    if (confirmModal.classList.contains('show')) {
        confirmModal.classList.remove('show');
        confirmModal.classList.add('hide');
        addExpnModal.classList.remove('hide');
        addExpnModal.classList.add('show');
    }
});

document.getElementById('done-b').addEventListener('click', function (event) {
    event.stopPropagation();

    document.getElementById('success-added-modal').classList.remove('show');
    document.getElementById('success-added-modal').classList.add('hide');
    document.getElementById('add-expense-container').classList.remove('show');
    document.getElementById('add-expense-container').classList.add('hide');

    location.reload();
});