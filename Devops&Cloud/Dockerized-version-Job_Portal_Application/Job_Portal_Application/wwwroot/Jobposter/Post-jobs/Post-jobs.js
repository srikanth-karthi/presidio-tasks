import { fetchData } from "../../Package/api.js";
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

$(function () {
  $("#title-list").select2();
});
const titleList = document.getElementById("title-list");

const skillsArray = await fetchData("api/Skill");
const jobTitles = await fetchData("api/Title");
populateData("title-list", jobTitles, "titleName", "titleId");
populateData("skill-list", skillsArray, "skillName", "skillId");

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
var Updateskills = [];
function rendermultisselect()
{
new MultiSelectTag("skill-list", {
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
});
}
rendermultisselect()
const form = document.querySelector("form");
const jobTitle = document.getElementById("title-list");
const jobDescription = document.getElementById("model-job-description");
const jobStatus = document.getElementsByName("job-status");
const salary = document.getElementById("salary");
const experience = document.getElementById("experience");


form.addEventListener("submit", async function (event) {
  event.preventDefault(); 

  if (jobTitle.value === "") {
    showToast("warning", "warning", "Please select a job title.");
    return;
  }

  if (jobDescription.value.trim() === "") {
    showToast("warning", "warning", "Please enter a job description.");
    return;
  }

  let selectedJobStatus = null;
  for (let status of jobStatus) {
    if (status.checked) {
      selectedJobStatus = status.value;
      break;
    }
  }
  if (!selectedJobStatus) {
    showToast("warning", "warning", "Please select a job status.");
    return;
  }

  if ( salary.value < 0) {
    showToast("warning", "warning", "Please enter a valid salary.");
    return;
  }

  if ( experience.value < 0) {
    showToast("warning", "warning", "Please enter a valid Experience.");
    return;
  }
  var SkillsRequired=[]
  Updateskills.forEach((skill)=>
  {
  SkillsRequired.push( skill.value)
  })



  const formData = {
    JobType: selectedJobStatus,
    TitleId: jobTitle.value,

    Lpa: salary.value == "" ? 0 : salary.value,

    JobDescription: jobDescription.value.trim(),
    SkillsRequired: SkillsRequired,
    ExperienceRequired: experience.value == "" ? 0 : experience.value,
  };
  try{
  await  fetchData("api/Job/add","POST",formData)
    showToast("success", "success", "Job Added Sucessfully.");
    Updateskills = [];
    SkillsRequired=[]
    rendermultisselect()

    form.reset();
  }
  catch(e){
  }
});

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
});
