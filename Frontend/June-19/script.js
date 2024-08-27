document.addEventListener('DOMContentLoaded', () => {
    const homeLink = document.getElementById('homeLink');
    const quotesLink = document.getElementById('quotesLink');
    const homePage = document.getElementById('home');
    const quotesPage = document.getElementById('quotes');
    const quotesContainer = document.getElementById('quotesContainer');
    const prevPageBtn = document.getElementById('prevPage');
    const nextPageBtn = document.getElementById('nextPage');
    const pageInfo = document.getElementById('pageInfo');
    const authorSearchInput = document.getElementById('authorSearchInput');
    const authorSearchBtn = document.getElementById('authorSearchBtn');

    let currentPage = 1;
    const quotesPerPage = 5;
    let currentQuotes = []; 

    homeLink.addEventListener('click', () => {
        homePage.style.display = 'block';
        quotesPage.style.display = 'none';
    });

    quotesLink.addEventListener('click', () => {
        homePage.style.display = 'none';
        quotesPage.style.display = 'block';
        fetchQuotes();
    });

    prevPageBtn.addEventListener('click', () => {
        if (currentPage > 1) {
            currentPage--;
            fetchQuotes();
        }
    });

    nextPageBtn.addEventListener('click', () => {
        currentPage++;
        fetchQuotes();
    });

    authorSearchBtn.addEventListener('click', () => {
        filterByAuthor(authorSearchInput.value.trim());
    });

    function fetchQuotes() {
        fetch(`https://dummyjson.com/quotes?limit=${quotesPerPage}&skip=${(currentPage - 1) * quotesPerPage}`)
            .then(response => response.json())
            .then(data => {
                currentQuotes = data.quotes; 
                displayQuotes(currentQuotes);
                updatePagination(data.total);
            })
            .catch(error => console.error('Error fetching quotes:', error));
    }

    function displayQuotes(quotes) {
        quotesContainer.innerHTML = '';
        quotes.forEach(quote => {
            const quoteElement = document.createElement('div');
            quoteElement.className = 'quote col-md-8 offset-md-2';
            quoteElement.innerHTML = `
                <p>"${quote.quote}"</p>
                <small>- ${quote.author}</small>
            `;
            quotesContainer.appendChild(quoteElement);
        });
    }

    function updatePagination(totalQuotes) {
        const totalPages = Math.ceil(totalQuotes / quotesPerPage);
        pageInfo.textContent = `Page ${currentPage} of ${totalPages}`;
        prevPageBtn.disabled = currentPage === 1;
        nextPageBtn.disabled = currentPage === totalPages;
    }

    function filterByAuthor(authorName) {
        if (!authorName) {
            displayQuotes(currentQuotes); 
            return;
        }
        const filteredQuotes = currentQuotes.filter(quote => quote.author.toLowerCase().includes(authorName.toLowerCase()));
        displayQuotes(filteredQuotes);
    }
});
