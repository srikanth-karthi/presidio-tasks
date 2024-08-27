document.getElementById('productForm').addEventListener('submit', function(event) {
    event.preventDefault();
    
    const productId = document.getElementById('Product-id').value;
    const productName = document.getElementById('Product-Name').value;
    const productPrice = document.getElementById('Product-price').value;
    const errorMessage = document.getElementById('error-message');
    
    const tableBody = document.getElementById('productTableBody');
    

    let idExists = false;
    for (let row of tableBody.rows) {
        if (row.cells[0].innerText === productId) {
            idExists = true;
            break;
        }
    }
    
    if (idExists) {
        errorMessage.innerText = "Product ID already exists.";
    } else {
        errorMessage.innerText = "";
        
        const newRow = document.createElement('tr');
        newRow.innerHTML = `
            <td>${productId}</td>
            <td>${productName}</td>
            <td>${productPrice}</td>
        `;
        
        tableBody.appendChild(newRow);
        
        document.getElementById('productForm').reset();  
    }
});
