document.getElementById('cross').style.display = 'none';

document.getElementById('menuButton').addEventListener('click', function() {
    document.getElementById('sidebar').classList.toggle('hidden');
    document.getElementById('company-logo').style.display = 'none';
    document.getElementById('cross').style.display = 'block';
    document.querySelector('.main-content').classList.toggle('expanded');
});

document.getElementById('cross').addEventListener('click', function() {
    const sidebar = document.getElementById('sidebar');
    const companyLogo = document.getElementById('company-logo');
    const crossButton = document.getElementById('cross');
    const mainContent = document.querySelector('.main-content');

    sidebar.classList.toggle('hidden');
    mainContent.classList.toggle('expanded');

    if (companyLogo.style.display === 'none' || companyLogo.style.display === '') {
        companyLogo.style.display = 'block';
        crossButton.style.display = 'none';
    } else {
        companyLogo.style.display = 'none';
        crossButton.style.display = 'block';
    }
});
