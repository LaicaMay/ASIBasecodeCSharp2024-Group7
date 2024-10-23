document.addEventListener("DOMContentLoaded", function () {
        document.querySelectorAll('.item-details').forEach(function (detailsLink) {
            detailsLink.addEventListener('click', function () {
                const parentRow = this.closest('tr').querySelector('td');


                const expenseId = parentRow.getAttribute('data-expense-id');
                const expenseName = parentRow.getAttribute('data-expense-name');
                const amount = parentRow.getAttribute('data-amount');
                const categoryName = parentRow.getAttribute('data-category-name');
                const date = parentRow.getAttribute('data-date');
                const description = parentRow.getAttribute('data-description');
                const startDate = parentRow.getAttribute('data-start-date');

                console.log('ExpneseId', expenseId);

                document.getElementById('modal-expense-id').value = expenseId;
                document.getElementById('modal-expense-name').textContent = expenseName;
                document.getElementById('modal-amount').textContent = `₱${amount}`;
                document.getElementById('modal-category-name').textContent = categoryName;
                document.getElementById('modal-date').textContent = (date === null || date === '') ? startDate : date;
                document.getElementById('modal-description').textContent = description;

                var modalId = document.getElementById('modal-expense-id')

                document.getElementById('expense-container').classList.remove('hide');
                document.getElementById('expense-container').classList.add('show');

                document.getElementById('add-expense-container').classList.remove('show');
                document.getElementById('add-expense-container').classList.add('hide');

                document.getElementById('edit').addEventListener('click', function () {
 
                    document.getElementById('edit-expense-id').value = expenseId;
                    document.getElementById('edit-expense-name').value = expenseName;
                    document.getElementById('edit-amount').value = amount;
                    document.getElementById('edit-date').value = date;
                    document.getElementById('edit-description').value = description;

                    const categoryDropdown = document.getElementById('edit-category-id');
                    for (let i = 0; i < categoryDropdown.options.length; i++) {
                        if (categoryDropdown.options[i].text === categoryName) {
                            categoryDropdown.selectedIndex = i;
                            break;
                        }
                    }

                    document.getElementById('expense-container').classList.remove('show');
                    document.getElementById('expense-container').classList.add('hide');

                    document.getElementById('edit-form-cont').classList.remove('hide');
                    document.getElementById('edit-form-cont').classList.add('show');
                });

                document.getElementById('del').onclick = function () {
                    const expenseId = document.getElementById('modal-expense-id').value;

                    if (confirm('Are you sure you want to delete this expense?')) {
                        fetch(`/Expense/DeleteExpense/${expenseId}`, {
                            method: 'DELETE',
                            headers: {
                                'Content-Type': 'application/json',
                            }
                        })
                        .then(response => {
                            if (response.ok) {
                                return response.json();
                            } else {
                                return Promise.reject('Failed to delete expense.');
                            }
                        })
                        .then(data => {
                            console.log(data.message);

                            const row = document.querySelector(`tr[data-expense-id='${expenseId}']`);
                            if (row) {
                                row.remove(); 
                            }

                            document.getElementById('expense-container').classList.remove('show');
                            document.getElementById('expense-container').classList.add('hide');

                            const successModal = document.getElementById('success-added-modal');
                            successModal.classList.remove('hide');
                            successModal.classList.add('show');
                            setTimeout(() => {
                                successModal.classList.remove('show');
                                successModal.classList.add('hide');
                            }, 3000);
                        })
                        .catch(error => {
                            console.error('Error:', error);
                            alert(error);
                        });
                    }
                };

            });
        });

        document.querySelector('.blur-bg').addEventListener('click', function () {
            document.getElementById('expense-container').classList.remove('show');
            document.getElementById('expense-container').classList.add('hide');
        });

        document.getElementById('edit').addEventListener('click', function (event) {
            event.stopPropagation();

            let editForm = document.getElementById('edit-form-cont');

            if (editForm.classList.contains('hide')) {
                editForm.classList.remove('hide');
                editForm.classList.add('show');
            }
        });

        document.getElementById('edit-add-blur').addEventListener('click', function (event) {
            event.stopPropagation();
            document.getElementById('edit-form-cont').classList.remove('show');
            document.getElementById('edit-form-cont').classList.add('hide');
        });

    });