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

        const categoryData = {
            ColorPick: categoryColor,
            CategoryName: categoryName,
            Description: categoryDescription
        };

        if (!categoryName || !categoryDescription) {
            alert('All fields are required.');
            return;
        }

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

                if (successAdded.classList.contains('hide')) {
                    successAdded.classList.remove('hide');
                    successAdded.classList.add('show');
                    document.getElementById('cat-confirmation-modal').classList.remove('show');
                    document.getElementById('cat-confirmation-modal').classList.add('hide');
                    document.getElementById('add-category-cont').classList.remove('show');
                    document.getElementById('add-category-cont').classList.add('hide');
                }
            } else {
                alert('Error: ' + data.message);
            }

        } catch (error) {
            console.error('Error:', error);
            alert('Something went wrong. Please try again later.');
        }
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

            document.getElementById('category-id').value = categoryId;
            document.getElementById('category-name').textContent = categoryName;
            document.getElementById('category-description').textContent = description;

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

        try {
            const response = await fetch(`/Expense/DeleteCategory/${categoryId}`, {
                method: 'DELETE',
                headers: {
                    'Content-Type': 'application/json'
                }
            });

            const data = await response.json();

            if (response.ok) {
                console.log(data.message);
                alert('Category deleted successfully');
                location.reload();
            } else {
                console.error('Failed to delete', data);
                alert(data.message || 'Failed to delete category');
            }

        } catch (error) {
            console.error('Error', error);
            alert('An error occurred while deleting the category.');
        }
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
                console.log(data.message);
                alert('Category updated successfully.');
                location.reload();

            } else {
                console.error('Failed to update category:', data);
                alert(data.message || 'Failed to update category.');
            }

        } catch (error) {
            console.error('Error:', error);
            alert('An error occurred while updating the category.');
        }
    });

});