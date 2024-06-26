import { fetchData } from "../../Package/api.js";
import { showToast } from "../../Package/toaster.js";

if (!localStorage.getItem("authToken")) {
  window.location.href = "/Auth/login.html?authid=3";
}

document.querySelector(".back-button").addEventListener("click", () => {
  localStorage.clear();
  window.location.href = "/Auth/login.html?authid=2";
});

const elements = {
  profile: document.querySelector(".profile"),
  name: document.querySelector(".company-name"),
  menuButton: document.getElementById("menuButton"),
  sidebar: document.getElementById("sidebar"),
  companyLogo: document.querySelector(".company-logo"),
  crossButton: document.getElementById("cross"),
  mainContent: document.querySelector(".main-content"),
  aboutMeText: document.getElementById("description-text"),
  editAboutMeBtn: document.querySelector(".edit-description"),
  editAdditionalDetailsModal: document.getElementById("editAdditionalDetailsModal"),
  editBtn: document.querySelector(".edit-additional-details"),
  updateDetailsForm: document.getElementById("editAdditionalDetailsForm"),
  modalClose: document.querySelector('.modal-close'),
  Logo: document.getElementById('companylogo'),
  crossButton: document.getElementById('cross'),
  mainContent: document.querySelector('.main-content')

};

let Profile;

async function loadProfile() {
  try {
    Profile = await fetchData("api/Company/profile");
    renderProfile(Profile);
  } catch (error) {
    console.error("Error fetching profile:", error);
    handleFetchError(error);
  }
}

function renderProfile(profile) {
  console.log(profile.logoUrl)
  elements.profile.innerHTML = `
    <img src="${profile.logoUrl ?profile.logoUrl+'?date='+Date.now()  :"../../assets/Company.png"}" width="60" height="60" alt="" />
    <div>
      <p>${profile.companyName.split(" ")[0]}</p>
    </div>
  `;

  elements.aboutMeText.textContent = profile.companyDescription;
  elements.name.textContent = profile.companyName;
elements.companyLogo.src=profile.logoUrl ?profile.logoUrl+'?date='+Date.now()  :"../../assets/Company.png"



  document.querySelector(".detail-text.location").textContent = profile.companyAddress;
  document.querySelector(".detail-text.email").textContent = profile.email;
  document.querySelector(".detail-text.size").textContent = profile.companySize;
  document.querySelector(".detail-text.address").textContent = profile.city;

  const companyWebsiteElement = document.querySelector(".detail-text.website");
  if (profile.companyWebsite) {
    companyWebsiteElement.innerHTML = `<a href="${profile.companyWebsite}" class="website-link" target="_blank" rel="noopener noreferrer">${profile.companyWebsite}</a>`;
  } else {
    companyWebsiteElement.textContent = "";
  }
}

function handleFetchError(error) {
  if (error.message.includes("401")) {
    showToast('error', 'Unauthorized', 'Unauthorized access. Please login again.');
  } else {
    showToast('error', 'Error', 'An error occurred while fetching profile details.');
  }
}

function updateProfileUI(profile) {
  document.querySelector(".detail-text.location").textContent = profile.companyAddress;
  document.querySelector(".detail-text.size").textContent = profile.companySize;
  document.querySelector(".detail-text.address").textContent = profile.city;

  const companyWebsiteElement = document.querySelector(".detail-text.website");
  if (profile.companyWebsite) {
    companyWebsiteElement.innerHTML = `<a href="${profile.companyWebsite}" class="website-link" target="_blank" rel="noopener noreferrer">${profile.companyWebsite}</a>`;
  } else {
    companyWebsiteElement.textContent = "";
  }
}

function editAboutMeHandler() {
  const aboutMeContainer = document.getElementById("description-container");
  const aboutMeTextContent = elements.aboutMeText.textContent.trim();

  const textarea = document.createElement("textarea");
  textarea.style.width = "100%";
  textarea.style.height = "130px";
  textarea.id = "description-textarea";
  textarea.maxLength = 255;
  textarea.value = aboutMeTextContent;

  aboutMeContainer.innerHTML = "";
  aboutMeContainer.appendChild(textarea);

  elements.editAboutMeBtn.innerHTML = '<img src="../../assets/save.png" width="20" height="20">';
  textarea.focus();

  elements.editAboutMeBtn.removeEventListener("click", editAboutMeHandler);
  elements.editAboutMeBtn.addEventListener("click", saveAboutMeHandler);
}

async function saveAboutMeHandler() {
  const aboutMeContainer = document.getElementById("description-container");
  const textarea = document.getElementById("description-textarea");
  const updatedText = textarea.value;

  if (!updatedText) {
    showToast("warning", "Warning", "About Me cannot be empty.");
    return;
  }

  const updateAboutMe = {
    CompanyName: Profile.companyName,
    CompanyAddress: Profile.companyAddress,
    City: Profile.city,
    CompanySize: Profile.companySize,
    CompanyWebsite: Profile.companyWebsite,
    CompanyDescription: updatedText,
  };

  try {
    const updateResponse = await fetchData("api/Company", "PUT", updateAboutMe);
    Profile.companyDescription = updateResponse.companyDescription;
    const paragraph = document.createElement("p");
    paragraph.textContent = Profile.companyDescription;
    paragraph.id = "description-text";
    aboutMeContainer.innerHTML = "";
    aboutMeContainer.appendChild(paragraph);
    showToast("success", "Success", "Company Profile updated successfully");
  } catch (error) {
    handleSaveError(error);
  }

  elements.editAboutMeBtn.innerHTML = '<img src="../../assets/edit.svg" width="20" height="20">';
  elements.editAboutMeBtn.removeEventListener("click", saveAboutMeHandler);
  elements.editAboutMeBtn.addEventListener("click", editAboutMeHandler);
}

function handleSaveError(error) {
  if (error.message.includes("404")) {
    showToast("error", "Error", "User not found. Please try again later.");
  } else {
    showToast("error", "Error", "An error occurred while updating About Me. Please try again later.");
  }
}

elements.menuButton.addEventListener('click', function () {
  sidebar.classList.toggle('hidden');
  elements.Logo.style.display = 'none';
  elements.crossButton.style.display = 'block';
  elements.mainContent.classList.toggle('expanded');
});

elements.crossButton.addEventListener('click', function () {
  sidebar.classList.toggle('hidden');
  elements.mainContent.classList.toggle('expanded');

  if (elements.Logo.style.display === 'none' || elements.Logo.style.display === '') {
    elements.companyLogo.style.display = 'block';
    elements.crossButton.style.display = 'none';
  } else {
    elements.companyLogo.style.display = 'none';
    elements.crossButton.style.display = 'block';
  }
});

elements.editBtn.addEventListener('click', () => {
  openEditModal();
});

document.querySelector('.custom-file-upload').addEventListener('click', function () {
  document.getElementById('file-upload').click();
});
document.getElementById('file-upload').addEventListener('change', async function(event) {
  const file = event.target.files[0];
  
  if (file) {
    try {
      const formData = new FormData();
      formData.append('logo', file);

      const updateResponse = await fetchData("api/Company/upload-logo", "POST", formData, true);
      
console.log(Profile.logoUrl)
      console.log(updateResponse.logoUrl);
      

      Profile.logoUrl = updateResponse.logoUrl+'?date='+Date.now();

renderProfile(Profile); 

    } catch (error) {
      console.error("Error uploading profile picture:", error);

      let errorMessage = 'An error occurred while uploading profile picture.';
      if (error.message.includes("500")) {
        errorMessage = 'Server error. Please try again later.';
      }
      
      showToast('error', 'Error', errorMessage);
    }
  }
});


function openEditModal() {
  elements.editAdditionalDetailsModal.style.display = 'block';
  document.getElementById('modal-email').value = document.querySelector('.detail-text.email').textContent.trim();
  document.getElementById('modal-location').value = document.querySelector('.detail-text.location').textContent.trim();
  document.getElementById('modal-size').value = document.querySelector('.detail-text.size').textContent.trim();
  document.getElementById('modal-address').value = document.querySelector('.detail-text.address').textContent.trim();
  document.getElementById('modal-website').value = document.querySelector('.detail-text.website a')?.href || '';

  elements.modalClose.addEventListener('click', () => {
    elements.editAdditionalDetailsModal.style.display = 'none';
  });

  window.addEventListener('click', (event) => {
    if (event.target == elements.editAdditionalDetailsModal) {
      elements.editAdditionalDetailsModal.style.display = 'none';
    }
  });
}

elements.updateDetailsForm.addEventListener('submit', async function (event) {
  event.preventDefault();
  await submitEditForm();
});

async function submitEditForm() {
  const formData = new FormData(elements.updateDetailsForm);

  const location = formData.get('modal-location');
  const size = formData.get('modal-size');
  const address = formData.get('modal-address');
  const website = formData.get('modal-website');

  if (!location.trim() || !size.trim() || !address.trim() || !website.trim()) {
    showToast('warning', 'Warning', 'Please fill in all required fields.');
    return;
  }

  if (website.length > 200) {
    showToast('warning', 'Warning', 'Website link should not exceed 200 characters.');
    return;
  }

  const updateProfile = {
    CompanyName: Profile.companyName,
    CompanyAddress: address,
    City: location,
    CompanySize: size,
    CompanyWebsite: website,
    CompanyDescription: Profile.companyDescription,
  };

  try {
    const updateResponse = await fetchData("api/Company", "PUT", updateProfile);
    Profile.city = updateResponse.city;
    Profile.companySize = updateResponse.companySize;
    Profile.companyAddress = updateResponse.companyAddress;
    Profile.companyWebsite = updateResponse.companyWebsite;
    updateProfileUI(Profile);
    showToast('success', 'Success', 'Profile details updated successfully');
  } catch (error) {
    console.error("Error updating profile details:", error);
    handleSaveError(error);
  }

  elements.editAdditionalDetailsModal.style.display = 'none';
}

elements.editAboutMeBtn.addEventListener("click", editAboutMeHandler);

loadProfile();
