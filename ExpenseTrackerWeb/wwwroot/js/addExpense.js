function getMonthYear(dateString) {
    const date = new Date(dateString);
    return date.toLocaleString('en-US', { month: 'long', year: 'numeric' });
}

function validateDateInput(inputId) {
    const balanceDateSet = document.getElementById('balance-date-set').value;
    const dateInput = document.getElementById(inputId);
    const selectedDate = dateInput.value;

    if (selectedDate) {
        const selectedMonthYear = getMonthYear(selectedDate);

        if (selectedMonthYear !== balanceDateSet) {
            dateInput.value = '';
            alert(`The selected date must fall within the balance period: ${balanceDateSet}.`);
        }
    }
}

document.getElementById('date-only').addEventListener('change', function () {
    validateDateInput('date-only');
});

document.getElementById('start-date').addEventListener('change', function () {
    validateDateInput('start-date');
});

document.getElementById('end-date').addEventListener('change', function () {
    validateDateInput('end-date');
});

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
    const amount = parseFloat(document.getElementById('amount').value.trim());
    const remainBalRaw = document.getElementById('remainingUserBalance').value.trim();
    const remainBal = parseFloat(remainBalRaw.replace(/,/g, ''));
    const categoryId = document.getElementById('category-id').value;
    const description = document.getElementById('description').value.trim();

    //console.log('RemainBal: ', remainBal);

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

    let totalAmount = 0;
    if (startDate && endDate && selectedDays.length > 0) {
        const start = new Date(startDate);
        const end = new Date(endDate);

        for (let current = new Date(start); current <= end; current.setDate(current.getDate() + 1)) {
            const dayOfWeek = current.toLocaleString('en-US', { weekday: 'long' });
            if (selectedDays.includes(dayOfWeek)) {
                totalAmount += amount;
            }
        }
    }    

    if (totalAmount > remainBal) {
        alert('Insufficient balance.');
        return;
    }

    if (amount <= 0) {
        alert('Please enter a valid amount.');
        return;
    }

    if (checkbox.checked) {
        date = null; 
        if (startDate >= endDate) {
            alert('Invalid start date and end date;')
            return;
        }

        if (endDate <= startDate) {
            alert('Invalid start date and end date;')
            return;
        }
    } else {      
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
        EndDate: endDate,   
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
            alert('Insufficient Remaining Balance. Please try again.');
            return;
        }
    })
    .catch(error => console.error('Error:', error));
});

//document.getElementById('add-id').addEventListener('click', function (event) {
//    event.stopPropagation();

//    let expenseCont = document.getElementById('add-expense-container');
//    let addExpnModal = document.getElementById('expense-add');
//    let balanceDropdown = document.getElementById('balance-dropdown');
//    let balanceDateSetInput = document.getElementById('balance-date-set');

//    // Get the first option's data-balance-date value
//    if (balanceDropdown.options.length > 0) {
//        let firstBalanceDate = balanceDropdown.options[0].getAttribute('data-balance-date');
//        balanceDateSetInput.value = firstBalanceDate; // Set the value to the input field
//    }

//    console.log('set date', balanceDateSetInput)

//    if (expenseCont.classList.contains('hide')) {
//        expenseCont.classList.remove('hide');
//        expenseCont.classList.add('show');
//        addExpnModal.classList.remove('hide');
//        addExpnModal.classList.add('show');
//    }
//});


document.getElementById('add-id').addEventListener('click', function (event) {
    event.stopPropagation();

    let expenseCont = document.getElementById('add-expense-container');
    let addExpnModal = document.getElementById('expense-add');
    let balanceDropdown = document.getElementById('balance-dropdown');
    let balanceDateSetInput = document.getElementById('balance-date-set');
    let dateOnlyInput = document.getElementById('date-only');
    let startDateInput = document.getElementById('start-date');
    let endDateInput = document.getElementById('end-date');

    //Get the first option's data-balance-date value
    if (balanceDropdown.options.length > 0) {
        let firstBalanceDate = balanceDropdown.options[0].getAttribute('data-balance-date');
        balanceDateSetInput.value = firstBalanceDate; // Set the value to the input field
    }

    //Get the value from the hidden input and parse it
    let balanceDateValue = balanceDateSetInput.value; // Format: "January 2024"
    if (balanceDateValue) {
        //Extract month and year
        let [month, year] = balanceDateValue.split(' ');

        //Create start and end dates in UTC
        let startDate = new Date(Date.UTC(year, new Date(`${month} 1`).getMonth(), 1)); //Start of the month
        let endDate = new Date(Date.UTC(year, new Date(`${month} 1`).getMonth() + 1, 0)); //End of the month

        //Format dates to "yyyy-MM-dd"
        let formatDate = (date) => date.toISOString().split('T')[0];
        dateOnlyInput.value = formatDate(startDate); //Keep original value for reference
        startDateInput.value = formatDate(startDate); //Set formatted start date
        endDateInput.value = formatDate(endDate); //Set formatted end date
    }

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
    let balanceDropdown = document.getElementById('balance-dropdown');
    let balanceDateSetInput = document.getElementById('balance-date-set');
    let dateOnlyInput = document.getElementById('date-only');
    let startDateInput = document.getElementById('start-date');
    let endDateInput = document.getElementById('end-date');

    //Get the first option's data-balance-date value
    if (balanceDropdown.options.length > 0) {
        let firstBalanceDate = balanceDropdown.options[0].getAttribute('data-balance-date');
        balanceDateSetInput.value = firstBalanceDate; // Set the value to the input field
    }

    //Get the value from the hidden input and parse it
    let balanceDateValue = balanceDateSetInput.value; // Format: "January 2024"
    if (balanceDateValue) {
        //Extract month and year
        let [month, year] = balanceDateValue.split(' ');

        //Create start and end dates in UTC
        let startDate = new Date(Date.UTC(year, new Date(`${month} 1`).getMonth(), 1)); //Start of the month
        let endDate = new Date(Date.UTC(year, new Date(`${month} 1`).getMonth() + 1, 0)); //End of the month

        //Format dates to "yyyy-MM-dd"
        let formatDate = (date) => date.toISOString().split('T')[0];
        dateOnlyInput.value = formatDate(startDate); //Keep original value for reference
        startDateInput.value = formatDate(startDate); //Set formatted start date
        endDateInput.value = formatDate(endDate); //Set formatted end date
    }

    if (expenseCont.classList.contains('hide')) {
        expenseCont.classList.remove('hide');
        expenseCont.classList.add('show');
        addExpnModal.classList.remove('hide');
        addExpnModal.classList.add('show');
    }
});

//document.getElementById('m-add-id').addEventListener('click', function (event) {
//    event.stopPropagation();

//    let expenseCont = document.getElementById('add-expense-container');
//    let addExpnModal = document.getElementById('expense-add');
//    let balanceDropdown = document.getElementById('balance-dropdown');
//    let balanceDateSetInput = document.getElementById('balance-date-set');

//    //Get the first option's data-balance-date value
//    if (balanceDropdown.options.length > 0) {
//        let firstBalanceDate = balanceDropdown.options[0].getAttribute('data-balance-date');
//        balanceDateSetInput.value = firstBalanceDate; //Set the value to the input field
//    }

//    console.log('set date', balanceDateSetInput)

//    if (expenseCont.classList.contains('hide')) {
//        expenseCont.classList.remove('hide');
//        expenseCont.classList.add('show');
//        addExpnModal.classList.remove('hide');
//        addExpnModal.classList.add('show');
//    }

//});


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

document.getElementById('done-del').addEventListener('click', function (event) {
    event.stopPropagation();

    document.getElementById('success-del-modal').classList.remove('show');
    document.getElementById('success-del-modal').classList.add('hide');
    document.getElementById('add-expense-container').classList.remove('show');
    document.getElementById('add-expense-container').classList.add('hide');

    location.reload();
});

