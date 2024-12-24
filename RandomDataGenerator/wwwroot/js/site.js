document.getElementById('generateData').addEventListener('click', () => {

    const fields = [];

    // Kullanıcıdan alınan alanları al
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

    // Alan varsa veriyi gönder
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

                // Tabloyu temizle
                const table = document.getElementById('generatedDataTable');
                const tbody = table.querySelector('tbody');
                const thead = table.querySelector('thead');

              
                tbody.innerHTML = ''; // Eski veriyi temizle
                thead.innerHTML = ''; // Başlıkları temizle
                

                // Başlıkları oluştur
                const headerRow = document.createElement("tr");
                Object.keys(data[0]).forEach(key => {
                    const th = document.createElement("th");
                    th.innerText = key.charAt(0).toUpperCase() + key.slice(1); // İlk harfi büyük yap
                    headerRow.appendChild(th);
                });
                thead.appendChild(headerRow); // Başlık satırını ekle

                // Veriyi tabloya ekle
                data.forEach((item) => {
                    let tr = document.createElement("tr");

                    let vals = Object.values(item);
                    vals.forEach((elem) => {
                        let td = document.createElement("td");
                        td.innerText = elem;
                        tr.appendChild(td);
                    });
                    tbody.appendChild(tr);
                });
            })
            .catch(error => {
                console.error('Error:', error);
                alert('An error occurred while generating data.');
            });
    } else {
        alert('Please add at least one field.');
    }
});
