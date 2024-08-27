import { showToast } from "../Package/toaster.js";
import { fetchData } from "../Package/api.js";




document.addEventListener("DOMContentLoaded", async function () {
  if(!localStorage.getItem('authToken'))
    {
      window.location.href = "/Auth/login.html?authid=3";

    }
    
  document.getElementById("cross").style.display = "none";
  document.getElementById("sidebar").classList.toggle("hidden");
  document.getElementById("company-logo").style.display = "none";
  document.getElementById("cross").style.display = "block";
  document.getElementById("menuButton").style.display = "block";
  document.querySelector(".main-content").classList.toggle("expanded");

  document.getElementById("menuButton").addEventListener("click", function () {
    document.getElementById("sidebar").classList.toggle("hidden");
    document.getElementById("sidebar").style.transform = "translateX(1px)";
    document.getElementById("company-logo").style.display = "none";
    document.getElementById("cross").style.display = "block";
    document.querySelector(".main-content").classList.toggle("expanded");
    document.getElementById("menuButton").style.display = "none";
  });

  document.getElementById("cross").addEventListener("click", function () {
    const sidebar = document.getElementById("sidebar");
    document.getElementById("menuButton").style.display = "block";

    const companyLogo = document.getElementById("company-logo");
    const crossButton = document.getElementById("cross");
    const mainContent = document.querySelector(".main-content");

    sidebar.classList.toggle("hidden");
    mainContent.classList.toggle("expanded");

    if (
      companyLogo.style.display === "none" ||
      companyLogo.style.display === ""
    ) {
      companyLogo.style.display = "block";
      crossButton.style.display = "none";
    } else {
      companyLogo.style.display = "none";
      crossButton.style.display = "block";
    }
  });

  $(function () {
    $("#title-list").select2();
    $("#company-list").select2();
  });

  const profileData = JSON.parse(localStorage.getItem("profile"));


  if (profileData) {
    const profileElement = document.querySelector(".profile");
  
  
    if (profileElement) {
      profileElement.innerHTML = `
    <img src="${profileData.profileUrl?profileData.profileUrl :'../assets/profile.png' }" width="60" height="60" alt="" />
        <div>
          <p>${profileData.name}</p>
    
        </div>
      `;
    }
  
    const nameElement = document.querySelector('.name');
  
  
    if (nameElement) {
      nameElement.textContent = profileData.name;
    }
  } else {
    console.error("Profile data not found in localStorage.");
  }
try{
  const companies = await fetchData("api/Company");

  const jobTitles = await fetchData("api/Title");
  populateData("title-list", jobTitles, "titleName", "titleId");

  populateData("company-list", companies, "companyName", "companyId");
}
catch
{

}

  function populateData(
    selectId,
    options,
    displayKey,
    valueKey,
    selectedValues = []
  ) {
    const select = document.getElementById(selectId);
    options.forEach((option) => {
      const optionElement = document.createElement("option");
      optionElement.value = option[valueKey];
      optionElement.textContent = option[displayKey];
      if (
        selectedValues.some(
          (selected) => selected[valueKey] === option[valueKey]
        )
      ) {
        optionElement.selected = true;
      }
      select.appendChild(optionElement);
    });
  }


  const elements = document.querySelectorAll(
    ".select2-container--default .select2-selection--single"
  );

  elements.forEach((element) => {
    element.style.backgroundColor = "#fff";
    element.style.border = "none";
    element.style.borderBottom = "2px solid rgba(214, 221, 235, 1)";
    element.style.borderRadius = "4px";
    element.style.transition = "border-bottom-color 0.3s";
    element.style.marginBottom = "8px";
    element.style.width = "100%";
  });

  const selectElements = document.querySelectorAll(
    ".select2.select2-container.select2-container--default"
  );

  selectElements.forEach((element) => {
    element.style.width = "100%";
  });
  const toggleButtons = document.querySelectorAll(".toggle-button");
  toggleButtons.forEach((button) => {
    button.addEventListener("click", function () {
      const content = this.nextElementSibling;
      content.classList.toggle("hidden");
    });
  });
  let maxpagereached=false
  async function GetJobs(pageNumber, pageSize) {
    maxpagereached=false
    try {
      return await fetchData(
        `api/User/recommended-jobs?pageNumber=${pageNumber}&pageSize=${pageSize}`
      );
    } catch (error) {
      if ( error.message.includes("404")) {
        maxpagereached=true
      } 
      return [];
    }
  }

  var jobItems = await GetJobs(1, 24);

  const jobList = document.getElementById("job-list");
  const listViewButton = document.getElementById("list-view-button");
  const gridViewButton = document.getElementById("grid-view-button");
  const itemsPerPage = 6;
  let currentPage = 1;
  let currentGridPage = 1;

  let totalPages = jobItems.length > itemsPerPage ? Math.ceil(jobItems.length / itemsPerPage) : 1;
  let gridview = false;
  let isfiltered = false;
  
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
  
  async function renderTable(pageNumber, itemsPerPage, gridview, isfiltered) {
    if (gridview) itemsPerPage = itemsPerPage * 2;
    const startIndex = (pageNumber - 1) * itemsPerPage;
    let endIndex = startIndex + itemsPerPage;
    if (endIndex > jobItems.length-6) {

      let additionalData;
      if(!maxpagereached)
        {
      if (isfiltered) {
        additionalData = await updateSearchCriteria(1+Math.floor( jobItems.length/24), 24);
        jobItems = jobItems.concat(additionalData);
      } else {
        additionalData = await GetJobs(1+Math.floor( jobItems.length/24), 24);
        jobItems = jobItems.concat(additionalData);
      }

    }
 


    }

    endIndex = Math.min(endIndex, jobItems.length);
  
    jobList.innerHTML = "";
    for (let i = startIndex; i < endIndex; i++) {
      if (i >= jobItems.length) break;
      const jobItem = jobItems[i];
      const jobItemDiv = document.createElement("div");
      jobItemDiv.classList.add("job-item");
      jobItem.companylogo = jobItem.companylogo ? jobItem.companylogo : "../assets/Company.png";
      const button = gridview ? "" : `<button class="apply-button" onclick="applyToJob('${jobItem.jobId}')">Apply</button>`;
      jobItemDiv.innerHTML = `
        <img src="${jobItem.companylogo}" width="90" alt="${jobItem.titleName}">
        <div class="company-details">
          <h3>${jobItem.titleName}</h3>
          <p>${jobItem.companyName} • ${jobItem.experienceRequired} yrs Experience</p>
          <span class="meta-tags ${getJobTypeClass(jobItem.jobType)}">${jobItem.jobType}</span>
        </div>
        ${button}`;
      jobList.appendChild(jobItemDiv);
    }
    totalPages = jobItems.length > itemsPerPage ? Math.ceil(jobItems.length / itemsPerPage) : 1;

    renderPagination(totalPages);
  }
  
  function renderPagination(totalPages) {
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
      button.addEventListener("click", function () {
        if (!isDisabled) {
          console.log(currentPage,totalPages)
    
currentPage = parseInt(text) || currentPage + (text === ">" ? 1 : -1);
          renderTable(currentPage, itemsPerPage, gridview, isfiltered);
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
  
  listViewButton.addEventListener("click", () => {
    gridview = false;
    currentPage = 1;
    currentGridPage = 1;
    renderTable(currentPage, itemsPerPage, gridview, isfiltered);
    jobList.classList.remove("grid-view");
    jobList.classList.add("list-view");
    listViewButton.classList.add("active");
    gridViewButton.classList.remove("active");
    document.querySelectorAll(".apply-button").forEach((btn) => (btn.style.display = "block"));
    document.getElementById("list-view-img").src = "../assets/list-active.svg";
    document.getElementById("grid-view-img").src = "../assets/grid.svg";
  });
  
  gridViewButton.addEventListener("click", () => {
    gridview = true;
    currentPage = 1;
    currentGridPage = 1;
    renderTable(currentPage, itemsPerPage, gridview, isfiltered);
    jobList.classList.remove("list-view");
    jobList.classList.add("grid-view");
    gridViewButton.classList.add("active");
    listViewButton.classList.remove("active");
    document.querySelectorAll(".apply-button").forEach((btn) => (btn.style.display = "none"));
    document.getElementById("list-view-img").src = "../assets/list.svg";
    document.getElementById("grid-view-img").src = "../assets/grid-active.svg";
  });
  
  renderTable(currentPage, itemsPerPage, gridview, isfiltered);
  


  window.applyToJob = async function (jobId) {
    console.log("Applying to job with ID:", jobId);
    try {
      await fetchData(`api/JobActivity/apply?jobId=${jobId}`, "POST");
  
      showToast("success", "Success", "Applied Sucessfully.");
    } catch (error) {
      if (error.message.includes("404")) {
        showToast("error", "Application Failed", "Job not Found.");
      } else if (error.message.includes("409")) {
        showToast("warning", "Warming", "Already Applied.");
      } else {
        showToast("error", "An error occurred", " Please Apply again Later");
      }
    }
  }


  async function updateSearchCriteria(PageNumber,PageSize) {
    document.querySelector(".job-header h1").textContent = "All Jobs";


    const searchCriteria = {};
  
    const selectedTitle = titleDropdown.value;
    if (selectedTitle) {
      searchCriteria.JobTitleId = selectedTitle;
    }
  
    const selectedCompany = companyDropdown.value;
    if (selectedCompany) {
      searchCriteria.CompanyId = selectedCompany;
    }
  
    const enteredLocation = locationInput.value.trim();
    if (enteredLocation !== "") {
      searchCriteria.Location = enteredLocation;
    }

  document.getElementById('sort').value=='recent'?    searchCriteria.RecentlyPosted = true:    searchCriteria.RecentlyPosted = false;

  
    let jobType;
    employmentCheckboxes.forEach((checkbox) => {
      if (checkbox.checked) {
        jobType = checkbox.id;
      }
    });
    if (jobType) {
      searchCriteria.JobType = jobType;
    }
  
    let jobLevel;
    levelCheckboxes.forEach((checkbox) => {
      if (checkbox.checked) {
        jobLevel = checkbox.id;
      }
    });
    if (jobLevel) {
      switch (jobLevel) {
        case "entry-level":
          searchCriteria.MinExperience = 0;
          searchCriteria.MaxExperience = 3;
          break;
        case "mid-level":
          searchCriteria.MinExperience = 3;
          searchCriteria.MaxExperience = 6;
          break;
        case "Mid-Senior-level":
          searchCriteria.MinExperience = 6;
          searchCriteria.MaxExperience = 12;
          break;
        case "senior-level":
          searchCriteria.MinExperience = 12;
          searchCriteria.MaxExperience = null;
   break
      }
    }
  
    let salaryRange;
    salaryCheckboxes.forEach((checkbox) => {
      if (checkbox.checked) {
        salaryRange = checkbox.id;
      }
    });
    if (salaryRange) {
      switch (salaryRange) {
        case "1-5-lpa":
          searchCriteria.MinLpa = 1;
          searchCriteria.MaxLpa = 5;
          break;
        case "5-10-lpa":
          searchCriteria.MinLpa = 5;
          searchCriteria.MaxLpa = 10;
          break;
        case "10-20-lpa":
          searchCriteria.MinLpa = 10;
          searchCriteria.MaxLpa = 20;
          break;
        case "20-plus-lpa":
          searchCriteria.MinLpa = 20;
          searchCriteria.MaxLpa = null;
          break;
      }
    }
  
    searchCriteria.PageNumber = PageNumber;
    searchCriteria.PageSize = PageSize;

    try{
      return   await fetchData('api/Job/search', 'POST', searchCriteria);

      }
      catch(error)
      {
      
        if ( error.message.includes("404")) {
          maxpagereached=true
          return []
        } 
      }

  
  }
  async function GetFilteredData()
  {
    maxpagereached=false
     currentPage = 1;
     currentGridPage = 1;
    isfiltered=true
    jobItems=await updateSearchCriteria(1,24)
    renderTable(currentPage, itemsPerPage,gridview,isfiltered);
  }
  
  employmentCheckboxes.forEach((checkbox) => {
    checkbox.addEventListener("change", GetFilteredData);
  });
  
  levelCheckboxes.forEach((checkbox) => {
    checkbox.addEventListener("change", GetFilteredData);
  });
  
  salaryCheckboxes.forEach((checkbox) => {
    checkbox.addEventListener("change", GetFilteredData);
  });
  searchButton.addEventListener("click", GetFilteredData);
  document.getElementById('sort').addEventListener("change", GetFilteredData);
});




const titleDropdown = document.getElementById("title-list");

const companyDropdown = document.getElementById("company-list");

const locationInput = document.getElementById("location-input");

const searchButton = document.getElementById("search-button");





const employmentCheckboxes = document.querySelectorAll(
  '#employment-filter input[type="checkbox"]'
);
const levelCheckboxes = document.querySelectorAll(
  '#level-filter input[type="checkbox"]'
);
const salaryCheckboxes = document.querySelectorAll(
  '#salary-filter input[type="checkbox"]'
);


function limitCheckboxSelection(checkboxes) {
  checkboxes.forEach((checkbox) => {
    checkbox.addEventListener("change", function () {
      if (this.checked) {
        checkboxes.forEach((cb) => {
          if (cb !== this) {
            cb.checked = false;
          }
        });
      }
    });
  });
}


limitCheckboxSelection(employmentCheckboxes);
limitCheckboxSelection(levelCheckboxes);
limitCheckboxSelection(salaryCheckboxes);



function toggleClassBasedOnScreenSize() {
  const screenWidth = window.innerWidth;

  const toggleButtons = document.querySelectorAll(".toggle-button");

  toggleButtons.forEach((button) => {
    const content = button.nextElementSibling;

    if (screenWidth < 768) {
      content.classList.add("hidden");
    } else {
      content.classList.remove("hidden");
    }
  });
}

window.addEventListener("resize", toggleClassBasedOnScreenSize);
