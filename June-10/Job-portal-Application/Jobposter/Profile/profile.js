import { fetchData } from "../../Pakage/api.js";
import { showToast } from "../../Pakage/toster.js";

  if(!localStorage.getItem('authToken'))
    {
      window.location.href = "/Auth/login.html?authid=3";

    }

    
    document.querySelector(".back-button").addEventListener("click",()=>
      {
        localStorage.clear()
        window.location.href = "/Auth/login.html?authid=2";
      })

  const profileElement = document.querySelector(".profile");
  const nameElement = document.querySelector('.company-name');
  const menuButton = document.getElementById('menuButton');
  const sidebar = document.getElementById('sidebar');
  const companyLogo = document.getElementById('company-logo');
  const crossButton = document.getElementById('cross');
  const mainContent = document.querySelector('.main-content');
  document.getElementById('editButton').addEventListener('click', function() {
    document.getElementById('modal').style.display = 'block';
  });
  
  document.getElementById('closeButton').addEventListener('click', function() {
    document.getElementById('modal').style.display = 'none';
  });
  
  window.onclick = function(event) {
    if (event.target === document.getElementById('modal')) {
      document.getElementById('modal').style.display = 'none';
    }
  };
  
  document.getElementById('editForm').addEventListener('submit', function(event) {
    event.preventDefault();
    // Handle form submission, e.g., save data and close modal
    document.getElementById('modal').style.display = 'none';
  });


  const editAboutMeBtn = document.querySelector(".edit-description");

  const aboutMeText = document.getElementById("description-text");
  var Profile;
  try {
     Profile = await fetchData("api/Company/profile");
console.log(Profile)
    profileElement.innerHTML = `
  <img src="${Profile.logoUrl?Profile.logoUrl :'../assets/Company.png' }" width="60" height="60" alt="" />
      <div>
        <p>${Profile.companyName.split(' ')[0]}</p>

      </div>
    `;


    document.querySelector(".company-logo").src =Profile.logoUrl?Profile.logoUrl :'../assets/Company.png'
 
    aboutMeText.textContent = Profile.companyDescription;
    nameElement.textContent = Profile.companyName;
  } catch (error) {
    console.log(error)
    console.log("sisydg")
    if (error.message.includes("401")) {
    }
  }




  editAboutMeBtn.addEventListener("click", editAboutMeHandler);

  function editAboutMeHandler() {
      const aboutMeContainer = document.getElementById("description-container");
      const aboutMeTextContent = aboutMeText.textContent.trim();

      const textarea = document.createElement("textarea");
      textarea.style.width = "100%";
      textarea.style.height = "130px";
      textarea.id = "description-textarea";
      textarea.maxLength = 255;
      textarea.value = aboutMeTextContent;

      aboutMeContainer.innerHTML = "";
      aboutMeContainer.appendChild(textarea);

      editAboutMeBtn.innerHTML = '<img src="../../assets/save.png" width="20" height="20">';
      textarea.focus();

      editAboutMeBtn.removeEventListener("click", editAboutMeHandler);
      editAboutMeBtn.addEventListener("click", saveAboutMeHandler);
  }

  async function saveAboutMeHandler() {
      const aboutMeContainer = document.getElementById("description-container");
      const textarea = document.getElementById("description-textarea");
      const updatedText = textarea.value;
      
      if (!updatedText) {
        showToast('warning', 'Warning', 'About Me cannot be empty.');
        return;
      }
      const paragraph = document.createElement("p");
      const updateAboutMe = {
        CompanyName:Profile.companyName,
        CompanyAddress:Profile.companyAddress,
        City :  Profile.city,
        CompanySize:Profile.companySize,
        CompanyWebsite:Profile.companyWebsite,
        CompanyDescription: updatedText,
      };

      try {
        const updateResponse = await fetchData(`api/Company`, "PUT", updateAboutMe);
        Profile.companyDescription = updateResponse.companyDescription;
        console.log(Profile.companyDescription)
        paragraph.textContent = Profile.companyDescription;
        showToast('success', 'Success', 'Company Profile  updated successfully');
      } catch (error) {
        if (error.message.includes("404")) {
          showToast('error', 'Error', 'User not found. Please try again later.');
        } else {
          showToast('error', 'Error', 'An error occurred while updating About Me. Please try again later.');
        }
      }
      

      paragraph.id = "description-text";
      aboutMeContainer.innerHTML = "";
      aboutMeContainer.appendChild(paragraph);

      editAboutMeBtn.innerHTML = '<img src="../../assets/edit.svg" width="20" height="20">';
      editAboutMeBtn.removeEventListener("click", saveAboutMeHandler);
      editAboutMeBtn.addEventListener("click", editAboutMeHandler);
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




