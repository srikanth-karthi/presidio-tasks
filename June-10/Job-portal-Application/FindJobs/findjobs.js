document.addEventListener("DOMContentLoaded", function() {

  document.getElementById('cross').style.display = 'none';
  document.getElementById('sidebar').classList.toggle('hidden');
  document.getElementById('company-logo').style.display = 'none';
  document.getElementById('cross').style.display = 'block';
  document.getElementById('menuButton').style.display = 'block';
  document.querySelector('.main-content').classList.toggle('expanded');
  
  document.getElementById('menuButton').addEventListener('click', function() {
      document.getElementById('sidebar').classList.toggle('hidden');
      document.getElementById('company-logo').style.display = 'none';
      document.getElementById('cross').style.display = 'block';
      document.querySelector('.main-content').classList.toggle('expanded');
      document.getElementById('menuButton').style.display = 'none';
  
  });
  
  document.getElementById('cross').addEventListener('click', function() {
      const sidebar = document.getElementById('sidebar');
  document.getElementById('menuButton').style.display = 'block';
  
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

  // Slider value update logic
  const salaryRangeMin = document.getElementById("salary-range-min");
  const salaryRangeMax = document.getElementById("salary-range-max");
  const rangeValues = document.getElementById("range-values");

  function updateSliderValue() {
    rangeValues.innerText = `${salaryRangeMin.value} LPA - ${salaryRangeMax.value} LPA`;
  }

  salaryRangeMin.addEventListener('input', updateSliderValue);
  salaryRangeMax.addEventListener('input', updateSliderValue);

  // Datalist population logic
  const jobTitles = [
    { titleId: "1", titleName: "Developer" },
    { titleId: "2", titleName: "Designer" }
  ];
  const companies = [
    { companyId: "1", companyName: "Google" },
    { companyId: "2", companyName: "Microsoft" }
  ];

  function populateDatalist(datalistId, options, displayKey) {
    const datalist = document.getElementById(datalistId);
    options.forEach(option => {
      const optionElement = document.createElement('option');
      optionElement.value = option[displayKey];
      datalist.appendChild(optionElement);
    });
  }

  populateDatalist('title-list', jobTitles, 'titleName');
  populateDatalist('company-list', companies, 'companyName');

  // Input selection handling
  function handleInputSelection(inputId, options, displayKey, valueKey) {
    const inputElement = document.getElementById(inputId);
    inputElement.addEventListener('input', function() {
      const selectedValue = inputElement.value;
      const selectedOption = options.find(option => option[displayKey] === selectedValue);
      if (selectedOption) {
        console.log(`Selected ${inputId}:`, selectedOption[valueKey]);
        // Handle the selected ID as needed
      }
    });
  }
  const toggleButtons = document.querySelectorAll('.toggle-button');
  toggleButtons.forEach(button => {
    button.addEventListener('click', function() {
      const content = this.nextElementSibling;
      content.classList.toggle('hidden');
    
    });
  
  })
  handleInputSelection('title-input', jobTitles, 'titleName', 'titleId');
  handleInputSelection('company-input', companies, 'companyName', 'companyId');
  const jobItems = [
    {
        logo: 'https://via.placeholder.com/40', // Placeholder image URL
        title: 'Social Media Assistant',
        company: 'Nomad',
        location: 'Paris, France',
        jobtype: 'FullTime',
        applied: 5,
        capacity: 10
    },
    {
        logo: 'https://via.placeholder.com/40', // Placeholder image URL
        title: 'Brand Designer',
        company: 'Dropbox',
        location: 'San Francisco, USA',
        jobtype: 'PartTime',
        applied: 2,
        capacity: 10
    },
    {
        logo: 'https://via.placeholder.com/40', // Placeholder image URL
        title: 'Interactive Developer',
        company: 'Terraform',
        location: 'Hamburg, Germany',
        jobtype: 'Hybrid',
        applied: 8,
        capacity: 12
    },
    {
        logo: 'https://via.placeholder.com/40', // Placeholder image URL
        title: 'Email Marketing',
        company: 'Revolut',
        location: 'Madrid, Spain',
        jobtype:'Internship',
        applied: 0,
        capacity: 10
    },
    
    {
        logo: 'https://via.placeholder.com/40', // Placeholder image URL
        title: 'Product Designer',
        company: 'ClassPass',
        location: 'Berlin, Germany',
        jobtype:'Freelance',
        applied: 5,
        capacity: 10
    },
    {
        logo: '../assets/', // Placeholder image URL
        title: 'Customer Manager',
        company: 'Pitch',
        location: 'Berlin, Germany',
        jobtype:'Freelance',
        applied: 5,
        capacity: 10
    }, {
      logo: 'https://via.placeholder.com/40', // Placeholder image URL
      title: 'Social Media Assistant',
      company: 'Nomad',
      location: 'Paris, France',
      jobtype: 'FullTime',
      applied: 5,
      capacity: 10
  },
  {
      logo: 'https://via.placeholder.com/40', // Placeholder image URL
      title: 'Brand Designer',
      company: 'Dropbox',
      location: 'San Francisco, USA',
      jobtype: 'PartTime',
      applied: 2,
      capacity: 10
  },
  {
      logo: 'https://via.placeholder.com/40', // Placeholder image URL
      title: 'Interactive Developer',
      company: 'Terraform',
      location: 'Hamburg, Germany',
      jobtype: 'Hybrid',
      applied: 8,
      capacity: 12
  },
  {
      logo: 'https://via.placeholder.com/40', // Placeholder image URL
      title: 'Email Marketing',
      company: 'Revolut',
      location: 'Madrid, Spain',
      jobtype:'Internship',
      applied: 0,
      capacity: 10
  },
  
  {
      logo: 'https://via.placeholder.com/40', // Placeholder image URL
      title: 'Product Designer',
      company: 'ClassPass',
      location: 'Berlin, Germany',
      jobtype:'Freelance',
      applied: 5,
      capacity: 10
  },
  {
      logo: '../assets/', // Placeholder image URL
      title: 'Customer Manager',
      company: 'Pitch',
      location: 'Berlin, Germany',
      jobtype:'Freelance',
      applied: 5,
      capacity: 10
  }, {
    logo: 'https://via.placeholder.com/40', // Placeholder image URL
    title: 'Social Media Assistant',
    company: 'Nomad',
    location: 'Paris, France',
    jobtype: 'FullTime',
    applied: 5,
    capacity: 10
},
{
    logo: 'https://via.placeholder.com/40', // Placeholder image URL
    title: 'Brand Designer',
    company: 'Dropbox',
    location: 'San Francisco, USA',
    jobtype: 'PartTime',
    applied: 2,
    capacity: 10
},
{
    logo: 'https://via.placeholder.com/40', // Placeholder image URL
    title: 'Interactive Developer',
    company: 'Terraform',
    location: 'Hamburg, Germany',
    jobtype: 'Hybrid',
    applied: 8,
    capacity: 12
},
{
    logo: 'https://via.placeholder.com/40', // Placeholder image URL
    title: 'Email Marketing',
    company: 'Revolut',
    location: 'Madrid, Spain',
    jobtype:'Internship',
    applied: 0,
    capacity: 10
},

{
    logo: 'https://via.placeholder.com/40', // Placeholder image URL
    title: 'Product Designer',
    company: 'ClassPass',
    location: 'Berlin, Germany',
    jobtype:'Freelance',
    applied: 5,
    capacity: 10
},
{
    logo: '../assets/', // Placeholder image URL
    title: 'Customer Manager',
    company: 'Pitch',
    location: 'Berlin, Germany',
    jobtype:'Freelance',
    applied: 5,
    capacity: 10
}, {
  logo: 'https://via.placeholder.com/40', // Placeholder image URL
  title: 'Social Media Assistant',
  company: 'Nomad',
  location: 'Paris, France',
  jobtype: 'FullTime',
  applied: 5,
  capacity: 10
},
{
  logo: 'https://via.placeholder.com/40', // Placeholder image URL
  title: 'Brand Designer',
  company: 'Dropbox',
  location: 'San Francisco, USA',
  jobtype: 'PartTime',
  applied: 2,
  capacity: 10
},
{
  logo: 'https://via.placeholder.com/40', // Placeholder image URL
  title: 'Interactive Developer',
  company: 'Terraform',
  location: 'Hamburg, Germany',
  jobtype: 'Hybrid',
  applied: 8,
  capacity: 12
},
{
  logo: 'https://via.placeholder.com/40', // Placeholder image URL
  title: 'Email Marketing',
  company: 'Revolut',
  location: 'Madrid, Spain',
  jobtype:'Internship',
  applied: 0,
  capacity: 10
},

{
  logo: 'https://via.placeholder.com/40', // Placeholder image URL
  title: 'Product Designer',
  company: 'ClassPass',
  location: 'Berlin, Germany',
  jobtype:'Freelance',
  applied: 5,
  capacity: 10
},
{
  logo: '../assets/', // Placeholder image URL
  title: 'Customer Manager',
  company: 'Pitch',
  location: 'Berlin, Germany',
  jobtype:'Freelance',
  applied: 5,
  capacity: 10
}, {
  logo: 'https://via.placeholder.com/40', // Placeholder image URL
  title: 'Social Media Assistant',
  company: 'Nomad',
  location: 'Paris, France',
  jobtype: 'FullTime',
  applied: 5,
  capacity: 10
},
{
  logo: 'https://via.placeholder.com/40', // Placeholder image URL
  title: 'Brand Designer',
  company: 'Dropbox',
  location: 'San Francisco, USA',
  jobtype: 'PartTime',
  applied: 2,
  capacity: 10
},
{
  logo: 'https://via.placeholder.com/40', // Placeholder image URL
  title: 'Interactive Developer',
  company: 'Terraform',
  location: 'Hamburg, Germany',
  jobtype: 'Hybrid',
  applied: 8,
  capacity: 12
},
{
  logo: 'https://via.placeholder.com/40', // Placeholder image URL
  title: 'Email Marketing',
  company: 'Revolut',
  location: 'Madrid, Spain',
  jobtype:'Internship',
  applied: 0,
  capacity: 10
},

{
  logo: 'https://via.placeholder.com/40', // Placeholder image URL
  title: 'Product Designer',
  company: 'ClassPass',
  location: 'Berlin, Germany',
  jobtype:'Freelance',
  applied: 5,
  capacity: 10
},
{
  logo: '../assets/', // Placeholder image URL
  title: 'Customer Manager',
  company: 'Pitch',
  location: 'Berlin, Germany',
  jobtype:'Freelance',
  applied: 5,
  capacity: 10
}, {
  logo: 'https://via.placeholder.com/40', // Placeholder image URL
  title: 'Social Media Assistant',
  company: 'Nomad',
  location: 'Paris, France',
  jobtype: 'FullTime',
  applied: 5,
  capacity: 10
},
{
  logo: 'https://via.placeholder.com/40', // Placeholder image URL
  title: 'Brand Designer',
  company: 'Dropbox',
  location: 'San Francisco, USA',
  jobtype: 'PartTime',
  applied: 2,
  capacity: 10
},
{
  logo: 'https://via.placeholder.com/40', // Placeholder image URL
  title: 'Interactive Developer',
  company: 'Terraform',
  location: 'Hamburg, Germany',
  jobtype: 'Hybrid',
  applied: 8,
  capacity: 12
},
{
  logo: 'https://via.placeholder.com/40', // Placeholder image URL
  title: 'Email Marketing',
  company: 'Revolut',
  location: 'Madrid, Spain',
  jobtype:'Internship',
  applied: 0,
  capacity: 10
},

{
  logo: 'https://via.placeholder.com/40', // Placeholder image URL
  title: 'Product Designer',
  company: 'ClassPass',
  location: 'Berlin, Germany',
  jobtype:'Freelance',
  applied: 5,
  capacity: 10
},
{
  logo: '../assets/', // Placeholder image URL
  title: 'Customer Manager',
  company: 'Pitch',
  location: 'Berlin, Germany',
  jobtype:'Freelance',
  applied: 5,
  capacity: 10
}
  ];

  // Job listing rendering logic
  const jobList = document.getElementById('job-list');
  const listViewButton = document.getElementById('list-view-button');
  const gridViewButton = document.getElementById('grid-view-button');
  const itemsPerPage = 6;
  let currentPage = 1;
  const totalItems = jobItems.length;
  const totalPages = Math.ceil(totalItems / itemsPerPage);

  function renderTable(pageNumber,itemsPerPage) {
    const startIndex = (pageNumber - 1) * itemsPerPage;
    const endIndex = startIndex + itemsPerPage;
    jobList.innerHTML = ''; // Clear existing job items
    for (let i = startIndex; i < endIndex; i++) {
      if (i >= jobItems.length) break; // Prevent index out of bounds
      const jobItem = jobItems[i];
      const jobItemDiv = document.createElement('div');
      jobItemDiv.classList.add('job-item');
      jobItemDiv.innerHTML = `
        <img src="../assets/dropbox.svg" width="90" alt="${jobItem.title}">
        <div class="company-details">
          <h3>${jobItem.title}</h3>
          <p>${jobItem.company} • ${jobItem.location}</p>
          <span class="meta-tags ${getJobTypeClass(jobItem.jobtype)}">${jobItem.jobtype}</span>
        </div>
        <button class="apply-button">Apply</button>
      `;
      jobList.appendChild(jobItemDiv);
    }
  }

  function getJobTypeClass(status) {
    switch (status) {
      case "FullTime":
        return "FullTime";
      case "PartTime":
        return "PartTime";
      case "Hybrid":
        return "Hybrid";
      case "Internship":
        return "Internship";
      case "Freelance":
        return "Freelance";
      default:
        return "FullTime";
    }
  }

  function renderPagination() {
    const pagination = document.getElementById("pagination");
    pagination.innerHTML = "";
    const createButton = (text, isActive = false, isDisabled = false) => {
      const button = document.createElement("button");
      button.textContent = text;
      if (isActive) {
        button.classList.add("active");
      }
      if (isDisabled) {
        button.disabled = true;
      }
      button.addEventListener("click", function() {
        if (!isDisabled) {
          currentPage = parseInt(text) || currentPage + (text === '>'? 1 : -1);
          renderTable(currentPage,itemsPerPage);
          renderPagination();
        }
      });
      return button;
    };
    pagination.appendChild(createButton("<", false, currentPage === 1));
    if (totalPages <= 7) {
      for (let i = 1; i <= totalPages; i++) {
        pagination.appendChild(createButton(i, i === currentPage));
      }
    } else {
      pagination.appendChild(createButton(1, 1 === currentPage));
      if (currentPage > 4) pagination.appendChild(createButton("..."));
      const startPage = Math.max(2, currentPage - 2);
      const endPage = Math.min(totalPages - 1, currentPage + 2);
      for (let i = startPage; i <= endPage; i++) {
        pagination.appendChild(createButton(i, i === currentPage));
      }
      if (currentPage < totalPages - 3) pagination.appendChild(createButton("..."));
      pagination.appendChild(createButton(totalPages, currentPage === totalPages));
    }
    pagination.appendChild(createButton(">", false, currentPage === totalPages));
  }

  listViewButton.addEventListener('click', () => {
    renderTable(currentPage,itemsPerPage);
    jobList.classList.remove('grid-view');
    jobList.classList.add('list-view');
    listViewButton.classList.add('active');
    gridViewButton.classList.remove('active');
    document.querySelectorAll('.apply-button').forEach(btn => btn.style.display = 'block');
    document.getElementById("list-view-img").src="../assets/list-active.svg";
    document.getElementById("grid-view-img").src="../assets/grid.svg";
  });

  gridViewButton.addEventListener('click', () => {
    renderTable(currentPage,itemsPerPage*2);
    jobList.classList.remove('list-view');
    jobList.classList.add('grid-view');
    gridViewButton.classList.add('active');
    listViewButton.classList.remove('active');
    document.querySelectorAll('.apply-button').forEach(btn => btn.style.display = 'none');
    document.getElementById("list-view-img").src="../assets/list.svg";
    document.getElementById("grid-view-img").src="../assets/grid-active.svg";
  });

  renderTable(currentPage,itemsPerPage);
  renderPagination();
});