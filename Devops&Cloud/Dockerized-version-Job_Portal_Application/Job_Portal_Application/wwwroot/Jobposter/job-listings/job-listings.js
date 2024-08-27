import { fetchData ,baseUrl} from "../../Package/api.js";
import { showToast } from "../../Package/toaster.js";
import { MultiSelectTag } from "../../Package/Multiselect.js";
if (!localStorage.getItem("authToken")) {
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

const authid = getQueryParam("authid");

if (authid == 1) {
  showToast("success", "Success", "Login Successful.");
  removeQueryParam("authid");
}

const profileData = JSON.parse(localStorage.getItem("profile"));

if (profileData) {
  const profileElement = document.querySelector(".profile");

  if (profileElement) {
    profileElement.innerHTML = `
      <img src="${
        profileData.profileUrl
          ? profileData.profileUrl
          : "../../assets/Company.png"
      }" width="60" height="60" alt="" />
          <div>
            <p>${profileData.name}</p>
          </div>
        `;
  }
}

const menuButton = document.getElementById("menuButton");
const sidebar = document.getElementById("sidebar");
const companyLogo = document.getElementById("company-logo");
const crossButton = document.getElementById("cross");
const mainContent = document.querySelector(".main-content");

menuButton.addEventListener("click", function () {
  sidebar.classList.toggle("hidden");
  companyLogo.style.display = "none";
  crossButton.style.display = "block";
  mainContent.classList.toggle("expanded");
});

crossButton.addEventListener("click", function () {
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

let currentPage = 1;
const itemsPerPage = 6;
var maxpagereached=false
var isfiltered=false
let currentPageapplicant = 1;
const itemsPerPageapplicant = 4;
const jobListBody = document.getElementById("job-list-body");
const jobList = document.querySelector(".job-list");
const pageNumber = document.getElementById("page-number");
const pageNumberapplicant = document.getElementById("page-number-applicant");
const prevPageButton = document.getElementById("prev-page");
const nextPageButton = document.getElementById("next-page");

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
function getStatusClass(status) {
  switch (status) {
    case "Applied":
      return " Applied";
    case "Hired":
      return " Hired";
    case "Interviewed":
      return " Interviewed";
    case "Rejected":
      return " Rejected";
    default:
      return "";
  }
}

async function GetJobs() {
  try {
    return await fetchData(`api/Job`);
  } catch (error) {

    return [];
  }
}

var jobs = await GetJobs();


function renderJobs(page) {
  jobListBody.innerHTML = "";
  const start = (page - 1) * itemsPerPage;
  const end = page * itemsPerPage;
  const paginatedJobs = jobs.slice(start, end);

  paginatedJobs.forEach((job, index) => {
    const row = document.createElement("tr");
    row.innerHTML = `
                <td style="text-align: left">${job.titleName}</td>
                <td class="${job.status ? "active" : "inactive"}">${
      job.status ? "active" : "inactive"
    }</td>
                <td>${formatDate(job.datePosted)}</td>
                <td class="meta-tags ${getJobTypeClass(job.jobType)}">${
      job.jobType
    }</td>
                <td>${
                  job.experienceRequired > 0
                    ? job.experienceRequired
                    : "not required"
                }</td>
            `;
    row.addEventListener("click", () => {
      fetchAndDisplayApplications(job);
    });
    jobListBody.appendChild(row);
    setTimeout(() => {
      row.classList.add("visible");
    }, index * 100);
  });
}

function updatePageNumber(page) {
  pageNumber.textContent = page;
}

prevPageButton.addEventListener("click", () => {
  if (currentPage > 1) {
    currentPage--;
    renderJobs(currentPage);
    updatePageNumber(currentPage);
  }
});

nextPageButton.addEventListener("click", () => {
  if (currentPage < Math.ceil(jobs.length / itemsPerPage)) {
    currentPage++;
    renderJobs(currentPage);
    updatePageNumber(currentPage);
  }
});


renderJobs(currentPage);
updatePageNumber(currentPage);

async function fetchApplications(jobId) {
  try {
    const response = await fetchData(`api/JobActivity/job/${jobId}`);
    return response;
  } catch (error) {
    console.error("Error fetching applications:", error);
    return [];
  }
}

document.querySelector(".back-jobdetails").addEventListener("click", () => {

  jobList.style.display = "block";
  const tabs = document.querySelectorAll(".job-navigation a");

isfiltered=false

  tabs.forEach((tab) => tab.classList.remove("active-tab"));

  document.querySelector(".job-applicants-tab").classList.add("active-tab")
  document.getElementById("job-stats-chart").style.display = "none";
  document.querySelector(".job-details").style.display = "none";
  document.querySelector(".pagination").style.display = "flex";
  document.querySelector(".header").style.display = "flex";
  document.querySelector(".job-details").style.display = "none";
  document.querySelector(".jobcontainer").style.display = "none";
  document.querySelector(".search-filter").reset();
});
var applications = [];

async function fetchAndDisplayApplications(job,isprofileviwed=true) {
        currentPageapplicant=1
        updatePageNumberApplicant(1)
    if(isprofileviwed)
        {
  applications = await fetchApplications(job.jobId);
        }

  document.querySelector(".job-title").textContent = job.titleName;
  document.querySelector(".job-info").textContent = job.jobType;
  jobList.style.display = "none";

  document.querySelector(".applicants-section").style.display = "block";
  document.querySelector(".applicants-header").setAttribute("jobid", job.jobId);
  document.querySelector(".job-details").style.display = "block";
  document.querySelector(".header").style.display = "none";

  document.querySelector(".pagination").style.display = "none";

  document.querySelector(
    ".totalno"
  ).textContent = `Total Applicants: ${applications.length}`;

  renderapplication(currentPageapplicant);
}
async function renderapplication(page,formData=null) {

    const start = (page - 1) * itemsPerPageapplicant;
    const end = page * itemsPerPageapplicant;
    const paginatedJobs = applications.slice(start, end);
if(isfiltered && !maxpagereached && end > applications.length - 4)
  {
    formData.pageNumber=      1 + Math.ceil(applications.length / 16)
   const additionalData = await fetchfilteruser(
      formData
    );
    applications = applications.concat(additionalData);
  }

  const tbody = document.getElementById("applicants-tbody");
  tbody.innerHTML = "";
      document.querySelector(".pagination-applicant").style.display="flex"
  console.log(applications.length)
if(applications.length <=0 && isfiltered)
  {
    tbody.innerHTML = "<div style='text-align: center;' colspan='100%'><h3>No filtered Applicants found</h3></div>";

    document.querySelector(".pagination-applicant").style.display="none"
    return
  }

  if(applications.length <=0)
    {
      tbody.innerHTML = "<td><h3> No Applicants found</h3><td>"
      document.querySelector(".pagination-applicant").style.display="none"
      return
    }

  paginatedJobs.forEach((applicant) => {
    const row = document.createElement("tr");

    row.innerHTML = `
     
          <td><img src="${
            applicant.logourl ? applicant.logourl : "../../assets/profile.png"
          }" alt="Avatar"> ${applicant.name}</td>

 <td class="meta-tags ${getStatusClass(applicant.status)}">${
      applicant.status
    }</td>
          <td>${formatDate(applicant.appliedDate)}</td>
          <td><button class="see-application">See Profile</button></td>
            <td class="options-cell">
                <div class="options">
                    <button class="option-btn" data-action="edit">Edit</button>
                    <button class="option-btn" data-action="delete">Delete</button>
                    <button class="option-btn" data-action="details">Details</button>
                </div>
                <div class="dropdown">
                    <button class="dropdown-btn">...</button>
                    <div class="dropdown-content show">
                 <button class="option-btn" onclick="HireProfile('${
                   applicant.userId
                 }', '${
      applicant.jobactivityId
    }')" data-action="edit">Hired</button>
                <button class="option-btn" onclick="RejectedProfile('${
                  applicant.userId
                }', '${
      applicant.jobactivityId
    }')" data-action="delete">Rejected</button>
                <button class="option-btn" onclick="InterviewedProfile('${
                  applicant.userId
                }', '${
      applicant.jobactivityId
    }')" data-action="details">Interviewed</button>

                    </div>
                </div>
            </td>
      `;
    const dropdownBtn = row.querySelector(".dropdown-btn");
    const dropdownContent = row.querySelector(".dropdown-content");
    dropdownBtn.addEventListener("click", (e) => {
      dropdownContent.classList.toggle("show");
    e.stopPropagation()

    });

    row.querySelector(".see-application").addEventListener("click", () => {
        viewprofile(applicant.userId,applicant.jobactivityId);
    });
    tbody.appendChild(row);
  });


  
}


function updatePageNumberApplicant(page) {
    pageNumberapplicant.textContent = page;
  }
  
  document.getElementById('prev-page-applicant').addEventListener("click", () => {
    if (currentPageapplicant > 1) {
        currentPageapplicant--;
        renderapplication(currentPageapplicant);
      updatePageNumberApplicant(currentPageapplicant);
    }
  });
  
  document.getElementById('next-page-applicant').addEventListener("click", () => {
    if (currentPageapplicant < Math.ceil(applications.length / itemsPerPageapplicant)) {
      currentPageapplicant++;
      renderapplication(currentPageapplicant);
      updatePageNumberApplicant(currentPageapplicant);
    }
  })

function formatDate(dateString) {
  const options = { year: "numeric", month: "long", day: "numeric" };
  return new Date(dateString).toLocaleDateString(undefined, options);
}

document
  .querySelector(".filter-btn")
  .addEventListener("click", async (event) => {
    event.preventDefault();
    isfiltered=true
    maxpagereached=false
    var formData = new FormData(document.querySelector(".search-filter"));

    let filterParams = {
      jobId: document.querySelector(".applicants-header").getAttribute("jobid"),
      pageNumber: 1,
      pageSize: 16,
      firstApplied:
        formData.has("firstApplied") && formData.get("firstApplied") === "on",
      perfectMatchSkills:
        formData.has("perfectMatchSkills") &&
        formData.get("perfectMatchSkills") === "on",
      perfectMatchExperience:
        formData.has("perfectMatchExperience") &&
        formData.get("perfectMatchExperience") === "on",
      hasExperienceInJobTitle:
        formData.has("hasExperienceInJobTitle") &&
        formData.get("hasExperienceInJobTitle") === "on",
    };

    applications = await fetchfilteruser(filterParams);
    currentPageapplicant=1
    updatePageNumberApplicant(currentPageapplicant);
    renderapplication(currentPageapplicant,filterParams);
  });

async function fetchfilteruser(data) {
  try {
    const response = await fetchData("api/JobActivity/filtered", "POST", data);

    return response;
  } catch (error) {
    if (error.message.includes("404")) {
      maxpagereached = true;
      return [];
    }
    console.error("Error fetching applications:", error);
    return [];
  }
}

function showCommentPopup(userId, jobactivityId, status) {
  const popup = document.getElementById("commentPopup");
  popup.classList.remove("hidden");

  document.getElementById("submitComment").onclick = function () {
    const comments = document.getElementById("commentText").value;
    document.getElementById("commentText").value=''
    updateApplicationStatusWithComments(
      userId,
      jobactivityId,
      status,
      comments
    );
    popup.classList.add("hidden");
  };

  document.getElementById("cancelComment").onclick = function () {
    popup.classList.add("hidden");
        document.getElementById("commentText").value=''
  };
}

function HireProfile(userId, jobactivityId) {
  updateApplicationStatusWithComments(userId, jobactivityId, "Hired");
}

function RejectedProfile(userId, jobactivityId) {
  showCommentPopup(userId, jobactivityId, "Rejected");
}

function InterviewedProfile(userId, jobactivityId) {
  showCommentPopup(userId, jobactivityId, "Interviewed");
}

const skillsArray = await fetchData("api/Skill");

async function updateApplicationStatusWithComments(
  userId,
  jobactivityId,
  status,
  comments = null
) {
  const applicationIndex = applications.findIndex(
    (applicant) =>
      applicant.userId === userId && applicant.jobactivityId === jobactivityId
  );
  if (applicationIndex !== -1) {
    console.log(applications[applicationIndex])
    applications[applicationIndex].status = status;
    const data = {
      resumeViewed: applications[applicationIndex].resumeViewed,
      jobactivityId: applications[applicationIndex].jobactivityId,
      status: status,
      comments: comments ? comments : "",
    };

    try {
      await fetchData("api/JobActivity/Update", "PUT", data);


        renderapplication(currentPageapplicant);
      
    } catch (error) {
      console.error("Error updating application status:", error);
    }
  } else {

  }
}
document.querySelector(".job-details-tab").addEventListener("click", () => {
  document.querySelector(".applicants-section").style.display = "none";
  document.querySelector(".applicants-section").style.display = "none";
  document.querySelector(".jobcontainer").style.display = "block";
  document.getElementById("job-stats-chart").style.display = "none";


  renderJobDetails();
});
document.querySelector(".job-applicants-tab").addEventListener("click", () => {
  document.querySelector(".applicants-section").style.display = "block";
  document.querySelector(".jobcontainer").style.display = "none";


  document.getElementById("job-stats-chart").style.display = "none";
});
document.querySelector(".job-analytics-tab").addEventListener("click", () => {
  document.querySelector(".applicants-section").style.display = "none";
  document.querySelector(".jobcontainer").style.display = "none";
  document.getElementById("job-stats-chart").style.display = "block";


  renderAnalytics();
});
const tabs = document.querySelectorAll(".job-navigation a");


function handleTabClick(event) {

  tabs.forEach((tab) => tab.classList.remove("active-tab"));


  event.target.classList.add("active-tab");
}


tabs.forEach((tab) => {
  tab.addEventListener("click", handleTabClick);
});
window.HireProfile = HireProfile;
window.RejectedProfile = RejectedProfile;
window.InterviewedProfile = InterviewedProfile;
window.profilebackbtn = profilebackbtn;
window.showProfile = showProfile;
window.showResume = showResume;
var Updateskills = [];
async function renderAnalytics() {
  const jobId = document
    .querySelector(".applicants-header")
    .getAttribute("jobid");
  const jobData = await fetchApplications(jobId);

  const jobStats = {
    applied: 0,
    interviewed: 0,
    rejected: 0,
    hired: 0,
  };

  jobData.forEach((job) => {
    if (job.status.toLowerCase() in jobStats) {
      jobStats[job.status.toLowerCase()]++;
    }
  });

  function renderChartData(data) {
    const tbody = document.getElementById("chart-data");
    tbody.innerHTML = "";

    for (const [status, count] of Object.entries(data)) {
      const row = document.createElement("tr");

      const statusCell = document.createElement("th");
      statusCell.scope = "row";

      statusCell.textContent = status.charAt(0).toUpperCase() + status.slice(1);

      const countCell = document.createElement("td");
      const size = count / Math.max(...Object.values(data));
      countCell.style.setProperty("--size", size);
      countCell.innerHTML = `<span class="data">${count}</span>`;

      row.appendChild(statusCell);
      row.appendChild(countCell);

      tbody.appendChild(row);
    }
  }


  renderChartData(jobStats);
}
function renderJobDetails() {
  const jobId = document
    .querySelector(".applicants-header")
    .getAttribute("jobid");
  const data = jobs.find((job) => job.jobId === jobId);

  var selected_values = data.skills;
  var jobskills = data.skills;

  const content = document.querySelector(".jobcontainer");
  content.innerHTML = ``;

  content.innerHTML = `
        <div class="header">
            <img src="${
              data.companylogo ? data.companylogo : "../../Company.png"
            }" alt="Company Logo" id="company-logo-job-details" class="company-logo-job-details">
            <h1 id="job-title">${data.titleName}</h1>
            <button id="edit-job-details" class="edit-job-details">Edit Job Details</button>
        </div>
        <div class="content">
            <section class="description">
                <h2>Description</h2>
                <p id="job-description">${data.jobDescription}</p>
            </section>
            <section class="details">
                <div class="right">
                    <div class="about-role">
                        <h2>About this role</h2>
                        <ul>
                            <li>Job Posted On: <span id="job-posted-on">${formatDate(
                              data.datePosted
                            )}</span></li>
                            <li>Job Status: <span id="apply-before" class="${
                              data.status ? "active" : "inactive"
                            }">${
    data.status ? "Active" : "Inactive"
  }</span></li>
                            <li >Job Type: <span id="job-type" class=${getJobTypeClass( data.jobType)}>${
                              data.jobType
                            }</span></li>
                            <li>Salary: <span id="salary">${
                              data.lpa ?   data.lpa+' Lpa' : "Not Mentioned"

                            } </span></li>
                              <li>Experience: <span id="experience">${
                              data.experienceRequired>0?  data.experienceRequired+ ' Years': 'No experience Required'
                            } </span></li>
                        </ul>
                    </div>
                  <section class="profile-section">
          <h2 style="margin-top: 30px; display:flex; margin-right:auto">Skills Required<button id="edit-skills" class="edit-btn"><img src="../../assets/edit.svg" alt="Edit"></button></h2>
          <p style="margin-top: 20px;margin-bottom: 30PX;"></p>
          <div class="skills-list" id="skills-list"></div>
          <div id="skillsModal" class="modal">
            <div class="modal-content" style="height:400px;">
              <h2>Key Skills <span class="close">&times;</span></h2>
              <select name="skill-list" id="skill-list" multiple></select>
              <button id="edit-skills-btn">Update Skills</button>
            </div>
          </div>
        </section>
                </div>
            </section>
        </div>
    `;


    document.getElementById('edit-job-details').addEventListener('click', function() {

      const jobDescription = document.getElementById('job-description').innerText;
      const jobStatus = document.querySelector('#apply-before').innerText === 'Active' ? 'active' : 'inactive';
      const jobType = document.getElementById('job-type').innerText;
      const salary = document.getElementById('salary').innerText.replace(' Lpa', '');
      const experience = document.getElementById('experience').innerText.replace(' Years', '');
    
    
      document.getElementById('model-job-description').value = jobDescription;
      document.querySelector(`input[name="job-status"][value="${jobStatus}"]`).checked = true;
      document.getElementById('model-job-type').value = jobType;
      document.getElementById('model-salary').value = salary;
      document.getElementById('model-experience').value = experience;
    
    
      document.getElementById('editjobModal').style.display = 'block';
    });
    
    document.getElementById('editjobform').addEventListener('submit', Editjobeventlisterner);
    document.querySelector('#editjobModal .close').addEventListener('click', function() {
      document.getElementById('editjobModal').style.display = 'none';
    });
  populateData(
    "skill-list",
    skillsArray,
    "skillName",
    "skillId",
    selected_values
  );
  new MultiSelectTag(
    "skill-list",
    {
      rounded: true,
      shadow: true,
      placeholder: "Search",
      tagColor: {
        textColor: "#327b2c",
        borderColor: "#92e681",
        bgColor: "#eaffe6",
      },
      onChange: function (values) {
        Updateskills = values;
      },
    },
    selected_values
  );
  renderSkills(selected_values);
  const skillsModal = document.getElementById("skillsModal");
  document.getElementById("edit-skills").addEventListener("click", () => {
    skillsModal.style.display = "block";
  });

  document.querySelector(".modal .close").addEventListener("click", () => {
    skillsModal.style.display = "none";
  });

  document
    .getElementById("edit-skills-btn")
    .addEventListener("click", async function () {
      Updateskills = Updateskills.map((skill) => ({
        skillId: skill.value,
        skillName: skill.label,
      }));
      const requestBody = {
        jobId: document
          .querySelector(".applicants-header")
          .getAttribute("jobid"),
        skillsToAdd: Updateskills.filter(
          (updateSkill) =>
            !selected_values.some(
              (userSkill) => userSkill.skillId === updateSkill.skillId
            )
        ).map((filteredSkill) => filteredSkill.skillId),

        skillsToRemove: selected_values
          .filter(
            (userSkill) =>
              !Updateskills.some(
                (updateSkill) => updateSkill.skillId === userSkill.skillId
              )
          )
          .map((filteredSkill) => filteredSkill.skillId),
      };
      try {
        var skillsresponse = await fetchData(
          "api/Job/jobskills",
          "PUT",
          requestBody
        );

        jobskills = updateUserSkillsArray(
          jobskills,
          skillsresponse,
          skillsArray
        );
        jobs.find((job) => job.jobId === jobId).skills = jobskills;
        renderSkills(jobskills);

        showToast("success", "Success", "Skills added successfully");
      } catch (error) {
        console.error("Error adding skills:", error);

        if (error.message.includes("400")) {
          showToast("error", "Error", "Bad request. Please check your input.");
        } else if (error.message.includes("401")) {
          showToast(
            "error",
            "Error",
            "Unauthorized access. Please login again."
          );
        } else {
          showToast(
            "error",
            "Error",
            "An error occurred while adding skills. Please try again later."
          );
        }
      }

      skillsModal.style.display = "none";
    });
}

function renderSkills(jobskills) {
  const skillsList = document.getElementById("skills-list");

  skillsList.innerHTML = "";
  jobskills.forEach((skill) => {
    const skillItem = document.createElement("div");
    skillItem.className = "skill-item";
    skillItem.innerHTML = `<span>${skill.skillName}</span>`;
    skillsList.appendChild(skillItem);
  });
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
    if (selectId !== "company-list") optionElement.value = option[valueKey];
    optionElement.textContent = option[displayKey];
    if (
      selectedValues.some((selected) => selected[valueKey] === option[valueKey])
    ) {
      optionElement.selected = true;
    }
    select.appendChild(optionElement);
  });
}
function updateUserSkillsArray(userSkills, response, skillsArray) {
  const addedSkills = response.result.addedSkills;
  const removedSkills = response.result.removedSkills;

  userSkills = userSkills.filter(
    (skill) => !removedSkills.includes(skill.skillId)
  );

  addedSkills.forEach((skillId) => {
    const skill = skillsArray.find((s) => s.skillId === skillId);
    if (skill) {
      userSkills.push(skill);
    }
  });

  return userSkills;
}


async function viewprofile(userid,jobactivityId) {
  var data = await fetchData(`api/User/profile/${userid}`);


  document.querySelector(".main-content").style.display = "none";
  document.querySelector(".user-profile").style.display = "block";
  const container = document.getElementById("user-profile-container");

  container.innerHTML = "";

  const sidebar = document.createElement("div");
  sidebar.className = "profile-sidebar";
  sidebar.innerHTML = `
      <div class="profile-picture">
        <img src="${
          data.profilePictureUrl
            ? data.profilePictureUrl + "?date=" + Date.now()
            : "../../assets/profile.png"
        }" alt="${data.name}">
      </div>
      <h1>${data.name}</h1>
      <div class="contact">
        <p>Email: ${data.email}</p>
        <p>Phone: ${data.phoneNumber ? data.phoneNumber : "Not provided"}</p>


    <p>Website:
      ${data.portfolioLink ? 
        `<a href="${data.portfolioLink}" class="website-link" target="_blank" rel="noopener noreferrer">${data.portfolioLink}</a>` 
        : "Not provided"}
    </p>

`;


  const mainContent = document.createElement("div");
  mainContent.className = "profile-main-content";
  mainContent.innerHTML = `
      <div class="tabs">
       <button class="active" onclick="showProfile()">Applicant Profile</button>
        <button onclick="showResume('${userid}','${jobactivityId}')">Resume</button>
      </div>
      <div class="profile-section">
        <h2>Personal Info</h2>
        <p>Full Name: ${data.name}</p>
        <p>Date of Birth: ${formatDate(data.dob)}</p>
        <p>Address: ${data.address ? data.address+","+data.city :data.city }</p>
        <h2>Professional Info</h2>
        <h3>About Me</h3>
        <p>${data.aboutMe ? data.aboutMe : "Not provided"}</p>
        <h3>Skill set</h3>
        <p>${
          data.userSkills && data.userSkills.length > 0
            ? data.userSkills.map((skill) => skill.skillName).join(", ")
            : "Not provided"
        }</p>
        <h3>Education</h3>
        <div class="education">
          ${
            data.educations && data.educations.length > 0
              ? data.educations
                  .map(
                    (education) => `
            <div class="education-item">
              <p>Institution: ${education.institutionName}</p>
              <p>Level: ${education.level}</p>
              <p>Start Year: ${formatDate(education.startYear)}</p>
              <p>End Year: ${
                education.isCurrentlyStudying
                  ? "Currently Studying"
                  : formatDate(education.endYear)
              }</p>
              <p>Percentage: ${education.percentage}%</p>
            </div>
          `
                  )
                  .join("")
              : "<p>No education provided</p>"
          }
        </div>
        <h3>Experience</h3>
        <div class="experience">
          ${
            data.experiences && data.experiences.length > 0
              ? data.experiences
                  .map(
                    (experience) => `
            <div class="experience-item">
              <p>Company: ${experience.companyName}</p>
              <p>Title: ${experience.titleName}</p>
              <p>Start Year: ${formatDate(experience.startYear)}</p>
              <p>End Year: ${formatDate(experience.endYear)}</p>
              <p>Duration: ${experience.experienceDuration} years</p>
            </div>
          `
                  )
                  .join("")
              : "<p>No experience provided</p>"
          }
        </div>
      </div>
    `;

  container.appendChild(sidebar);
  container.appendChild(mainContent);
  window.showProfile = showProfile;
window.showResume = showResume;
}

function profilebackbtn() {
  document.querySelector(".main-content").style.display = "block";
  document.querySelector(".user-profile").style.display = "none";
  const jobId = document
    .querySelector(".applicants-header")
    .getAttribute("jobid");
  const data = jobs.find((job) => job.jobId === jobId);
  fetchAndDisplayApplications(data,false);
}

function showProfile() {
  document.querySelector(".profile-section").style.display = "block";
  document.getElementById("resume-viewer").style.display = "none";
  document.querySelectorAll('.tabs button').forEach(btn => btn.classList.remove('active'));

}

function showResume(userId, jobActivityId, token=localStorage.getItem("authToken")) {
    document.querySelector(".profile-section").style.display = "none";
    document.querySelectorAll('.tabs button').forEach(btn => btn.classList.remove('active'));
    
    let resumeViewer = document.getElementById("resume-viewer");
    if (!resumeViewer) {
        resumeViewer = document.createElement("iframe");
        resumeViewer.id = "resume-viewer";
        resumeViewer.style.width = "600px";
        resumeViewer.style.height = "800px";
        document.querySelector(".profile-main-content").appendChild(resumeViewer);
    }

    const resumeUrl = `${baseUrl}api/Resume/view/${userId}/${jobActivityId}`;


    const blob = new Blob([`
        <!DOCTYPE html>
        <html>
  <body style="height:800px">
            <iframe id="inner-iframe" style="width:100%; height:100%;" frameborder="0"></iframe>
            <script>
                const innerIframe = document.getElementById('inner-iframe');
                const xhr = new XMLHttpRequest();
                xhr.open('GET', '${resumeUrl}', true);
                xhr.setRequestHeader('Authorization', 'Bearer ${token}');
                xhr.responseType = 'blob';
                xhr.onload = function() {
                    if (xhr.status === 200) {
                        const blob = xhr.response;
                        const url = URL.createObjectURL(blob);
                        innerIframe.src = url;
                    } else {
                        innerIframe.srcdoc = '<h3>User Doesnot provide resume</h3>';
                    }
                };
                xhr.send();
            </script>
        </body>
        </html>
    `], { type: 'text/html' });

    resumeViewer.src = URL.createObjectURL(blob);
    resumeViewer.style.display = "block";
}



const Editjobeventlisterner = async function(event) {
  const jobId = document
  .querySelector(".applicants-header")
  .getAttribute("jobid");
const data = jobs.find((job) => job.jobId === jobId);
  event.preventDefault();

  const jobDescription = document.getElementById('model-job-description').value;
  const jobStatus = document.querySelector('input[name="job-status"]:checked').value;
  const jobType = document.getElementById('model-job-type').value;
  const salary = document.getElementById('model-salary').value;
  const experience = document.getElementById('model-experience').value;

  if (jobDescription.trim() === '' || jobType.trim() === '') {
    alert('Please fill in all required fields.');
    return;
  }

  if (isNaN(salary) || salary < 0) {
    showToast('warning', 'warning', 'Please enter a valid salary (0 or more).');
    return;
  }

  if (isNaN(experience) || experience < 0) {
    showToast('warning', 'warning', 'Please enter valid experience (0 or more).');
    return;
  }

  const formData = {
    jobId: data.jobId,
    titleId: data.titleId,
    jobDescription: jobDescription.trim(),
    lpa: Number(salary),
    experienceRequired: Number(experience),
    jobType: jobType, 
    status: jobStatus === 'active'
  };

  var updatedjob = await fetchData("api/Job/update", "PUT", formData);

  const jobIndex = jobs.findIndex((job) => job.jobId === updatedjob.jobId);
  if (jobIndex !== -1) {
    jobs[jobIndex] = updatedjob;
  } else {
    console.error('Job not found');
  }

  renderJobs(currentPage);
  document.querySelector(".job-info").textContent = updatedjob.jobType;
  renderJobDetails();
  document.getElementById('editjobModal').style.display = 'none';
  document.getElementById('editjobform').reset();
};


document.addEventListener("click", (event) => {
console.log("hj")
  const dropdowns = document.querySelectorAll(".dropdown-content");
  dropdowns.forEach((dropdown) => {
    dropdown.classList.add("show");
  });
});