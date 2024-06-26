import { fetchData } from "../../Package/api.js";
import { showToast } from "../../Package/toaster.js";

if (!localStorage.getItem('authToken')) {
    window.location.href = "/Auth/login.html?authid=3";
}

function getQueryParam(param) {
    const urlParams = new URLSearchParams(window.location.search);
    return urlParams.get(param);
}

function removeQueryParam(param) {
    const url = new URL(window.location);
    const urlParams = new URLSearchParams(url.search);
    urlParams.delete(param);
    url.search = urlParams.toString();
    window.history.replaceState({}, document.title, url.toString());
}

const authid = getQueryParam('authid');

if (authid == 1) {
    showToast('success', 'Success', 'Login Successful.');
    removeQueryParam('authid');
}

const profileData = JSON.parse(localStorage.getItem("profile"));

if (profileData) {
    const profileElement = document.querySelector(".profile");

    if (profileElement) {
        profileElement.innerHTML = `
      <img src="${profileData.profileUrl ? profileData.profileUrl : '../assets/profile.png'}" width="60" height="60" alt="" />
          <div>
            <p>${profileData.name}</p>
          </div>
        `;
    }
}

const profileElement = document.querySelector(".profile");
const nameElement = document.querySelector('.name');
const menuButton = document.getElementById('menuButton');
const sidebar = document.getElementById('sidebar');
const companyLogo = document.getElementById('company-logo');
const crossButton = document.getElementById('cross');
const mainContent = document.querySelector('.main-content');

menuButton.addEventListener('click', function () {
    sidebar.classList.toggle('hidden');
    companyLogo.style.display = 'none';
    crossButton.style.display = 'block';
    mainContent.classList.toggle('expanded');
});

crossButton.addEventListener('click', function () {
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

let currentPage = 1;
const itemsPerPage = 6; // Adjust the number of items per page if necessary
const jobListBody = document.getElementById('job-list-body');
const jobList = document.querySelector('.job-list');
const pageNumber = document.getElementById('page-number');
const prevPageButton = document.getElementById('prev-page');
const nextPageButton = document.getElementById('next-page');

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


    async function GetJobs() {
        try {
            return await fetchData(`api/Job`);
        } catch (error) {
            console.log(error);
            return [];
        }
    }

    var jobs = await GetJobs();
    console.log(jobs);


    function renderJobs(page) {
        jobListBody.innerHTML = '';
        const start = (page - 1) * itemsPerPage;
        const end = page * itemsPerPage;
        const paginatedJobs = jobs.slice(start, end);

        paginatedJobs.forEach((job, index) => {
            const row = document.createElement('tr');
            row.innerHTML = `
                <td style="text-align: left">${job.titleName}</td>
                <td class="${job.status ? 'active' : 'inactive'}">${job.status ? 'active' : 'inactive'}</td>
                <td>${formatDate(job.datePosted)}</td>
                <td class="meta-tags ${getJobTypeClass(job.jobType)}">${job.jobType}</td>
                <td>${job.experienceRequired > 0 ? job.experienceRequired : 'not required'}</td>
            `;
            row.addEventListener('click', () => {
                fetchAndDisplayApplications(job);
            });
            jobListBody.appendChild(row);
            setTimeout(() => {
                row.classList.add('visible');
            }, index * 100);
        });
    }

    function updatePageNumber(page) {
        pageNumber.textContent = page;
    }

    prevPageButton.addEventListener('click', () => {
        if (currentPage > 1) {
            currentPage--;
            renderJobs(currentPage);
            updatePageNumber(currentPage);
        }
    });

    nextPageButton.addEventListener('click', () => {
        if (currentPage < Math.ceil(jobs.length / itemsPerPage)) {
            currentPage++;
            renderJobs(currentPage);
            updatePageNumber(currentPage);
        }
    });

    // Initial render
    renderJobs(currentPage);
    updatePageNumber(currentPage);


async function fetchApplications(jobId) {
    try {
        const response = await fetchData(`api/JobActivity/job/${jobId}`);

        return response;
    } catch (error) {
        console.error('Error fetching applications:', error);
        return [];
    }
}

document.querySelector('.back-jobdetails').addEventListener("click",()=>
  {
      jobList.style.display="block"
      
document.querySelector('.job-details').style.display="none"
document.querySelector('.header').style.display="block"
document.querySelector('.header').style.display="flex"
  }
)
async function fetchAndDisplayApplications(job) {

    var applications=[]
    if(applications.length<=0) {applications = await fetchApplications(job.jobId);}
    

    jobList.style.display="none"

    document.querySelector(".applicants-header").setAttribute("jobid",job.jobId)
document.querySelector('.job-details').style.display="block"
document.querySelector('.header').style.display="none"

     document.querySelector('.pagination').style.display = 'none';


document.querySelector(".totalno").textContent=`Total Applicants: ${applications.length}`

renderapplication(applications)
}
function renderapplication(applications)
{

  const tbody = document.getElementById('applicants-tbody');
  tbody.innerHTML=''
  applications.forEach(applicant => {
      const row = document.createElement('tr');

      row.innerHTML = `
     
          <td><img src="${applicant.logourl ? applicant.logourl : '../../assets/profile.png'}" alt="Avatar"> ${applicant.name}</td>

 <td class="meta-tags ${getJobTypeClass(applicant.status)}">${applicant.status}</td>
          <td>${applicant.appliedDate}</td>
          <td><button class="see-application">See Profile</button></td>
      `;
      row.addEventListener('click', () => {
        viewprofile(applicant.userId);
    });
      tbody.appendChild(row);
  });
}
function viewprofile(userid)
{
  console.log(userid)
}
function formatDate(dateString) {
    const options = { year: 'numeric', month: 'long', day: 'numeric' };
    return new Date(dateString).toLocaleDateString(undefined, options);
}

function viewApplication(applicationId) {
  
    console.log('View application:', applicationId);
}
document.querySelector(".filter-btn").addEventListener("click", async (event) => {
event.preventDefault()
  var formData = new FormData(document.querySelector(".search-filter"));

  let filterParams = {
      jobId:      document.querySelector(".applicants-header").getAttribute("jobid"),  
      pageNumber: 1,  
      pageSize: 5, 
      firstApplied: formData.has("firstApplied") && formData.get("firstApplied") === "on",
      perfectMatchSkills: formData.has("perfectMatchSkills") && formData.get("perfectMatchSkills") === "on",
      perfectMatchExperience: formData.has("perfectMatchExperience") && formData.get("perfectMatchExperience") === "on",
      hasExperienceInJobTitle: formData.has("hasExperienceInJobTitle") && formData.get("hasExperienceInJobTitle") === "on"
  };
  
  filterParams=await fetchfilteruser(filterParams) 
  renderfilterdata(filterParams)

});

function renderfilterdata(applications) {
    console.log(applications);
    
    // Clear existing table header and body
    var tableheader = document.querySelector(".filter-data");
    tableheader.innerHTML = '';
    tableheader.innerHTML = `
        <th>Logo</th>
        <th>Name</th>
        <th>Date of Birth</th>
        <th>Action</th>
    `;
    
    const tbody = document.getElementById('applicants-tbody');
    tbody.innerHTML = '';

    applications.forEach(applicant => {
        const row = document.createElement('tr');

        row.innerHTML = `
            <td><img src="${applicant.logourl ? applicant.logourl : '../../assets/profile.png'}" alt="Avatar"></td>
            <td>${applicant.name}</td>
            <td>${formatDate(applicant.dob)}</td>
            <td class="options-cell">
                <div class="options">
                    <button class="option-btn" data-action="edit">Edit</button>
                    <button class="option-btn" data-action="delete">Delete</button>
                    <button class="option-btn" data-action="details">Details</button>
                </div>
                <div class="dropdown">
                    <button class="dropdown-btn">...</button>
                    <div class="dropdown-content show">
                        <button class="option-btn" onclick="editProfile()" data-action="edit">Hired</button>
                        <button class="option-btn" data-action="delete">Rejected</button>
                        <button class="option-btn" data-action="details">Interviewed</button>
                    </div>
                </div>
            </td>
        `;

        // Add event listener for dropdown button click
        const dropdownBtn = row.querySelector('.dropdown-btn');
        const dropdownContent = row.querySelector('.dropdown-content');
        dropdownBtn.addEventListener('click', () => {
            dropdownContent.classList.toggle('show');
            console.log("Dropdown clicked");
        });

        // Handle profile view when 'See Profile' button is clicked
        // row.querySelector('.see-application').addEventListener('click', () => {
        //     viewProfile(applicant.userId);
        // });

        tbody.appendChild(row);
    });
}
async function fetchfilteruser(data) {
  try {
      const response = await fetchData("api/JobActivity/filtered","POST",data);

      return response;
  } catch (error) {
      console.error('Error fetching applications:', error);
      return [];
  }
}
function editProfile(userId) {
    console.log(`Editing profile for user ${userId}`);
}

function deleteProfile(userId) {
    console.log(`Deleting profile for user ${userId}`);
}

function viewDetails(userId) {
    console.log(`Viewing details for user ${userId}`);
}

function viewProfile(userId) {
    console.log(`Viewing profile for user ${userId}`);
}