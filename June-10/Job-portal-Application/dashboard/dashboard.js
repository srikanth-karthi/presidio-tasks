import { fetchData } from "../Package/api.js";
import { showToast } from "../Package/toaster.js";
// document.addEventListener("DOMContentLoaded", async function() {
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
  const appliedCountElement = document.querySelector(".Applied-count");
  const interviewedCountElement = document.querySelector(".interviewed-count");
  const canvas = document.getElementById("donutChart");
  const applicationsContainer = document.getElementById('applicationsContainer');
  const menuButton = document.getElementById('menuButton');
  const sidebar = document.getElementById('sidebar');
  const companyLogo = document.getElementById('company-logo');
  const crossButton = document.getElementById('cross');
  const mainContent = document.querySelector('.main-content');
  const legendElements = document.querySelectorAll(".chart-legend .element .legend-text .percentage");



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
  try {
    const userProfile = await fetchData("api/User/profile");
    const firstName = userProfile.name.split(' ')[0];

    const profileData = {
      name: firstName,
 
      profileUrl: userProfile.profilePictureUrl
    };

    localStorage.setItem("profile", JSON.stringify(profileData));

    profileElement.innerHTML = `
      <img src="${profileData.profileUrl?profileData.profileUrl :'../assets/profile.png' }" width="60" height="60" alt="" />
      <div>
        <p>${profileData.name}</p>
  
      </div>
    `;

    nameElement.textContent = profileData.name;
  } catch (error) {
    console.log(error)
    if (error.message.includes("401")) {
      console.log("sisydg")
    }
  }

  async function fetchJobHistory() {
    try {
      const jobHistory = await fetchData("api/JobActivity/user/appliedjobs");

      const data = {
        offered: 0,
        interviewed: 0,
        applied: 0,
        rejected: 0
      };

      const JobStatus = {
        Applied: 'Applied',
        Interviewed: 'Interviewed',
        Hired: 'Hired',
        Rejected: 'Rejected'
      };

      jobHistory.forEach(job => {
        switch (job.jobStatus) {
          case JobStatus.Hired:
            data.offered += 1;
            break;
          case JobStatus.Interviewed:
            data.interviewed += 1;
            break;
          case JobStatus.Applied:
            data.applied += 1;
            break;
          case JobStatus.Rejected:
            data.rejected += 1;
            break;
        }
      });

      appliedCountElement.textContent = jobHistory.length;
      interviewedCountElement.textContent = data.interviewed;

      const total = data.offered + data.interviewed + data.applied + data.rejected;

      if (total > 0) {
        data.offered = Number(((data.offered / total) * 100).toFixed(1));
        data.interviewed = Number(((data.interviewed / total) * 100).toFixed(1));
        data.applied = Number(((data.applied / total) * 100).toFixed(1));
        data.rejected = Number(((data.rejected / total) * 100).toFixed(1));
      }

      const recentJobHistory = jobHistory.slice(0, 3);

      const ctx = canvas.getContext("2d");
      const colors = {
        rejected: "#C4E4FF",
        interviewed: "#393ebc",
        applied: "#98ABEE",
        offered: "#201658"
      };

      function drawSegment(startAngle, endAngle, color) {
        ctx.beginPath();
        ctx.arc(100, 100, 70, startAngle, endAngle);
        ctx.arc(100, 100, 50, endAngle, startAngle, true);
        ctx.fillStyle = color;
        ctx.fill();
      }
      
      function animateChart(duration) {
        const total = data.offered + data.interviewed + data.applied + data.rejected;
        const angles = {
          offered: (data.offered / total) * 2 * Math.PI,
          interviewed: (data.interviewed / total) * 2 * Math.PI,
          applied: (data.applied / total) * 2 * Math.PI,
          rejected: (data.rejected / total) * 2 * Math.PI
        };

        let start = null;

        function animate(time) {
            if (!start) start = time;
            const progress = Math.min((time - start) / duration, 1);
        
            ctx.clearRect(0, 0, canvas.width, canvas.height);
        
            let startAngle = 0;
            Object.keys(angles).forEach((key) => {
              const endAngle = startAngle + angles[key] * progress;
              drawSegment(startAngle, endAngle, colors[key]);
              startAngle = endAngle;
            });
        
            if (progress < 1) {
              requestAnimationFrame(animate);
            }
          }
        
          requestAnimationFrame(animate);
        }
        
        animateChart(2000);

      legendElements[0].textContent = `${data.offered}%`;
      legendElements[1].textContent = `${data.interviewed}%`;
      legendElements[2].textContent = `${data.applied}%`;
      legendElements[3].textContent = `${data.rejected}%`;

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

      recentJobHistory.forEach(app => {
        const applicationDiv = document.createElement('div');
        applicationDiv.classList.add('application');

        applicationDiv.innerHTML = `
          <div class="company-logo">
          <img src="${app.logourl ? app.logourl : '../assets/Company.png'}" width="62" height="62" alt="Icon" />
        
          </div>
          <div class="company-details">
            <p style="margin-bottom: 13px; font-weight: 600;">${app.companyName}</p>
            <p>${app.titleName}</p>
          </div>
          <div class="applied-date">
            <p>Date Applied</p>
            <p>${app.appliedDate}</p>
          </div>
          <span class="${getStatusClass(app.jobStatus)}">${app.jobStatus}</span>
        `;

        applicationsContainer.appendChild(applicationDiv);
      });

    } catch (error) {
        if (error.message.includes("404")) {
        document.querySelector(".stats").style.display='none'
        document.querySelector(".applications").style.display='none'
        document.querySelector(".description").style.display='none'
        document.querySelector(".notfound").style.display='block'

        }
           else {            showToast('error', 'Server Error', 'Server error. Please try again later.');
          } 

    }
  }

  fetchJobHistory();



// });
