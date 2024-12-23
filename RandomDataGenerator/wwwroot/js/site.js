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
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(fields)
        })
            .then(response => response.json())
            .then(data => {
                console.log('Generated Data:', data);
                const table = document.getElementById('generatedDataTable');
                table.querySelector('tbody').innerHTML = ''; // Eski veriyi temizle

                data.forEach(item => {
                    const row = table.querySelector('tbody').insertRow();
                    row.insertCell(0).textContent = item.name;
                    row.insertCell(1).textContent = item.type;
                    row.insertCell(2).textContent = item.generatedValue;
                });
            })
            
    } else {
        alert('Please add at least one field.');
    }
});
