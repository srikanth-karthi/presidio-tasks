const baseUrl = "http://localhost:5117/";

async function fetchData(url, httpMethod = "GET", body = null) {
    const headers = {

        "Content-Type": "application/json", 
    };

    const response = await fetch(`${baseUrl}${url}`, {
        method: httpMethod,
        headers: headers,
        body: JSON.stringify(body) 
    });


    if (!response.ok) {
        const errorMessage = `Error ${response.status}: ${response.statusText}`;
        throw new Error(errorMessage);
    }

    const data = await response.json();
    return data;
}



let activeToasts = [];

function showToast(type, message1, message2, duration = 2000) {
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


document.addEventListener('DOMContentLoaded', function () {
    const jobSeekerTab = document.getElementById('job-seeker');
    const companyTab = document.getElementById('company');
    const jobSeekerForm = document.getElementById('job-seeker-form');
    const companyForm = document.getElementById('company-form');
    companyForm.style.display = 'none';
    jobSeekerTab.addEventListener('change', function () {
      if (jobSeekerTab.checked) {
        jobSeekerForm.style.display = 'block';
        companyForm.style.display = 'none';
      }
    });
  
    companyTab.addEventListener('change', function () {
      if (companyTab.checked) {
        jobSeekerForm.style.display = 'none';
        companyForm.style.display = 'block';
      }
    });
    const form = document.getElementById('registration-form');
    form.addEventListener('submit', async function (event) {
      event.preventDefault();
    
      let isValid = true;
      let data;
    let url='';
    const validateEmail = (email) => {
      return String(email)
       .toLowerCase()
       .match(/^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/);
    };
      const validateField = (fieldId, fieldName) => {
        const fieldValue = document.getElementById(fieldId).value.trim();
        if (!fieldValue) {
          showToast('warning', 'Warning', 'Please fill in all required fields.');
          isValid = false;
          return false;
        }
        return true;
      };
    
      if (jobSeekerTab.checked) {
        url='api/User/register'
        data = {
          email: document.getElementById('email').value,
          name: document.getElementById('name').value,
          password: document.getElementById('password').value,
          dob: document.getElementById('dob').value,
          city: document.getElementById('city').value,
        };
    
        if (!validateField('email', 'Email')) return;
        if (!validateField('name', 'Name')) return;
        if (!validateField('password', 'Password')) return;
        if (!validateField('dob', 'Date of Birth')) return;
        if (!validateField('city', 'City')) return;

        if (!validateEmail(document.getElementById('email').value)) {
          showToast('warning', 'Warning', 'Enter a valid Email.');

          isValid = false;
          return false;
        }
    
        const passwordLength = document.getElementById('password').value.length;
        if (passwordLength <8) {
          showToast('warning', 'Warning', 'The Password contains atleast 8 characters.');
          isValid = false;
          return false;
        }
      } else if (companyTab.checked) {
           url=  'api/Company/register'
        data = {
          companyName: document.getElementById('companyName').value,
          email: document.getElementById('companyEmail').value,
          password: document.getElementById('companyPassword').value,
          companyAddress: document.getElementById('companyAddress').value,
          city: document.getElementById('companyCity').value,
        };
    
    
        if (!validateField('companyName', 'Company Name')) return;
        if (!validateField('companyEmail', 'companyEmail')) return;
        if (!validateField('companyPassword', 'companyPassword')) return;
        if (!validateField('companyAddress', 'companyAddress')) return;
        if (!validateField('companyCity', 'City')) return;
    
        if (!validateEmail(document.getElementById('companyEmail').value)) {
          showToast('warning', 'Warning', 'Enter a valid Email.');
          isValid = false;
          return false;
        }
    
        const addressLength = document.getElementById('companyAddress').value.length;
        if (addressLength > 200) {
          showToast('warning', 'Warning', 'The Company Address cannot exceed 200 characters.');
          isValid = false;
          return false;
        }
        const passwordLength = document.getElementById('companyPassword').value.length;
        if (passwordLength <8) {
          showToast('warning', 'Warning', 'The Password contains atleast 8 characters.');
          isValid = false;
          return false;
        }
      }
    
      if (isValid) {
        try {
          const response = await fetchData(url, 'POST', data);
  
          showToast('success', 'Success', 'Register Sucessfull.');
          showToast('success', 'Success', 'Please login.');
  
      } catch (error) {
          console.error("Error updating profile details:", error);
      
          if (error.message.includes("401") || error.message.includes("400")) {
            showToast('error', 'Register Failed', 'Please Register again.');
          } 
           
          else if (error.message.includes("409") ) {
            showToast('warning', 'Register Failed', 'Email Already Registered.');
          } 
          else {
            showToast('error', 'An error occurred', ' Please Register again Later');
          }
        }
        // window.location.href = '/dashboard/dashboard.html';
      }
    });
    
  });
  