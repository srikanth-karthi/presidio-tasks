
import { fetchData } from "../Package/api.js";


if(!localStorage.getItem('authToken'))
    {
      window.location.href = "/Auth/login.html?authid=3";
   
    }
    
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



function formatDate(dateString) {
    const options = { year: 'numeric', month: 'long', day: 'numeric' };
    return new Date(dateString).toLocaleDateString(undefined, options);
}


  function showModal(jobData) {
    document.getElementById('companyLogo').src = jobData.logourl;
    document.getElementById('titleName').textContent = jobData.titleName;
    document.getElementById('companyName').textContent = jobData.companyName;
    document.getElementById('jobType').textContent = jobData.jobType;
    document.getElementById('jobStatus').textContent = jobData.jobStatus;
    document.getElementById('appliedDate').textContent = formatDate(jobData.appliedDate);
    document.getElementById('resumeViewed').textContent = jobData.resumeViewed ? 'Yes' : 'No';
    document.getElementById('comments').textContent = jobData.comments || 'Comments not provided';
    document.getElementById('updatedDate').textContent = jobData.updatedDate ? formatDate(jobData.updatedDate): 'Not yet updated';
    
    const modal = document.getElementById("jobModal");
    modal.style.display = "block";
  }

  document.querySelector(".close").onclick = function() {
    const modal = document.getElementById("jobModal");
    modal.style.display = "none";
  }

  window.onclick = function(event) {
    const modal = document.getElementById("jobModal");
    if (event.target == modal) {
      modal.style.display = "none";
    }
  }
  var jobhistory
  const itemsPerPage = 5;
let currentPage = 1;
  try {
 jobhistory = await fetchData("api/JobActivity/user/appliedjobs");

} catch (error) {
    if (error.message.includes("404")) {
    document.querySelector(".tab-container").style.display='none'
    document.querySelector(".applications-list").style.display='none'
    document.querySelector(".description").style.display='none'
    document.querySelector(".notfound").style.display='block'

    }
       else {            showToast('error', 'Server Error', 'Server error. Please try again later.');
      } 

}

function renderTable(page, filteredJobs) {
    const tableBody = document.getElementById("applicationsTable");
    tableBody.innerHTML = "";

    const startIndex = (page - 1) * itemsPerPage;
    const endIndex = Math.min(startIndex + itemsPerPage, filteredJobs.length);
    for (let i = startIndex; i < endIndex; i++) {
        const row = filteredJobs[i];
        const tr = document.createElement("tr");
        tr.innerHTML = `
            <td>${i + 1}</td>
            <td>
                <img src="${row.logourl ? row.logourl :"../assets/Company.png"}" alt="${row.companyName} logo" style="width:50px; height:auto; vertical-align:middle; margin-right:10px;">
                ${row.companyName}
            </td>
            <td>${row.titleName}</td>
            <td>${row.appliedDate}</td>
            <td class="status-td"><span class="${getStatusClass(row.applicationstatus)} data-center">${row.applicationstatus}</span></td>
        `;
        tr.addEventListener("click", function() {
            showModal(row);
        });
        tableBody.appendChild(tr);
        
    }
}

function getStatusClass(status) {
    switch (status) {
        case "Applied":
            return "status Applied";
        case "Hired":
            return "status Hired";
        case "Interviewed":
            return "status Interviewed";
        case "Rejected":
            return "status Rejected";
        default:
            return "status";
    }
}

function renderPagination(filteredJobs) {
    const pagination = document.getElementById("pagination");
    pagination.innerHTML = "";

    const totalPages = Math.ceil(filteredJobs.length / itemsPerPage);

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
                currentPage = parseInt(text) || currentPage + (text === '>' ? 1 : -1);
                renderTable(currentPage, filteredJobs);
                renderPagination(filteredJobs);
            }
        });
        return button;
    };

    // Previous button
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

function updateCategoryCounts() {
    const allCount = jobhistory.length;
    const AppliedCount = jobhistory.filter((item) => item.applicationstatus === "Applied").length;
    const InterviewedCount = jobhistory.filter((item) => item.applicationstatus === "Interviewed").length;
    const HiredCount = jobhistory.filter((item) => item.applicationstatus === "Hired").length;
    const RejectedCount = jobhistory.filter((item) => item.applicationstatus === "Rejected").length;

    document.getElementById("allCount").textContent = allCount;
    document.getElementById("AppliedCount").textContent = AppliedCount;
    document.getElementById("InterviewedCount").textContent = InterviewedCount;
    document.getElementById("HiredCount").textContent = HiredCount;
    document.getElementById("RejectedCount").textContent = RejectedCount;
}

updateCategoryCounts();
renderTable(currentPage, jobhistory);
renderPagination(jobhistory);
const tabs = document.querySelectorAll('.tab-item');

tabs.forEach((tab, index) => {
    tab.addEventListener('click', function(event) {
        event.preventDefault();

        tabs.forEach(item => item.classList.remove('active'));
        this.classList.add('active');

        let filteredJobs = jobhistory;
        switch (index) {
            case 0:
                filteredJobs = jobhistory;
                break;
            case 1:
                filteredJobs = jobhistory.filter(job => job.applicationstatus === 'Applied');
                break;
            case 2:
                filteredJobs = jobhistory.filter(job => job.applicationstatus === 'Interviewed');
                break;
            case 3:
                filteredJobs = jobhistory.filter(job => job.applicationstatus === 'Hired');
                break;
            case 4:
                filteredJobs = jobhistory.filter(job => job.applicationstatus === 'Rejected');
                break;
        }
console.log(filteredJobs)
        currentPage = 1; 
        renderTable(currentPage, filteredJobs);
        renderPagination(filteredJobs);
    });
});


