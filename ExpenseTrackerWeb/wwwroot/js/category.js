document.addEventListener('DOMContentLoaded', function () {

    document.getElementById('colorPickerImg').addEventListener('click', function () {
        document.getElementById('colorPickerModal').classList.remove('hide');
    });

    document.getElementById('colorInput').addEventListener('input', function () {
        const selectedColor = this.value;
        document.getElementById('colorCategory').value = selectedColor;
    });

    document.getElementById('closeColorPicker').addEventListener('click', function () {
        document.getElementById('colorPickerModal').classList.add('hide');
    });

    document.getElementById('cat-ok-btn').addEventListener('click', async function () {
        const categoryColor = document.getElementById('colorCategory').value.trim();
        const categoryName = document.getElementById('cat-name').value.trim();
        const categoryDescription = document.getElementById('cat-description').value.trim();    
        let successAdded = document.getElementById('cat-success-added-modal');
        let okBtn = document.getElementById('cat-ok-btn');
        let errorVal = document.getElementById('caterrorVal');
        let errolValName = document.getElementById('errorcat-name');
        let errorBlur = document.getElementById('blur-error-cat');

        errolValName.textContent = "";
        okBtn.disabled = true;

        if (!categoryName || !categoryDescription) {
            errorVal.classList.add('show');
            errorVal.classList.remove('hide');
            errorBlur.classList.add('show');
            errorBlur.classList.remove('hide');
            errolValName.textContent = "All fields are required.";
            okBtn.disabled = false;
            return;
        }

        const categoryData = {
            ColorPick: categoryColor,
            CategoryName: categoryName,
            Description: categoryDescription
        };     

        try {
            const response = await fetch('/Expense/AddCategory', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(categoryData)
            });

            const data = await response.json();

            if (response.ok && data.success) {

                document.getElementById('cat-name').value = '';
                document.getElementById('cat-description').value = '';
                okBtn.disabled = false

                if (successAdded.classList.contains('hide')) {
                    successAdded.classList.remove('hide');
                    successAdded.classList.add('show');
                    document.getElementById('cat-confirmation-modal').classList.remove('show');
                    document.getElementById('cat-confirmation-modal').classList.add('hide');
                    document.getElementById('add-category-cont').classList.remove('show');
                    document.getElementById('add-category-cont').classList.add('hide');
                    okBtn.disabled = false
                }
            } else {
                alert('Error: ' + data.message);
                okBtn.disabled = false
            }

        } catch (error) {
            console.error('Error:', error);
            alert('Something went wrong. Please try again later.');
            okBtn.disabled = false
        }
    });

    document.getElementById('blur-error-cat').addEventListener('click', function (event) {
        event.stopPropagation();

        let errorVal = document.getElementById('caterrorVal');
        let errorBlur = document.getElementById('blur-error-cat');

        errorVal.classList.add('hide');
        errorVal.classList.remove('show');
        errorBlur.classList.add('hide');
        errorBlur.classList.remove('show');
    });



    document.getElementById('cat-cancel-btn').addEventListener('click', function (event) {
        event.stopPropagation();

        let confirmModal = document.getElementById('cat-confirmation-modal');

        if (confirmModal.classList.contains('show')) {
            confirmModal.classList.remove('show');
            confirmModal.classList.add('hide');
        }
    })
    document.getElementById('category-save-btn').addEventListener('click', function (event) {
        event.stopPropagation();

        let confirmCatModal = document.getElementById('cat-confirmation-modal');

        if (confirmCatModal.classList.contains('hide')) {
            confirmCatModal.classList.remove('hide');
            confirmCatModal.classList.add('show');
        }
    });
    document.getElementById('cat-done-b').addEventListener('click', function (event) {
        event.stopPropagation();
        document.getElementById('cat-success-added-modal').classList.remove('show');
        document.getElementById('cat-success-added-modal').classList.add('hide');
        document.getElementById('category-add-blur').classList.remove('show')
        document.getElementById('category-add-blur').classList.add('hide')

        location.reload(); 
    });
    document.getElementById('category-add-id').addEventListener('click', function (event) {
        event.stopPropagation();

        let addCatModal = document.getElementById('add-category-cont');
        let blur = document.getElementById('category-add-blur');

        if (addCatModal.classList.contains('hide') && blur.classList.contains('hide')) {
            addCatModal.classList.remove('hide');
            blur.classList.remove('hide');
            addCatModal.classList.add('show');
            blur.classList.add('show');
        }
    });

    document.getElementById('category-add-blur').addEventListener('click', function (event) {
        event.stopPropagation();

        let addCatModal = document.getElementById('add-category-cont');
        let blur = document.getElementById('category-add-blur');

        if (addCatModal.classList.contains('show') && blur.classList.contains('show')) {
            addCatModal.classList.remove('show');
            blur.classList.remove('show');
            addCatModal.classList.add('hide');
            blur.classList.add('hide');
        }
    });
    document.querySelectorAll('.catDetails').forEach(function (element) {
        element.addEventListener('click', function (event) {
            event.stopPropagation(); 
            const parentRow = this.closest('tr').querySelector('td');

            const categoryId = parentRow.getAttribute('data-category-id');
            const categoryName = parentRow.getAttribute('data-name-category');
            const description = parentRow.getAttribute('data-description-category');
            const totalAmount = parentRow.getAttribute('data-totalamount');

            document.getElementById('category-id').value = categoryId;
            document.getElementById('category-name').textContent = categoryName;
            document.getElementById('category-description').textContent = description;
            document.getElementById('category-amount').textContent = totalAmount;

            let detailsCatModal = document.getElementById('category-container');
            let blur = document.getElementById('category-add-blur');

            if (detailsCatModal.classList.contains('hide') && blur.classList.contains('hide')) {
                detailsCatModal.classList.remove('hide');
                blur.classList.remove('hide');
                detailsCatModal.classList.add('show');
                blur.classList.add('show');
            } else {
                detailsCatModal.classList.remove('hide');
                blur.classList.remove('hide');
                detailsCatModal.classList.add('show');
                blur.classList.add('show');
            }      
        });
    });

    document.getElementById('categoryedit').addEventListener('click', function () {
        document.getElementById('edit-category-cont').classList.remove('hide');
        document.getElementById('edit-category-cont').classList.add('show');

        const categoryId = document.getElementById('category-id').value;
        const categoryName = document.getElementById('category-name').textContent;
        const description = document.getElementById('category-description').textContent;

        console.log('ID', categoryId);
        document.getElementById('edit-category-id').value = categoryId;
        document.getElementById('edit-category-name').value = categoryName;
        document.getElementById('edit-category-description').value = description; 
    });

    document.getElementById('categorydel').addEventListener('click', function (event) {
        event.stopPropagation();
        document.getElementById('del-category-cont').classList.remove('hide');
        document.getElementById('del-category-cont').classList.add('show');
        document.getElementById('category-container').classList.remove('show');
        document.getElementById('category-container').classList.add('hide');
    });

    document.getElementById('delCat-cancel').addEventListener('click', function (event) {
        event.stopPropagation();
        document.getElementById('del-category-cont').classList.remove('show');
        document.getElementById('del-category-cont').classList.add('hide');
        document.getElementById('category-add-blur').classList.remove('show');
        document.getElementById('category-add-blur').classList.add('hide');
    });

    document.getElementById('delCat-confirm').addEventListener('click', async function () {
        const categoryId = document.getElementById('category-id').value;
        document.getElementById('success-del-cat');

        try {
            const response = await fetch(`/Expense/DeleteCategory/${categoryId}`, {
                method: 'DELETE',
                headers: {
                    'Content-Type': 'application/json'
                }
            });

            const data = await response.json();

            if (response.ok) {
                document.getElementById('success-del-cat').classList.add('show');
                document.getElementById('success-del-cat').classList.remove('hide');
                document.getElementById('cat-blur-del').classList.add('show');
                document.getElementById('cat-blur-del').classList.remove('hide');
                document.getElementById('del-category-cont').classList.add('hide');
                document.getElementById('del-category-cont').classList.remove('show');
            } else {
                console.error('Failed to delete', data);
                alert(data.message || 'Failed to delete category');
            }

        } catch (error) {
            console.error('Error', error);
            alert('An error occurred while deleting the category.');
        }
    });

    document.getElementById('cat-blur-del').addEventListener('click', function (event) {
        event.stopPropagation();
        location.reload();
    });

    document.getElementById('category-add-blur').addEventListener('click', function (event) {
        event.stopPropagation();
        document.getElementById('edit-category-cont').classList.remove('show');
        document.getElementById('edit-category-cont').classList.add('hide');
    });

    document.getElementById('category-add-blur').addEventListener('click', function (event) {
        event.stopPropagation();

        let detailsCatModal = document.getElementById('category-container');
        let blur = document.getElementById('category-add-blur');

        if (detailsCatModal.classList.contains('show') && blur.classList.contains('show')) {
            detailsCatModal.classList.remove('show');
            blur.classList.remove('show');
            detailsCatModal.classList.add('hide');
            blur.classList.add('hide');
        }
    });

    document.getElementById('edit-category-btn').addEventListener('click', async function () {
        const editCatId = document.getElementById('edit-category-id').value;
        const editCatName = document.getElementById('edit-category-name').value;
        const editCatDescription = document.getElementById('edit-category-description').value;
        let updateModal = document.getElementById('success-del-cat');
        let notifUpdate = document.getElementById('notif-update');
        let editBtn = document.getElementById('edit-category-btn');
        let blur = document.getElementById('cat-blur-del');
        let editModal = document.getElementById('edit-category-cont');
        let editAction = document.getElementById('category-container');

        notifUpdate.textContent = "";
        editBtn.disabled = true;

        const editCategoryData = {
            CategoryId: editCatId,
            CategoryName: editCatName,
            Description: editCatDescription
        };

        try {
            const response = await fetch('/Expense/EditCategory', {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(editCategoryData)
            });

            const data = await response.json();

            if (response.ok) {
                updateModal.classList.add('show');
                updateModal.classList.remove('hide');
                blur.classList.add('show');
                blur.classList.remove('hide');
                editModal.classList.add('hide');
                editModal.classList.remove('show');
                editAction.classList.add('hide');
                editAction.classList.remove('show');
                notifUpdate.textContent = "Category updated successfully.";
                editBtn.disabled = false;
                

            } else {
                console.error('Failed to update category:', data);
                alert(data.message || 'Failed to update category.');
                editBtn.disabled = false;
            }

        } catch (error) {
            console.error('Error:', error);
            alert('An error occurred while updating the category.');
            editBtn.disabled = false;
        }
    });

});

