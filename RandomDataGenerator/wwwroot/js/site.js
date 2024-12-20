// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


document.getElementById('generateData').addEventListener('click', () => {

    const fields = [];
    document.querySelectorAll('.field-container').forEach(container => {
        const fieldName = container.querySelector('input').value.trim();
        const fieldType = container.querySelector('select').value;

        if (fieldName) {
            fields.push({
                name: fieldName,
                type: fieldType
            });
        }
    });

    if (fields.length > 0) {
        fetch('/Home/GenerateData', {
            method: 'POST',
            header: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(fields)
        })
            .then(response => response.json())
            .then(data => {
                console.log('Generated Data:', data);
                alert('Data generated Succesfully!');
            })
            .catch(error => {
                console.error('Error:', error);
            });
    } else {
        alert('Please add at least one field.');
    }
});

