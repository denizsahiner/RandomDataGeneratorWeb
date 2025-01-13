document.addEventListener('DOMContentLoaded', () => {
    const fieldList = document.querySelector('.field-list');
    const addFieldButton = document.getElementById('addField');
    const generateDataButton = document.getElementById('generateData');
    const tableBody = document.querySelector('#generatedDataTable tbody');
    const exportButton = document.getElementById('downloadData');
    const exportFormatSelect = document.getElementById('selectFormat');
 
    const addField = () => {
        const newField = document.createElement('div');
        newField.className = 'field-container';
        newField.innerHTML = `
            <input type="text" placeholder="Field Name" />
            <select class="type-dropdown">
                <option value="String">String</option>
                <option value="Number">Number</option>
                <option value="Boolean">Boolean</option>
                <option value="Date">Date</option>
                <option value="firstName">First Name</option>
                <option value="middleName">Middle Name</option>
                <option value="lastName">Surname</option>
                <option value="GUID">GUID</option>
                <option value="gender">Gender</option>
                <option value="ssn">Social Security Number</option>
                <option value="salary">Salary</option>
            </select>
            <button class="deleteField" title="Delete Field">&times;</button>
        `;
        fieldList.appendChild(newField);
    };

    const deleteField = (e) => {
        if (e.target.classList.contains('deleteField')) {
            e.target.parentElement.remove();
        }
    };

    const generateData = () => {
        const fields = collectFields();
        const rowCount = parseInt(document.getElementById('intInput').value, 10);

        if (!validateInputs(fields, rowCount)) return;

        fetch(`/Home/GenerateData?count=${rowCount}`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(fields),
        })
            .then(response => {
                if (!response.ok) {
                    throw new Error('Network response was not ok');
                }
                return response.json();
            })
            .then(data => {
                updateTable(data);
            })
            .catch(error => {
                console.error('Error:', error);
                alert('An error occurred while generating data.');
            });
    };

    const collectFields = () => {
        const fields = [];
        document.querySelectorAll('.field-container').forEach(container => {
            const fieldName = container.querySelector('input').value.trim();
            const fieldType = container.querySelector('select').value;
            if (fieldName) {
                fields.push({ name: fieldName, type: fieldType });
            }
        });
        return fields;
    };

    const validateInputs = (fields, rowCount) => {
        if (fields.length === 0) {
            alert('Please add at least one field.');
            return false;
        }

        if (isNaN(rowCount) || rowCount <= 0) {
            alert('Please enter a valid number of rows.');
            return false;
        }

        return true;
    };

    const updateTable = (data) => {
        const table = document.getElementById('generatedDataTable');
        const tbody = table.querySelector('tbody');
        const thead = table.querySelector('thead');

        tbody.innerHTML = '';
        thead.innerHTML = '';

        if (data.length > 0) {
            const headerRow = document.createElement("tr");
            Object.keys(data[0]).forEach(key => {
                const th = document.createElement("th");
                th.innerText = capitalizeFirstLetter(key);
                headerRow.appendChild(th);
            });
            thead.appendChild(headerRow);
        }

        data.forEach(item => {
            const tr = document.createElement("tr");
            Object.values(item).forEach(value => {
                const td = document.createElement("td");
                td.innerText = value;
                tr.appendChild(td);
            });
            tbody.appendChild(tr);
        });
    };

    const capitalizeFirstLetter = (string) => {
        return string.charAt(0).toUpperCase() + string.slice(1);
    };

    const downloadData = () => {
        const format = exportFormatSelect.value;

        fetch('/Home/DownloadData', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                format: format, 
            })
        })
            .then(response => response.blob())
            .then(blob => {
                const url = window.URL.createObjectURL(blob);
                const a = document.createElement('a');
                a.href = url;
                a.download = `generated_data.${format.toLowerCase()}`;  
                a.click();
                window.URL.revokeObjectURL(url);
            })
            .catch(error => {
                console.error('Download failed:', error);
                alert('An error occurred while downloading the data.');
            });
    };

    addFieldButton.addEventListener('click', addField);
    fieldList.addEventListener('click', deleteField);
    generateDataButton.addEventListener('click', generateData);
    exportButton.addEventListener('click', downloadData);

});
