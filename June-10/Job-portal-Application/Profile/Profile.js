document.addEventListener("DOMContentLoaded", async function () {
  const token =
    "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9eyJpZCI6ImVhZGU3YTU1LTg2NjgtNDViNy04MDk5LWY2NjRkMGFlM2RjNiIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IlVzZXIiLCJleHAiOjE3MTg5MzU4MDF9.YCzatpM8CwXLsCxDQ27hKKBCC5WOyjou9RxBLnN5nDE";

  localStorage.setItem("authToken", token);

  const baseUrl = "http://localhost:5117/";

  async function fetchData(url, httpMethod = "GET", body = null) {
    // try {
      const response = await fetch(`${baseUrl}${url}`, {
        method: httpMethod,
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${localStorage.getItem("authToken")}`,
        },
        body: body ? JSON.stringify(body) : undefined,
      });

      if (!response.ok) {
        throw new Error("Network response was not ok");
      }

      const data = await response.json();
      return data; // Return the data fetched from the API
    // } catch (error) {
    //   console.error("Error fetching data:", error);
    //   throw error; // Re-throw the error for handling outside of this function
    // }
  }

  document.getElementById("cross").style.display = "none";

  document.getElementById("menuButton").addEventListener("click", function () {
    document.getElementById("sidebar").classList.toggle("hidden");
    document.getElementById("company-logo").style.display = "none";
    document.getElementById("cross").style.display = "block";
    document.querySelector(".main-content").classList.toggle("expanded");
  });

  document.getElementById("cross").addEventListener("click", function () {
    const sidebar = document.getElementById("sidebar");
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
    $("#title-list-aoi").select2();
    $("#title-list").select2();
  });
  const userprofile=[]
  try {
    userprofile = await fetchData("api/User/profile");
    // Handle successful update
  } catch (error) {
    // Handle error
    if (error) {
    console.log(error)
      console.log(`Error updatidbfbdfng data: ${error.message}, Status Code: ${error.response.status}`);
    } else if (error.request) {
      // The request was made but no response was received
      console.error("No response received:", error.request);
    } else {
      // Something happened in setting up the request that triggered an Error
      console.error("Error setting up the request:", error.message);
    }
  }
  


  const skillsArray = await fetchData("api/Skill");
  const educations = userprofile.educations;
  const experience = userprofile.experiences;

  console.log(userprofile);
  var UserSkillsArray = userprofile.userSkills;
  var useraoi = await fetchData("api/UserAreasOfInterest");
  console.log(educations);
  const jobTitles = await fetchData("api/Title");
  const companies = [
    { companyId: "1", companyName: "Google" },
    { companyId: "2", companyName: "Microsoft" },
  ];

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
      if ("company-list" != selectId) optionElement.value = option[valueKey];

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
  var Updateskills = [];
  populateData("title-list", jobTitles, "titleName", "titleId");
  populateData("title-list-aoi", jobTitles, "titleName", "titleId");
  populateData("company-list", companies, "companyName", "companyId");
  populateData(
    "skill-list",
    skillsArray,
    "skillName",
    "skillId",
    UserSkillsArray
  );

  new MultiSelectTag("skill-list", {
    rounded: true, // default true
    shadow: true, // default false
    placeholder: "Search", // default Search..
    tagColor: {
      textColor: "#327b2c",
      borderColor: "#92e681",
      bgColor: "#eaffe6",
    },
    onChange: function (values) {
      Updateskills = values;
    },
  });

  ///////////////
  document.getElementById("about-me-text").textContent = userprofile.aboutMe;
  const editButton = document.querySelector(".edit-about-me");

  function editAboutMeHandler() {
    const aboutMeContainer = document.getElementById("about-me-container");
    const aboutMeText = document
      .getElementById("about-me-text")
      .textContent.trim();

    // Create a textarea element
    const textarea = document.createElement("textarea");
    textarea.style.width = "100%";
    textarea.style.height = "130px";
    textarea.id = "about-me-textarea";
    textarea.maxLength = 255;
    textarea.value = aboutMeText;

    aboutMeContainer.innerHTML = "";
    aboutMeContainer.appendChild(textarea);

    editButton.innerHTML =
      '<img src="../assets/save.png" width="20" height="20">';

    textarea.focus();

    editButton.removeEventListener("click", editAboutMeHandler);

    editButton.addEventListener("click", saveAboutMeHandler);
  }

  async function saveAboutMeHandler() {
    const aboutMeContainer = document.getElementById("about-me-container");
    const textarea = document.getElementById("about-me-textarea");
    const updatedText = textarea.value;

    const paragraph = document.createElement("p");

    updateaboutme = {
      dob: "1985-04-12",
      name: "Gayathri",
      address: "123 New Street",
      city: "",
      portfolioLink: "http://www.newportfolio.com",
      phoneNumber: "1234567890",
      resumeUrl: "http://www.newresume.com/resume.pdf",
      Aboutme: `${updatedText}`,
    };
    try {
      updateaboutme = await fetchData(`api/User/update`, "PUT", updateaboutme);
      // Handle successful update
    } catch (error) {
      // Handle error
      if (error.response) {
        // The request was made and the server responded with a status code
        console.error(`Error updating data: ${error.message}, Status Code: ${error.response.status}`);
      } else if (error.request) {
        // The request was made but no response was received
        console.error("No response received:", error.request);
      } else {
        // Something happened in setting up the request that triggered an Error
        console.error("Error setting up the request:", error.message);
      }
    }
    
    paragraph.id = "about-me-text";
    console.log(updateaboutme);
    paragraph.textContent = updateaboutme.aboutMe;

    aboutMeContainer.innerHTML = "";
    aboutMeContainer.appendChild(paragraph);

    editButton.innerHTML =
      '<img src="../assets/edit.svg" width="20" height="20">';

    editButton.removeEventListener("click", saveAboutMeHandler);

    editButton.addEventListener("click", editAboutMeHandler);
  }

  let activeToasts = [];

   function showToast(type, message1, message2, duration = 5000) {
    const toastContainer = document.createElement('div');
    toastContainer.classList.add('toast', type);
  
    let iconClass;
    switch(type) {
      case 'success':
        iconClass = 'fas fa-check';
        break;
      case 'error':
        iconClass = 'fas fa-times';
        break;
      case 'warning':
        iconClass = 'fas fa-exclamation';
        break;
      default:
        iconClass = 'fas fa-info';
    }
  
    toastContainer.innerHTML = `
      <div class="toast-content">
        <i class="${iconClass} check"></i>
        <div class="message">
          <span class="text text-1">${message1}</span>
          <span class="text text-2">${message2}</span>
        </div>
      </div>
      <i class="fas fa-times close"></i>
      <div class="progress"></div>
    `;
  
    const toastWrapper = document.querySelector('.toast-container') || createToastWrapper();
    toastWrapper.appendChild(toastContainer);
  
    const closeIcon = toastContainer.querySelector(".close");
    const progress = toastContainer.querySelector(".progress");
  
    activeToasts.push(toastContainer);
    updateToastPositions();
  
    toastContainer.classList.add("active");
    progress.classList.add("active");
  
    let timer1, timer2;
  
    timer1 = setTimeout(() => {
      toastContainer.classList.remove("active");
    }, duration);
  
    timer2 = setTimeout(() => {
      progress.classList.remove("active");
      toastWrapper.removeChild(toastContainer);
      activeToasts = activeToasts.filter(toast => toast !== toastContainer);
      updateToastPositions();
    }, duration + 300);
  
    closeIcon.addEventListener("click", () => {
      toastContainer.classList.remove("active");
  
      setTimeout(() => {
        progress.classList.remove("active");
        toastWrapper.removeChild(toastContainer);
        activeToasts = activeToasts.filter(toast => toast !== toastContainer);
        updateToastPositions();
      }, 300);
  
      clearTimeout(timer1);
      clearTimeout(timer2);
    });
  }
  
  function createToastWrapper() {
    const toastWrapper = document.createElement('div');
    toastWrapper.classList.add('toast-container');
    document.body.appendChild(toastWrapper);
    return toastWrapper;
  }
  
  function updateToastPositions() {
    activeToasts.forEach((toast, index) => {
      toast.style.top = `${index * (toast.offsetHeight + 10)}px`;
    });
  }






  
  var minDateString = userprofile.dob

  // Parse the date string
  var minDate = new Date(minDateString);

  // Extract year and month
  var minYear = minDate.getFullYear();
  var minMonth = (minDate.getMonth() + 1).toString().padStart(2, '0'); 

  // Format the date as YYYY-MM
  var minFormattedDate = minYear + '-' + minMonth;

  // Set the min attribute of the startDate input
  document.getElementById('startDate').setAttribute('min', minFormattedDate);
  document.getElementById('startYear').setAttribute('min', minFormattedDate);

  // Get the current date
  var today = new Date();
  var year = today.getFullYear();
  var month = (today.getMonth() + 1).toString().padStart(2, '0'); // Pad month with leading zero if necessary

  // Format the date as YYYY-MM
  var currentDate = year + '-' + month;
console.log(currentDate,minFormattedDate)
  // Set the max attribute of the startDate input
  document.getElementById('startDate').setAttribute('max', currentDate);
  document.getElementById('startYear').setAttribute('max', currentDate);

  editButton.addEventListener("click", editAboutMeHandler);

  function renderExperience() {
    document.querySelector(".Experiences").innerHTML = ``;
    experience.forEach(function (experience) {
      var newExperience = document.createElement("Experience");
      newExperience.classList.add("experience");
      newExperience.innerHTML = `
          <h3 class="job-title">${experience.titleName}<button class="edit-btn"><img src="../assets/edit.svg" width="20" height="20"></button></h3>
          <p><strong class="company-name">${experience.companyName}</strong></p>
          <p class="dates">${experience.startYear} - ${experience.endYear} </p>
          <p>${experience.experienceDuration}</p>
        `;

      document.querySelector(".Experiences").appendChild(newExperience);
    });
  }

  renderExperience();
  document.getElementById("Exp-submitBtn-add").addEventListener("click", async function (event) {
    event.preventDefault();
  
    // Collect values from form inputs
    var jobTitle = document.getElementById("title-list").value;
    var company = document.getElementById("company").value;
    var startDate = document.getElementById("startDate").value;
    var endDate = document.getElementById("endDate").value;
  
    // Validate required fields
    if (!jobTitle.trim() || !company.trim() || !startDate.trim() || !endDate.trim()) {
      showToast('warning', 'Warning', 'Please fill in all required fields');
      return;
    }
  
    // Validate start date and end date
    if (new Date(startDate) >= new Date(endDate)) {
      showToast('warning', 'Warning', 'End date must be greater than start date');
      return;
    }
  
    var startYear = new Date(startDate).toISOString().split("T")[0];
    var endYear = new Date(endDate).toISOString().split("T")[0];
  
    var experienceData = {
      companyName: company,
      endYear: endYear,
      startYear: startYear,
      titleId: jobTitle, // Assuming jobTitle is the titleId in your case
    };
    experience.push(
      await fetchData(`api/UserExperience`, "POST", experienceData)
    );
  
    document.getElementById("addExperienceForm").reset();
    renderExperience();
  
    Experiencemodal.style.display = "none";
  });
  
  
  document
    .getElementById("Exp-submitBtn-update")
    .addEventListener("click", async function (event) {
      event.preventDefault();
      var jobTitle = document.getElementById("title-list").value;
      var company = document.getElementById("company").value;
      var startDate = document.getElementById("startDate").value;
      var endDate = document.getElementById("endDate").value;

      Experiencemodal.style.display = "none";
    });

  // Modal logic
  var Experiencemodal = document.getElementById("addExperienceModal");
  var Educationmodal = document.getElementById("addEducationModal");
  var skilsModal = document.getElementById("skilsModal");
  var updateaoibtn = document.getElementById("update-aoi-btn");
  var addaoibtn = document.getElementById("add-aoi-btn");

  document
    .getElementById("add-experience")
    .addEventListener("click", function () {
      Experiencemodal.style.display = "block";

      document.getElementById("Exp-submitBtn-add").style.display = "block";
      document.getElementById("Exp-submitBtn-update").style.display = "none";
      document.getElementById("ExperienceModalTitle").firstChild.textContent =
        "Add Experience";
    });

  document.getElementById("addExperienceForm").onsubmit = function (event) {
    event.preventDefault();
    var jobTitle = document.getElementById("title-list").value;
    var company = document.getElementById("company").value;
    var startDate = document.getElementById("startDate").value;
    var endDate = document.getElementById("endDate").value;
    var duration = "1y 1m";
    console.log(jobTitle);
    Experiencemodal.style.display = "none";
  };

  //edit and delete not woking aftre render
  //options vale set
  //open diolog box

  function renderEducation() {
    document.querySelector(".educations").innerHTML = ``;
    educations.forEach(function (education) {
      var newEducation = document.createElement("education");
      newEducation.classList.add("education");
      var dates = education.isCurrentlyStudying
        ? `Currently Studying (Started ${education.startYear})`
        : `${education.startYear} - ${education.endYear}`;

      newEducation.innerHTML = `
          <h3 class="institution-name">${education.institutionName}<button class="edit-btn edit-education"><img src="../assets/edit.svg" width="20" height="20"></button><button class="delete-btn delete-education"><img src="../assets/delete.svg" width="20" height="20"></button></h3>
          <p><strong class="degree-level">${education.level}</strong></p>
          <p class="dates">${dates}</p>
          <p class="percentage">Percentage: ${education.percentage}%</p>
      `;

      document.querySelector(".educations").appendChild(newEducation);
    });
  }

  renderEducation();
  renderAOI();
  document
    .querySelectorAll(".delete-education")
    .forEach(function (deleteButton, index) {
      deleteButton.addEventListener("click", async function () {
        const educationId = educations[index].educationId;

        await fetchData(`api/UserEducation/${educationId}`, "DELETE");
        educations.splice(index, 1);
        renderEducation();
      });
    });

  document
    .getElementById("add-Education")
    .addEventListener("click", function () {
      document.getElementById("addEducationModal").style.display = "block";
      document.getElementById("Edu-submitBtn-update").style.display = "none";
      document.getElementById("Edu-submitBtn-add").style.display = "block";

      document.getElementById("EducationModalTitle").firstChild.textContent =
        "Add Education";
    });

  document
    .querySelectorAll(".edit-education")
    .forEach(function (editButton, index) {
      editButton.addEventListener("click", function () {
        const education = educations[index];
        document.getElementById("institutionName").value =
          education.institutionName;

        document
          .getElementById("addEducationForm")
          .setAttribute("data-Education-id", education.educationId);

        document.getElementById("level").value = education.level;
        document.getElementById("startYear").value = new Date(
          education.startYear
        )
          .toISOString()
          .slice(0, 7);
        document.getElementById("endYear").value = education.endYear
          ? new Date(education.endYear).toISOString().slice(0, 7)
          : "";
        document.getElementById("percentage").value = education.percentage;
        console.log(education.isCurrentlyStudying);
        document.getElementById("isCurrentlyStudying").value =
          education.isCurrentlyStudying;

        document.getElementById("addEducationModal").style.display = "block";
        document.getElementById("Edu-submitBtn-update").style.display = "block";
        document.getElementById("Edu-submitBtn-add").style.display = "none";

        document.getElementById("EducationModalTitle").firstChild.textContent =
          "Update Education";
      });
    });

    document.getElementById("Edu-submitBtn-add").addEventListener("click", async function (event) {
      event.preventDefault();
      var institutionName = document.getElementById("institutionName").value;
      var level = document.getElementById("level").value;
      var startYearString = document.getElementById("startYear").value;
      var endYearString = document.getElementById("endYear").value;
      var percentage = parseFloat(document.getElementById("percentage").value);
      var isCurrentlyStudying = document.getElementById("isCurrentlyStudying").checked;
    
      // Validate required fields
      if (!institutionName || !level || !startYearString || (!isCurrentlyStudying && !endYearString)) {
        showToast('warning', 'Warning', 'Please fill in all required fields');
        return;
      }
    
      // Validate start year format

      if (new Date(startYearString) >= new Date(endYearString)) {
        showToast('warning', 'Warning', 'End date must be greater than start date');
        return;
      }
      if (!isCurrentlyStudying && !endYearString) {
        showToast('warning', 'Warning', 'Invalid end year ');
        return;
      }
    
      // Validate end year format if not currently studying

      var startYear = new Date(startYearString).toISOString().split("T")[0];
      var endYear = isCurrentlyStudying ? null : new Date(endYearString).toISOString().split("T")[0];
    
      var neweducation = {
        level: level,
        startYear: startYear,
        endYear: endYear,
        percentage: percentage,
        institutionName: institutionName,
        isCurrentlyStudying: isCurrentlyStudying,
      };
    
      educations.push(
        await fetchData(`api/UserEducation`, "POST", neweducation)
      );
    
      document.getElementById("addEducationForm").reset();
      renderEducation();
      document.getElementById("addEducationModal").style.display = "none";
    });
    
    document.getElementById("Edu-submitBtn-update").addEventListener("click", async function (event) {
      event.preventDefault();
    
      var institutionName = document.getElementById("institutionName").value;
      var level = document.getElementById("level").value;
      var startYearString = document.getElementById("startYear").value;
      var endYearString = document.getElementById("endYear").value;
      var percentage = parseFloat(document.getElementById("percentage").value);
      var isCurrentlyStudying = document.getElementById("isCurrentlyStudying").checked;
    
      // Validate required fields
      if (!institutionName || !level || !startYearString || (!isCurrentlyStudying && !endYearString)) {
        showToast('warning', 'Warning', 'Please fill in all required fields');
        return;
      }
    
      // Validate start year format

      if (new Date(startYearString) >= new Date(endYearString)) {
        showToast('warning', 'Warning', 'End date must be greater than start date');
        return;
      }
    
      // Validate end year format if not currently studying
      if (!isCurrentlyStudying && !endYearString) {
        showToast('warning', 'Warning', 'Invalid end year ');
        return;
      }
    
      var startYear = new Date(startYearString).toISOString().split("T")[0];
      var endYear = isCurrentlyStudying ? null : new Date(endYearString).toISOString().split("T")[0];
    
      var updatededucation = {
        educationId: document.getElementById("addEducationForm").getAttribute("data-Education-id"),
        level: level,
        startYear: startYear,
        endYear: endYear,
        percentage: percentage,
        institutionName: institutionName,
        isCurrentlyStudying: isCurrentlyStudying,
      };
    
      updatededucation = await fetchData(`api/UserEducation`, "PUT", updatededucation);
      console.log(updatededucation);
      const index = educations.findIndex((edu) => edu.educationId === updatededucation.educationId);
      educations[index] = updatededucation;
    
      document.getElementById("addEducationForm").reset();
      renderEducation();
      document.getElementById("addEducationModal").style.display = "none";
    });
    

    
  document
    .getElementById("isCurrentlyStudying")
    .addEventListener("change", function () {
      var endYearInput = document.getElementById("endYear");
      if (this.checked) {
        endYearInput.value = "";
        endYearInput.disabled = true; // Optional: Disable the end year field
      } else {
        endYearInput.disabled = false; // Optional: Enable the end year field
      }
    });

  var addAreaOfInterestModal = document.getElementById(
    "addAreaOfInterestModal"
  );

  /////////////////////////////////
  document
    .getElementById("add-AreaOfInterest")
    .addEventListener("click", function () {
      addAreaOfInterestModal.style.display = "block";
      updateaoibtn.style.display = "none";
      addaoibtn.style.display = "block";

      document.getElementById("aoiModalTitle").firstChild.textContent =
        "Add Area of Interest ";
    });

  document.querySelectorAll(".close").forEach(function (closeButton) {
    closeButton.addEventListener("click", function () {
      addAreaOfInterestModal.style.display = "none";
      Experiencemodal.style.display = "none";
      skilsModal.style.display = "none";
      Educationmodal.style.display = "none";
    });
  });

  function renderAOI() {
    var aoiContainer = document.querySelector(".aoi-items");
    aoiContainer.innerHTML = ``;

    useraoi.forEach(function (areaOfInterest) {
      var newaoi = document.createElement("div");

      var title = areaOfInterest.title.titleName;

      var lpa = areaOfInterest.lpa;

      newaoi.classList.add("aoi-item");

      var lpaText = lpa === 0 ? `` : `(${lpa})`;

      newaoi.innerHTML = `
            <h5 class="job-title">${title} ${lpaText}<button class="edit-btn edit-aoi"><img src="../assets/edit.svg" width="20" height="20"></button><button class="delete-btn delete-aoi"><img src="../assets/delete.svg" width="20" height="20"></button></h5>
        `;

      aoiContainer.appendChild(newaoi);
    });
  }

  renderAOI();
  document
    .querySelectorAll(".delete-aoi")
    .forEach(function (deleteButton, index) {
      deleteButton.addEventListener("click", async function () {
        const areasOfInterestId = useraoi[index].areasOfInterestId;

        await fetchData(
          `api/UserAreasOfInterest/${areasOfInterestId}`,
          "DELETE"
        );
        useraoi.splice(index, 1);
        renderAOI();
      });
    });

  document.querySelectorAll(".edit-aoi").forEach(function (editButton, index) {
    editButton.addEventListener("click", function () {
      const aoi = useraoi[index];
      document.getElementById("title-list-aoi").value = aoi.titleId;

      document
        .getElementById("addAreaOfInterestForm")
        .setAttribute("data-aoi-id", aoi.areasOfInterestId);

      document.getElementById("lpa").value = aoi.lpa;

      updateaoibtn.style.display = "block";
      addaoibtn.style.display = "none";
      addAreaOfInterestModal.style.display = "block";
      document.getElementById("update-aoi-btn").textContent =
        "Update Area of Interest";
      document.getElementById("aoiModalTitle").firstChild.textContent =
        "Update Area of Interest";
    });
  });

  document
    .getElementById("add-aoi-btn")
    .addEventListener("click", async function (event) {
      event.preventDefault();
      const form = document.getElementById("addAreaOfInterestForm");
      const formData = new FormData(form);
      const jobTitle = formData.get("title");
      const lpa = formData.get("lpa");

      const newAreaOfInterest = {
        lpa: lpa == "" ? 0 : lpa,
        titleId: jobTitle,
      };
      console.log(newAreaOfInterest);
      addaoi = await fetchData(
        "api/UserAreasOfInterest",
        "POST",
        newAreaOfInterest
      );
      delete addaoi.userId;
      delete addaoi.titleId;

      useraoi.push(addaoi);
      renderAOI();
      addAreaOfInterestModal.style.display = "none";
    });

  document
    .getElementById("update-aoi-btn")
    .addEventListener("click", async function (event) {
      event.preventDefault();

      const form = document.getElementById("addAreaOfInterestForm");
      const formData = new FormData(form);
      var lpa = formData.get("lpa");
      lpa = lpa == "" ? 0 : lpa;
      const titleId = formData.get("title");

      const areasOfInterestId = form.getAttribute("data-aoi-id");

      const dataObject = {
        areasOfInterestId: areasOfInterestId,
        lpa: lpa,
        titleId: titleId,
      };

      updatedAoi = await fetchData(
        "api/UserAreasOfInterest",
        "PUT",
        dataObject
      );

      updatedAoi.title = jobTitles.find(
        (jobTitle) => jobTitle.titleId == updatedAoi.titleId
      );

      delete updatedAoi.userId;
      delete updatedAoi.titleId;

      const index = useraoi.findIndex(
        (aoi) => aoi.areasOfInterestId === updatedAoi.areasOfInterestId
      );
      useraoi[index] = updatedAoi;

      form.reset();
      renderAOI();
      addAreaOfInterestModal.style.display = "none";
    });

  document.getElementById("edit-skills").addEventListener("click", function () {
    skilsModal.style.display = "block";
  });
  // Skills modal logic
  const skillsList = document.getElementById("skills-list");

  // Function to render skills
  function renderSkills() {
    skillsList.innerHTML = "";
    UserSkillsArray.forEach((skill) => {
      const skillItem = document.createElement("div");
      skillItem.className = "skill-item";
      skillItem.innerHTML = `<span>${skill.skillName}</span>`;
      skillsList.appendChild(skillItem);
    });
  }

  document.getElementById("edit-skills").addEventListener("click", function () {
    skilsModal.style.display = "block";
  });

  // Close modal
  document
    .querySelector(".modal .close")
    .addEventListener("click", function () {
      skilsModal.style.display = "none";
    });

  document
    .getElementById("edits-skils-btn")
    .addEventListener("click", async function (event) {
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

      skillsresponse = await fetchData("api/User/skills", "POST", requestBody);
      UserSkillsArray = updateUserSkillsArray(
        UserSkillsArray,
        skillsresponse,
        skillsArray
      );
      console.log(UserSkillsArray);
      renderSkills();
      skilsModal.style.display = "none";
    });

  renderSkills();
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
