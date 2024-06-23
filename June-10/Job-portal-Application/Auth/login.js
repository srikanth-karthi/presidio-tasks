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

document.addEventListener('DOMContentLoaded', (event) => {
    const loginForm = document.getElementById('loginForm');

    loginForm.addEventListener('submit', async (event) => {
        event.preventDefault();

        const email = document.getElementById('email').value;
        const password = document.getElementById('password').value;

        const userType = document.querySelector('input[name="user-type"]:checked').id;

        if (!email.trim() || !password.trim()) {
          showToast('warning', 'Warning', 'Please fill in all required fields.');

            return;
        }

        const loginData = {
            email: email,
            password: password
        };

        let loginUrl;
        if (userType === 'job-seeker') {
            loginUrl = 'api/User/login';
        } else if (userType === 'company') {
            loginUrl = 'api/Company/login';
        }

        try {
            const response = await fetchData(loginUrl, 'POST', loginData);
            localStorage.setItem("authToken", response.token);
            showToast('success', 'Success', 'Login Sucessfull.');
            // window.location.href = "../dashboard/dashboard.html";
        } catch (error) {
            console.error("Error updating profile details:", error);
        
            if (error.message.includes("401") || error.message.includes("400")) {
              showToast('error', 'Login Failed', 'Invalid credentials.');
            } 
            else {
              showToast('error', 'An error occurred', ' Please login again Later');
            }
          }
        
    });
});

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