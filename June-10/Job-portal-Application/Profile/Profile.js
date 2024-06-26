import { showToast } from "../Package/toaster.js";
import { fetchData } from "../Package/api.js";
import { MultiSelectTag } from "../Package/Multiselect.js";

document.addEventListener("DOMContentLoaded", async function () {
if(!localStorage.getItem('authToken'))
  {
    window.location.href = "/Auth/login.html?authid=3";

  }
  const menuButton = document.getElementById('menuButton');
  const sidebar = document.getElementById('sidebar');
  const companyLogo = document.getElementById('company-logo');
  const crossButton = document.getElementById('cross');
  const mainContent = document.querySelector('.main-content');


  const profilePictureContainer = document.querySelector(".profile-picture");
  const userName = document.getElementById("name");
  const userLocation = document.getElementById("location");
  const viewResumeBtn = document.querySelector('.View-Resume');
  const editProfileBtn = document.querySelector(".Edit-profile");
  const profileSubmitBtn = document.getElementById("Profile-submitBtn-update");
  const fileInput = document.getElementById("edit-profilePhoto");
  const removeFileBtn = document.getElementById("remove-file-btn");
  const addProfileModal = document.getElementById("addProfileModal");
  const addProfileModalForm = document.getElementById("addProfileModalForm");
  const fileNameDisplay = document.getElementById("file-name");
  const uploadInstruction = document.getElementById("upload-instruction");
  const fileTypes = document.getElementById("file-types");
  const editNameInput = document.getElementById("edit-name");
  const editLocationInput = document.getElementById("edit-location");
  const aboutMeText = document.getElementById("about-me-text");
  const editAboutMeBtn = document.querySelector(".edit-about-me");
  const addExperienceModal = document.getElementById("addExperienceModal");
  const addEducationModal = document.getElementById("addEducationModal");
  const skillsModal = document.getElementById("skillsModal");
  const expSubmitBtnAdd = document.getElementById("Exp-submitBtn-add");
  const expSubmitBtnUpdate = document.getElementById("Exp-submitBtn-update");
  const eduSubmitBtnAdd = document.getElementById("Edu-submitBtn-add");
  const eduSubmitBtnUpdate = document.getElementById("Edu-submitBtn-update");
  const addExperienceForm = document.getElementById("addExperienceForm");
  const addEducationForm = document.getElementById("addEducationForm");
  const institutionNameInput = document.getElementById("institutionName");
  const levelInput = document.getElementById("level");
  const startYearInput = document.getElementById("startYear");
  const isCurrentlyStudyingCheckbox = document.getElementById('isCurrentlyStudying');
  const endYearInput = document.getElementById('endYear');
  const percentageInput = document.getElementById("percentage");
  const isCurrentlyStudyingInput = document.getElementById("isCurrentlyStudying");
  const skillsList = document.getElementById("skills-list");
  const titleList = document.getElementById("title-list");
  const companyInput = document.getElementById("company");
  const startDateInput = document.getElementById("startDate");
  const endDateInput = document.getElementById("endDate");
  const experiencesContainer = document.querySelector(".Experiences");
  const educationsContainer = document.querySelector(".educations");
  const addAreaOfInterestForm = document.getElementById("addAreaOfInterestForm");
  const addAreaOfInterestModal = document.getElementById("addAreaOfInterestModal");
  const updateAoiBtn = document.getElementById("update-aoi-btn");
  const addAoiBtn = document.getElementById("add-aoi-btn");
  const aoiContainer = document.querySelector(".aoi-items");
  var editAdditionalDetailsModal= document.getElementById('editAdditionalDetailsModal');
  var editBtn = document.querySelector('.edit-Aditional-details');
  var updateDetailsForm = document.getElementById('editAdditionalDetailsForm');
  const profileElement = document.querySelector(".profile");
  crossButton.style.display = "none";
  


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

  $(function () {
    $("#title-list-aoi").select2();
    $("#title-list").select2();
  });
  var userprofile=[],jobTitles=[],companies=[],skillsArray=[]

  try{
     companies = await fetchData("api/Company");
     userprofile = await fetchData("api/User/profile");

     

     


    profileElement.innerHTML = `
  <img src="${userprofile.profilePictureUrl?userprofile.profilePictureUrl+'?date='+Date.now() :'../assets/profile.png' }" width="60" height="60" alt="" />
      <div>
        <p>${userprofile.name.split(' ')[0]}</p>

      </div>
    `;
 
     skillsArray = await fetchData("api/Skill");
     jobTitles = await fetchData("api/Title");

  }
  catch
  {
  
  }

  populateData("title-list", jobTitles, "titleName", "titleId");
  populateData("title-list-aoi", jobTitles, "titleName", "titleId");
  populateData("company-list", companies, "companyName", "companyId");
  populateData("skill-list", skillsArray, "skillName", "skillId", userprofile.userSkills);



  const educations = userprofile.educations;
  const experiences = userprofile.experiences;
  var UserSkillsArray = userprofile.userSkills;
  var selected_values=UserSkillsArray
  var useraoi = userprofile.areasOfInterests;



  const profilePicture = userprofile.profilePictureUrl || "../assets/profile.png";
  profilePictureContainer.innerHTML = `<img src="${profilePicture+'?date='+Date.now()}" alt="User Profile Picture" />`;

  userName.innerText = userprofile.name;
  userLocation.innerHTML = `
      <img src="../assets/profile-location-icon.svg" alt="Location Icon"> ${userprofile.city}
  `;
  aboutMeText.textContent = userprofile.aboutMe;




  document.querySelector('.detail-text.phone').textContent=  userprofile. phoneNumber
  document.querySelector('.detail-text.email').textContent=  userprofile. email
 document.querySelector('.detail-text.dob').textContent=   new Date(  userprofile.   dob).toLocaleDateString('en-US');
document.querySelector('.detail-text.address').textContent=userprofile.  address
document.querySelector('.detail-text.portfolio').innerHTML = userprofile.portfolioLink ? `<a href="${userprofile.portfolioLink}" class="portfolio-link" target="_blank" rel="noopener noreferrer">${userprofile.portfolioLink}</a>`:null



  editBtn.addEventListener('click', ()=>
  {
    editAdditionalDetailsModal.style.display = 'block';
    document.getElementById('modal-email').value = document.querySelector('.detail-text.email').textContent.trim();
    document.getElementById('modal-phone').value = document.querySelector('.detail-text.phone').textContent.trim();
    const dobText = document.querySelector('.detail-text.dob').textContent.trim();
  

    function parseDate(dateString) {
      const parts = dateString.split('/');
      const date = new Date(parts[2], parts[0] - 1, parts[1]);
      const formattedDate = date.toISOString().split('T')[0];
      return formattedDate;
    }
  

    const formattedDob = parseDate(dobText);
    document.getElementById('modal-dob').value = formattedDob;
    document.getElementById('modal-address').value = document.querySelector('.detail-text.address').textContent.trim();
    document.getElementById('modal-portfolio').value = document.querySelector('.detail-text.portfolio').textContent.trim();
  });


  updateDetailsForm.addEventListener('submit', async function(event) {
    event.preventDefault();
  
    var formData = new FormData(updateDetailsForm);
  

    var email =formData.get('modal-email');
    var phoneNumber = formData.get('modal-phone');
    var dob = formData.get('modal-dob');
    var address = formData.get('modal-address');
    var portfolioLink = formData.get('modal-portfolio');
  

    if ( !phoneNumber.trim() || !dob.trim() || !address.trim()) {
      showToast('warning', 'Warning', 'Please fill in all required fields.');
      return;
    }
  

    if (!/^\d{10}$/.test(phoneNumber)) {
      showToast('warning', 'Warning', 'Phone number must be 10 digits.');
      return;
    }

    if (portfolioLink.length > 200) {
      showToast('warning', 'Warning', 'Portfolio link should not exceed 200 characters.');
      return;
    }
    var updatedDetails = {
      city: userprofile.city,
      resumeUrl: userprofile.resumeUrl,
      aboutMe: userprofile.aboutMe,
      name: userprofile.name,
      email: email,
      phoneNumber: phoneNumber,
      dob: dob,
      address: address,
      portfolioLink: portfolioLink === "" ? null : portfolioLink
    };
    
  
    try {
      const updateResponse = await fetchData(`api/User/update`, "PUT", updatedDetails);
  
      userprofile.email = updateResponse.email;
      userprofile.phoneNumber = updateResponse.phoneNumber;
      userprofile.dob = updateResponse.dob;
      userprofile.address = updateResponse.address;
      userprofile.portfolioLink = updateResponse.portfolioLink;
  
      document.querySelector('.detail-text.phone').textContent = userprofile.phoneNumber;
      document.querySelector('.detail-text.email').textContent = userprofile.email;
      document.querySelector('.detail-text.dob').textContent = new Date(userprofile.dob).toLocaleDateString('en-US');
      document.querySelector('.detail-text.address').textContent = userprofile.address;
      document.querySelector('.detail-text.portfolio').innerHTML = userprofile.portfolioLink ? `<a href="${userprofile.portfolioLink}" class="portfolio-link" target="_blank" rel="noopener noreferrer">${userprofile.portfolioLink}</a>` : null;
  
      yaervalidation(userprofile.dob);
  
      showToast('success', 'Success', 'Profile details updated successfully');
    } catch (error) {
      console.error("Error updating profile details:", error);
  
      if (error.message.includes("401")) {
        showToast('error', 'Unauthorized', 'Unauthorized access. Please login again.');
      } else if (error.message.includes("404")) {
        showToast('error', 'Not Found', 'User profile not found.');
      } else {
        showToast('error', 'Error', 'An error occurred while updating profile details.');
      }
    }
  
    editAdditionalDetailsModal.style.display = 'none';
  });
  
  

  viewResumeBtn.addEventListener('click', function(event) {
      if (userprofile.resumeUrl) {
          const token = localStorage.getItem("authToken");
          if (token) {
              const resumeUrlWithToken = `${userprofile.resumeUrl}?token=${encodeURIComponent(token)}`;
              window.open(resumeUrlWithToken, '_blank');
          } else {
              event.preventDefault();
              showToast('warning', 'Warning', 'Authentication token is missing.');
          }
      } else {
          event.preventDefault();
          showToast('warning', 'Warning', 'Please upload your resume');
      }
  });

  editProfileBtn.addEventListener('click', () => {
      editNameInput.value = userprofile.name;
      editLocationInput.value = userprofile.city;
      addProfileModal.style.display = "block";
  });

  profileSubmitBtn.addEventListener('click', async (event) => {
      event.preventDefault();
      if (! editNameInput.value.trim() || ! editLocationInput.value.trim() ) {
        showToast('warning', 'Warning', 'Please fill in all required fields.');
        return;
      }
    
      const updatedProfile = {
          dob: userprofile.dob,
          name: editNameInput.value,
          address: userprofile.address,
          city: editLocationInput.value,
          portfolioLink: userprofile.portfolioLink,
          phoneNumber: userprofile.phoneNumber,
          resumeUrl: userprofile.resumeUrl,
          aboutMe: userprofile.aboutMe,
      };

      try {
        const updateResponse = await fetchData(`api/User/update`, "PUT", updatedProfile);
        

        userprofile.city = updateResponse.city;
        userprofile.name = updateResponse.name;
        

        userName.innerText = userprofile.name;
        userLocation.innerHTML = `
          <img src="../assets/profile-location-icon.svg" alt="Location Icon"> ${userprofile.city}
        `;
  
        profileElement.innerHTML = `
     <img src="${userprofile.profilePictureUrl?userprofile.profilePictureUrl+'?date='+Date.now() :'../assets/profile.png' }" width="60" height="60" alt="" />
      <div>
        <p>${userprofile.name.split(' ')[0]}</p>

      </div>`

      
localStorage.setItem("profile", JSON.stringify(   {name: userprofile.name.split(' ')[0],
name: userprofile.name,
profileUrl: userprofile.profilePictureUrl}));
        showToast('success', 'Success', 'Profile updated successfully');
      } catch (error) {
        console.error("Error updating profile:", error);
      
        if (error.message.includes("401")) {
          showToast('error', 'Unauthorized', 'Unauthorized access. Please login again.');

        } else if (error.message.includes("404")) {
          showToast('error', 'Not Found', 'User profile not found.');
        } else {
          showToast('error', 'Error', 'An error occurred while updating profile.');
        }
      }
      

      const profilePic = fileInput.files[0];
      if (profilePic) {
          const formData = new FormData();
          formData.append("logo", profilePic);

          try {
            const uploadResponse = await fetchData("api/User/upload-User-profilepicture", "POST", formData, true);
   
            

            profilePictureContainer.innerHTML = `<img src="${uploadResponse.logoUrl}" alt="User Profile Picture" />`;
            

            userprofile.profilePictureUrl = uploadResponse.logoUrl+'?date='+Date.now();
              profileElement.innerHTML = `
           <img src="${userprofile.profilePictureUrl?userprofile.profilePictureUrl+'?date='+Date.now() :'../assets/profile.png' }" width="60" height="60" alt="" />
            <div>
              <p>${userprofile.name.split(' ')[0]}</p>

            </div>`

            
    localStorage.setItem("profile", JSON.stringify(   {name: userprofile.name.split(' ')[0],
      name: userprofile.name,
      profileUrl: userprofile.profilePictureUrl}));
            showToast('success', 'Success', 'Profile picture uploaded successfully');
          } catch (error) {
            console.error("Error uploading profile picture:", error);
          
            if (error.message.includes("401")) {
              showToast('error', 'Unauthorized', 'Unauthorized access. Please login again.');

            } else if (error.message.includes("500")) {
              showToast('error', 'Server Error', 'Server error. Please try again later.');
            } else {
              showToast('error', 'Error', 'An error occurred while uploading profile picture.');
            }
          }
          
          
      }

      addProfileModal.style.display = "none";
      addProfileModalForm.reset();
  });

  removeFileBtn.addEventListener('click', () => {
      fileInput.value = '';
      fileNameDisplay.textContent = '';
      uploadInstruction.style.display = "block";
      fileTypes.style.display = "block";
      removeFileBtn.style.display = "none";
  });

  fileInput.addEventListener('change', (event) => {
      const fileName = event.target.files[0] ? event.target.files[0].name : '';
      fileNameDisplay.textContent = fileName;
      uploadInstruction.style.display = fileName ? "none" : "block";
      fileTypes.style.display = fileName ? "none" : "block";
      removeFileBtn.style.display = fileName ? "block" : "none";
  });

 

  editAboutMeBtn.addEventListener("click", editAboutMeHandler);

  function editAboutMeHandler() {
      const aboutMeContainer = document.getElementById("about-me-container");
      const aboutMeTextContent = aboutMeText.textContent.trim();

      const textarea = document.createElement("textarea");
      textarea.style.width = "100%";
      textarea.style.height = "130px";
      textarea.id = "about-me-textarea";
      textarea.maxLength = 255;
      textarea.value = aboutMeTextContent;

      aboutMeContainer.innerHTML = "";
      aboutMeContainer.appendChild(textarea);

      editAboutMeBtn.innerHTML = '<img src="../assets/save.png" width="20" height="20">';
      textarea.focus();

      editAboutMeBtn.removeEventListener("click", editAboutMeHandler);
      editAboutMeBtn.addEventListener("click", saveAboutMeHandler);
  }

  async function saveAboutMeHandler() {
      const aboutMeContainer = document.getElementById("about-me-container");
      const textarea = document.getElementById("about-me-textarea");
      const updatedText = textarea.value;
      
      if (!updatedText) {
        showToast('warning', 'Warning', 'About Me cannot be empty.');
        return;
      }
      const paragraph = document.createElement("p");
      const updateAboutMe = {
          dob: userprofile.dob,
          name: userprofile.name,
          address: userprofile.address,
          city: userprofile.city,
          portfolioLink: userprofile.portfolioLink,
          phoneNumber: userprofile.phoneNumber,
          resumeUrl: userprofile.resumeUrl,
          aboutMe: updatedText,
      };

      try {
        const updateResponse = await fetchData(`api/User/update`, "PUT", updateAboutMe);
        userprofile.aboutMe = updateResponse.aboutMe;
        paragraph.textContent = updateResponse.aboutMe;
        showToast('success', 'Success', 'About Me updated successfully');
      } catch (error) {
        if (error.message.includes("404")) {
          showToast('error', 'Error', 'User not found. Please try again later.');
        } else {
          showToast('error', 'Error', 'An error occurred while updating About Me. Please try again later.');
        }
      }
      

      paragraph.id = "about-me-text";
      aboutMeContainer.innerHTML = "";
      aboutMeContainer.appendChild(paragraph);

      editAboutMeBtn.innerHTML = '<img src="../assets/edit.svg" width="20" height="20">';
      editAboutMeBtn.removeEventListener("click", saveAboutMeHandler);
      editAboutMeBtn.addEventListener("click", editAboutMeHandler);
  }

  function populateData(selectId, options, displayKey, valueKey, selectedValues = []) {

      const select = document.getElementById(selectId);
      options.forEach((option) => {
          const optionElement = document.createElement("option");
          if (selectId !== "company-list") optionElement.value = option[valueKey];
          optionElement.textContent = option[displayKey];
          if (selectedValues.some(selected => selected[valueKey] === option[valueKey])) {
              optionElement.selected = true;
          }
          select.appendChild(optionElement);
      });
  }


 
 


  function yaervalidation(dob) {
    var minDate = new Date(dob);
    var minYear = minDate.getFullYear();
    var minMonth = (minDate.getMonth() + 1).toString().padStart(2, '0');
    var minFormattedDate = `${minYear}-${minMonth}`;
    

    document.getElementById('startDate').setAttribute('min', minFormattedDate);
    document.getElementById('startYear').setAttribute('min', minFormattedDate);

    var today = new Date();
    var year = today.getFullYear();
    var month = (today.getMonth() + 1).toString().padStart(2, '0');
    var day = today.getDate().toString().padStart(2, '0');
    var maxDate = `${year}-${month}-${day}`;
    document.getElementById('modal-dob').setAttribute('max', maxDate);


    var currentDate = `${year}-${month}`;
    document.getElementById('startDate').setAttribute('max', currentDate);
    document.getElementById('startYear').setAttribute('max', currentDate);
}


yaervalidation(userprofile.dob); 

  
var Updateskills=[]
  

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
},selected_values);


  document.getElementById("add-experience").addEventListener("click", function () {
    addExperienceModal.style.display = "block";
    document.getElementById("Exp-submitBtn-add").style.display = "block";
    document.getElementById("Exp-submitBtn-update").style.display = "none";
    document.getElementById("ExperienceModalTitle").firstChild.textContent =
      "Add Experience";
  });
  
  document.getElementById("add-Education").addEventListener("click", function () {
  document.getElementById("addEducationModal").style.display = "block";
  document.getElementById("Edu-submitBtn-update").style.display = "none";
  document.getElementById("Edu-submitBtn-add").style.display = "block";
  
  
    document.getElementById("EducationModalTitle").firstChild.textContent =
      "Add Education";
  });

  function formatDate(dateString) {
    const date = new Date(dateString);
    const options = { year: 'numeric', month: 'long' };
    return date.toLocaleDateString(undefined, options);
  }
function renderExperience() {
  experiencesContainer.innerHTML = ``;
  experiences.forEach(function (experience) {

    const newExperience = document.createElement("div");
    newExperience.classList.add("experience");
    newExperience.innerHTML = `
    <div class="job-title">
  ${experience.titleName}
  <button class="edit-btn edit-experience">
    <img src="../assets/edit.svg" width="20" height="20">
  </button>
  <button class="delete-btn delete-experience">
    <img src="../assets/delete.svg" width="20" height="20">
  </button>
</div>
      <p><strong class="degree-level">${experience.companyName  }</strong></p>

<div class="dates">${ formatDate(experience.startYear)} - ${formatDate(experience.endYear)}</div>
${experience.experienceDuration > 0 ? `<div class="duration">${experience.experienceDuration} Years</div>` : ''}

    `;
    experiencesContainer.appendChild(newExperience);
  });
  assignExperienceEventListeners();
}
function renderSkills() {
  skillsList.innerHTML = "";
  UserSkillsArray.forEach((skill) => {
    const skillItem = document.createElement("div");
    skillItem.className = "skill-item";
    skillItem.innerHTML = `<span>${skill.skillName}</span>`;
    skillsList.appendChild(skillItem);
  });
}
function renderAOI() {
  aoiContainer.innerHTML = ``;
  useraoi.forEach((areaOfInterest) => {
    const newaoi = document.createElement("div");
    const title = areaOfInterest.titleName;
    const lpa = areaOfInterest.lpa;
    const lpaText = lpa === 0 ? `` : `(${lpa})`;
    newaoi.classList.add("aoi-item");
    newaoi.innerHTML = `
      <h5 class="job-title">${title} ${lpaText}
        <button class="edit-btn edit-aoi"><img src="../assets/edit.svg" width="20" height="20"></button>
        <button class="delete-btn delete-aoi"><img src="../assets/delete.svg" width="20" height="20"></button>
      </h5>
    `;
    aoiContainer.appendChild(newaoi);
  });
  assignAoiEventListeners();
}  
function renderEducation() {
  educationsContainer.innerHTML = ``;
  educations.forEach(function (education) {
    const newEducation = document.createElement("div");
    newEducation.classList.add("education");

    const dates = education.isCurrentlyStudying
      ? `Currently Studying (Started ${formatDate(education.startYear)})`
      : `${formatDate(education.startYear)} - ${formatDate(education.endYear)}`;

    newEducation.innerHTML = `
      <h3 class="institution-name">${education.institutionName}
        <button class="edit-btn edit-education">
          <img src="../assets/edit.svg" width="20" height="20">
        </button>
        <button class="delete-btn delete-education">
          <img src="../assets/delete.svg" width="20" height="20">
        </button>
      </h3>
      <p><strong class="degree-level">${education.level}</strong></p>
      <p class="dates">${dates}</p>
      <p class="percentage">Percentage: ${education.percentage}%</p>
    `;
    educationsContainer.appendChild(newEducation);
  });
  assignEducationEventListeners();
}
renderExperience();
renderEducation();
renderAOI();
renderSkills();

function assignExperienceEventListeners() {
  document.querySelectorAll(".edit-experience").forEach(function (editButton, index) {
    editButton.addEventListener("click", function () {
      const experience = experiences[index];
      titleList.value = experience.titleId;
      companyInput.value = experience.companyName;
      startDateInput.value = new Date(experience.startYear).toISOString().slice(0, 7);
      endDateInput.value = new Date(experience.endYear).toISOString().slice(0, 7);

      addExperienceModal.setAttribute("data-experience-id", experience.experienceId);
      addExperienceModal.style.display = "block";
      expSubmitBtnUpdate.style.display = "block";
      expSubmitBtnAdd.style.display = "none";
      document.getElementById("ExperienceModalTitle").firstChild.textContent = "Update Experience";
    });
  });

  document.querySelectorAll(".delete-experience").forEach(function (deleteButton, index) {
    deleteButton.addEventListener("click", async function () {
      const experienceId = experiences[index].experienceId;
      try {
        await fetchData(`api/UserExperience/${experienceId}`, "DELETE");
        experiences.splice(index, 1);
        renderExperience();
        showToast('success', 'Success', 'Experience deleted successfully');
      } catch (error) {
        if (error.message.includes("404")) {
          showToast('error', 'Error', 'Experience not found. Please try again later.');
        } else {
          showToast('error', 'Error', 'An error occurred while deleting the experience. Please try again later.');
        }
      }
      
    });
  });
}

function assignEducationEventListeners() {
  document.querySelectorAll(".edit-education").forEach(function (editButton, index) {
    editButton.addEventListener("click", function () {
      const education = educations[index];
      institutionNameInput.value = education.institutionName;
      levelInput.value = education.level;
      startYearInput.value = new Date(education.startYear).toISOString().slice(0, 7);
      endYearInput.value = education.endYear ? new Date(education.endYear).toISOString().slice(0, 7) : "";
      percentageInput.value = education.percentage;
      isCurrentlyStudyingInput.checked = education.isCurrentlyStudying;

      addEducationForm.setAttribute("data-education-id", education.educationId);
      addEducationModal.style.display = "block";
      eduSubmitBtnUpdate.style.display = "block";
      eduSubmitBtnAdd.style.display = "none";
      document.getElementById("EducationModalTitle").firstChild.textContent = "Update Education";
    });
  });

  document.querySelectorAll(".delete-education").forEach(function (deleteButton, index) {
    deleteButton.addEventListener("click", async function () {
      const educationId = educations[index].educationId;
      try {
        await fetchData(`api/UserEducation/${educationId}`, "DELETE");
        educations.splice(index, 1);
        renderEducation();
        showToast('success', 'Success', 'Education deleted successfully');
      } catch (error) {
        if (error.message.includes("404")) {
          showToast('error', 'Error', 'Education not found. Please try again later.');
        } else {
          showToast('error', 'Error', 'An error occurred while deleting the education. Please try again later.');
        }
      }
      
    });
  });
}


function assignAoiEventListeners() {
  document.querySelectorAll(".delete-aoi").forEach((deleteButton, index) => {
    deleteButton.addEventListener("click", async function () {
      const AreasOfInterestId = useraoi[index].areasOfInterestId;
      try {
        await fetchData(`api/UserAreasOfInterest/${AreasOfInterestId}`, "DELETE");
        useraoi.splice(index, 1);
        showToast('success', 'Success', 'Area of interest deleted successfully')
      } catch (error) {
        if (error.message.includes("404")) {
          showToast('error', 'Error', 'Area of interest not found. Please try again later.');
        } else {
          showToast('error', 'Error', 'An error occurred while deleting the area of interest. Please try again later.');
        }
      }
      
      renderAOI();
    });
  });

  document.querySelectorAll(".edit-aoi").forEach((editButton, index) => {
    editButton.addEventListener("click", function () {
      const aoi = useraoi[index];
      document.getElementById("title-list-aoi").value = aoi.titleId;
      addAreaOfInterestForm.setAttribute("data-aoi-id", aoi.areasOfInterestId);
      document.getElementById("lpa").value = aoi.lpa;
      updateAoiBtn.style.display = "block";
      addAoiBtn.style.display = "none";
      addAreaOfInterestModal.style.display = "block";
      document.getElementById("aoiModalTitle").firstChild.textContent = "Update Area of Interest";
    });
  });
}


expSubmitBtnAdd.addEventListener("click", async function (event) {
  event.preventDefault();

  const jobTitle = titleList.value;
  const company = companyInput.value;
  const startDate = startDateInput.value;
  const endDate = endDateInput.value;

  if (!jobTitle.trim() || !company.trim() || !startDate.trim() || !endDate.trim()) {
    showToast('warning', 'Warning', 'Please fill in all required fields');
    return;
  }

  if (new Date(startDate) >= new Date(endDate)) {
    showToast('warning', 'Warning', 'End date must be greater than start date');
    return;
  }

  const startYear = new Date(startDate).toISOString().split("T")[0];
  const endYear = new Date(endDate).toISOString().split("T")[0];

  let experienceData = {
    companyName: company,
    endYear: endYear,
    startYear: startYear,
    titleId: jobTitle,
  };

  try {
    experienceData = await fetchData(`api/UserExperience`, "POST", experienceData);
    experienceData.titleName = experienceData.title.titleName;
    experiences.push(experienceData);
    addExperienceForm.reset();
    renderExperience();
    showToast('success', 'Success', 'Experience added successfully')
  } catch (error) {
    if (error.message.includes("400")) {
      showToast('error', 'Error', 'Bad request. Please check your input.');
    } else {
      showToast('error', 'Error', 'An error occurred while adding experience. Please try again later.');
    }
  }
  
  addExperienceModal.style.display = "none";
});
isCurrentlyStudyingCheckbox.addEventListener('change', () => {
  if (isCurrentlyStudyingCheckbox.checked) {
      endYearInput.value = "";
      endYearInput.disabled = true;
  } else {
      endYearInput.disabled = false;
  }
});
expSubmitBtnUpdate.addEventListener("click", async function (event) {
  event.preventDefault();

  const jobTitle = titleList.value;
  const company = companyInput.value;
  const startDate = startDateInput.value;
  const endDate = endDateInput.value;

  if (!jobTitle.trim() || !company.trim() || !startDate.trim() || !endDate.trim()) {
    showToast('warning', 'Warning', 'Please fill in all required fields');
    return;
  }

  if (new Date(startDate) >= new Date(endDate)) {
    showToast('warning', 'Warning', 'End date must be greater than start date');
    return;
  }

  const startYear = new Date(startDate).toISOString().split("T")[0];
  const endYear = new Date(endDate).toISOString().split("T")[0];

  let updateExperience = {
    experienceId: addExperienceModal.getAttribute("data-experience-id"),
    companyName: company,
    endYear: endYear,
    startYear: startYear,
    titleId: jobTitle,
  };

  try {
    updateExperience = await fetchData(`api/UserExperience`, "PUT", updateExperience);
  
    const index = experiences.findIndex((exp) => exp.experienceId === updateExperience.experienceId);
    updateExperience.titleName = updateExperience.title.titleName;
    experiences[index] = updateExperience;
  
    addExperienceForm.reset();
    renderExperience();
    showToast('success', 'Success', 'Experience updated successfully')
  } catch (error) {
    if (error.message.includes("400")) {
      showToast('error', 'Error', 'Bad request. Please check your input.');
    } else if (error.message.includes("404")) {
      showToast('error', 'Error', 'Experience not found. Please try again later.');
    } else {
      showToast('error', 'Error', 'An error occurred while updating experience. Please try again later.');
    }
  }
  
  addExperienceModal.style.display = "none";
});

eduSubmitBtnAdd.addEventListener("click", async function (event) {
  event.preventDefault();

  const institutionName = institutionNameInput.value;
  const level = levelInput.value;
  const startYearString = startYearInput.value;
  const endYearString = endYearInput.value;
  const percentage = parseFloat(percentageInput.value);
  const isCurrentlyStudying = isCurrentlyStudyingInput.checked;

  if (!institutionName.trim() || !level.trim() || !startYearString.trim()  || (!isCurrentlyStudying && !endYearString.trim() ) || !percentage) {
    showToast('warning', 'Warning', 'Please fill in all required fields');
    return;
  }

  const startYear = new Date(startYearString).toISOString().split("T")[0];
  const endYear = isCurrentlyStudying ? null : new Date(endYearString).toISOString().split("T")[0];

  if (!isCurrentlyStudying && new Date(startYear) >= new Date(endYear)) {
    showToast('warning', 'Warning', 'End year must be greater than start year');
    return;
  }
  if (percentage >= 100) {
    showToast('warning', 'Warning', 'Percentage must be less than 100');
    return;
  }
  
  const educationData = {
    institutionName,
    level,
    startYear,
    endYear,
    percentage,
    isCurrentlyStudying,
  };

  try {
    const addedEducation = await fetchData(`api/UserEducation`, "POST", educationData);
    educations.push(addedEducation);
    addEducationForm.reset();
    renderEducation();
    showToast('success', 'Success', 'Education added successfully');
  } catch (error) {
    if (error.message.includes("400")) {
      showToast('error', 'Error', 'Bad request. Please check your input.');
    } else if (error.message.includes("401")) {
      showToast('error', 'Error', 'Unauthorized access. Please login again.');
    } else {
      showToast('error', 'Error', 'An error occurred while adding education. Please try again later.');
    }
  }
  
  addEducationModal.style.display = "none";
});

eduSubmitBtnUpdate.addEventListener("click", async function (event) {
  event.preventDefault();

  const institutionName = institutionNameInput.value;
  const level = levelInput.value;
  const startYearString = startYearInput.value;
  const endYearString = endYearInput.value;
  const percentage = parseFloat(percentageInput.value);
  const isCurrentlyStudying = isCurrentlyStudyingInput.checked;

  if (!institutionName.trim() || !level.trim() || !startYearString.trim()  || (!isCurrentlyStudying && !endYearString.trim() ) || !percentage) {
    showToast('warning', 'Warning', 'Please fill in all required fields');
    return;
  }
  const startYear = new Date(startYearString).toISOString().split("T")[0];
  const endYear = isCurrentlyStudying ? null : new Date(endYearString).toISOString().split("T")[0];

  if (!isCurrentlyStudying && new Date(startYear) >= new Date(endYear)) {
    showToast('warning', 'Warning', 'End year must be greater than start year');
    return;
  }
  if (percentage >= 100) {
    showToast('warning', 'Warning', 'Percentage must be less than 100');
    return;
  }
  
  const educationId = addEducationForm.getAttribute("data-education-id");
  let updateEducation = {
    educationId,
    institutionName,
    level,
    startYear,
    endYear,
    percentage,
    isCurrentlyStudying
  };
  
  try {
    updateEducation = await fetchData(`api/UserEducation`, "PUT", updateEducation);
    const index = educations.findIndex((edu) => edu.educationId === updateEducation.educationId);
  educations[index] = updateEducation;

  addEducationForm.reset();
  renderEducation();
    showToast('success', 'Success', 'Education updated successfully');
  } catch (error) {

  
    if (error.message.includes("400")) {
      showToast('error', 'Error', 'Enter proper Input');
    } else if (error.message.includes("404")) {
      showToast('error', 'Error', 'Education not found. Please try again later.');
    } else {
      showToast('error', 'Error', 'Internal Server Error. Please try again later.');
    }
  }
  

  addEducationModal.style.display = "none";
});

addAoiBtn.addEventListener("click", async function (event) {
  event.preventDefault();
  const formData = new FormData(addAreaOfInterestForm);
  const jobTitle = formData.get("title");
  const lpa = formData.get("lpa");
  if (jobTitle.trim() === "") {
    showToast('warning', 'Warning', 'Please fill in all required fields');
    return;
  }
  const newAreaOfInterest = {
    lpa: lpa === "" ? 0 : lpa,
    titleId: jobTitle,
  };

  try {
    const addedAoi = await fetchData("api/UserAreasOfInterest", "POST", newAreaOfInterest);
    addedAoi.titleName = addedAoi.title.titleName;
    useraoi.push(addedAoi);
    

    renderAOI();
    addAreaOfInterestForm.reset();
  
    showToast('success', 'Success', 'Area of Interest added successfully');
  } catch (error) {
    if (error.message.includes("400")) {
      showToast('error', 'Error', 'Enter proper input');
    } else if (error.message.includes("404")) {
      showToast('error', 'Error', 'Area of Interest not found. Please try again later.');
    } else {
      showToast('error', 'Error', 'Internal Server Error. Please try again later.');
    }
  }
  
  addAreaOfInterestModal.style.display = "none";
});

updateAoiBtn.addEventListener("click", async function (event) {
  event.preventDefault();
  const formData = new FormData(addAreaOfInterestForm);
  const lpa = formData.get("lpa") === "" ? 0 : formData.get("lpa");
  const titleId = formData.get("title");
  const AreasOfInterestId = addAreaOfInterestForm.getAttribute("data-aoi-id");

  try {
    const updatedAoi = await fetchData("api/UserAreasOfInterest", "PUT", {
      areasOfInterestId: AreasOfInterestId,
      lpa: lpa,
      titleId: titleId,
    });
  
    updatedAoi.titleName = jobTitles.find(jobTitle => jobTitle.titleId === updatedAoi.titleId).titleName;
  
    const index = useraoi.findIndex(aoi => aoi.areasOfInterestId === updatedAoi.areasOfInterestId);
    useraoi[index] = updatedAoi;
  

    addAreaOfInterestForm.reset();
    renderAOI();
  
    showToast('success', 'Success', 'Area of Interest updated successfully');
  } catch (error) {
    console.log(err)
    if (error.message.includes("400")) {
      showToast('error', 'Error', 'Enter proper input');
    } else if (error.message.includes("404")) {
      showToast('error', 'Error', 'Area of Interest not found. Please try again later.');
    } else {
      showToast('error', 'Error', 'Internal Server Error. Please try again later.');
    }
  }
  
  addAreaOfInterestModal.style.display = "none";
});
document.querySelectorAll('.close').forEach(closeBtn => {
  closeBtn.addEventListener('click', () => {
    closeBtn.closest('.modal').style.display = 'none';
  });
});

  document.getElementById("add-AreaOfInterest").addEventListener("click", function () {
      addAreaOfInterestModal.style.display = "block";
      updateAoiBtn.style.display = "none";
      addAoiBtn.style.display = "block";

      document.getElementById("aoiModalTitle").firstChild.textContent =
        "Add Area of Interest ";
    });


    document.querySelectorAll(".edit-aoi").forEach(function (editButton, index) {
      editButton.addEventListener("click", function () {
        const aoi = useraoi[index];
        document.getElementById("title-list-aoi").value = aoi.titleId;
  
        document
          .getElementById("addAreaOfInterestForm")
          .setAttribute("data-aoi-id", aoi.areasOfInterestId);
  
  
        document.getElementById("lpa").value = aoi.lpa;
  
        updateAoiBtn.style.display = "block";
        addAoiBtn.style.display = "none";
        addAreaOfInterestModal.style.display = "block";
        document.getElementById("update-aoi-btn").textContent =
          "Update Area of Interest";
        document.getElementById("aoiModalTitle").firstChild.textContent =
          "Update Area of Interest";
      });
    });
 
  document.getElementById("edit-skills").addEventListener("click", () => {
    skillsModal.style.display = "block";
  });

  document.querySelector(".modal .close").addEventListener("click", () => {
    skillsModal.style.display = "none";
  });

  document.getElementById("edit-skills-btn").addEventListener("click", 
    async function (event) {
      Updateskills = Updateskills.map((skill) => ({
        skillId: skill.value,
        skillName: skill.label,
      }));
      const requestBody = {
        skillsToAdd: Updateskills.filter(
          (updateSkill) =>
            !UserSkillsArray.some(
              (userSkill) => userSkill.skillId === updateSkill.skillId
            )
        ).map((filteredSkill) => filteredSkill.skillId),

        skillsToRemove: UserSkillsArray.filter(
          (userSkill) =>
            !Updateskills.some(
              (updateSkill) => updateSkill.skillId === userSkill.skillId
            )
        ).map((filteredSkill) => filteredSkill.skillId),
      };
      try {
        const skillsresponse = await fetchData("api/User/skills", "POST", requestBody);
      

        UserSkillsArray = updateUserSkillsArray(UserSkillsArray, skillsresponse, skillsArray);
      
        renderSkills();
      
        showToast('success', 'Success', 'Skills added successfully');
      } catch (error) {
        console.error("Error adding skills:", error);
      
        if (error.message.includes("400")) {
          showToast('error', 'Error', 'Bad request. Please check your input.');
        } else if (error.message.includes("401")) {
          showToast('error', 'Error', 'Unauthorized access. Please login again.');
 
        } else {
          showToast('error', 'Error', 'An error occurred while adding skills. Please try again later.');
        }
      }
      
      skillsModal.style.display = "none";
    });

  const elements = document.querySelectorAll('.select2-container--default .select2-selection--single');


elements.forEach(element => {
  element.style.backgroundColor = '#fff';
  element.style.border = 'none';
  element.style.borderBottom = '2px solid rgba(214, 221, 235, 1)';
  element.style.borderRadius = '4px';
  element.style.transition = 'border-bottom-color 0.3s';
  element.style.marginBottom = '8px';
});




});

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
document.querySelector(".back-button").addEventListener("click",()=>
{
  localStorage.clear()
  window.location.href = "/Auth/login.html?authid=2";
})