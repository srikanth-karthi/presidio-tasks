(function() {
    fetch('https://dummyjson.com/products')
    .then(res => res.json())
    .then(data => {
        const tableBody = document.getElementById('product-table-body');
        data.products.forEach(product => {
            const row = document.createElement('tr');
            row.innerHTML = `
                <td>${product.title}</td>
                <td><img src="${product.thumbnail}" height="90" width="90" alt=""></td>
                <td>${product.description}</td>
                <td>${product.category}</td>
                <td>${product.price}</td>
                <td class="rating">${product.rating}</td>
                <td>${product.stock}</td>
                <td>${product.brand}</td>
            `;
            tableBody.appendChild(row);

        
            const ratingCell = row.querySelector('.rating');
            if (product.rating >= 4.5) {
                ratingCell.style.color = 'green';
            } else if (product.rating >= 3.5) {
                ratingCell.style.color = 'orange';
            } else {
                ratingCell.style.color = 'red';
            }
        });
    })
    .catch(error => console.error('Error fetching data:', error));
})();
