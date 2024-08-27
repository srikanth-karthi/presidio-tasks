import { fetchData } from "../../Package/api.js";
import { showToast } from "../../Package/toaster.js";

  if(!localStorage.getItem('authToken'))
    {
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

function getGreetingBasedOnTime() {
  const now = new Date();
  const hours = now.getHours();

  if (hours >= 0 && hours < 12) {
    return "Good morning,";
  } else if (hours >= 12 && hours < 18) {
    return "Good afternoon,";
  } else {
    return "Good evening,";
  }
}


document.querySelector('.greeting').textContent=getGreetingBasedOnTime()

  const profileElement = document.querySelector(".profile");
  const nameElement = document.querySelector('.name');
  const menuButton = document.getElementById('menuButton');
  const sidebar = document.getElementById('sidebar');
  const companyLogo = document.getElementById('company-logo');
  const crossButton = document.getElementById('cross');
  const mainContent = document.querySelector('.main-content');

  try {
    const Profile = await fetchData("api/Company/profile");
console.log(Profile)
    const firstName = Profile.companyName.split(' ')[0];

    const profileData = {
      name: firstName,
      email: Profile.email,
      profileUrl: Profile.logoUrl
    };

    localStorage.setItem("profile", JSON.stringify(profileData));

    profileElement.innerHTML = `
      <img src="${profileData.profileUrl?profileData.profileUrl :'../../assets/profile.png' }" width="60" height="60" alt="" />
      <div>
        <p>${profileData.name}</p>

      </div>
    `;

    nameElement.textContent = profileData.name;
  } catch (error) {
    console.log(error)
    console.log("sisydg")
    if (error.message.includes("401")) {
    }
  }






  menuButton.addEventListener('click', function() {
    sidebar.classList.toggle('hidden');
    companyLogo.style.display = 'none';
    crossButton.style.display = 'block';
    mainContent.classList.toggle('expanded');
  });

  crossButton.addEventListener('click', function() {
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








  async function GetJobs() {
    try {
      return await fetchData(
        `api/Job`
      );
    } catch (error) {
      console.log(error);
      return [];
    }
  }
  function animateNumber(element, start, end, duration) {
    let startTime = null;
  
    function animation(currentTime) {
      if (startTime === null) startTime = currentTime;
      const timeElapsed = currentTime - startTime;
      const progress = Math.min(timeElapsed / duration, 1);
  
      element.textContent = Math.floor(progress * (end - start) + start);
  
      if (progress < 1) {
        requestAnimationFrame(animation);
      } else {
        element.textContent = end; 
      }
    }
  
    requestAnimationFrame(animation);
  }
  

    var jobItems = await GetJobs();
    console.log(jobItems);
  
    let open = 0, closed = 0;
  
    jobItems.forEach(job => {

      console.log(job.companyName)
      if (job.status) open += 1;
      else closed += 1;
    });
  

    const totalJobsElement = document.querySelector('.posted');
    const openJobsElement = document.querySelector('.open');
    const closedJobsElement = document.querySelector('.close');
  
    animateNumber(totalJobsElement, 0, jobItems.length, 2000);
    animateNumber(openJobsElement, 0, open, 2000);
    animateNumber(closedJobsElement, 0, closed, 2000);




  
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
 var  jobList=document.getElementById('job-list')
  async function renderTable(pageNumber, itemsPerPage,) {
    const startIndex = (pageNumber - 1) * itemsPerPage;
    let endIndex = startIndex + itemsPerPage;

  
    jobList.innerHTML = "";
    jobItems=jobItems.slice(0, 3);
    for (let i = startIndex; i < endIndex; i++) {
      if (i >= jobItems.length) break;
      const jobItem = jobItems[i];
      const jobItemDiv = document.createElement("div");
      jobItemDiv.classList.add("job-item");
      jobItem.companylogo = jobItem.companylogo ? jobItem.companylogo : "../../assets/Company.png";
      jobItemDiv.innerHTML = `
        <img src="${jobItem.companylogo}" width="50"  height="50" alt="${jobItem.titleName}">
        <div class="company-details">
          <h3>${jobItem.titleName}</h3>
          <p>${jobItem.companyName} • ${jobItem.experienceRequired} yrs Experience</p>
          <span class="meta-tags ${getJobTypeClass(jobItem.jobType)}">${jobItem.jobType}</span>
        </div>`;
      jobList.appendChild(jobItemDiv);
    }

  }
  renderTable(1,3)
// });
